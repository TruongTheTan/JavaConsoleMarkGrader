import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CustomResponse } from './../models/customer-response';

@Injectable({
    providedIn: 'root',
})
export class GlobalHttpHandler {
    private readonly message = new BehaviorSubject('');
    private readonly isSuccessResponse = new BehaviorSubject(true);

    handleSuccess(customResponse: CustomResponse<unknown>) {
        this.message.next(customResponse.message);
        this.isSuccessResponse.next(true);
    }

    handleError(error: HttpErrorResponse) {
        let errorMessage = '';

        if (error.error !== null) {
            const customResponse = error.error as CustomResponse<unknown>;
            errorMessage = customResponse.message;
        } else if (error.status === 401) {
            errorMessage = 'Unauthorized';
        }

        this.message.next(errorMessage);
        this.isSuccessResponse.next(false);
    }

    getErrorMessage(): Observable<string> {
        return this.message.asObservable();
    }

    getResponseType(): Observable<boolean> {
        return this.isSuccessResponse.asObservable();
    }
}
