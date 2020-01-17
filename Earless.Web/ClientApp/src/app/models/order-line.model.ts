import { Product } from './product.model';

export class OrderLine {
  public id: number;
  public product: Product;
  public quantity: number;
  public fulfilled: number;
}
