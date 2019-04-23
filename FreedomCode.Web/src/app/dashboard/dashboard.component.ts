import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../_services';
import { User, Role } from '../_models';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  username: string;
  currentUser: User;
  constructor(
    private routeTo: Router,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(u => this.currentUser = u);
  }

  get isAdmin() {
    return this.currentUser && this.currentUser.role === Role.Admin;
  }
  get isUser() {
    return this.currentUser && this.currentUser.role === Role.User;
  }

  ngOnInit() {
    this.username = this.currentUser.firstName;
  }

  logout() {
    this.authenticationService.logout();
    this.routeTo.navigate(['/']);
  }

}
