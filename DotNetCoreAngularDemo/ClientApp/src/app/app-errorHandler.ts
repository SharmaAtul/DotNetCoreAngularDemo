
import { ToastrService } from 'ngx-toastr';
import { NgZone, ErrorHandler , Inject, Injector } from '@angular/core';


export class AppErrorHandler implements ErrorHandler {
  constructor(
    private ngZone: NgZone,
    @Inject(Injector) private injector: Injector) { }

  private get toastrService(): ToastrService {
    return this.injector.get(ToastrService);
  }

  public handleError(error: any): void {
    this.ngZone.run(() => {
      let errorTitle = 'Error';
      let errMsg = 'An unexpected error ocurred';

      //if (error instanceof Response) {
      //  const contentType = error.headers.get("Content-Type")

      //  if (contentType && contentType == "application/json") {
      //    const body = error.json();
      //    errorTitle = body.message || errorTitle;
      //    errMsg = body.detailedMessage || JSON.stringify(body);
      //  } else {
      //    errMsg = error.text();
      //  }
      //}
      this.toastrService.error(errMsg, errorTitle);
    });
  }
}
