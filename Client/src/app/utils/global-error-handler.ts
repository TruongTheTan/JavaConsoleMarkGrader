import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class GlobalErrorHandler {
    private message = new BehaviorSubject('' as string);

    handleHttpError(error: HttpErrorResponse) {
        let errorMessage = '';

        if (typeof error.error === 'object') errorMessage = error.statusText;
        else errorMessage = error.error;

        this.message.next(errorMessage);
    }

    getErrorMessage() {
        return this.message.asObservable();
    }
}
