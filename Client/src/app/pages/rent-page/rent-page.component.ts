import { AfterViewInit, Component, OnInit } from '@angular/core';
import { CardGridComponent } from '../../components/card-grid/card-grid.component';
import { PageTitleComponent } from '../../components/page-title/page-title.component';
import { AdminAddPanelComponent } from "../../components/admin-add-panel/admin-add-panel.component";
import { AuthService } from '../../services/auth.service';
import { ICar } from '../../models/Car';
import { RentCarsService } from '../../services/rent-cars.service';

@Component({
  selector: 'app-rent-page',
  standalone: true,
  imports: [
    CardGridComponent,
    PageTitleComponent,
    AdminAddPanelComponent
],
  templateUrl: './rent-page.component.html',
  styleUrl: './rent-page.component.css'
})
export class RentPageComponent implements OnInit{
  cars: ICar[] = []

  constructor(
    public authService: AuthService,
    private rentCarsService: RentCarsService
  ) { }

  ngOnInit(): void {
    this.rentCarsService.getCars().subscribe({
      next: (cars: ICar[]) => { 
        console.log(cars)
        this.cars = cars
        console.log(this.cars)
      },
    })
  }
}