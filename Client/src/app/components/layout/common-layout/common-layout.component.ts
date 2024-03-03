import { AuthService } from './../../../services/auth.service';
import { Component } from '@angular/core';
import { UserStore } from 'src/app/stores/user.store';

@Component({
    selector: 'app-common-layout',
    templateUrl: './common-layout.component.html',
    styleUrls: ['./common-layout.component.css'],
})
export class CommonLayoutComponent {
    role = 'Admin';

    constructor(private userStore: UserStore, private authService: AuthService) {
        //this.role = userStore.getUser().roleName;
    }

    logout() {
        this.authService.logout();
    }
}
