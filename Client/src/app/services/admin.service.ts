import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import api from '../api/api';
import { CustomResponse } from '../models/custom-response';
import { CreateUser, GetUser } from '../models/user';
import { UserStore } from '../stores/user.store';
import { GlobalHttpHandler } from '../utils/global-http-handler';

@Injectable({
    providedIn: 'root',
})
export class AdminService {
    // Inject services
    private readonly http = inject(HttpClient);
    private readonly userStore = inject(UserStore);
    private readonly errorHandler = inject(GlobalHttpHandler);

    //

    getUserList() {
        this.http.get<CustomResponse<GetUser[]>>(api.USER_LIST_API).subscribe({
            next: (customResponse) => this.userStore.updateList(customResponse.data),
            error: (error) => this.errorHandler.handleError(error),
        });
    }

    createNewUser(createUser: CreateUser) {
        this.http.post(api.CREATE_USER_API, createUser).subscribe({
            next: (a) => {},
            error: (error) => this.errorHandler.handleError(error),
        });
    }
}
