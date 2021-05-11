import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({providedIn:"root"})
export class SpinnerService {
  public static show$ = new BehaviorSubject<boolean>(false);

  public static start() {
    this.show$.next(true);
  }
  public static end() {
    this.show$.next(false);
  }
  public static endAfter(value: number) {
    setTimeout(()=>{
      this.show$.next(false);
    },value)
  }


}
