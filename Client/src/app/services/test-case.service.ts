import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { TEST_CASE_CREATE_API, TEST_CASE_LIST_API } from '../api/api';
import { CustomResponse } from '../models/custom-response';
import { CreateTestCase, GetTestCase } from '../models/test-case';
import { GlobalHttpHandler } from '../utils/global-http-handler';
import { TestCaseStore } from './../stores/test-case.store';

@Injectable({
    providedIn: 'root',
})
export class TestCaseService {
    // Inject services
    private readonly http = inject(HttpClient);
    private readonly testCaseStore = inject(TestCaseStore);
    private readonly httpResultHandler = inject(GlobalHttpHandler);

    getTestCaseList() {
        this.http.get<CustomResponse<GetTestCase[]>>(TEST_CASE_LIST_API).subscribe({
            next: (customResponse) => this.testCaseStore.updateTestCaseList(customResponse.data),
            error: (error) => this.httpResultHandler.handleError(error),
        });
    }

    createNewTestCase(createTestCase: CreateTestCase) {
        this.http.post<CustomResponse<unknown>>(TEST_CASE_CREATE_API, createTestCase).subscribe({
            next: (customResponse) => {
                this.getTestCaseList();
                this.httpResultHandler.handleSuccess(customResponse);
            },
            error: (error) => this.httpResultHandler.handleError(error),
        });
    }
}
