import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Ticket } from '../../Models/ticket';
import { TicketService } from '../ticket-service';

@Component({
  selector: 'app-ticket-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticket-component.html',
  styleUrl: './ticket-component.css',
})
export class TicketComponent {
  ticketSvc: TicketService = inject(TicketService);

  ticket: Ticket;
  tickets: Ticket[];
  errMsg: string;

  empId: string = '';
  departmentId: string = '';
  ticketTypeId: string = '';
  status: string = '';

  constructor() {
    this.ticket = new Ticket();
    this.tickets = [];
    this.errMsg = '';
    this.loadAllTickets();
  }


  newTicket() {
    this.ticket = new Ticket();
  }

  loadAllTickets() {
    this.ticketSvc.getAllTickets().subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getTicketById() {
    this.ticketSvc.getTicketById(this.ticket.ticketId).subscribe({
      next: (res) => {
        this.ticket = res;
        this.errMsg = '';
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
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  updateTicket() {
    this.ticketSvc.updateTicket(this.ticket).subscribe({
      next: () => {
        alert('Ticket Updated Successfully');
        this.loadAllTickets();
        this.newTicket();
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
      },
      error: (err) => (this.errMsg = err.error),
    });
  }


  getTicketsByEmployee() {
    this.ticketSvc.getTicketsByEmpId(this.empId).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getTicketsByStatus() {
    this.ticketSvc.getTicketsByStatus(this.status).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getTicketsByDepartment() {
    this.ticketSvc.getTicketsByDepartmentId(this.departmentId).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
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
        },
        error: (err) => (this.errMsg = err.error),
      });
  }

  getTicketsByTicketType() {
    this.ticketSvc.getTicketsByTicketTypeId(this.ticketTypeId).subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getOverdueTickets() {
    this.ticketSvc.getOverdueTickets().subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
}
