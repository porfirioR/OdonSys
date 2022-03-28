import { Routes } from '@angular/router';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { PrincipalPageComponent } from './core/components/principal-page/principal-page.component';

export const AppRoutes: Routes = [
  {
    path: '',
    component: PrincipalPageComponent,
    children: [
      {
        path: 'admin',
        loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
      },
      { path: '', component: PrincipalPageComponent },
      // { path: 'login', component: LoginComponent },
      { path: 'página-no-encontrada', component: NotFoundComponent },
      // { path: 'error-del-sistema', component: ServerErrorComponent },
      { path: '**', redirectTo: '/página-no-encontrada' }
    ]
  }
];

