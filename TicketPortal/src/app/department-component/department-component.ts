import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Department } from '../../Models/department';
import { DepartmentService } from '../department-service';

@Component({
  selector: 'app-department',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './department-component.html',
})
export class DepartmentComponent {
  deptSvc: DepartmentService = inject(DepartmentService);

  department: Department;
  departments: Department[];
  errMsg: string;

  constructor() {
    this.department = new Department();
    this.departments = [];
    this.errMsg = '';
    this.loadDepartments();
  }

  newDepartment() {
    this.department = new Department();
  }

  loadDepartments() {
    this.deptSvc.getAllDepartments().subscribe({
      next: (res) => {
        this.departments = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  getDepartment() {
    this.deptSvc.getDepartmentById(this.department.departmentId).subscribe({
      next: (res) => {
        this.department = res;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  addDepartment() {
    this.deptSvc.addDepartment(this.department).subscribe({
      next: () => {
        alert('Department Added');
        this.loadDepartments();
        this.newDepartment();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  updateDepartment() {
    this.deptSvc.updateDepartment(this.department).subscribe({
      next: () => {
        alert('Department Updated');
        this.loadDepartments();
        this.newDepartment();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }

  deleteDepartment() {
    this.deptSvc.deleteDepartment(this.department.departmentId).subscribe({
      next: () => {
        alert('Department Deleted');
        this.loadDepartments();
        this.newDepartment();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
}
