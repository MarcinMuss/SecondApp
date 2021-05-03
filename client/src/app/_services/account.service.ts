import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({                  //service sa wstrzykiwane do komponentow lub innych uslug 
  providedIn: 'root'           // i są singelton tzn. dane przechowywane w usludze nie sa 
})                             //niszczone dopoki app nie zostanie zamknieta. Komponenty
export class AccountService {  //sa niszczone gdy nie sa uzywane (taka roznica)
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable(); //$ - observable

  constructor(private http: HttpClient) { }

  login(model: any) { // otrzymujemy z API UserDTo
    return this.http.post(this.baseUrl + 'account/login', model).pipe( //.pipe - to co chcemy zrobic z danymi 
      map((response: User) => {                                        //przed zasubskrybowaniem ich
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
