import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TicketReply } from '../../Models/ticketreply';
import { TicketreplyService } from '../ticketreply-service';

@Component({
  selector: 'app-ticketreply-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticketreply-component.html',
})
export class TicketreplyComponent {
  replySvc = inject(TicketreplyService);

  reply: TicketReply = new TicketReply();
  replies: TicketReply[] = [];
  errMsg = '';

  empId = '';
  repliedBy: 'creator' | 'assignee' = 'creator';

  constructor() {
    this.loadAllReplies();
  }

  newReply() {
    this.reply = new TicketReply();
  }

  submitReply() {
    this.reply.repliedByCreatorEmpId = undefined;
    this.reply.repliedByAssignedEmpId = undefined;

    if (this.repliedBy === 'creator') {
      this.reply.repliedByCreatorEmpId = this.empId;
    } else {
      this.reply.repliedByAssignedEmpId = this.empId;
    }

    this.replySvc.addReply(this.reply).subscribe({
      next: () => {
        alert('Reply Added');
        this.loadRepliesByTicket();
        this.newReply();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  updateReply() {
    this.replySvc
      .updateReply(this.reply.replyId, this.reply)
      .subscribe({
        next: () => {
          alert('Reply Updated');
          this.loadRepliesByTicket();
          this.newReply();
        },
        error: (err) => (this.errMsg = err.error),
      });
  }

  deleteReply() {
    this.replySvc.deleteReply(this.reply.replyId).subscribe({
      next: () => {
        alert('Reply Deleted');
        this.loadRepliesByTicket();
        this.newReply();
      },
      error: (err) => (this.errMsg = err.error),
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
