import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CustomResponse } from '../models/custom-response';
import { CreateUser, GetUser } from '../models/user';
import { UserStore } from '../stores/user.store';
import { GlobalHttpHandler } from '../utils/global-http-handler';

@Injectable({
    providedIn: 'root',
})
export class AdminService {
    // APIs
    private readonly USER_LIST_API = 'User/list';
    private readonly CREATE_USER_API = 'User/create';

    // Inject services
    private readonly http = inject(HttpClient);
    private readonly userStore = inject(UserStore);
    private readonly errorHandler = inject(GlobalHttpHandler);

    //

    getUserList() {
        this.http.get<CustomResponse<GetUser[]>>(this.USER_LIST_API).subscribe({
            next: (customResponse) => this.userStore.updateList(customResponse.data),
            error: (error) => this.errorHandler.handleError(error),
        });
    }

    createNewUser(createUser: CreateUser) {
        this.http.post(this.CREATE_USER_API, createUser).subscribe({
            next: (a) => {},
            error: (error) => this.errorHandler.handleError(error),
        });
    }
}
