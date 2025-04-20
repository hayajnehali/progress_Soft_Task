export class PaginatedResult<T>{
    totalNumberOf: number=0;  // Assuming every model will have an id
    collection: T[]=[];
    pageSize :number= 5;
    pageIndex :number=1
}