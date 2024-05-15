import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent {
    // Injection
    private readonly authService = inject(AuthService);
    private readonly formBuilder = inject(FormBuilder);
    private readonly socialAuth = inject(SocialAuthService);

    // Form
    readonly loginForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.pattern('^[A-Za-z0-9_.]+@gmail.com$')]],
        password: ['', Validators.required],
    });

    constructor() {
        this.googleLogin();
    }

    login() {
        // truongthetan1601@gmail.com, 123@123A, student
        // teacher@gmail.com, 123@123A, teacher
        // admin@gmail.com, T@n75541972, admin

        if (this.loginForm.valid) {
            const { email, password } = this.loginForm.controls;
            this.authService.login(email.value!, password.value!);
        }
    }

    private googleLogin() {
        this.socialAuth.authState.pipe(takeUntilDestroyed()).subscribe((user: SocialUser) => {
            if (user !== null) {
                console.table(user);
                this.authService.googleLogin(user.idToken, user.provider);
            }
        });
    }
}
