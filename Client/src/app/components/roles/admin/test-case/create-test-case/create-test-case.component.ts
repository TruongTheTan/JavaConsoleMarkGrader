import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-create-test-case',
    templateUrl: './create-test-case.component.html',
    styleUrls: ['./create-test-case.component.css'],
})
export class CreateTestCaseComponent {
    readonly semesterList = ['Spring2024', 'Spring2025', 'Spring2026'];

    readonly createTestCaseForm = new FormGroup({
        username: new FormControl('', Validators.required),

        email: new FormControl('', [
            Validators.required,
            Validators.pattern('^[A-Za-z0-9_.]+@gmail.com$'),
        ]),

        mark: new FormControl('', [
            Validators.required,
            Validators.min(1),
            Validators.max(10),
            Validators.pattern('^[1-9]+$'),
        ]),

        semester: new FormControl('', [
            Validators.required,
            Validators.pattern(`^(${this.semesterList.join('|')})$`),
        ]),
    });

    constructor() {}

    submit() {
        if (this.createTestCaseForm.invalid) {
            alert('Invalid info');
        } else {
        }
    }

    reset() {
        this.createTestCaseForm.reset();
    }
}
