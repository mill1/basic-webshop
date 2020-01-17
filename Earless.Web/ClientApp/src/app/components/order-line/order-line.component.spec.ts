import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderLineComponent } from './order-line.component';

describe('OrderLineComponent', () => {
  let component: OrderLineComponent;
  let fixture: ComponentFixture<OrderLineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderLineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
