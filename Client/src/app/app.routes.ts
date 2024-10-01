import { Routes } from '@angular/router';
import { adminGuard } from './guards/admin.guard';

export const routes: Routes = [
  { 
    path: '',  
    redirectTo: 'rent',
    pathMatch: 'full' 
  },
  { 
    path: 'login', 
    loadChildren: () => import('./pages/login-page/login-page.routes').then(l => l.LOGIN_PAGE_ROUTES) },
  { 
    path: 'registration', 
    loadChildren: () => import('./pages/registration-page/registration-page.routes').then(r => r.REGISTRATION_PAGE_ROUTES) },
  { 
    path: 'winner', 
    loadChildren: () => import('./pages/winner-page/winner-page.routes').then(w => w.WINNER_PAGE_ROUTES) },
  { 
    path: 'rent', 
    loadChildren: () => import('./pages/rent-page/rent-page.routes').then(r => r.RENT_PAGE_ROUTES) },
  { 
    path: 'elections', 
    loadChildren: () => import('./pages/elections-page/elections-page.routes').then(e => e.ELECTIONS_PAGE_ROUTES) 
  },
  { 
    path: 'about', 
    loadChildren: () => import('./pages/about-page/about-page.routes').then(a => a.ABOUT_PAGE_ROUTES) 
  },
  { 
    path: 'processing', 
    loadChildren: () => import('./pages/processing-page/processing-page.routes').then(p => p.PROCESSING_PAGE_ROUTES),
    canActivate: [adminGuard]
  },
  { 
    path: 'users', 
    loadChildren: () => import('./pages/users-page/users-page.routes').then(p => p.USERS_PAGE_ROUTES),
    canActivate: [adminGuard]
  },
  {
    path: '**',
    redirectTo: 'rent'
  },
];
