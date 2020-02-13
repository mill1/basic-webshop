import {
    HttpClientTestingModule,
    HttpTestingController
  } from '@angular/common/http/testing';
  import { TestBed, async, inject, getTestBed } from '@angular/core/testing';
  import { OrderLineService } from './order-line.service';
  import { API_BASE_URL } from '../injection-tokens/api-base-url.token';
  import { TestingObjects } from './testing-objects';
  import { OrderLineDTO } from '../dto/order-line.dto';

  let httpTestingController: HttpTestingController;
  let orderLineService: OrderLineService;
  let testUrl: string;
  const testData = new TestingObjects();

  describe('Service: OrderLine', () => {
    beforeEach(async(() => {
      TestBed.configureTestingModule({
        providers: [
          OrderLineService,
          { provide: API_BASE_URL, useValue: 'https://mockurl.com' }
        ],
        imports: [HttpClientTestingModule]
      });
      const injector = getTestBed();
      orderLineService = injector.get(OrderLineService);
      httpTestingController = injector.get(HttpTestingController);
      testUrl = injector.get(API_BASE_URL);
    }));

    afterEach(async(() => {
      httpTestingController.verify();
    }));

    it('should create orderLine-service', async(
      inject([OrderLineService], (service: OrderLineService) => {
        expect(service).toBeTruthy();
      })
    ));

    // it('01. should be able to map OrderLine to OrderLineDto', async(() => {
    //   expect(orderLineService.mapOrderLineToOrderLineDTO(testData.testOrderLine)).toEqual(
    //     testData.testOrderLineDTO
    //   );
    // }));
});
