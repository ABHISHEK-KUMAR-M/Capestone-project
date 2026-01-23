import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Employee } from '../../Models/employee';
import { EmployeeService } from '../employee-service';
import { DepartmentService } from '../department-service';
import { Department } from '../../Models/department';
import { AuthService } from '../auth-service';

@Component({
  selector: 'app-employee-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './employee-component.html',
  styleUrl: './employee-component.css',
})
export class EmployeeComponent {
 
  empSvc: EmployeeService = inject(EmployeeService);
  deptSvc:DepartmentService=inject(DepartmentService);
  AuthSvc:AuthService=inject(AuthService);

  employee: Employee;
  employees: Employee[];
  errMsg: string;
 
  empId: string = '';
  departmentId: string = '';
  departments:Department[];
 
  constructor() {
    this.employee = new Employee();
    this.employees = [];
    this.errMsg = '';
    this.departments=[];
    this.loadAllEmployees();
  }
 
  newEmployee() {
    this.employee = new Employee();
  }
  loadAllDepartments(){
    this.deptSvc.getAllDepartments().subscribe({
      next:(res)=>{
        this.departments=res;
        this.errMsg="";
        this.loadAllDepartments();
      },
      error:(err)=>(this.errMsg=err.error)
    })
  }
  loadAllEmployees() {
    this.empSvc.getAllEmployees().subscribe({
      next: (res) => {
        this.employees = res;
        this.errMsg = '';
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
 
  getEmployeeById() {
    this.empSvc.getEmployeeById(this.empId).subscribe({
      next: (res) => {
        this.employee = res;
        this.errMsg = '';
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
 
  getEmployeesByDepartment(deptId:string) {
    this.empSvc.getByDepartmentId(deptId).subscribe({
      next: (res) => {
        this.employees = res;
        this.errMsg = '';
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
 
  addEmployee() {
    this.empSvc.addEmployee(this.employee).subscribe({
      next: () => {
        alert('Employee Added Successfully');
        this.loadAllEmployees();
        this.newEmployee();
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
 
  updateEmployee() {
    this.empSvc.updateEmployee(this.employee).subscribe({
      next: () => {
        alert('Employee Updated Successfully');
        this.loadAllEmployees();
        this.newEmployee();
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
 
  deleteEmployee() {
    this.empSvc.deleteEmployee(this.empId).subscribe({
      next: () => {
        alert('Employee Deleted Successfully');
        this.loadAllEmployees();
        this.newEmployee();
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
 
  login() {
    this.empSvc.login(this.empId, this.employee.password).subscribe({
      next: (res) => {
        alert('Login Successful');
        sessionStorage.setItem('employee', JSON.stringify(res));
        this.errMsg = '';
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    });
  }
}
 
 
 
 