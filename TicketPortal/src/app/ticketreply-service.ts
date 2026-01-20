import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketReply } from '../Models/ticketreply';

@Injectable({
  providedIn: 'root',
})
export class TicketreplyService {
  http: HttpClient = inject(HttpClient);

  token;
  baseUrl: string = 'http://localhost:5181/api/TicketReply/';
  httpOptions;

  constructor() {
    this.token = sessionStorage.getItem('token');
    this.httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token,
      }),
    };
  }

  getAllReplies(): Observable<TicketReply[]> {
    return this.http.get<TicketReply[]>(this.baseUrl, this.httpOptions);
  }

  getReplyById(replyId: number): Observable<TicketReply> {
    return this.http.get<TicketReply>(
      this.baseUrl + replyId,
      this.httpOptions
    );
  }

  getRepliesByTicket(ticketId: number): Observable<TicketReply[]> {
    return this.http.get<TicketReply[]>(
      this.baseUrl + 'ticket/' + ticketId,
      this.httpOptions
    );
  }

  addReply(reply: TicketReply): Observable<any> {
    return this.http.post(this.baseUrl, reply, this.httpOptions);
  }

  updateReply(reply: TicketReply): Observable<any> {
    return this.http.put(this.baseUrl, reply, this.httpOptions);
  }

  deleteReply(replyId: number): Observable<any> {
    return this.http.delete(
      this.baseUrl + replyId,
      this.httpOptions
    );
  }
}
