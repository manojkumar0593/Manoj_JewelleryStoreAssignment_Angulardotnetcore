import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Observable, Subject, Subscription } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { DataService } from 'src/app/shared/services/data-service.service';
import { SpinnerService } from 'src/app/shared/services/spinner-service.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit, OnDestroy {
  private unsubscribe$: Subject<void> = new Subject<void>();
  formLogin: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  // response: Observable<any> = new Subscription();

  constructor(private dataService: DataService,
    private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private messageService: MessageService) {

          if (this.dataService.userDetail && this.dataService.userDetail.emailId) {
            this.router.navigate(['/home']);
        }

        }

  ngOnInit(): void {
    this.formLogin = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
  });

  // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';
  }

  get form() { return this.formLogin.controls; }

  onFormSubmit() {
        this.submitted = true;


        if (this.formLogin.invalid) {
            return;
        }

        this.loading = true;
        this.login();

    }
    login() {
      const requestData = {
        emailId: this.form.username.value,
        password: this.form.password.value
      }
      this.dataService.httpParams = new HttpParams();
      this.dataService.postJSONFromService('v1/User/Login',requestData)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        data => {
          console.log(data.body);
          if(data.body !== null){
            this.dataService.userDetail=data.body;
            this.dataService.userDetailEvent.next(data.body);
            this.router.navigate([this.returnUrl]);
          } else {
            console.log("user details not found");
            this.messageService.add({severity:'error', summary:'Error Occured: ' + 'User not found'});
            this.loading = false;
          }
          SpinnerService.end();
        },
        error => {
          SpinnerService.end();
          console.log(error);
          // this.messageService.add({severity:'error', summary:'Error Occured: ' + error});
          this.loading = false;

          if(error && error.status == 401) {
            this.messageService.add({severity:'error', summary:'Access denied. You are not authorized to use this tool. Please contact admin.'});

          } else {
           this.messageService.add({severity:'error', summary:'Error Occured: ' + error.message});
          }
        });

    }



    ngOnDestroy() {
      this.unsubscribe$.unsubscribe();
    }
}
