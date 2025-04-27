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
    gender: string | undefined;
    dateOfBirth: string | undefined;   
    date: Date | undefined;
    email: string | undefined;
    phone: string | undefined; 
  }
  
  