import { Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { PrincipalPageComponent } from './core/components/principal-page/principal-page.component';
import { UnauthorizedComponent } from './core/components/unauthorized/unauthorized.component';

export const AppRoutes: Routes = [
  {
    path: '',
    component: PrincipalPageComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [MsalGuard],
    title: 'Dr. Cano',
    children: [
      {
        path: 'admin',
        loadChildren: () => import('./admin/admin.module').then((m) => m.AdminModule)
      },
      {
        path: 'trabajo',
        loadChildren: () => import('./workspace/workspace.module').then((m) => m.WorkspaceModule)
      },
      { path: 'inicio', redirectTo: '' }
    ]
  },
  // { path: 'login',
  //   component: AuthenticateComponent,
  //   title: 'Autenticación',
  //   canActivate: [PublicGuard]
  // },
  // { path: 'registrar',
  //   component: RegisterUserComponent,
  //   title: 'Registro',
  //   canActivate: [PublicGuard]
  // },
  {
    path: 'página-no-encontrada',
    component: NotFoundComponent,
    title: 'Página no encontrada'
  },
  {
    path: 'sin-autorización',
    component: UnauthorizedComponent,
    title: 'Sin-autorización'
  },
  {
    path: '**',
    redirectTo: '/página-no-encontrada'
  }
]
