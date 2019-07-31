

export interface KeyValuePair {
  id: number,
  name:string
}
export interface SaveVehicle {
  id: number,
  modelId:number,
  makeId: number,
  features: number[],
  isRegistered: boolean
  contact: Contact
}

export interface VehicleQuery {
  makeId: number,
  modelId: number,
  sortColumn: string,
  isSortAscending: boolean,
  pageIndex: number,
  pageSize: number
}

export interface TableColumn {
  title: string,
  key: string,
  isSortable:boolean
}

export interface Vehicle {
  id: number,
  make: KeyValuePair,
  model: KeyValuePair,
  features: KeyValuePair[],
  isRegistered: boolean
  contact: Contact,
  lastUpate:string
}
export interface Contact {
  name: string,
  phone: string,
  email: string
}

