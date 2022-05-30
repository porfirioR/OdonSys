import { Routes } from '@angular/router';
import { AuthenticateComponent } from './core/components/authenticate/authenticate.component';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { PrincipalPageComponent } from './core/components/principal-page/principal-page.component';
import { RegisterUserComponent } from './core/components/register-user/register-user.component';
import { AuthGuard } from './core/guards/auth.guard';

export const AppRoutes: Routes = [
  {
    path: '',
    component: PrincipalPageComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'admin',
        loadChildren: () => import('./admin/admin.module').then((m) => m.AdminModule)
      },
      {
        path: 'trabajo',
        loadChildren: () => import('./workspace/workspace.module').then((m) => m.WorkspaceModule)
      }
    ]
  },
  { path: 'login', component: AuthenticateComponent },
  { path: 'registrar', component: RegisterUserComponent },
  { path: 'página-no-encontrada', component: NotFoundComponent },
  { path: '**', redirectTo: '/página-no-encontrada' },
  
];
