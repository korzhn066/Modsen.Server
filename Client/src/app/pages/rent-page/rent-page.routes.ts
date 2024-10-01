import { Routes } from '@angular/router';
import { RentPageComponent } from './rent-page.component';
import { AddCarPageComponent } from '../add-car-page/add-car-page.component';
import { carTypeResolver } from '../../resolvers/car-type.resolver';

export const RENT_PAGE_ROUTES: Routes = [
  { 
    path: '', 
    component: RentPageComponent 
  },
  {
    path: 'add',
    component: AddCarPageComponent,
    resolve: {
      carType: carTypeResolver
    }
  },
  { 
    path: ':id',
    loadChildren: () => import('../car-page/car-page.routes').then(c => c.CAR_PAGE_ROUTES),
    resolve: {
      carType: carTypeResolver
    }
  },
];