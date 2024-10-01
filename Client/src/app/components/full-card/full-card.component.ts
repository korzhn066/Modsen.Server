import { Component, Input } from '@angular/core';

import { NzTableModule } from 'ng-zorro-antd/table';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import { ICar } from '../../models/Car';

interface Person {
  name: string;
  age: number;
}

@Component({
  selector: 'app-full-card',
  standalone: true,
  imports: [
    NzTableModule,
    NzCarouselModule,
],
  templateUrl: './full-card.component.html',
  styleUrl: './full-card.component.css'
})
export class FullCardComponent {
  @Input() car?: ICar 
  
  listOfData: Person[] = [
    {
      name: 'John Brown',
      age: 32,
    },
    {
      name: 'Jim Green',
      age: 42,
    },
    {
      name: 'Joe Black',
      age: 32,
    },
  ];

  array = [1, 2, 3, 4];
}
