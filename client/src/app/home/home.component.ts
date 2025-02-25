import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from '../register/register.component';

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

  registerMode: boolean = false;
  users: any;
  // baseUrl = environment.apiUrl;
  baseUrl = 'https://localhost:5001/api/';

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.http.get(this.baseUrl + 'users').subscribe({
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
}
