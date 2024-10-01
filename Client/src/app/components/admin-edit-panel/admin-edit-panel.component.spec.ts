import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminEditPanelComponent } from './admin-edit-panel.component';

describe('AdminEditPanelComponent', () => {
  let component: AdminEditPanelComponent;
  let fixture: ComponentFixture<AdminEditPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminEditPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminEditPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
