import { UserStore } from './../stores/user.store';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetUser } from '../models/user';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(
        private http: HttpClient,
        private cookie: CookieService,
        private userStore: UserStore,
        private router: Router
    ) {}

    login(email: string, password: string) {
        this.http
            .post<GetUser>('User/basic-login', {
                email,
                password,
            })
            .subscribe((data) => {
                this.userStore.setUser({ ...data });
                this.cookie.set('token', data.token);

                switch (data.roleName) {
                    case 'Admin':
                        this.router.navigateByUrl('/admin');
                        break;
                    case 'Teacher':
                        this.router.navigateByUrl('/teacher');
                        break;
                    case 'Student':
                        this.router.navigateByUrl('/student');
                        break;
                }
            });
    }

    logout() {
        this.cookie.delete('token');
    }
}
