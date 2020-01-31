import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { OrderComponent } from './order.component';
import { TestingObjects } from '../../services/testing-objects';
import { of } from 'rxjs';
import { OrderService } from 'src/app/services/order.service';

const testData = new TestingObjects();

class MockOrderService {
  updateOrder() {}
  getOrder() {}
}

describe('OrderComponent', () => {
  let component: OrderComponent;
  let fixture: ComponentFixture<OrderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        OrderComponent
      ],
      imports: [
        RouterTestingModule
      ],
      providers: [
        { provide: OrderService, useClass: MockOrderService }
      ]
    }).compileComponents();
  }));

  beforeEach(async(() => {
    fixture = TestBed.createComponent(OrderComponent);
    component = fixture.componentInstance;
    component.order$ = of(testData.testOrder);
    fixture.detectChanges();
  }));

  afterEach(async(() => {
    fixture.destroy();
  }));

  it('should create', async(() => {
    expect(component).toBeTruthy();
  }));

  it('01. Should be able to perform a test', () => {
    'The test';
    console.log('Test my test');
    expect('my value').toBe('my value');
  });
});
