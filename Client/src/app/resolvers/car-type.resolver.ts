import { ResolveFn } from '@angular/router';
import { CarType } from '../enums/car-type';

export const carTypeResolver: ResolveFn<CarType> = (route, state) => {
  const routeType: string = state.url.split('/')[1]

  if (routeType === 'elections') {
    return CarType.Elections
  } else if (routeType === 'rent') {
    return CarType.Rent
  } else {
    return CarType.Processing
  }
};
