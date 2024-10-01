import { Observable } from "rxjs";
import { ICar } from "./Car";
import { CarType } from "../enums/car-type";

export interface ICarService {
    getCars(): Observable<ICar[]>
    getCarById(id: string): Observable<ICar>
    deleteCarById(id: string): Observable<any>
    addCar(name: string, decription: string, json: string, formData: FormData): Observable<any>
    moveCar(id: string, carType: CarType): Observable<any>
    updateCar(car: ICar): Observable<any>
}