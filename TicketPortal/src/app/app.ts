import { Component, inject, signal } from '@angular/core';
import { RouterOutlet,RouterLink,RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DepartmentComponent } from "./department-component/department-component";
import { SlaComponent } from "./sla-component/sla-component";
import { EmployeeComponent } from "./employee-component/employee-component";
import { TicketreplyComponent } from "./ticketreply-component/ticketreply-component";
import { TicketComponent } from "./ticket-component/ticket-component";
import { TicketTypeComponent } from "./tickettype-component/tickettype-component";
import { HomeComponent } from './home-component/home-component';
import { AuthService } from './auth-service';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive,CommonModule,FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TicketPortal');
  auth=inject(AuthService);
  empName=signal(sessionStorage.getItem('empName'));
}

