import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { OrderLine } from '../../models/order-line.model';
import { OrderLineService } from 'src/app/services/order-line.service';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-order-line',
  templateUrl: './order-line.component.html',
  styleUrls: ['./order-line.component.css']
})
export class OrderLineComponent {
  orderLine$: Observable<OrderLine>;

  constructor(
    private orderLineService: OrderLineService,
    private avRoute: ActivatedRoute,
    private router: Router
  ) {
    this.orderLine$ = this.avRoute.paramMap.pipe(
      map((params: ParamMap) => params.get('id')),
      switchMap((id: string) => this.orderLineService.getOrderLine(+id))
    );
  }
}
