import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  http:HttpClient=inject(HttpClient);
  baseUrl:string="http://localhost:5082/api/Auth/";
  userName:string="ramesh@ey.com";
  role:string="Admin";
  empNameSignal = signal<string | null>(null);
  empIdSignal = signal<string | null>(null);
  empRoleSignal = signal<string | null>(null);
  secretKey:string="I am a Developer with maestro scooty.";

  constructor(){
    this.setAllSignals();
  }
  setAllSignals(){
    this.empNameSignal.set(sessionStorage.getItem('empName'));
    this.empIdSignal.set(sessionStorage.getItem('empId'));
    this.empRoleSignal.set(sessionStorage.getItem('empRole'));
  }

  getToken():Observable<string>{
    return this.http.get(this.baseUrl+this.userName+"/"+this.role+"/"+this.secretKey,{responseType:'text'});
  }

  setLogin(empName: string,empId:string,empRole:string) {
    sessionStorage.setItem('empName', empName);
    sessionStorage.setItem('empId', empId); 
    sessionStorage.setItem('empRole', empRole); 
    this.empNameSignal.set(empName);
    this.empIdSignal.set(empId);
    this.empRoleSignal.set(empRole);
  }

  logout() {
    sessionStorage.clear();
    this.empNameSignal.set(null);
  }
}
