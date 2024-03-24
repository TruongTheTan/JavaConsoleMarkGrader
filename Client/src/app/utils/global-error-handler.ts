import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { GetUser } from '../models/user';

@Injectable({
    providedIn: 'root',
})
export class GlobalErrorHandler {
    private message = new BehaviorSubject('' as string);

    handleHttpError(error: HttpErrorResponse) {
        this.message.next(error.error);
    }

    getUser() {
        return this.message.asObservable();
    }
}
