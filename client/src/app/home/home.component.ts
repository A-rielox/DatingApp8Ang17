import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from '../register/register.component';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  // constructor(private http: HttpClient) {}
  http = inject(HttpClient);
  private accountService = inject(AccountService);

  registerMode: boolean = false;
  users: any;
  // baseUrl = environment.apiUrl;
  baseUrl = 'https://localhost:5001/api/';

  // si ya est[a logeado que entonces haga el request
  ngOnInit(): void {
    if (this.accountService.currentUser()) {
      this.getUsers();
    }
  }

  getUsers() {
    this.http
      .get(this.baseUrl + 'users' /*, this.getHttpOptions()*/)
      .subscribe({
        next: (res) => (this.users = res),
        error: (err) => console.log(err),
      });
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  // private getHttpOptions() {
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: `Bearer ${this.accountService.currentUser()?.token}`,
  //     }),
  //   };
  // }
}
