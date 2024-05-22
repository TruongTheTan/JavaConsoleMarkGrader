import { Component, inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GetUser } from 'src/app/models/user';
import { UserStore } from 'src/app/stores/user.store';
import { AdminService } from '../../../../services/admin.service';

@Component({
    selector: 'app-users',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
    //
    private readonly adminService = inject(AdminService);
    private readonly userStore = inject(UserStore);

    userList = {} as Observable<GetUser[]>;

    readonly userListHeaders = ['Name', 'Email', 'Phone', 'Role', 'Status (Is active)'];
    readonly userKeys = ['userName', 'email', 'phoneNumber', 'roleName', 'isActive'] as (keyof GetUser)[];

    ngOnInit(): void {
        this.adminService.getUserList();
        this.userList = this.userStore.getUserList;
    }

    onTableButtonClick(event: { data: GetUser; eventName: string }) {
        if (event.eventName === 'detail') {
        } else {
        }
    }
}
