import { Injectable } from '@angular/core';
import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Order } from '../models/order.model';
import { API_BASE_URL } from '../injection-tokens/api-base-url.token';
import { OrderLine } from '../models/order-line.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  orderUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  private baseUrl: string;

  constructor(private httpService: HttpClient,
    @Inject(API_BASE_URL) private apiServerUrl: string
  ) {
    this.orderUrl = `${apiServerUrl}/api/order/`;
  }

  getOrders(): Observable<Order[]> {
    return this.httpService.get<Order[]>(this.orderUrl);
  }

  getOrder(orderId: number): Observable<Order> {
    return this.httpService.get<Order>(this.orderUrl + orderId);
  }

  addOrder(order): Observable<Order> {
    return this.httpService.post<Order>(this.orderUrl, order, this.httpOptions);
  }

  updateOrder(order): Observable<Order> {
    return this.httpService.put<Order>(this.orderUrl, order, this.httpOptions);
  }

  deleteOrder(orderId: number): Observable<number> {
    return this.httpService.delete<number>(this.orderUrl + orderId);
  }
}
