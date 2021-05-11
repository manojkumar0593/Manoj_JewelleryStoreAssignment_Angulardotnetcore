import { HttpClient, HttpEvent, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { SpinnerService } from "./spinner-service.service";
import { catchError, map, timeout } from 'rxjs/operators';
import { BehaviorSubject, Observable, throwError } from "rxjs";
import { UserDetail } from "../interface/UserDetail";
import { MessageService } from "primeng/api";

@Injectable({providedIn:"root"})
export class DataService {
  dataUrl: string;
  public userDetail = new UserDetail();
  public userDetailEvent = new BehaviorSubject<UserDetail>(new UserDetail());

  public httpParams = new HttpParams();

  constructor(private http: HttpClient, private messageService: MessageService) {
    this.dataUrl=environment.apiEndPoint;
  }

  getJSONFromService(methodName: string){
    SpinnerService.start();

    return this.http.get(this.dataUrl + methodName,
      {params: this.httpParams, observe: 'response'})
      .pipe(
        timeout(150000),
        map(this.extractData, this),
        catchError(err => {
          this.handleError(err);
          return throwError(err);
        })
      );
  }

  postJSONFromService(methodName: string, requestBody: any, requestParams?: HttpParams){
    SpinnerService.start();
    let headers = new HttpHeaders();
    // headers.set('Content-Type', 'application/json; charset=utf-8');
    headers.set('Content-Type', 'application/json');
    return this.http.post(this.dataUrl + methodName,
      (requestBody != null ? JSON.stringify(requestBody): null),
      {headers: headers,params: requestParams, observe: 'response'})
      .pipe(
        timeout(150000),
        map(this.extractData, this),
        catchError(err => {
          this.handleError(err);
          return throwError(err);
        })
      );
  }

  public downloadFile(methodName: string, requestBody: any, requestParams?: HttpParams): Observable<HttpEvent<Blob>> {
    SpinnerService.start();
    let headers = new HttpHeaders();
    // headers.set('Content-Type', 'application/json; charset=utf-8');
    headers.set('Content-Type', 'application/json');
    return this.http.post(this.dataUrl + methodName,
      (requestBody != null ? JSON.stringify(requestBody): null),
      {headers: headers,params: requestParams,
         observe: 'response',
        responseType: 'blob'})
      .pipe(
        timeout(300000),
        map(res => {
          SpinnerService.end();
          return res;
        }),
        catchError(err => {
          this.handleError(err);
          return throwError(err);
        })
      );
  }

  extractData(res) {
    let body: any;
    body=res;
    return body;
  }

  handleError(error: any) {
    const errMsg = error.message || error.statusText || error || 'Server Error';
    console.log('Error Occurred : ', errMsg);
    console.log(error);
    // this.messageService.add({severity:'error', summary:'Error Occured: ' + errMsg});
  }
}
