import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SLA } from '../Models/sla';

@Injectable({
  providedIn: 'root',
})
export class SlaService {
  http: HttpClient = inject(HttpClient);
  token;
  baseUrl: string = 'http://localhost:5181/api/Sla/';
  httpOptions;

  constructor() {
    this.token = sessionStorage.getItem('token');
    this.httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token,
      }),
    };
  }

  getAllSlas(): Observable<SLA[]> {
    return this.http.get<SLA[]>(this.baseUrl, this.httpOptions);
  }

  getSlaById(slaId: string): Observable<SLA> {
    return this.http.get<SLA>(this.baseUrl + slaId, this.httpOptions);
  }

  addSla(sla: SLA): Observable<any> {
    return this.http.post(this.baseUrl, sla, this.httpOptions);
  }

  updateSla(sla: SLA): Observable<any> {
    return this.http.put(this.baseUrl, sla, this.httpOptions);
  }

  deleteSla(slaId: string): Observable<any> {
    return this.http.delete(this.baseUrl + slaId, this.httpOptions);
  }
}
