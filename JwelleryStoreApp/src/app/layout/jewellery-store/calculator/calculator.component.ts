import { HttpParams } from '@angular/common/http';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { DataService } from 'src/app/shared/services/data-service.service';
import { SpinnerService } from 'src/app/shared/services/spinner-service.service';
import { PrintScreenComponent } from './print-screen/print-screen.component';
import * as fileSaver from 'file-saver';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.scss']
})
export class CalculatorComponent implements OnInit, OnDestroy {
  private unsubscribe$: Subject<void> = new Subject<void>();
  formData: any;
  isPriviledged: boolean = false;
  constructor(private dataService: DataService,private messageService: MessageService, public dialogService: DialogService) { }
  @ViewChild('myForm') public form: NgForm;

  ngOnInit(): void {
    if(this.dataService.userDetail && this.dataService.userDetail.role == 'Privileged') {
      this.isPriviledged = true;
    }
    this.formData = {
      price: null,
      weight: null,
      totalPrice: null,
      discount: this.isPriviledged ? 2 : 0
    };

    this.getGoldPrice();
  }

  getGoldPrice() {
    this.dataService.httpParams = new HttpParams();
    this.dataService.httpParams = this.dataService.httpParams.append('jewel', 'gold');
    this.dataService.getJSONFromService('v1/Jewellery/GetJewelPrice')
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe(
      data => {
        console.log(data.body);
        if(data !== null){
          this.formData.price = data.body;
        } else {
          this.messageService.add({severity:'warn', summary:'Price not found'});
        }
        SpinnerService.end();
      },
      error => {
        SpinnerService.end();
        console.log(error);
        if(error && error.status == 401) {
          this.messageService.add({severity:'error', summary:'Access denied. You are not authorized to use this tool. Please contact admin.'});

        } else {
         this.messageService.add({severity:'error', summary:'Error Occured: ' + error});
        }
      });
  }

  CalculateTotalPrice() {
    if(!this.form.valid) {
      Object.values(this.form.controls).forEach(control => {
        control.markAsTouched();
      });
      return;
    }
    if(this.formData) {
      this.formData.totalPrice = ((this.formData.price || 0)  * (this.formData.weight || 0)) *
      ((1-((this.formData.discount || 0) / 100)));

    } else {
      this.formData.totalPrice = null;
    }
  }

  changeDiscount() {
    if(this.formData.discount && (this.formData.discount > 99 ||  this.formData.discount < 0)) {
      this.messageService.add({severity:'warn', summary:'Discount percentage should be in the range of 0 and 99'});
      this.formData.discount=null;
    }
  }

  PrintToScreen() {
    const ref = this.dialogService.open(PrintScreenComponent, {
      header: 'Jewellery Store Summary',
      width: '70%',
      data: {'core':this.formData, 'isPriviledged':this.isPriviledged}
    });

  ref.onClose.subscribe((response:any) => {
      if (response) {

      }
  });
  }

  PrintToFile() {


    const requestData = {
      price: this.formData.price,
      weight: this.formData.weight,
      discount: this.formData.discount,
      totalPrice: this.formData.totalPrice,
      isPriviledged: this.isPriviledged
    }
    this.dataService.httpParams = new HttpParams();
    this.dataService.downloadFile('v1/Jewellery/DownloadSummaryPdf',requestData)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe(
      data => {
        console.log(data);
        if(data !== null){
          this.saveToFileSystem(data);
        }
        // SpinnerService.end();
      },
      error => {
        SpinnerService.end();
        console.log(error);

        if(error && error.status == 401) {
          this.messageService.add({severity:'error', summary:'Access denied. You are not authorized to use this tool. Please contact admin.'});

        } else {
         this.messageService.add({severity:'error', summary:'Error Occured: ' + error.message});
        }
      });


  }

  PrintToPaper() {


    const requestData = {
      price: this.formData.price,
      weight: this.formData.weight,
      discount: this.formData.discount,
      totalPrice: this.formData.totalPrice,
      isPriviledged: this.isPriviledged
    }
    this.dataService.httpParams = new HttpParams();
    this.dataService.postJSONFromService('v1/Jewellery/PrintReport',requestData)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe(
      data => {
        console.log(data);
        if(data !== null){
          console.log('got response for print');
        }
        // SpinnerService.end();
      },
      error => {
        SpinnerService.end();
        console.log(error);

        if(error && error.status == 401) {
          this.messageService.add({severity:'error', summary:'Access denied. You are not authorized to use this tool. Please contact admin.'});

        } else {
         this.messageService.add({severity:'error', summary:'Error Occured: Status Code ' + error.status + " ." +  error.error || error.message});
        }
      });



  }

  Reset() {
    this.formData.weight= null;
    this.formData.totalPrice= null;
    this.formData.discount= this.isPriviledged ? 2 : 0;
  }

  private saveToFileSystem(data) {
    if(data) {
      let type='';
      let defaultFileName;
      if(data.headers !== undefined) {
        type=data.headers.get('Content-Type');
        const disposition = data.headers.get('Content-Disposition');
        if(disposition) {
          const match = disposition.match(/.*filename=\*?([^;\"]+)\"?.*/);
          if(match[1]) {
            defaultFileName=match[1];
          }
        }
        defaultFileName = defaultFileName.replace(/[<>:"\/\\|?*]+/g,'_');
      }
      const blob = new Blob([data.body], {type: type});
      fileSaver.saveAs(blob, defaultFileName);
      }

  }

  ngOnDestroy() {
    this.unsubscribe$.unsubscribe();
  }

}
