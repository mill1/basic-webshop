import { OrderLine } from './order-line.model';
import { Moment } from 'moment';

export class Order {
  public id: number;
  public date: Moment;
  public remark?: string;
  public orderLines?: OrderLine[];
}
