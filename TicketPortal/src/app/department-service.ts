import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Department } from '../Models/department';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  http: HttpClient = inject(HttpClient);
  token;
  baseUrl: string = 'http://localhost:5181/api/Department/';
  httpOptions;

  constructor() {
    this.token = sessionStorage.getItem('token');
    this.httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token,
      }),
    };
  }

  getAllDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.baseUrl, this.httpOptions);
  }

  getDepartmentById(departmentId: string): Observable<Department> {
    return this.http.get<Department>(this.baseUrl + departmentId, this.httpOptions);
  }

  addDepartment(department: Department): Observable<any> {
    return this.http.post(this.baseUrl, department, this.httpOptions);
  }

  updateDepartment(department: Department): Observable<any> {
    return this.http.put(this.baseUrl, department, this.httpOptions);
  }

  deleteDepartment(departmentId: string): Observable<any> {
    return this.http.delete(this.baseUrl + departmentId, this.httpOptions);
  }
}
