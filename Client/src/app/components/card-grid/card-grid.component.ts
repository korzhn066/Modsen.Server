import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzGridModule } from 'ng-zorro-antd/grid';

import { ICar } from '../../models/Car';
import { CardComponent } from '../card/card.component';

@Component({
  selector: 'app-card-grid',
  standalone: true,
  imports: [
    NzCardModule,
    NzGridModule,
    RouterLink,
    CardComponent
  ],
  templateUrl: './card-grid.component.html',
  styleUrl: './card-grid.component.css'
})
export class CardGridComponent {
  @Input()
  cars: ICar[] = []
}
