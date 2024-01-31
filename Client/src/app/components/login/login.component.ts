import { Component, OnInit } from '@angular/core';
import { GetUser } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { UserStore } from 'src/app/stores/user.store';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent {
    email = '';
    password = '';
    user = {} as GetUser;

    count = 0;

    constructor(private authService: AuthService, private userStore: UserStore) {}

    login() {
        // truongthetan1601@gmail.com, 123@123A, student
        // teacher@gmail.com, 123@123A, teacher
        // admin@gmail.com, 123@123A, admin

        if (this.email.trim() === '' || this.password === '') {
            alert('Must fill in email and password');
        } else {
            this.authService.login(this.email, this.password);
            this.userStore.loggedUserSubject.subscribe((user) => (this.user = user));
        }
    }
}
