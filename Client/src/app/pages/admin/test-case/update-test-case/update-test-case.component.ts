import { Component, effect, Input, OnChanges, signal, SimpleChanges } from '@angular/core';

@Component({
    selector: 'app-update-test-case',
    templateUrl: './update-test-case.component.html',
    styleUrls: ['./update-test-case.component.css'],
})
export class UpdateTestCaseComponent implements OnChanges {
    @Input() testCaseId = signal(0);
    @Input() a = '';

    /**
     *
     */
    constructor() {
        this.fetchTestCaseById();
    }
    ngOnChanges(changes: SimpleChanges): void {
        console.log(this.a);
    }

    fetchTestCaseById() {
        effect(() => {
            console.log(11);
            if (this.testCaseId() === 1) {
                console.log(this.testCaseId());
            }
        });
    }
}
