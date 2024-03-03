import { UserStore } from 'src/app/stores/user.store';
import { AdminService } from './../../../../services/admin.service';
import { Component, OnInit } from '@angular/core';
import { GetUser } from 'src/app/models/user';

@Component({
    selector: 'app-users',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
    userList = [] as GetUser[];

    constructor(private adminService: AdminService, private userStore: UserStore) {}

    ngOnInit(): void {
        this.adminService.getUserList();
        this.userStore.getUserList().subscribe((userList) => (this.userList = userList));
    }
}
