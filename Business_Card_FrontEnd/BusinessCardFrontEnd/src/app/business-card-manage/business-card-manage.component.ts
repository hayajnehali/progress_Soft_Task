import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import {MatDatepickerModule} from '@angular/material/datepicker';

import {MatInputModule} from '@angular/material/input';
import { BusinessCardService } from '../../services/business-card.service';
import { BusinessCard } from '../../model/BusinessCard';
@Component({
  selector: 'app-business-card-manage',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    RouterModule, 
    MatFormFieldModule,   
    MatDatepickerModule,
    MatSelectModule, 
    MatNativeDateModule,
    MatButtonModule,
    ReactiveFormsModule, 
    MatInputModule,
    MatButtonModule,   
    ],
  templateUrl: './business-card-manage.component.html',
  styleUrl: './business-card-manage.component.scss'
})
export class BusinessCardManageComponent implements OnInit {
 constructor(private businessCardService:BusinessCardService){ 
  }

  ngOnInit(): void {  
  }

  card: BusinessCard = {
    businessCardId:0,
    name: '',
    gender: '',
    dateOfBirth: new Date(),
    email: '',
    phone: '',
    photo: '',
    address: ''
  };
  
  onSubmit(form: any) {
    if (form.valid) {
      console.log('Submitted Business Card:', this.card); 
        this.businessCardService.create(this.card).subscribe({
          next:(req)=>{ 
            alert('Data submitted successfully!');
            this.card=req
          },
          error: (err) => alert('Error submitting data: ' + err.message)
          }) 
    }
  }
  
  onReset() {
    this.card = new BusinessCard();
  }
  
}