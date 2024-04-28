import { Component, inject, OnInit } from '@angular/core';
import { GetUser } from 'src/app/models/user';
import { UserStore } from 'src/app/stores/user.store';
import { AdminService } from '../../../../../services/admin.service';

@Component({
    selector: 'app-users',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
    //
    private readonly adminService = inject(AdminService);
    private readonly userStore = inject(UserStore);

    userList = [] as GetUser[];

    ngOnInit(): void {
        this.adminService.getUserList();
        this.userStore.getUserList().subscribe((userList) => (this.userList = userList));
    }
}
