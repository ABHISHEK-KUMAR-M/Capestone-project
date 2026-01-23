import { Component, inject, signal, Signal } from '@angular/core';
import { Employee } from '../../Models/employee';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../auth-service';
import { EmployeeService } from '../employee-service';
@Component({
  selector: 'app-login-component',
  imports: [FormsModule,CommonModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
})
export class LoginComponent {
  EmpSvc:EmployeeService=inject(EmployeeService);
  authSvc:AuthService=inject(AuthService);
  Emp:Employee;
  router:Router=inject(Router);
  EmpId:string;
  EmpPassword:string;
  errMsg:string;
  constructor(){
    this.EmpId="";
    this.EmpPassword="";
    this.errMsg="";
    this.Emp=new Employee();
  }
  login(){
    this.EmpSvc.login(this.EmpId,this.EmpPassword).subscribe({
      next:(response:any)=>{
        this.Emp=response;
        this.authSvc.setLogin(this.Emp.name,this.Emp.empId,this.Emp.role);
        this.errMsg="";
        this.router.navigate(['']);
      },
        error: err => {
       this.errMsg =Object.values(err.error?.errors || {}).flat().join(',');
      }
    })
  }
}
