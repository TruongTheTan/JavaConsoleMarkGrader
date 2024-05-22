import { Component, inject, OnDestroy, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { GetTestCase } from 'src/app/models/test-case';
import { TestCaseStore } from 'src/app/stores/test-case.store';
import { TestCaseService } from '../../../../services/test-case.service';

@Component({
    selector: 'app-test-case-list',
    templateUrl: './test-case-list.component.html',
    styleUrls: ['./test-case-list.component.css'],
})
export class TestCaseListComponent implements OnDestroy {
    readonly testCaseId = signal(0);
    testCaseList = {} as Observable<GetTestCase[]>;

    ss = '';
    readonly testCaseKeys = [
        'inputs',
        'outputs',
        'mark',
        'semesterName',
        'isInputByLine',
    ] as (keyof GetTestCase)[];

    readonly testCaseListHeaders = [
        'Test Case Inputs',
        'Test Case Outputs',
        'Mark',
        'Semester',
        'Is input by line',
    ];

    //
    private readonly testCaseStore = inject(TestCaseStore);
    private readonly testCaseService = inject(TestCaseService);

    constructor() {
        this.testCaseService.fetchTestCaseList();
        this.testCaseList = this.testCaseStore.getTestCaseList;
    }

    ngOnDestroy(): void {
        this.testCaseService.unsubscribeAll();
    }

    onTableButtonClick(event: { data: GetTestCase; eventName: string }) {
        if (event.eventName === 'detail') {
            this.testCaseId.set(event.data.id);
            this.ss = this.generateRandomString(5);
        } else {
            const result = window.confirm('Are you sure you want to delete this test case?');

            if (result) {
            }
        }
    }

    generateRandomString(length: number) {
        const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        let result = '';
        for (let i = 0; i < length; i++) {
            result += characters.charAt(Math.floor(Math.random() * characters.length));
        }
        return result;
    }
}
