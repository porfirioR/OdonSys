import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { NotFoundComponent } from './layout/not-found/not-found.component';
import { PrincipalPageComponent } from './layout/principal-page/principal-page.component';

export const AppRoutes: Routes = [
  {
    path: '',
    component: AppComponent,
    children: [
      { path: '', component: PrincipalPageComponent },
      {
        path: 'admin',
        loadChildren: () => import('./admin/admin.module').then((m) => m.AdminModule)
      }
    ]
  },
  // { path: 'login', component: LoginComponent },
  { path: 'página-no-encontrada', component: NotFoundComponent },
  { path: '**', redirectTo: '/página-no-encontrada' }
];
