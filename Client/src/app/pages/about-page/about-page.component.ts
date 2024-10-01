import { Component } from '@angular/core';

import { PageTitleComponent } from '../../components/page-title/page-title.component';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';

@Component({
  selector: 'app-about-page',
  standalone: true,
  imports: [
    PageTitleComponent,
    NzCollapseModule
  ],
  templateUrl: './about-page.component.html',
  styleUrl: './about-page.component.css'
})
export class AboutPageComponent {

}
