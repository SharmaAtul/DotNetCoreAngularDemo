import { Component } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { Vehicle, KeyValuePair, VehicleQuery, TableColumn } from '../models/vehicle';
import { MasterService } from '../services/master.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin';

@Component({
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css'],
  selector: 'vehicle-list'
})

export class VehicleListComponent {
  private readonly PAGE_SIZE = 10;
  vquery: any = {
    modelId: 0,
    makeId: 0,
    sortColumn: "make",
    isSortAscending: true,
    pageIndex: 1,
    pageSize:this.PAGE_SIZE
  };
  queryResult: any = {};
  vehicles: Vehicle[];
  makes: KeyValuePair[];
  columns: TableColumn[] = [
    { title: "Make", key: "make", isSortable: true },
    { title: "Model", key: "model", isSortable: true },
    { title: "Contact Name", key: "contactName", isSortable: true },
    { title: "", key: "", isSortable: false}
  ];
  //vQuery: {};

  constructor(private vehicleService: VehicleService,
    private masterService: MasterService) { }

  ngOnInit() {
    
    this.masterService.getMakes().subscribe(m => this.makes = m );
    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.vquery).subscribe(result => {
        this.queryResult = result;
    });
  }

  onFilterChange() {
    this.vquery.pageIndex = 1;
    this.populateVehicles();
  }

  onResetFilter() {
    this.vquery = {
      modelId: 0,
      makeId: 0,
      sortColumn: "make",
      isSortAscending: true,
      pageIndex: 1,
      pageSize: this.PAGE_SIZE
    }

    this.onFilterChange();
  }

  sortBy(columnName) {
    if (this.vquery.sortColumn === columnName)
      this.vquery.isSortAscending = !this.vquery.isSortAscending;
    else {
      this.vquery.sortColumn = columnName
      this.vquery.isSortAscending = true;
    }

    this.populateVehicles();
  }

  //Pagination: initializing p to one
  onPageChanged(page) {
    this.vquery.pageIndex = page;
    this.populateVehicles();
  }
}
