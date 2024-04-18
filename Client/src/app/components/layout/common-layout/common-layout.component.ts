import { Component } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from './../../../services/auth.service';
@Component({
    selector: 'app-common-layout',
    templateUrl: './common-layout.component.html',
    styleUrls: ['./common-layout.component.css'],
})
export class CommonLayoutComponent {
    role = '';
    isAuthenticated = false;

    constructor(private authService: AuthService, private cookie: CookieService) {
        this.role = localStorage.getItem('role')?.toString()!;
        this.isAuthenticated = this.cookie.get('token') ? true : false;
    }

    logout() {
        this.authService.logout();
    }
}
