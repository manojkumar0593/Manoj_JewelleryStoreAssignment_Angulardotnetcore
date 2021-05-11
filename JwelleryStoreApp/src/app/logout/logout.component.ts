import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserDetail } from '../shared/interface/UserDetail';
import { DataService } from '../shared/services/data-service.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.scss']
})
export class LogoutComponent implements OnInit {

  constructor(private dataService: DataService, private router: Router, private zone: NgZone) {
    this.dataService.userDetail = null;
    this.dataService.userDetailEvent.next(new UserDetail);
    // sessionStorage.clear();
  }

  ngOnInit(): void {
  }

  login() {
    this.zone.run(()=>this.router.navigate(['/store']));
  }

}
