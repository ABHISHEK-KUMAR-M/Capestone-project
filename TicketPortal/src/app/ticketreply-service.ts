import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketReply } from '../Models/ticketreply';

@Injectable({
  providedIn: 'root',
})
export class TicketreplyService {
  http: HttpClient = inject(HttpClient);

  baseUrl = 'https://team2ticketportalwebapi-h9aye2frb5fqfgfy.canadacentral-01.azurewebsites.net/api/TicketReply/';
  token = sessionStorage.getItem('token');

  httpOptions = {
    headers: new HttpHeaders({
      Authorization: 'Bearer ' + this.token,
    }),
  };

  addReply(reply: TicketReply): Observable<any> {
    return this.http.post(this.baseUrl, reply, this.httpOptions);
  }

  updateReply(replyId: number, reply: TicketReply): Observable<any> {
    return this.http.put(
      this.baseUrl + replyId,
      reply,
      this.httpOptions
    );
  }

  deleteReply(replyId: number): Observable<any> {
    return this.http.delete(
      this.baseUrl + replyId,
      this.httpOptions
    );
  }

  getReplyById(replyId: number): Observable<TicketReply> {
    return this.http.get<TicketReply>(
      this.baseUrl + replyId,
      this.httpOptions
    );
  }

  getAllReplies(): Observable<TicketReply[]> {
    return this.http.get<TicketReply[]>(
      this.baseUrl,
      this.httpOptions
    );
  }

  getRepliesByTicket(ticketId: number): Observable<TicketReply[]> {
    return this.http.get<TicketReply[]>(
      this.baseUrl + 'ticket/' + ticketId,
      this.httpOptions
    );
  }

  getRepliesByEmployee(empId: string): Observable<TicketReply[]> {
    return this.http.get<TicketReply[]>(
      this.baseUrl + 'employee/' + empId,
      this.httpOptions
    );
  }
}
