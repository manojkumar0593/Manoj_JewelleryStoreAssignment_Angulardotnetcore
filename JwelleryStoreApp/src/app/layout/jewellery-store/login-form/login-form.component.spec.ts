import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DebugElement } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { MessageService } from 'primeng/api';
import { UserDetail } from 'src/app/shared/interface/UserDetail';
import { DataService } from 'src/app/shared/services/data-service.service';

import { LoginFormComponent } from './login-form.component';

describe('LoginFormComponent', () => {
  let component: LoginFormComponent;
  let fixture: ComponentFixture<LoginFormComponent>;
  let submitBtn: DebugElement;
  let usernameTxt: DebugElement;
  let passwordTxt: DebugElement;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports:[HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        FormsModule],
      declarations: [ LoginFormComponent ],
      providers:[DataService, MessageService]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    submitBtn = fixture.debugElement.query(By.css("button"));
    usernameTxt = fixture.debugElement.query(By.css("input[type='text'"));
    passwordTxt = fixture.debugElement.query(By.css("input[type='password'"));

  });

  it('Without entering username and password triggers validation on submit', () => {
    let $event;
    submitBtn.triggerEventHandler('click', $event);
    // console.log($event);
    expect(component.formLogin.valid).toBeFalsy();
  });

  it('username field validity', () => {
    let username = component.formLogin.controls['username'];
    expect(username.valid).toBeFalsy();
  });

  it('username field required validity', () => {
    let errors = {};
    let username = component.formLogin.controls['username'];
    errors = username.errors || {};
    expect(errors['required']).toBeTruthy();
  });


  it('submitting a login form and checkin if login is called', () => {

    expect(component.formLogin.valid).toBeFalsy();
    //console.log(component.formLogin);
    component.formLogin.controls['username'].setValue("admin");
    component.formLogin.controls['password'].setValue("admin123");
    fixture.detectChanges();
     expect(component.formLogin.valid).toBeTruthy();

    spyOn(component, 'login').and.returnValue();
    var btn = submitBtn.nativeElement;
    btn.click();

    expect(component.login).toHaveBeenCalled();

  });

});
