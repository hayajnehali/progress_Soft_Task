import { Component, OnInit, ViewChild } from '@angular/core';
import { BusinessCard, BusinessCardFilter } from '../../model/BusinessCard';
import { MatTableDataSource, MatTableModule } from '@angular/material/table'; // 👈 Import Material table
import { CommonModule } from '@angular/common';
import { BusinessCardService } from '../../services/business-card.service';
import { HttpClientModule } from '@angular/common/http';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input'; 
import * as QRCode from 'qrcode';
import { MatDatepickerModule } from '@angular/material/datepicker';

@Component({
  selector: 'app-business-card-list',
  standalone: true,
  imports: [CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatTableModule,
    RouterModule,
    FormsModule, 
    MatFormFieldModule,   
    MatSelectModule, 
    MatNativeDateModule,
    MatButtonModule,
    ReactiveFormsModule, 
    MatInputModule,    
    MatDatepickerModule],
  templateUrl: './business-card-list.component.html',
  styleUrl: './business-card-list.component.scss'
})
export class BusinessCardListComponent implements OnInit { 
  displayedColumns: string[] = ['name','gender','dob','email','phone','photo','address','action'];;
  dataSource = new MatTableDataSource<BusinessCard>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;  
  totalNumberOf: number = 0;
  cards: BusinessCard[] = [];
  qrImageUrl: string = '';
  filter:BusinessCardFilter=new BusinessCardFilter();
 constructor(private businessCardService:BusinessCardService){
  this.dataSource = new MatTableDataSource(this.cards);
  }

  ngOnInit(): void { 
    this.getBusinessCardList()
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator; 
  }
  onReset() {
    this.filter=new BusinessCardFilter();
    this.getBusinessCardList()
    }
  getBusinessCardList(){
    this.filter.dateOfBirth= this.filter.date?.toISOString()
    this.businessCardService.getAll(this.filter).subscribe(
      (req)=>{
        this.dataSource.data=req.collection
        this.totalNumberOf=req.totalNumberOf
    //    this.dataSource.paginator = this.paginator; 
    
      })
    }
    delete(item:BusinessCard){
      this.businessCardService.delete(item.businessCardId!).subscribe({
        next:(req)=>{
          this.dataSource.data= this.dataSource.data.filter(x=>x.businessCardId!=item.businessCardId);
          this.totalNumberOf --;
          alert('Data deleted successfully!')
        },
      error:(err)=>{
        alert('Error delete data: ' + err.message)
      }})
      }







      exportAllAsSingleQR() { 
        const jsonData = JSON.stringify(this.dataSource.data); 
        QRCode.toDataURL(jsonData)
          .then(url => { 
            this.qrImageUrl = url;
          })
          .catch(err => {
            alert('Failed to generate QR:' + err.message) 
          });
      }

      pageChanged(event: PageEvent) {
        this.filter.pageIndex = event.pageIndex + 1;
        this.filter.pageSize = event.pageSize;
        this.getBusinessCardList();
      }
}
