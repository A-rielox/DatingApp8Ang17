import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { map } from 'rxjs';
import { User } from '../_models/user';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  // constructor(private http: HttpClient) {}
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  // private currentUserSource = new BehaviorSubject<User | null>(null);
  // currentUser$ = this.currentUserSource.asObservable();
  currentUser = signal<User | null>(null);

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((user) => {
        if (user) {
          this.setCurrentUser(user);
        }

        return user;
      }),
    );
  }

  logout() {
    // this.currentUserSource.next(null);
    this.currentUser.set(null);
    localStorage.removeItem('user');
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
      }),
    );
  }

  // lo ocupo desde app.component.ts
  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }
}
