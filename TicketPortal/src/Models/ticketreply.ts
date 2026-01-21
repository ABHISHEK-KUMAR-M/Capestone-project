export class TicketReply {
  replyId: number;
  ticketId: number;
  repliedByCreatorEmpId?: string;
  repliedByAssignedEmpId?: string;
  message: string;
  createdAt: Date;

  constructor(
    replyId: number = 0,
    ticketId: number = 0,
    repliedByCreatorEmpId: string = '',
    repliedByAssignedEmpId: string = '',
    message: string = '',
    createdAt: Date = new Date()
  ) {
    this.replyId = replyId;
    this.ticketId = ticketId;
    this.repliedByCreatorEmpId = repliedByCreatorEmpId;
    this.repliedByAssignedEmpId = repliedByAssignedEmpId;
    this.message = message;
    this.createdAt = createdAt;
  }
}
