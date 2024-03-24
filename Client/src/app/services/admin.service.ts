import { GlobalErrorHandler } from '../utils/global-error-handler';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { CreateUser, AuthenticationUser, GetUser } from '../models/user';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { ModalComponent } from '../components/modal/modal.component';
import { UserStore } from '../stores/user.store';

@Injectable({
    providedIn: 'root',
})
export class AdminService {
    private readonly USER_LIST_API = 'User/list';
    private readonly CREATE_USER_API = 'User/create';
    private readonly API = '';

    constructor(private http: HttpClient, private userStore: UserStore) {}

    getUserList() {
        this.http.get<GetUser[]>(this.USER_LIST_API).subscribe((response) => {
            this.userStore.updateList(response);
        });
    }

    createNewUser(createUser: CreateUser) {
        this.http.post<any>(this.CREATE_USER_API, createUser).subscribe((response) => {});
    }
}
