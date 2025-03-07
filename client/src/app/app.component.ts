import { Component, inject, OnInit } from '@angular/core';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './_services/account.service';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  private accountService = inject(AccountService);

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userStr = localStorage.getItem('user');

    if (!userStr) return;

    const user = JSON.parse(userStr);
    this.accountService.setCurrentUser(user);
  }
}
