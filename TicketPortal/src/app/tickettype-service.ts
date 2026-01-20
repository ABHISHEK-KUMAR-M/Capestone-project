import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketType } from '../Models/tickettype';

@Injectable({
  providedIn: 'root',
})
export class TicketTypeService {
  http: HttpClient = inject(HttpClient);
  token;
  baseUrl: string = 'http://localhost:5181/api/TicketType/';
  httpOptions;

  constructor() {
    this.token = sessionStorage.getItem('token');
    this.httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token,
      }),
    };
  }

  getAllTicketTypes(): Observable<TicketType[]> {
    return this.http.get<TicketType[]>(this.baseUrl, this.httpOptions);
  }

  getTicketTypeById(ticketTypeId: string): Observable<TicketType> {
    return this.http.get<TicketType>(
      this.baseUrl + ticketTypeId,
      this.httpOptions
    );
  }

  addTicketType(ticketType: TicketType): Observable<any> {
    return this.http.post(this.baseUrl, ticketType, this.httpOptions);
  }

  updateTicketType(ticketType: TicketType): Observable<any> {
    return this.http.put(this.baseUrl, ticketType, this.httpOptions);
  }

  deleteTicketType(ticketTypeId: string): Observable<any> {
    return this.http.delete(
      this.baseUrl + ticketTypeId,
      this.httpOptions
    );
  }
}
