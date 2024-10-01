import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CarFactoryService } from '../../services/car-factory.service';
import { CarType } from '../../enums/car-type';
import { FormsModule } from '@angular/forms';
import { ICar } from '../../models/Car';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { PageTitleComponent } from "../../components/page-title/page-title.component";

@Component({
  selector: 'app-edit-car-page',
  standalone: true,
  imports: [
    FormsModule,
    NzTableModule,
    NzCarouselModule,
    NzInputModule,
    NzButtonModule,
    PageTitleComponent
],
  templateUrl: './edit-car-page.component.html',
  styleUrl: './edit-car-page.component.css'
})
export class EditCarPageComponent implements OnInit{
  carType: CarType = CarType.Rent
  car?: ICar

  constructor(
    private route: ActivatedRoute,
    private carFactoryService: CarFactoryService,
    private router: Router
  ) { 
    this.carType = route.snapshot.data['carType']
  }

  updateCar() {
    if (this.car == null) {
      return
    }

    this.carFactoryService.getCarService(this.carType).updateCar(this.car).subscribe({
      next: () => {},
      error: error => console.log(error)
    })
  }

  addParametr() {
    this.car?.content.parametrs.push({name: '', body: ''})
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.carFactoryService.getCarService(this.carType).getCarById(params['id']).subscribe((car: ICar) => {
        this.car = car
      }) 
    })
  }

  redirect(to: CarType) {
    switch (to) {
      case CarType.Rent: 
        this.router.navigate(["rent"])
        break
      case CarType.Processing: 
        this.router.navigate(["processing"])
        break
      case CarType.Elections: 
        this.router.navigate(["elections"])
        break
    }
  }
}
