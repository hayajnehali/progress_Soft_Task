import { PaginatedResult } from "./paginated.result";

export class BusinessCard {
    businessCardId: number | undefined;
    name: string | undefined;
    gender: string | undefined;
    dateOfBirth: Date | undefined;
    email: string | undefined;
    phone: string | undefined;
    photo: string | undefined;
    address: string | undefined;
  }
  

  export class BusinessCardFilter extends PaginatedResult<BusinessCard> { 
    name: string | undefined;
    businessCardId: number | undefined;
    // gender: string;
    // dateOfBirth: Date;
    // email: string;
    // phone: string;
    // photo: string;
    // address: string;
  }
  
  