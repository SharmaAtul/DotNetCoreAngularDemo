"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AppErrorHandler = /** @class */ (function () {
    function AppErrorHandler(ngZone, 
    /*@Inject(ToastrService)*/ toastrService) {
        this.ngZone = ngZone;
        this.toastrService = toastrService;
    }
    AppErrorHandler.prototype.handleError = function (error) {
        var _this = this;
        this.ngZone.run(function () {
            _this.toastrService.error("Error occured", "Error", { timeOut: 3000 });
        });
    };
    return AppErrorHandler;
}());
exports.AppErrorHandler = AppErrorHandler;
//# sourceMappingURL=app-errorHandler.js.map