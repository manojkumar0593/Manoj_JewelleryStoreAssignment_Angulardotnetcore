import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { LogoutComponent } from './logout/logout.component';
import { APIInterceptor } from './shared/interceptor/http-interceptop';
import { AuthGuardGuard } from './shared/guard/auth-guard.guard';
import { MessageService } from 'primeng/api';
@NgModule({
  declarations: [
    AppComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [AuthGuardGuard , { provide: HTTP_INTERCEPTORS, useClass: APIInterceptor, multi: true },
    MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
