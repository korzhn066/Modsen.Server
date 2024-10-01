import { Injectable } from '@angular/core';
import { ICarService } from '../models/СarService';
import { CarType } from '../enums/car-type';
import { ElectionCarsService } from './election-cars.service';
import { ProcessingCarsService } from './processing-cars.service';
import { RentCarsService } from './rent-cars.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { CARS_ELECTIONS_API } from '../environments/constants';

@Injectable({
  providedIn: 'root'
})
export class CarFactoryService {
  constructor(
    private http: HttpClient,
    private electionCarsService: ElectionCarsService,
    private rentCarsService: RentCarsService,
    private processingCarsService: ProcessingCarsService
  ) { }

  public getCarService(carType: CarType) : ICarService {
    if (carType === CarType.Rent) {
      return this.rentCarsService
    } else if (carType === CarType.Elections){
      return this.electionCarsService
    } else {
      return this.processingCarsService
    }
  }

  public getWinningCarId() : Observable<any> {
    return this.http.get(`${CARS_ELECTIONS_API}cars/winning`)
  }
}
