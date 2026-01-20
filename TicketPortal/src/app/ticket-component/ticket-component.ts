import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Ticket } from '../../Models/ticket';
import { TicketService } from '../ticket-service';

@Component({
  selector: 'app-ticket',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticket-component.html',
})
export class TicketComponent {
  ticketSvc: TicketService = inject(TicketService);

  ticket: Ticket;
  tickets: Ticket[];
  errMsg: string;

  constructor() {
    this.ticket = new Ticket();
    this.tickets = [];
    this.errMsg = '';
    this.loadTickets();
  }

  newTicket() {
    this.ticket = new Ticket();
  }

  loadTickets() {
    this.ticketSvc.getAllTickets().subscribe({
      next: (res) => {
        this.tickets = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getTicket() {
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
        alert('Ticket Created');
        this.loadTickets();
        this.newTicket();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  updateTicket() {
    this.ticketSvc.updateTicket(this.ticket).subscribe({
      next: () => {
        alert('Ticket Updated');
        this.loadTickets();
        this.newTicket();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  deleteTicket() {
    this.ticketSvc.deleteTicket(this.ticket.ticketId).subscribe({
      next: () => {
        alert('Ticket Deleted');
        this.loadTickets();
        this.newTicket();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
}
