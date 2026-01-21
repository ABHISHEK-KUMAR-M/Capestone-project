import { Component, inject } from '@angular/core';
import { AuthService } from '../auth-service';

@Component({
  selector: 'app-home-component',
  imports: [],
  templateUrl: './home-component.html',
  styleUrl: './home-component.css',
})
export class HomeComponent {
  authSvc: AuthService = inject(AuthService);
  token:string = "";
  constructor() {
    // if(sessionStorage.getItem('userName'))
    // {
        this.authSvc.getToken().subscribe({
        next: (response: any) => {
          this.token = response;
          sessionStorage.setItem("token",this.token);
          console.log(response);
        },
        error: (err) => { 
          alert(err.message); console.log(err); 
          console.log("abhishek is a fool")
        }
      });
    // }
  }
}
