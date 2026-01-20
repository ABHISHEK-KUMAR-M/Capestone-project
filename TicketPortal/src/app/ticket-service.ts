import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ticket } from '../Models/ticket';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  http: HttpClient = inject(HttpClient);
  token;
  baseUrl: string = 'http://localhost:5181/api/Ticket/';
  httpOptions;

  constructor() {
    this.token = sessionStorage.getItem('token');
    this.httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token,
      }),
    };
  }

  getAllTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.baseUrl, this.httpOptions);
  }

  getTicketById(ticketId: number): Observable<Ticket> {
    return this.http.get<Ticket>(this.baseUrl + ticketId, this.httpOptions);
  }

  addTicket(ticket: Ticket): Observable<any> {
    return this.http.post(this.baseUrl, ticket, this.httpOptions);
  }

  updateTicket(ticket: Ticket): Observable<any> {
    return this.http.put(this.baseUrl, ticket, this.httpOptions);
  }

  deleteTicket(ticketId: number): Observable<any> {
    return this.http.delete(this.baseUrl + ticketId, this.httpOptions);
  }
}
