import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICar } from '../models/Car';
import { ICarService } from '../models/СarService';
import { CarType } from '../enums/car-type';
import { CARS_CONTROL_API } from '../environments/constants';

@Injectable({
  providedIn: 'root'
})
export class ElectionCarsService implements ICarService {
  constructor(private http: HttpClient) { }

  public getCars(): Observable<ICar[]> {
    return this.http.get<ICar[]>(`${CARS_CONTROL_API}cars/election`, {
      responseType: 'json'
    })
  }

  public deleteCarById(id: string): Observable<any> {
    return this.http.delete(`${CARS_CONTROL_API}cars/election/${id}`)
  }

  public getCarById(id: string): Observable<ICar> {
    return this.http.get<ICar>(`${CARS_CONTROL_API}cars/election/${id}`)
  }

  public updateCar(car: ICar) : Observable<any> {
    return this.http.put(`${CARS_CONTROL_API}cars/election`, {
      "id": car._id,
      "name": car.name,
      "description": car.description,
      "json": JSON.stringify(car.content)
    })
  }

  public addCar(name: string, decription: string, json: string, formData: FormData): Observable<any> {
    return this.http.post(`${CARS_CONTROL_API}cars/election?Name=${name}&Description=${decription}
      &Json={parametrs:${json}}`,
      formData
    )
  }

  public moveCar(id: string, carType: CarType): Observable<any> {
    return this.http.post(`${CARS_CONTROL_API}cars/election/move`, {
      "id": id,
      "carType": carType
    })
  }
}

