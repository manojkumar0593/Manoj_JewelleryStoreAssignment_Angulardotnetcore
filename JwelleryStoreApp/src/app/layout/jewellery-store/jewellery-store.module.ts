import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JewelleryStoreRoutingModule } from './jewellery-store-routing.module';
import { LoginFormComponent } from './login-form/login-form.component';
import { CalculatorComponent } from './calculator/calculator.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogModule  } from "primeng/dialog";
import {DialogService, DynamicDialogModule} from 'primeng/dynamicdialog';
import { TooltipModule } from "primeng/tooltip";
import { JewelleryStoreComponent } from './jewellery-store.component';
import { PrintScreenComponent } from './calculator/print-screen/print-screen.component';


@NgModule({
  declarations: [
    LoginFormComponent,
    CalculatorComponent,
    JewelleryStoreComponent,
    PrintScreenComponent
  ],
  imports: [
    CommonModule,
    JewelleryStoreRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    TooltipModule,
    DynamicDialogModule
  ],
  providers:[DialogService]
})
export class JewelleryStoreModule { }
