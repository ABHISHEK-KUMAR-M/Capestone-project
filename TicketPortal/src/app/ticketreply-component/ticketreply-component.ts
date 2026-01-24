import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { TicketReply } from '../../Models/ticketreply';
import { TicketreplyService } from '../ticketreply-service';

import { TicketService } from '../ticket-service';
import { Ticket } from '../../Models/ticket';
import { AuthService } from '../auth-service';

@Component({
  selector: 'app-ticketreply-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticketreply-component.html',
  styleUrls: ['./ticketreply-component.css']
 })
export class TicketreplyComponent {
  replySvc = inject(TicketreplyService);
  ticketSvc = inject(TicketService);
  authSvc=inject(AuthService);

  reply: TicketReply = new TicketReply();
  replies: TicketReply[] = [];
  errMsg = '';

  repliedBy: 'creator' | 'assignee' = 'creator';

  loggedInEmpId: string = '';

  allTickets: Ticket[] = [];
  filteredTickets: Ticket[] = [];


  selectedTicket: Ticket | undefined;

  constructor() {
    this.loggedInEmpId = sessionStorage.getItem('empId') || '';

    this.loadTicketsForDropdown();   
    this.loadAllReplies();           
  }

  newReply() {
    const keepTicketId = this.reply.ticketId; 
    this.reply = new TicketReply();
    this.reply.ticketId = keepTicketId;
    this.reply.message = '';
  }

  loadTicketsForDropdown() {
    this.ticketSvc.getAllTickets().subscribe({
      next: (res) => {
        this.allTickets = res;

        this.filteredTickets = this.allTickets.filter(t =>
          t.createdByEmpId === this.loggedInEmpId ||
          t.assignedToEmpId === this.loggedInEmpId
        );

        if (this.reply.ticketId) {
          this.selectedTicket = this.filteredTickets.find(t => t.ticketId === this.reply.ticketId);
        }
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  onTicketChange() {
    this.selectedTicket = this.filteredTickets.find(t => t.ticketId === this.reply.ticketId);
    if (!this.selectedTicket) return;

    if (this.selectedTicket.assignedToEmpId === this.loggedInEmpId) {
      this.repliedBy = 'assignee';
    } else if (this.selectedTicket.createdByEmpId === this.loggedInEmpId) {
      this.repliedBy = 'creator';
    }
      this.loadRepliesByTicket();
    }

  submitReply() {
    if (!this.reply.ticketId) {
      this.errMsg = "Please select Ticket";
      return;
    }

    this.selectedTicket = this.filteredTickets.find(t => t.ticketId === this.reply.ticketId);

    if (!this.selectedTicket) {
      this.errMsg = "Selected ticket not found";
      return;
    }

    this.reply.repliedByCreatorEmpId = null as any;
    this.reply.repliedByAssignedEmpId = null as any;

    if (this.repliedBy === 'creator') {
      this.reply.repliedByCreatorEmpId = this.selectedTicket.createdByEmpId;
      this.reply.repliedByAssignedEmpId = null as any;
    }
    else {
      this.reply.repliedByAssignedEmpId = this.selectedTicket.assignedToEmpId || null as any;
      this.reply.repliedByCreatorEmpId = null as any;
    }

    this.replySvc.addReply(this.reply).subscribe({
      next: () => {
        alert('Reply Added');
        this.loadRepliesByTicket();
        this.newReply();
        this.errMsg = '';
      },
      error: err => {
        this.errMsg =err.error?.errors?Object.values(err.error?.errors || {})
                          .flat()
                          .join(', '):err.error;
      }
    });
  }

  updateReply() {
    this.replySvc.updateReply(this.reply.replyId, this.reply).subscribe({
      next: () => {
        alert('Reply Updated');
        this.loadAllReplies();
        this.newReply();
      },
      error: err => {
        this.errMsg =err.error?.errors?Object.values(err.error?.errors || {})
                          .flat()
                          .join(', '):err.error;
      }
    });
  }

  deleteReply() {
    this.replySvc.deleteReply(this.reply.replyId).subscribe({
      next: () => {
        alert('Reply Deleted');

        this.replies = this.replies.filter(r => r.replyId !== this.reply.replyId);

        this.newReply();
        this.errMsg = '';
      },
      error: err => {
        this.errMsg =err.error?.errors?Object.values(err.error?.errors || {})
                          .flat()
                          .join(', '):err.error;
      }
    });
  }

  getReplyById() {
    this.replySvc.getReplyById(this.reply.replyId).subscribe({
      next: (res) => (this.reply = res),
      error: (err) => (this.errMsg = err.error),
    });
  }

  loadAllReplies() {
    this.replySvc.getAllReplies().subscribe({
      next: (res) => (this.replies = res),
      error: (err) => (this.errMsg = err.error),
    });
  }

  loadRepliesByTicket() {
    if (!this.reply.ticketId) return;

    this.replySvc.getRepliesByTicket(this.reply.ticketId).subscribe({
      next: (res) => (this.replies = res),
      error: (err) => (this.errMsg = err.error),
    });
  }

  loadRepliesByEmployee() {
    const empId = prompt('Enter Employee ID');
    if (!empId || empId.trim() === '') {
      alert('Employee ID is required');
      return;
    }

    this.replySvc.getRepliesByEmployee(empId).subscribe({
      next: (res) => {
        this.replies = res;
        this.errMsg = '';
      },
      error: (err) => {
        this.errMsg = err.error;
      }
    });
  }
}