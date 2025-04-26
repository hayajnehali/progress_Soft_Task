import { Component } from '@angular/core'; 
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
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';

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
  previewData: any[] = [];
  previewHeaders: string[] = [];   
  constructor(private http: HttpClient,private businessCardService:BusinessCardService) {}

  onFileSelected(event: any) {
    const file = event.target.files[0];
    const fileExtension = file.name.split('.').pop().toLowerCase();
    const reader = new FileReader();

    reader.onload = () => {
      const content = reader.result as string;

      if (fileExtension === 'csv') {
        this.parseCSV(content);
      } else if (fileExtension === 'xml') {
        this.parseXML(content);
      } else {
        alert('Unsupported file format');
      }
    };

    reader.readAsText(file);
  }

  parseCSV(csvData: string) {
    Papa.parse(csvData, {
      header: true,
      skipEmptyLines: true,
      complete: (result) => {
        this.previewData = result.data; 
        this.previewHeaders = Object.keys(this.previewData[0]);
      }
    });
  }

  parseXML(xmlData: string) {
    xml2js.parseString(xmlData, { explicitArray: false }, (err, result) => {
      const records = result?.Root?.Record;
      const dataArray = Array.isArray(records) ? records : [records];

      this.previewData = dataArray;
      this.previewHeaders = Object.keys(this.previewData[0]);
    });
  }
 
  submitData() {
      console.log('Submitted Business Card:', this.previewData); 
        this.businessCardService.createBulk(this.previewData).subscribe({
          next:(req)=>alert('Data submitted successfully!'),
          error: (err) => alert('Error submitting data: ' + err.message)
        }) 
  }
}

