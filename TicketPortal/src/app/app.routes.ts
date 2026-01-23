import { Routes } from '@angular/router';
import { HomeComponent } from './home-component/home-component';
import { TicketComponent } from './ticket-component/ticket-component';
import { LoginComponent } from './login-component/login-component';
import { LogoutComponent } from './logout-component/logout-component';
import { RegisterComponent } from './register-component/register-component';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';
import { SlaComponent } from './sla-component/sla-component';
import { DepartmentComponent } from './department-component/department-component';
import { TicketreplyComponent } from './ticketreply-component/ticketreply-component';
import { EmployeeComponent } from './employee-component/employee-component';
import { userAccessGuard } from './user-access-guard';
export const routes: Routes = [
    {path:'',component:HomeComponent},
    {path:'ticket',component:TicketComponent,canActivate:[userAccessGuard]},
    {path:'tickettype',component:TicketTypeComponent,canActivate:[userAccessGuard]},
    {path:'department',component:DepartmentComponent,canActivate:[userAccessGuard]},
    {path:'sla',component:SlaComponent,canActivate:[userAccessGuard]},
    {path:'register',component:RegisterComponent},
    {path:'login',component:LoginComponent},
    {path:'logout',component:LogoutComponent,canActivate:[userAccessGuard]},
    {path:'ticketreply',component:TicketreplyComponent,canActivate:[userAccessGuard]},
    {path:'employee',component:EmployeeComponent,canActivate:[userAccessGuard]}
];
 
 