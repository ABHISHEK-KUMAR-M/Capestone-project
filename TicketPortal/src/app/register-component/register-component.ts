import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../employee-service';
import { Employee } from '../../Models/employee';
import { Router } from '@angular/router';
import { DepartmentService } from '../department-service';
import { Department } from '../../Models/department';

@Component({
  selector: 'app-register-component',
  imports: [FormsModule,CommonModule],
  templateUrl: './register-component.html',
  styleUrl: './register-component.css',
})
export class RegisterComponent {
EmpSvc:EmployeeService=inject(EmployeeService);
DeptSvc:DepartmentService=inject(DepartmentService);
Departments:Department[];
emp:Employee;
router:Router=inject(Router)
errMsg:string;
constructor(){
  this.emp=new Employee("","","","","User","");
  this.errMsg="";
  this.Departments=[];
  this.showAllDepartments();
}
showAllDepartments(){
  this.DeptSvc.getAllDepartments().subscribe({
    next:(response:any)=>{
      this.Departments=response;
      this.errMsg="";
    },
      error: err => 
         {this.errMsg =err.error?.errors?Object.values(err.error?.errors || {})
                          .flat()
                          .join(', '):err.error;}
  })
}
 
register() {
    this.EmpSvc.addEmployee(this.emp).subscribe({
        next: (response: any) => {
            alert("New user registered");
            this.errMsg = "";
            this.router.navigate(['']);
        },
        error: (err) =>  {this.errMsg =err.error?.errors?Object.values(err.error?.errors || {})
                          .flat()
                          .join(', '):err.error;}
    });
}
 
}
 