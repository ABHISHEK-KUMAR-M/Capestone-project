export class TicketType {
  ticketTypeId: string;
  typeName: string;
  description?: string;
  departmentId: string;
  slaId: string;

  constructor(
    ticketTypeId: string = '',
    typeName: string = '',
    description: string = '',
    departmentId: string = '',
    slaId: string = ''
  ) {
    this.ticketTypeId = ticketTypeId;
    this.typeName = typeName;
    this.description = description;
    this.departmentId = departmentId;
    this.slaId = slaId;
  }
}
