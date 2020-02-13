import {
    HttpClientTestingModule,
    HttpTestingController
  } from '@angular/common/http/testing';
  import { TestBed, async, inject, getTestBed } from '@angular/core/testing';
  import { ProductService } from './product.service';
  import { API_BASE_URL } from '../injection-tokens/api-base-url.token';
  import { TestingObjects } from './testing-objects';
  import { ProductDTO } from '../dto/product.dto';

  let httpTestingController: HttpTestingController;
  let productService: ProductService;
  let testUrl: string;
  const testData = new TestingObjects();

  describe('Service: Product', () => {
    beforeEach(async(() => {
      TestBed.configureTestingModule({
        providers: [
          ProductService,
          { provide: API_BASE_URL, useValue: 'https://mockurl.com' }
        ],
        imports: [HttpClientTestingModule]
      });
      const injector = getTestBed();
      productService = injector.get(ProductService);
      httpTestingController = injector.get(HttpTestingController);
      testUrl = injector.get(API_BASE_URL);
    }));

    afterEach(async(() => {
      httpTestingController.verify();
    }));

    it('should create product-service', async(
      inject([ProductService], (service: ProductService) => {
        expect(service).toBeTruthy();
      })
    ));

    it('01. should be able to map Product to ProductDto', async(() => {
      expect(productService.mapProductToProductDTO(testData.testProduct)).toEqual(
        testData.testProductDto
      );
    }));
});
