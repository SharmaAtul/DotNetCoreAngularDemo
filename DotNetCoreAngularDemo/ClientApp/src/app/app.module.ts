import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { MasterService } from './services/master.service';
import { VehicleService } from './services/vehicle.service';
import { AppErrorHandler } from './app-errorHandler';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { VehicleViewComponent } from './vehicle-view/vehicle-view.component';
import { PhotoService } from './services/photo.service';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehicleViewComponent,
    VehicleListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    HttpModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    NgxPaginationModule,
    RouterModule.forRoot([
      { path: '', redirectTo:"vehicles", pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'home', component: HomeComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'vehicles', component: VehicleListComponent },
      { path: 'vehicles/:id', component: VehicleViewComponent },
      { path: 'vehicles/new', component: VehicleFormComponent },
      { path: 'vehicles/edit/:id', component: VehicleFormComponent },
    ])
  ],
  providers: [
    MasterService,
    VehicleService,
    PhotoService,
    { provide: ErrorHandler, useClass: AppErrorHandler}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
