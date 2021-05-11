import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'jewelleryStore', pathMatch:'prefix'},
      {
        path: 'jewelleryStore',
        loadChildren: () =>
          import(
            './jewellery-store/jewellery-store.module'
          ).then(m => m.JewelleryStoreModule)

      },
      {
        path: '',
        redirectTo: '/jewelleryStore',
        pathMatch: 'full'
      }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
