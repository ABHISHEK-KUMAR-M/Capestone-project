import { Component, inject } from '@angular/core';
import { AuthService } from '../auth-service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-logout-component',
  imports: [],
  templateUrl: './logout-component.html',
  styleUrl: './logout-component.css',
})
export class LogoutComponent {
  authSvc:AuthService=inject(AuthService);
  router:Router=inject(Router);
  constructor(){
    this.authSvc.logout();
    this.router.navigate(['']);
  }
}
