import { HttpClient, HttpErrorResponse, HttpXhrBackend } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CREATE_USER_API, USER_LIST_API } from '../api/api';
import { CustomResponse } from '../models/custom-response';
import { Subscriptions } from '../models/subcription';
import { CreateUser, GetUser } from '../models/user';
import { UserStore } from '../stores/user.store';
import { GlobalHttpHandler } from './../utils/global-http-handler';

@Injectable({
    providedIn: 'root',
})
export class AdminService extends Subscriptions {
    // Inject services
    private readonly http = inject(HttpClient);
    private readonly userStore = inject(UserStore);
    private readonly globalHttpHandler = inject(GlobalHttpHandler);

    private readonly apiKey = `https://emailvalidation.abstractapi.com/v1/?api_key=1d6495eafd9045e486e37c8d0d2b40b8&email=`;

    //

    getUserList() {
        const sub = this.http.get<CustomResponse<GetUser[]>>(USER_LIST_API).subscribe({
            next: (customResponse) => this.userStore.updateList(customResponse.data),
            error: (error) => this.globalHttpHandler.handleError(error),
        });

        this.subscriptions.push(sub);
    }

    createNewUser(createUser: CreateUser) {
        const newHttpClient = new HttpClient(new HttpXhrBackend({ build: () => new XMLHttpRequest() }));

        const sub = newHttpClient
            .get<{ deliverability: string }>(this.apiKey + createUser.email.trim(), {})
            .subscribe({
                next: (data) => {
                    // Create user if email is valid
                    if (data.deliverability === 'DELIVERABLE') {
                        this.http.post<CustomResponse<unknown>>(CREATE_USER_API, createUser).subscribe({
                            next: (customResponse) => this.globalHttpHandler.handleSuccess(customResponse),
                            error: (error) => this.globalHttpHandler.handleError(error),
                        });

                        // Inform error
                    } else {
                        const error = {
                            status: 404,
                            error: { message: 'Email is not eligible for delivery' } as CustomResponse<unknown>,
                        } as HttpErrorResponse;
                        this.globalHttpHandler.handleError(error);
                    }
                },
                error: (error) => this.globalHttpHandler.handleError(error),
            });

        this.subscriptions.push(sub);
    }
}
