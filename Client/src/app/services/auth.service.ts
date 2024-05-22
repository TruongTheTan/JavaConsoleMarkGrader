import { SocialAuthService } from '@abacritt/angularx-social-login';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { BASIC_LOGIN_API, CHANGE_PASSWORD_API, GOOGLE_LOGIN_API, RESET_PASSWORD_API } from '../api/api';
import { CustomResponse } from '../models/custom-response';
import { Subscriptions } from '../models/subcription';
import { GlobalHttpHandler } from '../utils/global-http-handler';
import { AuthenticationUser } from './../models/user';
import { UserStore } from './../stores/user.store';

@Injectable({
    providedIn: 'root',
})
export class AuthService extends Subscriptions {
    // Inject services
    private readonly http = inject(HttpClient);
    private readonly cookie = inject(CookieService);
    private readonly userStore = inject(UserStore);
    private readonly router = inject(Router);
    private readonly socialAuth = inject(SocialAuthService);
    private readonly globalHttpHandler = inject(GlobalHttpHandler);
    //

    login(email: string, password: string) {
        const loginObject = {
            email,
            password,
        };

        const sub = this.http.post<CustomResponse<AuthenticationUser>>(BASIC_LOGIN_API, loginObject).subscribe({
            next: (customResponse) => {
                this.globalHttpHandler.handleSuccess(customResponse);

                const authenticationUser = customResponse.data;
                this.handleUserStorage(authenticationUser);
                this.redirectToPageByRole(authenticationUser.roleName);
            },
            error: (error) => this.globalHttpHandler.handleError(error),
        });
        this.subscriptions.push(sub);
    }

    googleLogin(idToken: string, provider: string) {
        const googleLoginObject = { idToken, provider };

        const sub = this.http
            .post<CustomResponse<AuthenticationUser>>(GOOGLE_LOGIN_API, googleLoginObject)
            .subscribe({
                next: (customResponse) => {
                    this.globalHttpHandler.handleSuccess(customResponse);

                    const authenticationUser = { ...customResponse.data };
                    this.handleUserStorage(authenticationUser);
                    this.redirectToPageByRole(authenticationUser.roleName);
                },
                error: (error) => {
                    this.globalHttpHandler.handleError(error);
                    this.socialAuth.signOut();
                },
            });

        this.subscriptions.push(sub);
    }

    resetPasswordToDefault(email: string) {
        const sub = this.http.patch(RESET_PASSWORD_API, { email }).subscribe({
            next: (user) => alert('Password reset to default'),
            error: (error: HttpErrorResponse) => this.globalHttpHandler.handleError(error),
        });

        this.subscriptions.push(sub);
    }

    changePassword(email: string, oldPassword: string, newPassword: string) {
        const changePasswordObj = {
            email,
            oldPassword,
            newPassword,
        };

        const sub = this.http.patch(CHANGE_PASSWORD_API, changePasswordObj).subscribe({
            next: () => alert('ok'),
            error: (error: HttpErrorResponse) => this.globalHttpHandler.handleError(error),
        });

        this.subscriptions.push(sub);
    }

    logout() {
        this.socialAuth.signOut();
        this.cookie.delete('token');
        this.router.navigateByUrl('');
        localStorage.removeItem('role');
    }

    private handleUserStorage(user: AuthenticationUser) {
        console.log(user);

        this.userStore.setUser({ ...user });
        localStorage.setItem('role', user.roleName);
        this.cookie.set('token', user.token, {
            secure: true,
            sameSite: 'Strict',
        });
    }

    private redirectToPageByRole(role: string) {
        switch (role) {
            case 'Admin':
                this.router.navigateByUrl('/app/admin/user');
                break;
            case 'Teacher':
                this.router.navigateByUrl('/app/teacher');
                break;
            case 'Student':
                this.router.navigateByUrl('/app/student');
                break;
        }
    }
}
