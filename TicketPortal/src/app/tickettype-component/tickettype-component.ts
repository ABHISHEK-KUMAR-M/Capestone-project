import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TicketType } from '../../Models/tickettype';
import { TicketTypeService } from   '../tickettype-service';

@Component({
  selector: 'app-tickettype',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './tickettype-component.html',
})
export class TicketTypeComponent {
  ticketTypeSvc: TicketTypeService = inject(TicketTypeService);

  ticketType: TicketType;
  ticketTypes: TicketType[];
  errMsg: string;

  constructor() {
    this.ticketType = new TicketType();
    this.ticketTypes = [];
    this.errMsg = '';
    this.loadTicketTypes();
  }

  newTicketType() {
    this.ticketType = new TicketType();
  }

  loadTicketTypes() {
    this.ticketTypeSvc.getAllTicketTypes().subscribe({
      next: (res) => {
        this.ticketTypes = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getTicketType() {
    this.ticketTypeSvc
      .getTicketTypeById(this.ticketType.ticketTypeId)
      .subscribe({
        next: (res) => {
          this.ticketType = res;
          this.errMsg = '';
        },
        error: (err) => (this.errMsg = err.error),
      });
  }

  addTicketType() {
    this.ticketTypeSvc.addTicketType(this.ticketType).subscribe({
      next: () => {
        alert('Ticket Type Added');
        this.loadTicketTypes();
        this.newTicketType();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  updateTicketType() {
    this.ticketTypeSvc.updateTicketType(this.ticketType).subscribe({
      next: () => {
        alert('Ticket Type Updated');
        this.loadTicketTypes();
        this.newTicketType();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  deleteTicketType() {
    this.ticketTypeSvc
      .deleteTicketType(this.ticketType.ticketTypeId)
      .subscribe({
        next: () => {
          alert('Ticket Type Deleted');
          this.loadTicketTypes();
          this.newTicketType();
        },
        error: (err) => (this.errMsg = err.error),
      });
  }
}
