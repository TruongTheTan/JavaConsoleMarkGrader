import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { GetTestCase } from '../models/test-case';

@Injectable({ providedIn: 'root' })
export class TestCaseStore {
    private readonly testCaseList = new BehaviorSubject([] as GetTestCase[]);
    private readonly testCase = new BehaviorSubject({} as GetTestCase);

    get getTestCaseList() {
        return this.testCaseList.asObservable();
    }

    get getTestCase() {
        return this.testCase.asObservable();
    }

    setTestCaseList(list: GetTestCase[]) {
        this.testCaseList.next(list);
    }

    setTestCase(testCase: GetTestCase) {
        this.testCase.next(testCase);
    }
}
