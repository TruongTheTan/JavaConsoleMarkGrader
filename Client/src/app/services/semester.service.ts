import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CustomResponse } from '../models/custom-response';
import { GetSemester } from '../models/semester';
import { GlobalHttpHandler } from '../utils/global-http-handler';
import { SemesterStore } from './../stores/semester.store';

@Injectable({
    providedIn: 'root',
})
export class SemesterService {
    // APIs
    private readonly SEMESTER_LIST_API = '';
    private readonly CREATE_SEMESTER_API = '';
    private readonly UPDATE_SEMESTER_API = '';
    private readonly GET_SEMESTER_API = '';

    // Injection
    private readonly http = inject(HttpClient);
    private readonly semesterStore = inject(SemesterStore);
    private readonly httpResultHandler = inject(GlobalHttpHandler);

    GetSemesterList() {
        this.http.get<CustomResponse<GetSemester[]>>(this.SEMESTER_LIST_API).subscribe({
            next: (customResponse) => {
                this.httpResultHandler.handleSuccess(customResponse);
                this.semesterStore.updateSemesterList(customResponse.data);
            },
            error: (error) => this.httpResultHandler.handleError(error),
        });
    }

    GetSemesterById(id: number) {
        this.http.get<CustomResponse<GetSemester>>(`${this.GET_SEMESTER_API}/${id}`).subscribe({
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
