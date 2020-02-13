import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderLine } from '../models/order-line.model';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../injection-tokens/api-base-url.token';
import { OrderLineDTO } from '../dto/order-line.dto';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class OrderLineService {

  orderLineUrl: string;

  constructor(private httpService: HttpClient,
    @Inject(API_BASE_URL) private apiServerUrl: string
  ) {
    this.orderLineUrl = `${apiServerUrl}/api/orderline/`;
  }

  getOrderLine(orderLineId: number): Observable<OrderLine> {
    return this.httpService.get<OrderLine>(this.orderLineUrl + orderLineId);
  }

  mapOrderLineDTOtoOrderLine(orderLineDTO: OrderLineDTO, availableProducts:  Product[]): OrderLine {
    const orderLine = new OrderLine();
    orderLine.id = orderLineDTO.id;
    orderLine.product = availableProducts.find(p => p.id === orderLineDTO.productId);
    orderLine.quantity = orderLineDTO.quantity;
    orderLine.fulfilled = orderLineDTO.fulfilled;

    return orderLine;
  }

  mapOrderLineToOrderLineDTO(orderLine: OrderLine): OrderLineDTO {
    const orderLineDTO = new OrderLineDTO();
    orderLineDTO.id = orderLine.id;
    orderLineDTO.productId = orderLine.product.id;
    orderLineDTO.quantity = orderLine.quantity;
    orderLineDTO.fulfilled = orderLine.fulfilled;

    return orderLineDTO;
  }

}
