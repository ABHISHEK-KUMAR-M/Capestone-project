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
  empNameSignal = signal<string | null>(sessionStorage.getItem('empName'));
  secretKey:string="I am a Developer with maestro scooty.";
  getToken():Observable<string>{
    return this.http.get(this.baseUrl+this.userName+"/"+this.role+"/"+this.secretKey,{responseType:'text'});
  }

  setLogin(empName: string) {
    sessionStorage.setItem('empName', empName);
    this.empNameSignal.set(empName);
  }

  logout() {
    sessionStorage.clear();
    this.empNameSignal.set(null);
  }
}
