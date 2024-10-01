import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAddPanelComponent } from './admin-add-panel.component';

describe('AdminAddPanelComponent', () => {
  let component: AdminAddPanelComponent;
  let fixture: ComponentFixture<AdminAddPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminAddPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminAddPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
