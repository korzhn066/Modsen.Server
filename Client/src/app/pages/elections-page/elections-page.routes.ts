import { Routes } from '@angular/router';
import { ElectionsPageComponent } from './elections-page.component';
import { CarPageComponent } from '../car-page/car-page.component';
import { AddCarPageComponent } from '../add-car-page/add-car-page.component';
import { CarType } from '../../enums/car-type';
import { carTypeResolver } from '../../resolvers/car-type.resolver';

export const ELECTIONS_PAGE_ROUTES: Routes = [
  { 
    path: '', 
    component: ElectionsPageComponent 
  },
  {
    path: 'add',
    component: AddCarPageComponent,
    data: {
      carType: CarType.Elections
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