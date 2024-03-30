import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateUser, GetUser } from '../models/user';
import { UserStore } from '../stores/user.store';
import { GlobalErrorHandler } from '../utils/global-error-handler';

@Injectable({
    providedIn: 'root',
})
export class AdminService {
    private readonly USER_LIST_API = 'User/list';
    private readonly CREATE_USER_API = 'User/create';
    private readonly API = '';

    constructor(
        private http: HttpClient,
        private userStore: UserStore,
        private errorHander: GlobalErrorHandler
    ) {}

    getUserList() {
        this.http.get<GetUser[]>(this.USER_LIST_API).subscribe({
            next: (response) => this.userStore.updateList(response),
            error: (error: HttpErrorResponse) => this.errorHander.handleHttpError(error),
        });
    }

    createNewUser(createUser: CreateUser) {
        this.http.post(this.CREATE_USER_API, createUser).subscribe({
            next: () => this.userStore.getUserList(),
            error: (error: HttpErrorResponse) => this.errorHander.handleHttpError(error),
        });
    }
}
