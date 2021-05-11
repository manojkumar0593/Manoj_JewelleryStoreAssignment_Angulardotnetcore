import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { DialogService, DynamicDialogConfig, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog';

import { PrintScreenComponent } from './print-screen.component';

describe('PrintScreenComponent', () => {
  let component: PrintScreenComponent;
  let fixture: ComponentFixture<PrintScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports:[
        RouterTestingModule,
        DynamicDialogModule],
      declarations: [ PrintScreenComponent
      ],
      providers:[
        DialogService, DynamicDialogRef,DynamicDialogConfig]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrintScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
