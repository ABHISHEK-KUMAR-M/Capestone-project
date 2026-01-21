import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../Models/employee';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  http: HttpClient = inject(HttpClient);
  token;
  baseUrl: string = 'http://localhost:5082/api/Employee/';
  httpOptions;

  constructor() {
    this.token = sessionStorage.getItem('token');
    this.httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token,
      }),
    };
  }

  getAllEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.baseUrl, 
      this.httpOptions
    );
  }

  getEmployeeById(empId: string): Observable<Employee> {
    return this.http.get<Employee>(this.baseUrl + empId, 
      this.httpOptions

    );
  }

  getByDepartmentId(departmentId: string): Observable<Employee[]> {
    return this.http.get<Employee[]>(
      this.baseUrl + 'department/' + departmentId,
      this.httpOptions
    );
  }

  addEmployee(employee: Employee): Observable<any> {
    return this.http.post(this.baseUrl, employee, 
      this.httpOptions
    );
  }

  updateEmployee(employee: Employee): Observable<any> {
    return this.http.put(this.baseUrl, employee, 
      this.httpOptions
    );
  }

  deleteEmployee(empId: string): Observable<any> {
    return this.http.delete(this.baseUrl + empId, 
      this.httpOptions
    );
  }

  login(empId: string, password: string): Observable<Employee> {
    return this.http.get<Employee>(
      this.baseUrl + 'login/' + empId + '/' + password,
      this.httpOptions
    );
  }
}
