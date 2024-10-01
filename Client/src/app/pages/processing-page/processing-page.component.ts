import { Component, OnInit } from '@angular/core';
import { PageTitleComponent } from '../../components/page-title/page-title.component';
import { CardGridComponent } from '../../components/card-grid/card-grid.component';
import { AdminAddPanelComponent } from "../../components/admin-add-panel/admin-add-panel.component";
import { ProcessingCarsService } from '../../services/processing-cars.service';
import { ICar } from '../../models/Car';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-processing-page',
  standalone: true,
  imports: [
    PageTitleComponent,
    CardGridComponent,
    AdminAddPanelComponent
],
  templateUrl: './processing-page.component.html',
  styleUrl: './processing-page.component.css'
})
export class ProcessingPageComponent implements OnInit {
  cars?: ICar[]

  constructor(
    public authService: AuthService,
    private processingCarService: ProcessingCarsService
  ) {
  }

  ngOnInit(): void {
    this.processingCarService.getCars().subscribe({
      next: (cars: ICar[]) => this.cars = cars,
      error: () => { }
    })
  }
}
