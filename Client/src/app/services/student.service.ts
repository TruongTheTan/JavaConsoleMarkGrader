import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import api from '../api/api';

@Injectable({
    providedIn: 'root',
})
export class StudentService {
    private readonly http = inject(HttpClient);

    submitFile(file: File) {
        const formData: FormData = new FormData();

        formData.append('file', file);

        this.http.post(api.SUBMIT_FILE, formData).subscribe((data) => {
            console.log(data);
        });
    }
}
