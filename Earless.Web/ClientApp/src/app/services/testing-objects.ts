
import { Order } from '../models/order.model';
import { OrderLine } from '../models/order-line.model';
import * as moment from 'moment';
import { Product } from '../models/product.model';
import { OrderLineDTO } from '../dto/order-line.dto';

export class TestingObjects {

  public testProductCategory = {id: 1, name : 'Gehoorapparaten'};

  public testProduct = {id: 1, productCategory: this.testProductCategory,
         name: 'Phonak Audéo M90-R – oplaadbaar', description: 'Phonak Audéo M90-R – oplaadbaar', price: 1599};

  public testProductDto = {id: 1, productCategoryId: 1,
         name: 'Phonak Audéo M90-R – oplaadbaar', description: 'Phonak Audéo M90-R – oplaadbaar', price: 1599};



  public testOrderLine:    OrderLine =    {id: 1, product: this.testProduct, quantity: 1, fulfilled: 0};
  public testOrderLineDTO: OrderLineDTO = {id: 1, productId: 1, quantity: 1, fulfilled: 0};

  public testOrderLines =    [this.testOrderLine];
  public testOrderLinesDTO = [this.testOrderLineDTO];

  public testOrder =    {id: 1, date: moment(new Date()), remark: 'twee keer bellen.', orderLines: this.testOrderLines};
  public testOrderDTO = {id: 1, date: moment(new Date()), remark: 'twee keer bellen.', orderLines: this.testOrderLinesDTO};

  public testOrders = [this.testOrder];
  public testOrdersDTO = [this.testOrderDTO];

}
