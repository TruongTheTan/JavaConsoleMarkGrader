import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { GetUser } from '../models/user';

@Injectable({
    providedIn: 'root',
})
export class StudentService {
    private readonly studentAPI = 'Student/submit?semesterId=1';

    constructor(private http: HttpClient) {}

    submitFile(file: File) {
        const formData: FormData = new FormData();

        formData.append('file', file);

        this.http.post(this.studentAPI, formData).subscribe((data) => {
            console.log(data);
        });
    }
}
