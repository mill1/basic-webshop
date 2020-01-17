import { Component, OnInit, ɵConsole } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order.model';
import * as moment from 'moment';
import { OrderLine } from 'src/app/models/order-line.model';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-order-add-edit',
  templateUrl: './order-add-edit.component.html',
  styleUrls: ['./order-add-edit.component.css']
})
export class OrderAddEditComponent implements OnInit {
  actionType: string;
  orderId: number;
  order: Order;
  editMode = false;
  subtotal: number;
  total: number;
  indexEditedRecord: number;

  constructor(private orderService: OrderService,
    private avRoute: ActivatedRoute,
    private router: Router) {
    this.actionType = '';

    this.avRoute.paramMap.pipe(
      map((params: ParamMap) =>
        params.get('id')
      )).subscribe(p => this.orderId = +p);
  }

  ngOnInit() {

    if (this.orderId > 0) {
      this.actionType = 'Edit';
      this.loadOrder();
    } else {
      this.actionType = 'Add';
      this.initializeOrder();
    }
  }

  loadOrder() {
    this.orderService.getOrder(this.orderId)
      .subscribe(data => {
        this.order = data;
        this.calculateTotal();
      });
  }

  initializeOrder() {
    this.order = {
      id: 0,
      date: moment(),
      remark: '',
      orderLines: OrderLine[0] = [],
    };
  }

  viewRecord() {
    this.editMode = false;
    this.calculateTotal();
    this.indexEditedRecord = -1;
  }

  editRecord(index) {
    this.editMode = true;
    this.indexEditedRecord = index;
  }

  addOrderLine() {
    const newOrderLine = {
      id: 0,
      quantity: 0,
      fulfilled: 0,
      product: {
        id: 1,
        name: 'Phonak Audéo M90-R – oplaadbaar',
        description: 'Phonak Audéo M90-R – oplaadbaar',
        price: 1599,
        productCategory:  { id: 1, name: 'Gehoorapparaten' },
      }
    };
    this.order.orderLines.push(newOrderLine);
  }

  deleteOrderLine(index) {
    this.order.orderLines.splice(index, 1);
    this.calculateTotal();
  }

  calculateTotal() {

    this.total = 0;

    this.order.orderLines.forEach(orderLine => {
      this.total += orderLine.product.price * orderLine.quantity;
    });
  }

  hasMultipleOrderlines() {
    return this.order.orderLines.length > 1;
  }

  save() {

    for (let index = this.order.orderLines.length - 1; index >= 0; index--) {
      const orderLine: OrderLine = this.order.orderLines[index];

      if (orderLine.quantity < 1) {
        this.order.orderLines.splice(index, 1);
      }
    }

    if (this.actionType === 'Add') {
      this.orderService.addOrder(this.order)
        .subscribe((data) => {
          this.router.navigate(['/orders']);
        });
    }

    if (this.actionType === 'Edit') {
      this.orderService.updateOrder(this.order)
        .subscribe((data) => {
          this.router.navigate(['/order', data.id]);
        });
    }
  }
}
