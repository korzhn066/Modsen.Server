import { Component, HostBinding, HostListener, Input, Output } from '@angular/core';
import { PageTitleComponent } from '../../components/page-title/page-title.component';
import { FileDragAndDropDirective } from '../../directives/file-drag-and-drop.directive';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { NzInputModule } from 'ng-zorro-antd/input';
import { ICarParametr } from '../../models/CarParametr';
import { FormsModule } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { CarType } from '../../enums/car-type';
import { CarFactoryService } from '../../services/car-factory.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-car-page',
  standalone: true,
  imports: [
    PageTitleComponent,
    FileDragAndDropDirective,
    NzInputModule,
    NzButtonModule,
    FormsModule
  ],
  templateUrl: './add-car-page.component.html',
  styleUrl: './add-car-page.component.css'
})
export class AddCarPageComponent {
  carType: CarType = CarType.Rent

  constructor (
    private carFactory: CarFactoryService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.carType = route.snapshot.data['carType']
  }
  
  files: FileList[] = []
  name: string = ''
  decription: string = ''
  carParametrs: ICarParametr[] = []

  addParametr() {
    this.carParametrs.push({name: '', body: ''})
  }

  onFileChange(files: any) {
    this.files = this.files.concat(files)
  }

  send() {
    const formData: FormData = new FormData()  

    for (let file of this.files) {
      formData.append('formFiles', file.item(0)!, file.item(0)!.name)
    }

    this.carFactory.getCarService(this.carType).addCar(
      this.name, 
      this.decription, 
      JSON.stringify(this.carParametrs),
      formData
    ).subscribe({
      next: () => this.redirect(this.carType)
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