import { Injectable } from '@angular/core';
import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of, forkJoin } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { Product } from '../models/product.model';
import { API_BASE_URL } from '../injection-tokens/api-base-url.token';
import { ProductCategory } from '../models/product-category.model';
import { ProductDTO } from '../dto/product.dto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  AppUrl: string;
  ApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  private baseUrl: string;

  constructor(private httpService: HttpClient,
    @Inject(API_BASE_URL) private apiServerUrl: string
  ) {
    this.AppUrl = `${apiServerUrl}/`;
    this.ApiUrl = 'api/product/';
  }

  getProduct(productId: number): Observable<Product> {

    const productDTO$ = this.httpService.get<ProductDTO>(this.AppUrl + this.ApiUrl + productId);
    const results$: Observable<[ProductCategory[], ProductDTO]> = forkJoin([this.getProductCategories(), productDTO$]);
    const product$ = results$.pipe(map(ct => this.mapProductDTOtoProduct(ct[1], ct[0])));

    return product$;
  }

  getProducts(): Observable<Product[]> {
    const productDTOs$: Observable<ProductDTO[]> = this.httpService.get<ProductDTO[]>(this.AppUrl + this.ApiUrl);
    const results$: Observable<[ProductCategory[], ProductDTO[]]> = forkJoin([this.getProductCategories(), productDTOs$]);
    return this.mapComplexTypes$ToProducts$(results$);
  }

  getProductCategories(): Observable<ProductCategory[]> {
    return this.httpService.get<ProductCategory[]>(this.AppUrl + this.ApiUrl + 'Categories').pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  mapComplexTypes$ToProducts$(complexTypes$: Observable<[ProductCategory[], ProductDTO[]]>): Observable<Product[]> {
    const products$ = complexTypes$.pipe(map(ct => this.mapProductDTOsToProducts(ct[1], ct[0])));
    return products$;
  }

  mapProductDTOsToProducts(productDTOs: ProductDTO[], productCategories: ProductCategory[] ): Product[] {
    const products: Product[] = productDTOs.map(productDTO => this.mapProductDTOtoProduct(productDTO, productCategories));
    return products;
  }

  mapProductDTOtoProduct(productDTO: ProductDTO, availableProductCategories:  ProductCategory[]): Product {
    const product = new Product();
    product.id = productDTO.id;
    product.name = productDTO.name;
    product.description = productDTO.description;
    product.price = productDTO.price;
    product.productCategory = availableProductCategories.find(pc => pc.id === productDTO.productCategoryId);

    return product;
  }

  mapProductToProductDTO(product: Product): ProductDTO {

    const productDTO: ProductDTO = {
      id: product.id,
      name: product.name,
      description: product.description,
      price: product.price,
      productCategoryId: product.productCategory.id
    };

    return productDTO;
  }

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
