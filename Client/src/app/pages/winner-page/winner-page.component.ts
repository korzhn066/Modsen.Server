import { Component, OnInit } from '@angular/core';
import { PageTitleComponent } from '../../components/page-title/page-title.component';
import { FullCardComponent } from "../../components/full-card/full-card.component";
import { ICar } from '../../models/Car';
import { CarFactoryService } from '../../services/car-factory.service';
import { CarType } from '../../enums/car-type';

@Component({
  selector: 'app-winner-page',
  standalone: true,
  imports: [PageTitleComponent, FullCardComponent],
  templateUrl: './winner-page.component.html',
  styleUrl: './winner-page.component.css'
})
export class WinnerPageComponent implements OnInit {
  car?: ICar

  constructor (private carFactoryService: CarFactoryService) {}

  ngOnInit(): void {
    this.carFactoryService.getWinningCarId().subscribe({
      next: (data: any) => {
        this.carFactoryService.getCarService(CarType.Elections).getCarById(data.id).subscribe({
          next: (car: ICar) => this.car = car
        })
      }
    })
  }
}
