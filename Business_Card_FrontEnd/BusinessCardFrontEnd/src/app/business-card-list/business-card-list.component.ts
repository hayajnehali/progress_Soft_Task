import { Component, OnInit, ViewChild } from '@angular/core';
import { BusinessCard, BusinessCardFilter } from '../../model/BusinessCard';
import { MatTableDataSource, MatTableModule } from '@angular/material/table'; // 👈 Import Material table
import { CommonModule } from '@angular/common';
import { BusinessCardService } from '../../services/business-card.service';
import { HttpClientModule } from '@angular/common/http';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-business-card-list',
  standalone: true,
  imports: [CommonModule,MatTableModule,MatPaginatorModule,MatTableModule,RouterModule],
  templateUrl: './business-card-list.component.html',
  styleUrl: './business-card-list.component.scss'
})
export class BusinessCardListComponent implements OnInit {

 
    


  displayedColumns: string[] = ['name','gender','dob','email','phone','photo','address','action'];;
  dataSource = new MatTableDataSource<BusinessCard>();
  @ViewChild(MatPaginator) paginator!: MatPaginator; // Declare paginator without initialization 
  totalNumberOf: number = 0;
  cards: BusinessCard[] = [];
 constructor(private businessCardService:BusinessCardService){
  this.dataSource = new MatTableDataSource(this.cards);
  }

  ngOnInit(): void { 
    this.getBusinessCardList()
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator; 
  }
  goToAdd() {
    throw new Error('Method not implemented.');
    }
  getBusinessCardList(){

this.businessCardService.getAll(new BusinessCardFilter()).subscribe(
  (req)=>{
    this.dataSource.data=req.collection
    this.dataSource.paginator = this.paginator; 
    this.totalNumberOf=req.totalNumberOf
  })
  }


}
