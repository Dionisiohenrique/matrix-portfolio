import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/home/home').then(m => m.Home) },
  { path: 'projects', loadComponent: () => import('./pages/projects/projects').then(m => m.Projects) },
  { path: 'contact', loadComponent: () => import('./pages/contact/contact').then(m => m.Contact) },
  { path: 'admin', loadComponent: () => import('./pages/admin/admin').then(m => m.Admin) },
];
