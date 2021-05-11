import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path: '', component: AppComponent, pathMatch: 'prefix',
    children: [
      { path: 'store', loadChildren:()=> import('./layout/layout.module').then(m=>m.LayoutModule) },
      { path: '', redirectTo:'store', pathMatch:'prefix' },
      { path: 'logout', loadChildren:()=> import('./logout/logout.module').then(m=>m.LogoutModule) }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
