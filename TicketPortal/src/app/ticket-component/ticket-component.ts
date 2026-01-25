import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Ticket } from '../../Models/ticket';
import { TicketService } from '../ticket-service';
import { TicketTypeService } from '../tickettype-service';
import { TicketType } from '../../Models/tickettype';
import { EmployeeService } from '../employee-service';
import { Employee } from '../../Models/employee';
import { AuthService } from '../auth-service';
import { tick } from '@angular/core/testing';
 
@Component({
  selector: 'app-ticket-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticket-component.html',
  styleUrl: './ticket-component.css',
})
export class TicketComponent {
 
  ticketSvc = inject(TicketService);
  ticketTypeSvc = inject(TicketTypeService);
  empSvc = inject(EmployeeService);
  authSvc=inject(AuthService);
  emp:Employee;
 
  tickets: Ticket[] = [];
  ticket: Ticket;
  empId:string;
 
  employees: Employee[] = [];
  ticketTypes: TicketType[] = [];
  validationErrors: string[] = [];
  selectedTicketId: number | null = null;
  selectedTicket:Ticket|null=null;
  errMsg:string;
 
  constructor() {
    this.empId="";
    this.errMsg="";
    this.ticket=new Ticket();
    this.emp=new Employee();
    this.loadAllTickets();
    this.loadAllTicketTypes();
  }
  newTicket(){
    this.ticket=new Ticket();
  }
 
  loadAllTickets() {
    if(this.authSvc.empRoleSignal()=='Admin'){  
        this.ticketSvc.getAllTickets().subscribe({
        next: res => this.tickets = res,
        error: err => {
        this.errMsg =err.error;
      }
      });
    }
    else{
          this.ticketSvc.getTicketsByEmpId(this.authSvc.empIdSignal()??'').subscribe({
            next: (res) => {
              this.tickets = res;
              this.errMsg = '';
              // this.loadAllDepartments();
              // this.loadAllTicketTypes();
            },
            error: err => {
              this.errMsg = err.error;
   
   
            },
          });
    }
  }
 
  loadAllTicketTypes() {
    this.ticketTypeSvc.getAllTicketTypes().subscribe({
      next: res => this.ticketTypes = res,
      error: err => {
       this.errMsg =err.error;
      }
    });
  }
  showTicket(ticket:Ticket){
    this.selectedTicket=ticket;
  }
 
  selectTicket(t: Ticket) {
    this.selectedTicketId = t.ticketId;
    // this.selectedTicket=t;
    this.ticket = { ...t };
    this.ticketTypeSvc.getTicketTypeById(t.ticketTypeId).subscribe({
      next: (tt) => {
        this.empSvc.getByDepartmentId(tt.departmentId).subscribe({
          next: (emps) => this.employees = emps,
          error: err =>
            this.errMsg =err.error
        });
      },
      error: err => {
        this.errMsg =err.error;
      }
    });
  }
 
 
  assignTicket() {
    console.log(this.ticket);
    if(this.ticket.assignedToEmpId==this.ticket.createdByEmpId){
      alert("Assigned Employee can't be the same as Creator Employee.");
    }
    else{
      if(this.ticket.assignedToEmpId!=null) this.ticket.status='InProgress';
      this.ticketSvc.updateTicket(this.ticket.ticketId, this.ticket).subscribe({
        next: () => {
          alert('Employee assigned successfully');
          this.selectedTicketId = null;
          this.ticket = new Ticket();
          this.employees = [];
          this.loadAllTickets();
        },
        error: err => {
          this.errMsg =err.error?.errors?Object.values(err.error?.errors || {})
                            .flat()
                            .join(', '):err.error;
        }
      });
    }
  }


  closeTicketModal() {
    this.selectedTicket = null;
    this.employees = [];
  }
 
 
  addTicket() {
    this.ticket.createdByEmpId=this.authSvc.empIdSignal() ?? '';
    this.ticket.assignedToEmpId="null";
    this.ticket.status='Open';
    this.ticketSvc.addTicket(this.ticket).subscribe({
      next: () => {
        alert('Ticket created');
        this.ticket = new Ticket();
        this.loadAllTickets();
      },
      error: err => {
        this.errMsg =err.error?.errors?Object.values(err.error?.errors || {})
                          .flat()
                          .join(', '):err.error;
      }
    });
  }
 
 
  updateTicket() {
    // if(this.ticket.)
    if(this.ticket.status=='Resolved'){
      this.ticket.resolvedAt=new Date();
      console.log(this.ticket);
    }
    this.ticketSvc.updateTicket(this.ticket.ticketId,this.ticket).subscribe({
      next: () => {
        alert('Ticket Updated Successfully');
        this.loadAllTickets();
        this.newTicket();
        this.loadAllTicketTypes();
        // this.loadAllDepartments();
      },
      error: err => {
        this.errMsg =err.error?.errors?Object.values(err.error?.errors || {})
                          .flat()
                          .join(', '):err.error;
      }
    });
  }
 
  deleteTicket(ticketId:number) {
    this.ticketSvc.deleteTicket(ticketId).subscribe({
      next: () => {
        alert('Ticket Deleted Successfully');
        this.loadAllTickets();
        this.newTicket();
        this.loadAllTicketTypes();
        // this.loadAllDepartments();
      },
      error: err => {
        this.errMsg =err.error?.errors?Object.values(err.error?.errors || {})
                          .flat()
                          .join(', '):err.error;
      }
    });
  }
 
 
  getTicketsByEmployee() {
    this.ticketSvc.getTicketsByEmpId(this.empId).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
        // this.loadAllDepartments();
        this.loadAllTicketTypes();
      },
      error: err => {
        this.errMsg = err.error;
      },
    });
  }
 
  getTicketsByStatus() {
    this.ticketSvc.getTicketsByStatus(this.ticket.status).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
        this.loadAllTicketTypes();
        if(this.authSvc.empRoleSignal()=='User'){
          this.tickets=this.tickets.filter(t=>t.assignedToEmpId==this.authSvc.empIdSignal()||t.createdByEmpId==this.authSvc.empIdSignal());
        }
        // this.loadAllDepartments();
      },
      error: err => {
        this.errMsg = err.error;
      },
    });
  }
 
  getTicketsByTicketType() {
    this.ticketSvc.getTicketsByTicketTypeId(this.ticket.ticketTypeId).subscribe({
      next: (res) => {
        this.tickets = res;
        if(this.authSvc.empRoleSignal()=='User'){
          this.tickets=this.tickets.filter(t=>t.assignedToEmpId==this.authSvc.empIdSignal()||t.createdByEmpId==this.authSvc.empIdSignal());
        }
        this.errMsg = '';
        this.loadAllTicketTypes();
        console.log(res);
        // this.loadAllDepartments();
      },
      error: err => {
        this.errMsg = err.error;
      },
    });
  }
 
  getOverdueTickets() {
    this.ticketSvc.getOverdueTickets().subscribe({
      next: (res) => {
        this.tickets = res;
        if(this.authSvc.empRoleSignal()=='User'){
          this.tickets=this.tickets.filter(t=>t.assignedToEmpId==this.authSvc.empIdSignal()||t.createdByEmpId==this.authSvc.empIdSignal());
        }
        this.errMsg = '';
        this.loadAllTicketTypes();
        // this.loadAllDepartments();
      },
      error: err => {
        this.errMsg = err.error;
      },
    });
  }
}
 
 
 
 
