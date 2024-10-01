import { Directive, EventEmitter, HostBinding, HostListener, Output } from '@angular/core';

@Directive({
  selector: '[appFileDragAndDrop]',
  standalone: true
})
export class FileDragAndDropDirective {
  @Output() private filesChangeEmiter : EventEmitter<File[]> = new EventEmitter();

  constructor() { }

  @HostListener('dragover', ['$event']) public onDragOver(evt: any){
    evt.preventDefault();
    evt.stopPropagation();
  }

  @HostListener('dragleave', ['$event']) public onDragLeave(evt: any){
    evt.preventDefault();
    evt.stopPropagation();
  }

  @HostListener('drop', ['$event']) public onDrop(evt: any){
    evt.preventDefault();
    evt.stopPropagation();
    let files = evt.dataTransfer.files;
    let valid_files : Array<File> = files;
    this.filesChangeEmiter.emit(valid_files);
  }

}
