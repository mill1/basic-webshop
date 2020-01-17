import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderLine } from '../models/order-line.model';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../injection-tokens/api-base-url.token';

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
}
