import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SLA } from '../../Models/sla';
import { SlaService } from '../sla-service';
 
@Component({
  selector: 'app-sla',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './sla-component.html',
})
export class SlaComponent {
  slaSvc: SlaService = inject(SlaService);
 
  sla: SLA;
  slas: SLA[];
  ticketTypeId: string;
  errMsg: string;
 
  constructor() {
    this.sla = new SLA();
    this.slas = [];
    this.ticketTypeId = '';
    this.errMsg = '';
    this.loadSlas();
  }
 
  newSla() {
    this.sla = new SLA();
  }
 
  loadSlas() {
    this.slaSvc.getAllSlas().subscribe({
      next: (response) => {
        this.slas = response;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
 
  getSla() {
    this.slaSvc.getSlaById(this.sla.slaId).subscribe({
      next: (response) => {
        this.sla = response;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
 
 
  getSlaByTicketType() {
    this.slaSvc.getSlaByTicketTypeId(this.ticketTypeId).subscribe({
      next: (response) => {
        this.sla = response;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
 
  addSla() {
    this.slaSvc.addSla(this.sla).subscribe({
      next: () => {
        alert('SLA Added');
        this.loadSlas();
        this.newSla();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
 
  updateSla() {
    this.slaSvc.updateSla(this.sla.slaId, this.sla).subscribe({
      next: () => {
        alert('SLA Updated');
        this.loadSlas();
        this.newSla();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
 
  deleteSla() {
    this.slaSvc.deleteSla(this.sla.slaId).subscribe({
      next: () => {
        alert('SLA Deleted');
        this.loadSlas();
        this.newSla();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
}
 
 