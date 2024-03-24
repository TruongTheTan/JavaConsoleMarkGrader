import { GlobalErrorHandler } from '../utils/global-error-handler';
import { UserStore } from './../stores/user.store';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { AuthenticationUser, GetUser } from '../models/user';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly BASIC_LOGIN_API = 'User/basic-login';
    private readonly GOOGLE_LOGIN_API = 'User/google-login';

    constructor(
        private http: HttpClient,
        private cookie: CookieService,
        private userStore: UserStore,
        private router: Router,
        private errorHander: GlobalErrorHandler
    ) {}

    login(email: string, password: string) {
        const loginObject = {
            email,
            password,
        };

        this.http.post<AuthenticationUser>(this.BASIC_LOGIN_API, loginObject).subscribe({
            next: (user) => {
                this.handleUserStorage(user);
                this.redirectToPageByRole(user.roleName);
            },
            error: (error: HttpErrorResponse) => this.errorHander.handleHttpError(error),
        });
    }

    googleLogin(idToken: string, provider: string) {
        const googleLoginObject = { idToken, provider };

        this.http.post<AuthenticationUser>(this.GOOGLE_LOGIN_API, googleLoginObject).subscribe({
            next: (user) => {
                this.handleUserStorage(user);
                this.redirectToPageByRole(user.roleName);
            },
            error: (error: HttpErrorResponse) => this.errorHander.handleHttpError(error),
        });
    }

    resetPasswordToDefault(email: string) {
        // TODO:
    }

    logout() {
        this.cookie.delete('token');
        this.router.navigateByUrl('');
    }

    private handleUserStorage(user: AuthenticationUser) {
        this.userStore.setUser({ ...user });
        this.cookie.set('token', user.token);
    }

    private redirectToPageByRole(role: string) {
        switch (role) {
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
    }
}
