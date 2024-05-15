import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GetTestCase } from '../models/test-case';

@Injectable({ providedIn: 'root' })
export class TestCaseStore {
    private readonly testCaseList = new BehaviorSubject([] as GetTestCase[]);

    get getTestCaseList(): Observable<GetTestCase[]> {
        return this.testCaseList.asObservable();
    }

    updateTestCaseList(list: GetTestCase[]) {
        this.testCaseList.next(list);
    }
}
