import {
  Component,
  EventEmitter,
  inject,
  input,
  Input,
  OnInit,
  output,
  Output,
} from '@angular/core';
import { AccountService } from '../_services/account.service';
import { FormsModule } from '@angular/forms';
import { TitleCasePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, TitleCasePipe],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);

  // @Input() usersFromHomeComponent: any = {};
  usersFromHomeComponent = input.required<any>();
  // @Output() cancelRegister = new EventEmitter();
  cancelRegister = output<boolean>();
  model: any = {};

  ngOnInit(): void {}

  register() {
    this.accountService.register(this.model).subscribe({
      next: (res) => {
        this.cancel(); // cierro el register form
        this.toastr.success('Bienvenido ');
      },
      error: (err) => {
        console.log(err);
        this.toastr.error(err.error + '  💩');
      },
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
