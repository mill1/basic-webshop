import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { OrdersComponent } from './components/orders/orders.component';
import { OrderComponent } from './components/order/order.component';
import { OrderLineComponent } from './components/order-line/order-line.component';
import { OrderAddEditComponent } from './components/order-add-edit/order-add-edit.component';
import { ProductsComponent } from './components/products/products.component';
import { ProductComponent } from './components/product/product.component';

const routes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'orders', component: OrdersComponent },
    { path: 'order/:id', component: OrderComponent },
    { path: 'orders/add', component: OrderAddEditComponent },
    { path: 'order/edit/:id', component: OrderAddEditComponent },
    { path: 'orderline/:id', component: OrderLineComponent },
    { path: 'products', component: ProductsComponent },
    { path: 'product/:id', component: ProductComponent },
    { path: '**', redirectTo: '/' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
