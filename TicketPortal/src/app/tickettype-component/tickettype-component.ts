import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TicketType } from '../../Models/tickettype';
import { TicketTypeService } from '../tickettype-service';
import { Department } from '../../Models/department';
import { DepartmentService } from '../department-service';
import { SLA } from '../../Models/sla';
import { SlaService } from '../sla-service';

@Component({
  selector: 'app-tickettype',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './tickettype-component.html',
  styleUrl: './tickettype-component.css'
})
export class TicketTypeComponent {

  ticketTypeSvc: TicketTypeService = inject(TicketTypeService);
  departmentSvc: DepartmentService = inject(DepartmentService);
  slaSvc: SlaService = inject(SlaService);
  ticketType: TicketType;
  ticketTypes: TicketType[];
  errMsg: string;
  departments: Department[];
  slas: SLA[];
  slaId: string;
  departmentId: string;
  constructor() {
    this.ticketType = new TicketType('', '', '', '', '');
    this.ticketTypes = [];
    this.departments = [];
    this.slas = [];
    this.slaId = '';
    this.departmentId = '';
    this.errMsg = '';
    this.loadTicketTypes();
    this.loadDepartments();
    this.loadSlas();
  }
  loadDepartments() {
    this.departmentSvc.getAllDepartments().subscribe({
      next: (res: Department[]) => {
        this.departments = res;
        this.errMsg = '';
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
  loadSlas() {
    this.slaSvc.getAllSlas().subscribe({
      next: (res: SLA[]) => {
        this.slas = res;
        this.errMsg = '';
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
newTicketType() {
    this.ticketType = new TicketType('','','','','');
  }
  loadTicketTypes() {
    this.ticketTypeSvc.getAllTicketTypes().subscribe({
      next: (response:any) => {
        this.ticketTypes = response;
        this.errMsg = '';
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      },
    });
  }
  getTicketType() {
    this.ticketTypeSvc
      .getTicketTypeById(this.ticketType.ticketTypeId).subscribe({
        next: (response:any) => {
          this.ticketType = response;
          this.errMsg = '';
        },
          error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      },
      });
  }
  addTicketType() {
    this.ticketTypeSvc.addTicketType(this.ticketType).subscribe({
      next: () => {
        alert('Ticket Type Added.');
        this.loadTicketTypes();
        this.newTicketType();
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      },
    });
  }
  updateTicketType() {
    this.ticketTypeSvc.updateTicketType(this.ticketType.ticketTypeId, this.ticketType).subscribe({
      next: () => {
        alert('Ticket Type Updated.');
        this.loadTicketTypes();
        this.newTicketType();
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      },
    });
  }
  deleteTicketType() {
    this.ticketTypeSvc.deleteTicketType(this.ticketType.ticketTypeId).subscribe({
        next: () => {
          alert('Ticket Type Deleted');
          this.newTicketType();
          this.loadTicketTypes();
        },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      },
      });
  }
  GetBySlaId(slaId:string) {
    this.ticketTypeSvc.GetBySlaId(slaId).subscribe({
      next: (res: TicketType[]) => {
        this.ticketTypes = res;
        this.errMsg = '';
        this.departmentId = '';
        //this.loadTicketTypes();
      },
      error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
  GetByDepartmentId(deptId:string) {
    this.ticketTypeSvc.GetByDepartmentId(deptId).subscribe({
      next: (res: TicketType[]) => {
        this.ticketTypes = res;
        this.errMsg = '';
        this.slaId = '';
        //this.loadTicketTypes();
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
}