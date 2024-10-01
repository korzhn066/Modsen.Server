import { Injectable } from '@angular/core';
import { ICarService } from '../models/СarService';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ICar } from '../models/Car';
import { CarType } from '../enums/car-type';
import { CARS_CONTROL_API } from '../environments/constants';

@Injectable({
  providedIn: 'root'
})
export class ProcessingCarsService implements ICarService {
  constructor(private http: HttpClient) { }

  public getCars(): Observable<any> {
    return this.http.get<any>(`${CARS_CONTROL_API}cars/processing`, {
      responseType: 'json'
    })
  }

  public deleteCarById(id: string): Observable<any> {
    return this.http.delete(`${CARS_CONTROL_API}cars/processing/${id}`)
  }

  public getCarById(id: string): Observable<ICar> {
    return this.http.get<ICar>(`${CARS_CONTROL_API}cars/processing/${id}`)
  }

  public updateCar(car: ICar) : Observable<any> {
    return this.http.put(`${CARS_CONTROL_API}cars/processing`, {
      "id": car._id,
      "name": car.name,
      "description": car.description,
      "json": JSON.stringify(car.content)
    })
  }

  public addCar(name: string, decription: string, json: string, formData: FormData): Observable<any> {
    return this.http.post(`${CARS_CONTROL_API}cars/processing?Name=${name}&Description=${decription}
      &Json={parametrs:${json}}`,
      formData
    )
  }

  public moveCar(id: string, carType: CarType): Observable<any> {
    return this.http.post(`${CARS_CONTROL_API}cars/processing/move`, {
      "id": id,
      "carType": carType
    })
  }
}
