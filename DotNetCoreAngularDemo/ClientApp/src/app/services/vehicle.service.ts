import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { SaveVehicle } from '../models/vehicle';
import { forEach } from '@angular/router/src/utils/collection';

@Injectable()
export class VehicleService {

  constructor(private http: Http) { }

  create(vehicle) {
    return this.http.post("/api/vehicles",vehicle).map(res => res.json());
  }

  update(vehicle:SaveVehicle) {
    return this.http.put("/api/vehicles/"+vehicle.id, vehicle).map(res => res.json());
  }

  delete(id: number) {
    return this.http.delete("/api/vehicles/" + id).map(res => res.json());
  }

  getVehicle(id) {
    return this.http.get("/api/vehicles/"+id).map(res => res.json());
  }

  getVehicles(fQuery) {
    var queryData = this.toQueryParams(fQuery);

      return this.http.get("/api/vehicles/" + "?" + queryData).map(res => res.json());
  }

  toQueryParams(obj) {
    var params = [];

    for (var property in obj) {
      var value = obj[property];

      if (value != null && value != undefined)
        params.push(encodeURIComponent(property) + "=" + encodeURIComponent(value));
    }

    return params.join("&");
  }
}
