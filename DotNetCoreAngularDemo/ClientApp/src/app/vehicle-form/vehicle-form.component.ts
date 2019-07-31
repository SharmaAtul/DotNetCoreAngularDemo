import * as _ from 'underscore'
import { Component } from '@angular/core';
import { MasterService } from '../services/master.service';
import { VehicleService } from '../services/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SaveVehicle, Vehicle } from '../models/vehicle';
import 'rxjs/add/observable/forkJoin';

@Component({
  selector: "vehicle",
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent {
  makes: any[];
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id: 0,
    isRegistered: false,
    makeId: 0,
    modelId: 0,
    features: [],
    contact: {
      email: "",
      name: "",
      phone: ""
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private masterService: MasterService,
    private vehicleService: VehicleService,
    private toastrService: ToastrService
  ) {
    route.params.subscribe(p => { this.vehicle.id = +p["id"] || 0 });
  }

  ngOnInit() {

    var source = [
      this.masterService.getMakes(),
      this.masterService.getFeatures()
    ];

    if (this.vehicle.id)
      source.push(this.vehicleService.getVehicle(this.vehicle.id));

    Observable.forkJoin(source).subscribe(data => {
      this.makes = data[0];
      this.features = data[1];

      if (this.vehicle.id) {
        this.setVehicle(data[2]);
        this.populateModels();
      }
    },
      err => {
        if (err.status == 404)
          this.router.navigate(['/vehicles/edit/' + x.id]);
      }
    )
  }

  private setVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.contact = v.contact;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.features = _.pluck(v.features, 'id');
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels()
{
    var selectedMake = this.makes.find(x => x.id == this.vehicle.makeId);
    if (selectedMake !== undefined)
      this.models = selectedMake.models;
}

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked)
      this.vehicle.features.push(featureId);
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    var $result = (this.vehicle.id) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle);

    $result.subscribe(result => {
      this.toastrService.success("Vehicle updated succesfully.", "Success", { timeOut: 3000 });
      this.router.navigate(['/vehicles/' + result.id]);
    })
  }
}
