import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { GetTestCase } from 'src/app/models/test-case';
import { TestCaseStore } from 'src/app/stores/test-case.store';
import { TestCaseService } from '../../../../services/test-case.service';

@Component({
    selector: 'app-test-case-list',
    templateUrl: './test-case-list.component.html',
    styleUrls: ['./test-case-list.component.css'],
})
export class TestCaseListComponent {
    testCaseList = {} as Observable<GetTestCase[]>;

    //
    private readonly testCaseStore = inject(TestCaseStore);
    private readonly testCaseService = inject(TestCaseService);

    constructor() {
        this.testCaseService.getTestCaseList();
        this.testCaseList = this.testCaseStore.getTestCaseList;
    }
}
