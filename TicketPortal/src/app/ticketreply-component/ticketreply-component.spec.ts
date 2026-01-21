import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketreplyComponent } from './ticketreply-component';

describe('TicketreplyComponent', () => {
  let component: TicketreplyComponent;
  let fixture: ComponentFixture<TicketreplyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketreplyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketreplyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
