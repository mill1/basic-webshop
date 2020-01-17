import { ProductCategory } from './product-category.model';

export class Product {
    public id: number;
    public productCategory: ProductCategory;
    public name: string;
    public description: string;
    public price: number;
}
