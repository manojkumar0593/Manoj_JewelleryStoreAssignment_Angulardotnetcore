import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DataService } from '../services/data-service.service';


@Injectable()
export class APIInterceptor implements HttpInterceptor {
    constructor(private dataService: DataService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      let headers = new HttpHeaders({
      // headers.set('Content-Type', 'application/json; charset=utf-8');
      'Content-Type': 'application/json',
      'Cache': 'secure',
      'Cache-Control': 'no-cache, no-store, max-age=0, must-revalidate',
      'Pragma': 'no-cache',
      'Expires': '-1',
      'Content-Security-Policy':"default-src 'self'; script-src 'self' 'unsafe-inline' 'unsafe-eval'; style-src 'self' 'unsafe-inline'",
      'X-Content-Type-Options': 'nosniff',
      'Referrer-Policy':'same-origin',
      'Feature-Policy': "vibrate 'self'; usermedia *; sync-xhr 'self'",
      'Access-Control-Allow-Origin':'*',
      'Access-Control-Allow-Credential':'true',
      'Access-Control-Allow-Method':'*',
      });
      if(!(request && request.body && request.body.toString() === '[object FormData]')) {
        headers.append('Content-Type', 'application/json');
      }
      request = request.clone({
        headers: headers
      });
        const currentUser = this.dataService.userDetail;
        if (currentUser && currentUser.emailId) {
            request = request.clone({
              //headers: headers
                setHeaders: {
                    Authorization: `Bearer ${currentUser.token}`
                }
            });
        }

        return next.handle(request);
    }
}
