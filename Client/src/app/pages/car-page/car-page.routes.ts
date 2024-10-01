import { Routes } from '@angular/router';
import { CarPageComponent } from '../car-page/car-page.component';
import { AddCarPageComponent } from '../add-car-page/add-car-page.component';
import { CarType } from '../../enums/car-type';
import { carTypeResolver } from '../../resolvers/car-type.resolver';
import { EditCarPageComponent } from '../edit-car-page/edit-car-page.component';

export const CAR_PAGE_ROUTES: Routes = [
  { 
    path: '', 
    component: CarPageComponent 
  },
  {
    path: 'edit',
    component: EditCarPageComponent,
    resolve: {
      carType: carTypeResolver
    }
  }
];