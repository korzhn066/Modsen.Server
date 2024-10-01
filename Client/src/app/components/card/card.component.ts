import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NzCardModule } from 'ng-zorro-antd/card';

import { ICar } from '../../models/Car';

@Component({
  selector: 'app-card',
  standalone: true,
  imports: [
    NzCardModule,
    RouterModule
  ],
  templateUrl: './card.component.html',
  styleUrl: './card.component.css'
})
export class CardComponent {
  @Input()
  car: ICar = { _id: '', photos: [''], name: '', description: '', content: '' }
}
