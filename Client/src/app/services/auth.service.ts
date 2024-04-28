import { SocialAuthService } from '@abacritt/angularx-social-login';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { CustomResponse } from '../models/custom-response';
import { GlobalHttpHandler } from '../utils/global-http-handler';
import { AuthenticationUser } from './../models/user';
import { UserStore } from './../stores/user.store';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    // APIs
    private readonly BASIC_LOGIN_API = 'Auth/basic-login';
    private readonly GOOGLE_LOGIN_API = 'Auth/google-login';
    private readonly RESET_PASSWORD_API = 'User/reset-password';
    private readonly CHANGE_PASSWORD_API = 'User/change-password';

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

        this.http
            .post<CustomResponse<AuthenticationUser>>(this.BASIC_LOGIN_API, loginObject)
            .subscribe({
                next: (customResponse) => {
                    this.globalHttpHandler.handleSuccess(customResponse);

                    const authenticationUser = customResponse.data;
                    this.handleUserStorage(authenticationUser);
                    this.redirectToPageByRole(authenticationUser.roleName);
                },
                error: (error) => this.globalHttpHandler.handleError(error),
            });
    }

    googleLogin(idToken: string, provider: string) {
        const googleLoginObject = { idToken, provider };

        this.http
            .post<CustomResponse<AuthenticationUser>>(this.GOOGLE_LOGIN_API, googleLoginObject)
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
    }

    resetPasswordToDefault(email: string) {
        this.http.patch(this.RESET_PASSWORD_API, { email }).subscribe({
            next: (user) => alert('Password reset to default'),
            error: (error: HttpErrorResponse) => this.globalHttpHandler.handleError(error),
        });
    }

    changePassword(email: string, oldPassword: string, newPassword: string) {
        const changePasswordObj = {
            email,
            oldPassword,
            newPassword,
        };

        this.http.patch(this.CHANGE_PASSWORD_API, changePasswordObj).subscribe({
            next: () => alert('ok'),
            error: (error: HttpErrorResponse) => this.globalHttpHandler.handleError(error),
        });
    }

    logout() {
        this.socialAuth.signOut();
        this.cookie.delete('token');
        this.router.navigateByUrl('');
        localStorage.removeItem('role');
    }

    private handleUserStorage(user: AuthenticationUser) {
        this.userStore.setUser({ ...user });
        this.cookie.set('token', user.token);
        localStorage.setItem('role', user.roleName);
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
