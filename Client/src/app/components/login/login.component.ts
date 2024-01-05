import { AuthService } from './../../services/auth.service';
import { Component } from '@angular/core';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    title = 'ádfasdfas';

    constructor(private authService: AuthService) {}

    login() {
        this.authService.getTestCase();
    }
}
