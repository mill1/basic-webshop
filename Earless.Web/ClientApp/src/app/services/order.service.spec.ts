import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';
import { TestBed, async, inject, getTestBed } from '@angular/core/testing';
import { OrderService } from './order.service';
import { API_BASE_URL } from '../injection-tokens/api-base-url.token';
import { TestingObjects } from './testing-objects';
import { OrderDTO } from '../dto/order.dto';

let httpTestingController: HttpTestingController;
let orderService: OrderService;
let testUrl: string;
const testData = new TestingObjects();
const fakeOrderDTO: OrderDTO = null;

describe('Service: Order', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      providers: [
        OrderService,
        { provide: API_BASE_URL, useValue: 'https://mockurl.com' }
      ],
      imports: [HttpClientTestingModule]
    });
    const injector = getTestBed();
    orderService = injector.get(OrderService);
    httpTestingController = injector.get(HttpTestingController);
    testUrl = injector.get(API_BASE_URL);
  }));

  afterEach(async(() => {
    httpTestingController.verify();
  }));

  it('should create order-service', async(
    inject([OrderService], (service: OrderService) => {
      expect(service).toBeTruthy();
    })
  ));

    // it('01. should be able to map Order to OrderDto', async(() => {
    //   expect(orderService.mapOrderToOrderDTO(testData.testOrder)).toEqual(
    //     testData.testOrderDTO
    //   );
    // }));

//  describe('addOrder', () => {
//     it('01. Should be able to add Order', async(() => {
//       orderService
//         .addOrder(testData.testOrder)
//         .subscribe(response => expect(response).toEqual(testData.testOrder));

//       const req = httpTestingController.expectOne(
//         `${testUrl}/api/order/`
//       );
//       expect(req.request.method).toBe('POST');
//     }));
//   });

  // describe('updateOrder', () => {
  //   it('01. Should be able to update Order', async(() => {
  //     orderService
  //       .updateOrder(testData.testOrder)
  //       .subscribe(response => expect(response).toEqual(testData.testOrder));

  //     const req = httpTestingController.expectOne(
  //       `${testUrl}/api/order/`
  //     );
  //     expect(req.request.method).toBe('PUT');
  //   }));
  // });

  describe('deleteOrder', () => {
    it('01. Should be able to delete Order', async(() => {
      orderService.deleteOrder(testData.testOrder.id).subscribe();

      const req = httpTestingController.expectOne(
        `${testUrl}/api/order/${testData.testOrder.id}`
      );

      expect(req.request.method).toBe('DELETE');
    }));
  });

  // describe('getOrder', () => {
  //   it('01. Should be able to get Order', async(() => {
  //     orderService
  //       .getOrder(testData.testOrder.id)
  //       .subscribe(response => expect(response).toEqual(testData.testOrder));

  //     const testResponseDTO = fakeOrderDTO;
  //     const req = httpTestingController.expectOne(
  //       `${testUrl}/api/order/${testData.testOrder.id}`
  //     );

  //     expect(req.request.method).toBe('GET');
  //     req.flush(testResponseDTO);
  //   }));
  // });

  // describe('getOrders', () => {
  //   it('01. Should be able to get Orders', async(() => {
  //     orderService
  //       .getOrders()
  //       .subscribe(response => expect(response).toEqual(testData.testOrders));

  //     const req = httpTestingController.expectOne(`${testUrl}/api/order/`);

  //     expect(req.request.method).toBe('GET');
  //   }));
  // });
});
