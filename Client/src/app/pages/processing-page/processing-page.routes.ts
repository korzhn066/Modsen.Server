import { Routes } from '@angular/router';
import { ProcessingPageComponent } from './processing-page.component';
import { carTypeResolver } from '../../resolvers/car-type.resolver';
import { AddCarPageComponent } from '../add-car-page/add-car-page.component';

export const PROCESSING_PAGE_ROUTES: Routes = [
  { 
    path: '', 
    component: ProcessingPageComponent
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