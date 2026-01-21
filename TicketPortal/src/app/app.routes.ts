import { Routes } from '@angular/router';
import { HomeComponent } from './home-component/home-component';
import { TicketComponent } from './ticket-component/ticket-component';
import { LoginComponent } from './login-component/login-component';
import { LogoutComponent } from './logout-component/logout-component';
import { RegisterComponent } from './register-component/register-component';
import { DepartmentComponent } from './department-component/department-component';
import { SlaComponent } from './sla-component/sla-component';

export const routes: Routes = [
    {path:'',component:HomeComponent},
    {path:'ticket',component:TicketComponent},
    {path:'register',component:RegisterComponent},
    {path:'login',component:LoginComponent},
    {path:'logout',component:LogoutComponent},
    {path:'department',component:DepartmentComponent},
    {path:'sla',component:SlaComponent}

];
