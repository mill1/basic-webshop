import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule, APP_INITIALIZER, ErrorHandler } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { ConfigService } from './services/config.service';
import { API_BASE_URL } from './injection-tokens/api-base-url.token';
import { OrderService } from './services/order.service';
import { OrdersComponent } from './components/orders/orders.component';
import { OrderAddEditComponent } from './components/order-add-edit/order-add-edit.component';
import { OrderComponent } from './components/order/order.component';
import { OrderLineComponent } from './components/order-line/order-line.component';
import { OrderLineService } from './services/order-line.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductsComponent } from './components/products/products.component';
import { ProductComponent } from './components/product/product.component';

export function init(configService: ConfigService): Function {
    return () => {

        const cachePromise = configService.loadCache();
        return Promise.all([cachePromise]);
    };
}

export function getApiBaseUrlFactory(configService: ConfigService) {
    return configService.getCachedConfig().apiServerUrl;
}

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        OrdersComponent,
        OrderAddEditComponent,
        OrderComponent,
        OrderLineComponent,
        ProductsComponent,
        ProductComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        AppRoutingModule,
        NgbModule
    ],
    providers: [
        OrderService,
        OrderLineService,
        {
            provide: APP_INITIALIZER,
            useFactory: init,
            deps: [ConfigService],
            multi: true
        },
        ConfigService,
        {
            provide: API_BASE_URL,
            useFactory: getApiBaseUrlFactory,
            deps: [ConfigService]
        },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
