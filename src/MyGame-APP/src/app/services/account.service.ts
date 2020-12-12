import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ResultData } from './base.service';
import { map, filter, tap } from 'rxjs/operators'
import { User } from '../models/user.model';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    private userSubject: BehaviorSubject<User>;
    public userObservable: Observable<User>;
    constructor(
        private http: HttpClient) {
        this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
        this.userObservable = this.userSubject.asObservable();
    }

    public get user(): User {
        return this.userSubject.value;
    }

    login(login: string, password: string): Observable<User> {
        return this.http.post<ResultData<User>>(`${environment.apiUrl}auth`, { login, password })
            .pipe(
                filter(result => result.success),
                map(user => {
                    localStorage.setItem('user', JSON.stringify(user.data));
                    this.userSubject.next(user.data);
                    return user.data;
                }));
    }

    logout() {
        localStorage.removeItem('user');
        this.userSubject.next(null);
    }
}
