import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order.model';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
  order$: Observable<Order>;

  constructor(private orderService: OrderService,
    private avRoute: ActivatedRoute,
    private router: Router) {

    this.avRoute.paramMap.pipe(
      map((params: ParamMap) =>
        params.get('id')
      )).subscribe(p => this.order$ = this.orderService.getOrder(+p));
  }

  delete(orderId) {
    const confirmed = confirm('Do you want to delete order with id: ' + orderId);
    if (confirmed) {
      this.orderService.deleteOrder(orderId).subscribe((data) => {
        this.router.navigate(['/orders']);
      });
    }
  }
}
