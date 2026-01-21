export class Ticket {
  ticketId: number;
  title: string;
  description: string;
  ticketTypeId: string;
  createdByEmpId: string;
  assignedToEmpId?: string;
  status: string;
  createdAt: Date;
  dueAt: Date;
  resolvedAt?: Date;

  constructor(
    ticketId: number =0,
    title: string = '',
    description: string = '',
    ticketTypeId: string = '',
    createdByEmpId: string = '',
    assignedToEmpId: string = '',
    status: string = 'Open',
    createdAt: Date = new Date(),
    dueAt: Date = new Date(),
    resolvedAt?: Date
  ) {
    this.ticketId = ticketId;
    this.title = title;
    this.description = description;
    this.ticketTypeId = ticketTypeId;
    this.createdByEmpId = createdByEmpId;
    this.assignedToEmpId = assignedToEmpId;
    this.status = status;
    this.createdAt = createdAt;
    this.dueAt = dueAt;
    this.resolvedAt = resolvedAt;
  }
}
