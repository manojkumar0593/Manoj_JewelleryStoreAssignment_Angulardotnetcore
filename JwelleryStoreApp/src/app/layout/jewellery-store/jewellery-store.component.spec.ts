import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JewelleryStoreComponent } from './jewellery-store.component';

describe('JewelleryStoreComponent', () => {
  let component: JewelleryStoreComponent;
  let fixture: ComponentFixture<JewelleryStoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JewelleryStoreComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JewelleryStoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
