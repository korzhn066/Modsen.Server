import { Component, Input, OnInit } from '@angular/core';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { CarType } from '../../enums/car-type';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CarFactoryService } from '../../services/car-factory.service';

@Component({
  selector: 'app-admin-edit-panel',
  standalone: true,
  imports: [
    NzButtonModule,
    NzIconModule,
    RouterLink
  ],
  templateUrl: './admin-edit-panel.component.html',
  styleUrl: './admin-edit-panel.component.css'
})
export class AdminEditPanelComponent{
  @Input() carType: CarType = CarType.Rent
  
  enum: typeof CarType = CarType

  constructor(
    private carFactoryService: CarFactoryService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  delete() {
    this.route.params.subscribe(params => {
      this.carFactoryService.getCarService(this.carType).deleteCarById(params['id']).subscribe({
        next: () => this.redirect(this.carType)
      })
    })
  }

  move(to: CarType) {
    this.route.params.subscribe(params => {
      this.carFactoryService.getCarService(this.carType).moveCar(params['id'], to).subscribe({
        next: () => this.redirect(to)
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
