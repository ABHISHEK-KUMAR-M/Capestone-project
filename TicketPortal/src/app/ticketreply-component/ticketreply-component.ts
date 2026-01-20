import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TicketReply } from '../../Models/ticketreply';
import { TicketreplyService } from '../ticketreply-service';

@Component({
  selector: 'app-ticketreply-component',
  imports: [FormsModule, CommonModule],
  templateUrl: './ticketreply-component.html',
  styleUrl: './ticketreply-component.css',
})

export class TicketreplyComponent {
  replySvc: TicketreplyService = inject(TicketreplyService);

  reply: TicketReply;
  replies: TicketReply[];
  errMsg: string;

  empId: string;
  role: string | null;

  constructor() {
    this.reply = new TicketReply();
    this.replies = [];
    this.errMsg = '';

    this.empId = sessionStorage.getItem('empId') || '';
    this.role = sessionStorage.getItem('role');
  }

  newReply() {
    this.reply = new TicketReply();
  }

  loadRepliesByTicket() {
    if (!this.reply.ticketId) {
      this.errMsg = 'Enter Ticket ID';
      return;
    }

    this.replySvc.getRepliesByTicket(this.reply.ticketId).subscribe({
      next: (res) => {
        this.replies = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  sendReply() {
    if (this.role === 'Creator') {
      this.reply.repliedByCreatorEmpId = this.empId;
      this.reply.repliedByAssignedEmpId = undefined;
    } else {
      this.reply.repliedByAssignedEmpId = this.empId;
      this.reply.repliedByCreatorEmpId = undefined;
    }

    this.replySvc.addReply(this.reply).subscribe({
      next: () => {
        alert('Reply Sent');
        this.loadRepliesByTicket();
        this.reply.message = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  deleteReply(replyId: number) {
    this.replySvc.deleteReply(replyId).subscribe({
      next: () => {
        alert('Reply Deleted');
        this.loadRepliesByTicket();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
}

