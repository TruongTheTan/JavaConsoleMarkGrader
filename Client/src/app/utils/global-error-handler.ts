import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { GetUser } from '../models/user';

@Injectable({
    providedIn: 'root',
})
export class GlobalErrorHandler {
    readonly message = signal('sss');

    handleHttpError(error: HttpErrorResponse) {
        this.message.set(error.error);
        console.log(this.message());
    }
}
