import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { ProductCategory } from 'src/app/models/product-category.model';

// inline class
export class ProductCategoryProducts {
  productCategory: ProductCategory;
  products: Product[];
}

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  products$: Observable<Product[]>;
  products: Product[];
  productcategories$: Observable<ProductCategory[]>;
  productspercategory$: Observable<Product[]> = new Observable<Product[]>();
  productcategoryproducts: ProductCategoryProducts[] = [];

  constructor(private productService: ProductService) {
    this.productcategories$ = this.productService.getProductCategories();
    this.products$ = this.productService.getProducts();
    this.products$.subscribe(x => {
      this.products = x ;
      this.initialize();
    });
  }

  initialize() {
    this.productcategories$.subscribe(pcs => {
      pcs.forEach(element => {
        const pcp = new ProductCategoryProducts();
        pcp.productCategory = element;
        pcp.products = this.getProductsPerCategory(pcp.productCategory.id);
        this.productcategoryproducts.push(pcp);
      });
    });
  }

  getProductsPerCategory(id) {
    const filteredproducts = this.products.filter(prod => prod.productCategory.id === id);
    return filteredproducts;
  }
}

