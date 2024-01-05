import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class AuthService implements AA {
    constructor(private http: HttpClient) {}
    GetAll(): void {
        console.log(444);
    }

    login() {
        console.log(123);
    }

    getTestCase() {
        this.http
            .get('https://localhost:7124/api/TestCase/list?semesterId=1')
            .subscribe((res) => {
                console.log(res);
            });
    }
}

export interface AA {
    GetAll(): void;
}
