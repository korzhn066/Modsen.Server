import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WinnerPageComponent } from './winner-page.component';

describe('WinnerPageComponent', () => {
  let component: WinnerPageComponent;
  let fixture: ComponentFixture<WinnerPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WinnerPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WinnerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
