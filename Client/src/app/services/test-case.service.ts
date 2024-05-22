import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { GET_TEST_CASE_BY_ID_API, TEST_CASE_CREATE_API, TEST_CASE_LIST_API } from '../api/api';
import { CustomResponse } from '../models/custom-response';
import { Subscriptions } from '../models/subcription';
import { CreateTestCase, GetTestCase } from '../models/test-case';
import { GlobalHttpHandler } from '../utils/global-http-handler';
import { TestCaseStore } from './../stores/test-case.store';

@Injectable({
    providedIn: 'root',
})
export class TestCaseService extends Subscriptions {
    // Inject services
    private readonly http = inject(HttpClient);
    private readonly testCaseStore = inject(TestCaseStore);
    private readonly httpResultHandler = inject(GlobalHttpHandler);

    fetchTestCaseList() {
        const sub = this.http.get<CustomResponse<GetTestCase[]>>(TEST_CASE_LIST_API).subscribe({
            next: (customResponse) => this.testCaseStore.setTestCaseList(customResponse.data),
            error: (error) => this.httpResultHandler.handleError(error),
        });

        this.subscriptions.push(sub);
    }

    createNewTestCase(createTestCase: CreateTestCase) {
        const sub = this.http.post<CustomResponse<unknown>>(TEST_CASE_CREATE_API, createTestCase).subscribe({
            next: (customResponse) => {
                this.fetchTestCaseList();
                this.httpResultHandler.handleSuccess(customResponse);
            },
            error: (error) => this.httpResultHandler.handleError(error),
        });

        this.subscriptions.push(sub);
    }

    getTestCaseById(id: number) {
        const sub = this.http.get<CustomResponse<GetTestCase>>(GET_TEST_CASE_BY_ID_API + id).subscribe({
            next: (customResponse) => {
                this.testCaseStore.setTestCase(customResponse.data);
                this.httpResultHandler.handleSuccess(customResponse);
            },
            error: (error) => this.httpResultHandler.handleError(error),
        });

        this.subscriptions.push(sub);
    }
}
