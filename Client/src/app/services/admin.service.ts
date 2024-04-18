import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CustomResponse } from '../models/customer-response';
import { CreateUser, GetUser } from '../models/user';
import { UserStore } from '../stores/user.store';
import { GlobalHttpHandler } from '../utils/global-http-handler';

@Injectable({
    providedIn: 'root',
})
export class AdminService {
    private readonly USER_LIST_API = 'User/list';
    private readonly CREATE_USER_API = 'User/create';
    private readonly TEST_CASE_LIST_API = '';
    private readonly CREATE_TEST_CASE_API = '';

    constructor(
        private http: HttpClient,
        private userStore: UserStore,
        private errorHandler: GlobalHttpHandler
    ) {}

    getUserList() {
        this.http.get<CustomResponse<GetUser[]>>(this.USER_LIST_API).subscribe({
            next: (customResponse) => this.userStore.updateList(customResponse.data),
            error: (error) => this.errorHandler.handleError(error),
        });
    }

    createNewUser(createUser: CreateUser) {
        this.http.post(this.CREATE_USER_API, createUser).subscribe({
            next: (a) => this.userStore.getUserList(),
            error: (error) => this.errorHandler.handleError(error),
        });
    }

    getTestCaseList() {}

    createNewTestCase() {}
}
