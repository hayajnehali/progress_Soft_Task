import { Component, ViewChild } from '@angular/core'; 
import * as Papa from 'papaparse';
import * as xml2js from 'xml2js';
import { HttpClient } from '@angular/common/http'; 
import { BusinessCard, BusinessCardFilter } from '../../model/BusinessCard';
import { MatTableDataSource, MatTableModule } from '@angular/material/table'; 
import { CommonModule } from '@angular/common';
import { BusinessCardService } from '../../services/business-card.service';
import { HttpClientModule } from '@angular/common/http';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { BrowserMultiFormatReader } from '@zxing/library';

@Component({
  selector: 'app-business-card-import',
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
        MatButtonModule,
        ],
  templateUrl: './business-card-import.component.html',
  styleUrl: './business-card-import.component.scss'
})  
export class BusinessCardImportComponent { 
  scannedResult: string = ''; 
  selectedFile: File | null = null;
  private codeReader = new BrowserMultiFormatReader();
  displayedColumns: string[] = ['name','gender','dob','email','phone','photo','address'];
  dataSource = new MatTableDataSource<BusinessCard>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;  
    
  constructor(private http: HttpClient,private businessCardService:BusinessCardService,private router: Router) {}
  submitData() { 
    this.dataSource.data.forEach((x)=>{
      x.businessCardId=0; 
    })
        this.businessCardService.createBulk(this.dataSource.data).subscribe({
          next:(req)=>{
            alert('Data submitted successfully!')
            this.router.navigate(['../']);
          },
          error: (err) => alert('Error submitting data: ' + err.message)
        }) 
  }


  // Function to scan the QR code from an image
  scanQRCodeFromImage(file: File) {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      const imageElement = new Image();
      imageElement.src = e.target.result;
      imageElement.onload = () => { 
        this.codeReader.decodeFromImage(imageElement)
          .then(result => { 
           const rawData = result.getText(); 
           const parsedList: BusinessCard[] = JSON.parse(rawData); 
           this.dataSource.data = parsedList.map(card => ({
             ...card,
             dateOfBirth: card.dateOfBirth ? new Date(card.dateOfBirth) : undefined
           })); 
         //  this.previewHeaders = Object.keys(this.previewData[0] || {});

          })
          .catch(err => {
            console.error('QR code scan error: ', err);
          });
      };
    };
    reader.readAsDataURL(file); 
  }

  // Function to handle file input for QR code scanning
  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.scanQRCodeFromImage(file);
    }
  }


onFileSelected(event: any) {
  this.selectedFile = event.target.files[0]; 
  if (!this.selectedFile) return; 
  const formData = new FormData();
  formData.append('file', this.selectedFile); 
  this.businessCardService.uploadFile(formData).subscribe({
    next: (res) =>{ 
      console.log(res) 
      this.dataSource.data=res
    },
    error:(err) => alert('Error submitting data: ' + err.message)
  });
}

}

