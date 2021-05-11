import { AfterViewChecked, ChangeDetectorRef, Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { Data, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DataService } from '../shared/services/data-service.service';
import { SpinnerService } from '../shared/services/spinner-service.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit, AfterViewChecked, OnDestroy {

  displaySpinner = false;
  user: any;
  spinnerSubscription: Subscription;
  constructor(private cd: ChangeDetectorRef, private router: Router, private dataService: DataService,private zone: NgZone) { }

  ngAfterViewChecked() {
    this.spinnerSubscription = SpinnerService.show$.subscribe(show=> {
      if (this.displaySpinner !== show) {
        this.displaySpinner = show;
        this.cd.detectChanges();
      }
    });

  }
  ngOnInit() {

    this.dataService.userDetailEvent.subscribe(
      (data)=>
      {
        this.user = data;
      });
    this.user = {
      emailId:"",
      userName: ""
    }
  }
  logout() {
    this.zone.run(()=> this.router.navigate(['/logout']));

  }

  ngOnDestroy() {
    this.spinnerSubscription.unsubscribe();
  }
}
