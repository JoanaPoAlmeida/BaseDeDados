import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { LeituraComponent } from './leitura/leitura.component';
import { DboLeituraComponent } from './dbo-leitura/dbo-leitura.component';

export const routes: Routes = [
  { path: '', redirectTo: '/leitura', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'leitura',
        component: LeituraComponent
      },
      {
        path: 'dbo-leitura',
        component: DboLeituraComponent
      },
    ]
  },
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
