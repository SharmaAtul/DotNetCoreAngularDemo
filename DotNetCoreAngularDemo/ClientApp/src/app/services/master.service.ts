import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class MasterService {

  constructor(private http: Http) { }

  getMakes() {
    return this.http.get("/api/master/makes").map(res=>res.json());
  }

  getFeatures() {
    return this.http.get("/api/master/features").map(res => res.json());
  }
}
