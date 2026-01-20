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
  styleUrl: './sla-component.css',
})
export class SlaComponent {
  slaSvc: SlaService = inject(SlaService);

  sla: SLA;
  slas: SLA[];
  errMsg: string;

  constructor() {
    this.sla = new SLA();
    this.slas = [];
    this.errMsg = '';
    this.loadSlas();
  }

  newSla() {
    this.sla = new SLA();
  }

  loadSlas() {
    this.slaSvc.getAllSlas().subscribe({
      next: (res) => {
        this.slas = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getSla() {
    this.slaSvc.getSlaById(this.sla.slaId).subscribe({
      next: (res) => {
        this.sla = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  addSla() {
    this.slaSvc.addSla(this.sla).subscribe({
      next: () => {
        alert('SLA Added Successfully');
        this.loadSlas();
        this.newSla();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  updateSla() {
    this.slaSvc.updateSla(this.sla).subscribe({
      next: () => {
        alert('SLA Updated Successfully');
        this.loadSlas();
        this.newSla();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  deleteSla(slaId: string) {
    if (!confirm('Are you sure you want to delete this SLA?')) return;

    this.slaSvc.deleteSla(slaId).subscribe({
      next: () => {
        alert('SLA Deleted Successfully');
        this.loadSlas();
        this.newSla();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  editSla(sla: SLA) {
    this.sla = { ...sla };
  }
}
