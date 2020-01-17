import { Injectable } from '@angular/core';
import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Product } from '../models/product.model';
import { API_BASE_URL } from '../injection-tokens/api-base-url.token';
import { ProductCategory } from '../models/product-category.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  private baseUrl: string;

  constructor(private httpService: HttpClient,
    @Inject(API_BASE_URL) private apiServerUrl: string
  ) {
    this.myAppUrl = `${apiServerUrl}/`;
    this.myApiUrl = 'api/product/';
  }

  getProduct(productId: number): Observable<Product> {
    return this.httpService.get<Product>(this.myAppUrl + this.myApiUrl + productId);
  }

  getProducts(): Observable<Product[]> {
    return this.httpService.get<Product[]>(this.myAppUrl + this.myApiUrl);
  }

  getProductCategories(): Observable<ProductCategory[]> {
    return this.httpService.get<ProductCategory[]>(this.myAppUrl + this.myApiUrl + 'Categories').pipe(
      retry(1),
      catchError(this.errorHandler)
    );
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
