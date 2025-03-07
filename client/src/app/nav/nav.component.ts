import { AsyncPipe, NgIf, TitleCasePipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [
    FormsModule /*, AsyncPipe*/,
    BsDropdownModule,
    TitleCasePipe,
    RouterLink,
    RouterLinkActive,
  ],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent implements OnInit {
  // constructor(public accountService: AccountService) {}
  accountService = inject(AccountService);
  private router = inject(Router);
  private toastr = inject(ToastrService);

  model: any = { username: 'tom', password: 'P@ssword1' };

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: (res: any) => {
        console.log(res);
        this.router.navigateByUrl('/members');
        this.toastr.success('Bienvenido ' + res.username);
      },
      error: (err: any) => {
        console.log(err), this.toastr.error(err.error + '  💩');
      },
    });
  }

  logout() {
    this.toastr.success('Hasta luego.');
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
