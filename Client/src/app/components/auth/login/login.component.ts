import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent {
    readonly loginForm = new FormGroup({
        email: new FormControl('', [
            Validators.required,
            Validators.pattern('^[A-Za-z0-9_.]+@gmail.com$'),
        ]),
        password: new FormControl('', Validators.required),
    });

    constructor(private authService: AuthService, private socialAuth: SocialAuthService) {
        this.googleLogin();
    }

    login() {
        // truongthetan1601@gmail.com, 123@123A, student
        // teacher@gmail.com, 123@123A, teacher
        // admin@gmail.com, 123@123A, admin

        if (this.loginForm.valid) {
            const { email, password } = this.loginForm.controls;
            this.authService.login(email.value!, password.value!);
        }
    }

    private googleLogin() {
        this.socialAuth.authState.subscribe((user: SocialUser) => {
            if (user !== null) {
                console.table(user);
                this.authService.googleLogin(user.idToken, user.provider);
            }
        });
    }
}
