import { Routes, RouterModule } from '@angular/router';

export const CoreRoutes: Routes = [
  {
    path: '',
    children: [
      // {
      //   path: 'administracion',
      //   loadChildren: () => import('./../admin/admin.module').then(m => m.AdminModule)
      // },
      // { path: '', component: PrincipalPageComponent },
      // { path: 'login', component: LoginComponent },
      // { path: 'página-no-encontrada', component: NotFoundComponent },
      // { path: 'error-del-sistema', component: ServerErrorComponent },
      { path: '**', redirectTo: '/página-no-encontrada' }
    ]
  }
];
