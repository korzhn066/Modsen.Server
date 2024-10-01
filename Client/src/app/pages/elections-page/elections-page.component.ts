import { Component, OnInit } from '@angular/core';
import { PageTitleComponent } from '../../components/page-title/page-title.component';
import { CardGridComponent } from '../../components/card-grid/card-grid.component';
import { AdminAddPanelComponent } from '../../components/admin-add-panel/admin-add-panel.component';
import { AuthService } from '../../services/auth.service';
import { ElectionCarsService } from '../../services/election-cars.service';
import { ICar } from '../../models/Car';

@Component({
  selector: 'app-elections-page',
  standalone: true,
  imports: [
    PageTitleComponent,
    CardGridComponent,
    AdminAddPanelComponent
  ],
  templateUrl: './elections-page.component.html',
  styleUrl: './elections-page.component.css'
})
export class ElectionsPageComponent implements OnInit {
  cars: ICar[] = []

  constructor (
    public authService: AuthService, 
    private electionCarsService: ElectionCarsService
  ) { }

  ngOnInit(): void {
    this.electionCarsService.getCars().subscribe({
      next: (cars: ICar[]) => this.cars = cars,
      error: () => { }
    })
  }
}