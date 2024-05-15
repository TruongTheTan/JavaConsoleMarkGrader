import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import api from '../api/api';
import { CustomResponse } from '../models/custom-response';
import { GetSemester } from '../models/semester';
import { GlobalHttpHandler } from '../utils/global-http-handler';
import { SemesterStore } from './../stores/semester.store';

@Injectable({
    providedIn: 'root',
})
export class SemesterService {
    // Injection
    private readonly http = inject(HttpClient);
    private readonly semesterStore = inject(SemesterStore);
    private readonly httpResultHandler = inject(GlobalHttpHandler);

    getSemesterList() {
        this.http.get<CustomResponse<GetSemester[]>>(api.SEMESTER_LIST_API).subscribe({
            next: (customResponse) => this.semesterStore.updateSemesterList(customResponse.data),
            error: (error) => this.httpResultHandler.handleError(error),
        });
    }

    getSemesterById(id: number) {
        this.http.get<CustomResponse<GetSemester>>(`${api.GET_SEMESTER_API}/${id}`).subscribe({
            next: (customResponse) => {
                this.httpResultHandler.handleSuccess(customResponse);
                this.semesterStore.updateSemester(customResponse.data);
            },
            error: (error) => this.httpResultHandler.handleError(error),
        });
    }

    createSemester() {}

    updateSemester() {}
}
