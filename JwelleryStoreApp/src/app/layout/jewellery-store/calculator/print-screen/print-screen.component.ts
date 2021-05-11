import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-print-screen',
  templateUrl: './print-screen.component.html',
  styleUrls: ['./print-screen.component.scss']
})
export class PrintScreenComponent implements OnInit {

  storedData: any;
  constructor(private router: Router, public ref: DynamicDialogRef, public config: DynamicDialogConfig) { }

  ngOnInit(): void {
    if(this.config.data) {
      this.storedData = this.config.data.core;
      this.storedData.isPriviledged = this.config.data.isPriviledged;
    }
  }

  OnClose() {
    const response ={
      data: {

      }
    }
    this.ref.close(response);
  }
}
