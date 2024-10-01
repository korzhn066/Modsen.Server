import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';

@Component({
  selector: 'app-admin-add-panel',
  standalone: true,
  imports: [
    NzButtonModule,
    NzIconModule,
    RouterLink
  ],
  templateUrl: './admin-add-panel.component.html',
  styleUrl: './admin-add-panel.component.css'
})
export class AdminAddPanelComponent {
  
}
