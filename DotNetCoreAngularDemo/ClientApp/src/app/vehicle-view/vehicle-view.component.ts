import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from '../services/vehicle.service';
import { Vehicle, KeyValuePair } from '../models/vehicle';
import { MasterService } from '../services/master.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin';
import { ToastrService } from 'ngx-toastr';
import { PhotoService } from '../services/photo.service';

@Component({
  styleUrls:[],
  templateUrl: './vehicle-view.component.html',
  selector:'vehicle-view'
})

export class VehicleViewComponent {
  @ViewChild("fileInput") fileInput: ElementRef;
  vehicle: Vehicle;
  vehicleId: number;
  features: any = {};
  currentTab = 'basic';

  constructor(
    private vehicleService: VehicleService,
    private masterService:MasterService,
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService,
    private photoService:PhotoService) {
    route.params.subscribe(p => { this.vehicleId = +p["id"] })
  }

  ngOnInit() {
    var source = [
      this.vehicleService.getVehicle(this.vehicleId),
      //this.masterService.getFeatures()
    ];

    Observable.forkJoin(source).subscribe(data => {
      this.vehicle = data[0];
      //this.features = data[1];
    },
      err => {
        if (err.status == 404)
          this.router.navigate(["/home"]);
      });
  }

  uploadFile() {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    this.photoService.upload(this.vehicleId, nativeElement.files[0]).subscribe(x=>console.log(x));
  }

  deleteVehicle()
  {
    if (confirm("Are you sure to delete?"))
    {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.toastrService.success("Vehicle deleted succesfully.", "Success", { timeOut: 3000 });
          this.router.navigate(['/vehicles']);
        });
    }
  }
}



