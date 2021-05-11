import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';

import { CalculatorComponent } from './calculator.component';

describe('CalculatorComponent', () => {
  let component: CalculatorComponent;
  let fixture: ComponentFixture<CalculatorComponent>;
  let calculateBtn: DebugElement;
  let printToFileBtn: DebugElement;
  let printToPaperBtn: DebugElement;
  let printToScreenBtn: DebugElement;
  let resetBtn: DebugElement;
  let priceTxt: DebugElement;
  let weightTxt: DebugElement;
  let totalPriceLbl: DebugElement;
  let discountTxt: DebugElement;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CalculatorComponent ],
      imports: [RouterTestingModule,
        HttpClientTestingModule,
        FormsModule],
        providers:[MessageService, DialogService]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    calculateBtn = fixture.debugElement.query(By.css("button[type='submit"));

    printToFileBtn=fixture.debugElement.query(By.css(".printFile"));
    printToPaperBtn=fixture.debugElement.query(By.css(".printPaper"));
    printToScreenBtn=fixture.debugElement.query(By.css(".printScreen"));
    resetBtn=fixture.debugElement.query(By.css(".reset"));
    priceTxt=fixture.debugElement.query(By.css("input[name='price']"));
    weightTxt=fixture.debugElement.query(By.css("input[name='weight']"));
    totalPriceLbl=fixture.debugElement.query(By.css("div[name='totalPrice']"));
    discountTxt=fixture.debugElement.query(By.css("input[name='discount']"));
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('price field validity', () => {
    expect(priceTxt.nativeElement.value).toBeFalsy();
  });
  it('check total price field', () => {
    expect(priceTxt.nativeElement).toHaveClass('ng-valid');
  });

  it('check total price on calculate', () => {
    priceTxt.nativeElement.value=5000;
    weightTxt.nativeElement.value=10;

    fixture.detectChanges();
    calculateBtn.nativeElement.click();

    expect(totalPriceLbl.nativeElement.textContent).toBeTruthy();
  });

  it('check print to file is disabled', () => {
    expect(printToFileBtn.nativeElement.disabled).toBeTruthy();
  });

});
