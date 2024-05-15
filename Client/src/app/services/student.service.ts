import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class StudentService {
    private readonly studentAPI = 'Student/submit?semesterId=1';

    private readonly http = inject(HttpClient);

    submitFile(file: File) {
        const formData: FormData = new FormData();

        formData.append('file', file);

        this.http.post(this.studentAPI, formData).subscribe((data) => {
            console.log(data);
        });
    }
}
