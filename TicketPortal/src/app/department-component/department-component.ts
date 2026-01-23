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
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }

  getDepartment() {
    this.deptSvc.getDepartmentById(this.department.departmentId).subscribe({
      next: (res) => {
        this.department = res;
        this.errMsg = '';
      },
        error: (err) => (this.errMsg = err.error)
      
    });
  }

  addDepartment() {
    this.deptSvc.addDepartment(this.department).subscribe({
      next: () => {
        alert('Department Added');
        this.loadDepartments();
        this.newDepartment();
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }

  updateDepartment() {
    this.deptSvc.updateDepartment(this.department.departmentId, this.department).subscribe({
      next: () => {
        alert('Department Updated');
        this.loadDepartments();
        this.newDepartment();
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }

  deleteDepartment() {
  // Displaying the alert before calling the API, to confirm delete operation
  alert('Attempting to delete department...');

  // Call the service to delete the department
  this.deptSvc.deleteDepartment(this.department.departmentId).subscribe({
    next: () => {
      // This alert should show after the department has been deleted
      alert('Department Deleted');

      // Reload the departments
      this.loadDepartments();
      
      // Reset the department form
      this.newDepartment();

      // Optionally, you can log to the console to verify if everything worked
      console.log('Department deleted successfully');
    },
    error: err => {
      // If there's an error, you can display a custom error message
      this.errMsg = Object.values(err.error?.errors || {}).flat().join(',');
      alert(`Error: ${this.errMsg}`); // Show error in alert if deletion fails
    }
  });
}

  }

