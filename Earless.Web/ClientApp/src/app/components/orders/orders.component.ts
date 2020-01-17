import { Component, OnInit, EventEmitter } from '@angular/core';
import { Observable, of, from } from 'rxjs';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order.model';
import { NgbModal, ModalDismissReasons, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { switchMap, startWith, catchError, tap, map } from 'rxjs/operators';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orders$: Observable<Order[]>;
  orders: Array<Order> = [];
  modalTitle = 'Delete';
  modalContent = 'Are you sure you want to delete the order?';
  deleteOrderEvent: EventEmitter<void> = new EventEmitter<void>();

  constructor(private orderService: OrderService, private modalService: NgbModal) {
  }

  ngOnInit() {
    const orderRefresh$: Observable<void> = this.deleteOrderEvent.pipe(
      startWith(null as void)
    );

    this.orders$ = orderRefresh$.pipe(
      switchMap(() => this.orderService.getOrders())
    );

    this.orders$.subscribe(orders => {
      this.orders = orders;
    });
  }

  open(content: any, orderId: number) {
    const modalRef: NgbModalRef = this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
    from(modalRef.result).pipe(
      tap(result => console.log(`Closed with: ${result}; delete order with id ${orderId}`)),
      catchError(reason => { throw new Error(`Dismissed ${this.getDismissReason(reason)}`); }),
      map(() => orderId),
      switchMap(id => this.orderService.deleteOrder(id))
    ).subscribe(
      deletedId => this.deleteOrderEvent.emit(),
      error => console.error(error)
    );
  }

  getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
