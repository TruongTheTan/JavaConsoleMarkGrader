import { Component, inject } from '@angular/core';
import { GetTestCase } from 'src/app/models/test-case';
import { TestCaseStore } from 'src/app/stores/test-case.store';
import { TestCaseService } from './../../../../../services/test-case.service';

@Component({
    selector: 'app-test-case-list',
    templateUrl: './test-case-list.component.html',
    styleUrls: ['./test-case-list.component.css'],
})
export class TestCaseListComponent {
    testCaseList = [] as GetTestCase[];

    //
    private readonly testCaseService = inject(TestCaseService);
    private readonly testCaseStore = inject(TestCaseStore);

    constructor() {
        this.testCaseService.getTestCaseList();
        this.testCaseStore.getTestCaseList().subscribe((list) => (this.testCaseList = list));
    }
}
