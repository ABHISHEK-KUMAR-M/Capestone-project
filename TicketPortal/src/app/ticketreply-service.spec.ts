import { TestBed } from '@angular/core/testing';

import { TicketreplyService } from './ticketreply-service';

describe('TicketreplyService', () => {
  let service: TicketreplyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TicketreplyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
