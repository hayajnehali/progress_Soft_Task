import { Routes } from '@angular/router';  
import { BusinessCardManageComponent } from './business-card-manage/business-card-manage.component';
import { BusinessCardListComponent } from './business-card-list/business-card-list.component';
import { BusinessCardImportComponent } from './business-card-import/business-card-import.component';

export const routes: Routes = [
    { path: '', component: BusinessCardListComponent },
    { path: 'manage', component: BusinessCardManageComponent },
    { path: 'import', component: BusinessCardImportComponent }
];
