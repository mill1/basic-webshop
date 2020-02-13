import { OrderLineDTO } from './order-line.dto';
import { Moment } from 'moment';

export class OrderDTO {
  public id: number;
  public date: Moment;
  public remark?: string;
  public orderLines?: OrderLineDTO[];
}