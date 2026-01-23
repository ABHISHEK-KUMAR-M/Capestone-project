import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ticket } from '../Models/ticket';

@Injectable({
  providedIn: 'root',
})

export class TicketService {
  private http = inject(HttpClient);

  private baseUrl = 'http://localhost:5082/api/Ticket/';

  private httpOptions = {
    headers: new HttpHeaders({
      Authorization: 'Bearer ' + sessionStorage.getItem('token'),
    }),
  };

  getAllTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.baseUrl, this.httpOptions);
  }

  getTicketById(ticketId: number): Observable<Ticket> {
    return this.http.get<Ticket>(
      `${this.baseUrl}+${ticketId}`,
      this.httpOptions
    );
  }

  addTicket(ticket: Ticket): Observable<any> {
    return this.http.post<void>(this.baseUrl,ticket,this.httpOptions);
  }

  updateTicket(ticketId:number,ticket: Ticket): Observable<any> {
    return this.http.put<void>(this.baseUrl + ticketId,ticket,this.httpOptions);
  }

  deleteTicket(ticketId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${ticketId}`,this.httpOptions);
  }

  getTicketsByEmpId(empId: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(
      `${this.baseUrl}empId/${empId}`,
      this.httpOptions
    );
  }

  getTicketsByStatus(status: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(
      `${this.baseUrl}status/${status}`,
      this.httpOptions
    );
  }

  getTicketsByTicketTypeId(ticketTypeId: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(
      `${this.baseUrl}byTickettype/${ticketTypeId}`,
      this.httpOptions
    );
  }

  getOverdueTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(
      `${this.baseUrl}overdue`,
      this.httpOptions
    );
  }
}
