import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardGuard } from 'src/app/shared/guard/auth-guard.guard';
import { CalculatorComponent } from './calculator/calculator.component';
import { JewelleryStoreComponent } from './jewellery-store.component';
import { LoginFormComponent } from './login-form/login-form.component';

const routes: Routes = [
  {
    path: '',
    component: JewelleryStoreComponent,
    children : [
      {
        path: 'login', component: LoginFormComponent
      },
      {
        path: 'home', component: CalculatorComponent, canActivate: [AuthGuardGuard]
      },
      {
        path: '', redirectTo: 'home', canActivate: [AuthGuardGuard]
      },
      {
        path: '**', redirectTo: 'not-found'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class JewelleryStoreRoutingModule { }
