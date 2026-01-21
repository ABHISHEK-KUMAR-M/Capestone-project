export class SLA {
  slaId: string;
  responseTime: number;
  resolutionHours: number;
  description?: string;

  constructor(
    slaId: string = '',
    responseTime: number = 0,
    resolutionHours: number = 0,
    description: string = ''
  ) {
    this.slaId = slaId;
    this.responseTime = responseTime;
    this.resolutionHours = resolutionHours;
    this.description = description;
  }
}
