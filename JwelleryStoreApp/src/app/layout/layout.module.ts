import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';
import { LayoutComponent } from './layout.component';
import { LoaderComponent } from '../shared/loader/loader.component';
import { FormsModule } from '@angular/forms';
import { ToastModule } from "primeng/toast";


@NgModule({
  declarations: [
    LayoutComponent,
    LoaderComponent
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    ToastModule
  ]
})
export class LayoutModule { }
