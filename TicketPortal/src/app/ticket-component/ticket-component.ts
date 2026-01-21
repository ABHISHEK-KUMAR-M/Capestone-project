import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Ticket } from '../../Models/ticket';
import { TicketService } from '../ticket-service';
import { TicketTypeService } from '../tickettype-service';
import { TicketType } from '../../Models/tickettype';
import { EmployeeService } from '../employee-service';
import { DepartmentService } from '../department-service';
import { Department } from '../../Models/department';

@Component({
  selector: 'app-ticket-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticket-component.html',
  styleUrl: './ticket-component.css',
})
export class TicketComponent {
  ticketSvc: TicketService = inject(TicketService);
  ticketTypeSvc:TicketTypeService=inject (TicketTypeService)
  empSvc:EmployeeService=inject(EmployeeService)
  deptSvc:DepartmentService=inject(DepartmentService)
  ticket: Ticket;
  tickets: Ticket[];
  errMsg: string;

  empId: string = '';
  departmentId: string = '';
  ticketTypeId: string = '';
  status: string = '';

  ticketTypes:TicketType[];
  departments:Department[];
  constructor() {
    this.ticketTypes=[];
    this.ticket = new Ticket();
    this.tickets = [];
    this.departments=[];
    this.errMsg = '';
    this.loadAllTickets();
    this.loadAllTicketTypes();
    this.loadAllDepartments();
  }
  loadAllTicketTypes(){
    this.ticketTypeSvc.getAllTicketTypes().subscribe({
      next:(res)=>{
        this.ticketTypes=res;
        this.errMsg="";
      },
      error:(err)=>(this.errMsg=err.error)
    })
  }
  loadAllDepartments(){
    this.deptSvc.getAllDepartments().subscribe({
      next:(res)=>{
        this.departments=res;
        this.errMsg="";
        this.loadAllDepartments();
      },
      error:(err)=>(this.errMsg=err.error)
    })
  }

  newTicket() {
    this.ticket = new Ticket();
  }

  loadAllTickets() {
    this.ticketSvc.getAllTickets().subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
        this.loadAllTicketTypes();
        this.loadAllDepartments();
        
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getTicketById() {
    this.ticketSvc.getTicketById(this.ticket.ticketId).subscribe({
      next: (res) => {
        this.ticket = res;
        this.errMsg = '';
        this.loadAllTicketTypes();
        this.loadAllDepartments();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  addTicket() {
    this.ticketSvc.addTicket(this.ticket).subscribe({
      next: () => {
        alert('Ticket Created Successfully');
        this.loadAllTickets();
        this.newTicket();
        this.loadAllDepartments();
        this.loadAllTicketTypes();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  updateTicket() {
    this.ticketSvc.updateTicket(this.ticket.ticketId,this.ticket).subscribe({
      next: () => {
        alert('Ticket Updated Successfully');
        this.loadAllTickets();
        this.newTicket();
        this.loadAllTicketTypes();
        this.loadAllDepartments();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  deleteTicket() {
    this.ticketSvc.deleteTicket(this.ticket.ticketId).subscribe({
      next: () => {
        alert('Ticket Deleted Successfully');
        this.loadAllTickets();
        this.newTicket();
        this.loadAllTicketTypes();
        this.loadAllDepartments();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }


  getTicketsByEmployee() {
    this.ticketSvc.getTicketsByEmpId(this.empId).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
        this.loadAllDepartments();
        this.loadAllTicketTypes();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getTicketsByStatus() {
    this.ticketSvc.getTicketsByStatus(this.status).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
        this.loadAllTicketTypes();
        this.loadAllDepartments();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getTicketsByDepartment() {
    this.ticketSvc.getTicketsByDepartmentId(this.departmentId).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
        this.loadAllTicketTypes();
        this.loadAllDepartments();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getTicketsByDepartmentAndStatus() {
    this.ticketSvc
      .getTicketsByDepartmentAndStatus(this.departmentId, this.status)
      .subscribe({
        next: (res) => {
          this.tickets = res;
          this.errMsg = '';
          this.loadAllTicketTypes();
          this.loadAllDepartments();
        },
        error: (err) => (this.errMsg = err.error),
      });
  }

  getTicketsByTicketType() {
    this.ticketSvc.getTicketsByTicketTypeId(this.ticketTypeId).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
        this.loadAllTicketTypes();
        this.loadAllDepartments();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getOverdueTickets() {
    this.ticketSvc.getOverdueTickets().subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
        this.loadAllTicketTypes();
        this.loadAllDepartments();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
}
