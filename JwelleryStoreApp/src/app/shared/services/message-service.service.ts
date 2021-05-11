import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";


@Injectable()
export class DisplayMessageService {

  public static growlMessages$ = new BehaviorSubject<any>({
    "severity":"-1",
    "summary":"",
    "visibleTime":""
  });

  public static dialogMessages$ = new BehaviorSubject<any>({
    "header":"-1",
    "message":""
  });

  public static showMessage(messageData: any) {
    this.growlMessages$.next(messageData);
  }

  public static showDialog(dialogData: any) {
    this.dialogMessages$.next(dialogData);
  }

}
