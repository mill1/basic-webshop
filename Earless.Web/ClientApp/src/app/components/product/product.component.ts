import { Component } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { Product } from '../../models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  product$: Observable<Product>;

  constructor(
    private productService: ProductService,
    private avRoute: ActivatedRoute,
    private router: Router
  ) {
    this.product$ = this.avRoute.paramMap.pipe(
      map((params: ParamMap) => params.get('id')),
      switchMap((id: string) => this.productService.getProduct(+id))
    );
  }
}
