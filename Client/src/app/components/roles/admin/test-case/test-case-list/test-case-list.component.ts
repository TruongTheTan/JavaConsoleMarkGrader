import { Component } from '@angular/core';
import { GetTestCase } from 'src/app/models/test-case';

@Component({
    selector: 'app-test-case-list',
    templateUrl: './test-case-list.component.html',
    styleUrls: ['./test-case-list.component.css'],
})
export class TestCaseListComponent {
    testCaseList = [
        { id: 1, inputs: ['as', 'as', 'ass'], outputs: ['3'], mark: 0, semesterName: 'Spring2024' },
    ] as GetTestCase[];
}
