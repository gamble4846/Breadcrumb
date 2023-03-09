import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfigService } from './Services/Other Services/ConfigService/config.service';
import { TokenInterceptorService } from './Services/Other Services/TokenInterceptor/token-interceptor.service';
import { NavigationModule } from './Modules/NavigationModule/navigation.module';

registerLocaleData(en);

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NavigationModule
  ],
  providers: [
    { provide: NZ_I18N, useValue: en_US },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true },
    { provide: APP_INITIALIZER, multi: true, deps: [ConfigService], useFactory: (ConfigService: ConfigService) => { return () => { return ConfigService.loadEverything(); }; }, },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
