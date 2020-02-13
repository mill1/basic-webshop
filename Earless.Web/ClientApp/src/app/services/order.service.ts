import { Injectable } from '@angular/core';
import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of, forkJoin } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { Order } from '../models/order.model';
import { API_BASE_URL } from '../injection-tokens/api-base-url.token';
import { ProductService } from './product.service';
import { OrderLineService } from './order-line.service';
import { Product } from '../models/product.model';
import { OrderDTO } from '../dto/order.dto';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  AppUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  private baseUrl: string;

  constructor(private httpService: HttpClient, private orderLineService: OrderLineService, private productService: ProductService,
    @Inject(API_BASE_URL) private apiServerUrl: string
  ) {
    this.AppUrl = `${apiServerUrl}/api/order/`;
  }

  getOrder(orderId: number): Observable<Order> {
    const orderDTO$ = this.httpService.get<OrderDTO>(this.AppUrl + orderId);
    const results$: Observable<[Product[], OrderDTO]> = forkJoin([this.getProducts(), orderDTO$]);
    const order$ = results$.pipe(map(ct => this.mapOrderDTOtoOrder(ct[1], ct[0])));

    return order$;
  }

  getOrders(): Observable<Order[]> {
    const orderDTOs$: Observable<OrderDTO[]> = this.httpService.get<OrderDTO[]>(this.AppUrl);
    const results$: Observable<[Product[], OrderDTO[]]> = forkJoin([this.getProducts(), orderDTOs$]);
    return this.mapComplexTypes$ToOrders$(results$);
  }

  addOrder(order): Observable<Order> {
    const orderDTO$ = this.httpService.post<OrderDTO>(this.AppUrl, this.mapOrderToOrderDTO(order), this.httpOptions);
    const results$: Observable<[Product[], OrderDTO]> = forkJoin([this.getProducts(), orderDTO$]);

    const order$ = results$.pipe(map(ct => this.mapOrderDTOtoOrder(ct[1], ct[0])));

    return order$;
  }

  updateOrder(order): Observable<Order> {
    const orderDTO$ = this.httpService.put<OrderDTO>(this.AppUrl, this.mapOrderToOrderDTO(order), this.httpOptions);
    const results$: Observable<[Product[], OrderDTO]> = forkJoin([this.getProducts(), orderDTO$]);
    const order$ = results$.pipe(map(ct => this.mapOrderDTOtoOrder(ct[1], ct[0])));
    return order$;
  }

  deleteOrder(orderId: number): Observable<number> {
    return this.httpService.delete<number>(this.AppUrl + orderId);
  }

  getProducts(): Observable<Product[]> {
    return this.productService.getProducts();
  }

  mapComplexTypes$ToOrders$(complexTypes$: Observable<[Product[], OrderDTO[]]>): Observable<Order[]> {
    const orders$ = complexTypes$.pipe(map(ct => this.mapOrderDTOsToOrders(ct[1], ct[0])));
    return orders$;
  }

  mapOrderDTOsToOrders(orderDTOs: OrderDTO[], products: Product[] ): Order[] {
    const orders: Order[] = orderDTOs.map(orderDTO => this.mapOrderDTOtoOrder(orderDTO, products));
    return orders;
  }

  mapOrderDTOtoOrder(orderDTO: OrderDTO, availableProducts:  Product[]): Order {
    const order = new Order();
    order.id =  orderDTO.id;
    order.date = orderDTO.date;
    order.remark = orderDTO.remark;
    order.orderLines = orderDTO.orderLines.map(ol => this.orderLineService.mapOrderLineDTOtoOrderLine(ol, availableProducts));

    return order;
  }

  mapOrderToOrderDTO(order: Order): OrderDTO {

    const orderDTO: OrderDTO = {
      id: order.id,
      date: order.date,
      remark: order.remark,
      orderLines: order.orderLines.map(ol => this.orderLineService.mapOrderLineToOrderLineDTO(ol))
    };

    return orderDTO;
  }
}
