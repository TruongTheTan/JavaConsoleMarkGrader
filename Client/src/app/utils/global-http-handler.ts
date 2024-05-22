import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CustomResponse } from '../models/custom-response';

@Injectable({
    providedIn: 'root',
})
export class GlobalHttpHandler {
    private readonly message = new BehaviorSubject('');
    private readonly isSuccessResponse = new BehaviorSubject(true);

    //
    handleSuccess(customResponse: CustomResponse<unknown>) {
        this.message.next(customResponse.message);
        this.isSuccessResponse.next(true);
    }

    handleError(error: HttpErrorResponse) {
        let errorMessage = '';

        switch (true) {
            case error.error instanceof ProgressEvent:
                errorMessage = error.statusText;
                break;

            case error.status === 401:
                errorMessage = 'Unauthorized, please login';
                break;

            case error.status === 429:
                errorMessage = 'Too Many Requests';
                break;

            default:
                const customResponse = error.error as CustomResponse<unknown>;
                errorMessage = customResponse.message;
                break;
        }

        this.message.next(errorMessage);
        this.isSuccessResponse.next(false);
    }

    get getErrorMessage(): Observable<string> {
        return this.message.asObservable();
    }

    get getResponseType(): Observable<boolean> {
        return this.isSuccessResponse.asObservable();
    }
}
