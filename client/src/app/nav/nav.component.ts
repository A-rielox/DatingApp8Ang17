import { AsyncPipe, NgIf, TitleCasePipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule /*, AsyncPipe*/, BsDropdownModule, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent implements OnInit {
  // constructor(public accountService: AccountService) {}
  accountService = inject(AccountService);

  model: any = { username: 'tom', password: 'P@ssword1' };

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: (res: any) => {
        console.log(res);
      },
      error: (err: any) => console.log(err),
    });
  }

  logout() {
    this.accountService.logout();
  }
}
