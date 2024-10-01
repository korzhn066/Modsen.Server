import { Component } from '@angular/core';

import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputGroupComponent } from 'ng-zorro-antd/input';
import { NzButtonComponent } from 'ng-zorro-antd/button';;

@Component({
  selector: 'app-registration-page',
  standalone: true,
  imports: [
    NzFormModule,
    NzInputGroupComponent,
    NzButtonComponent,],
  templateUrl: './registration-page.component.html',
  styleUrl: './registration-page.component.css'
})
export class RegistrationPageComponent {
  submitForm(): void {
    
  }
}
