import { Component, effect, inject, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomFormValidation } from 'src/app/utils/custom-form-validation';
import { CreateTestCase } from '../../../../models/test-case';
import { TestCaseService } from '../../../../services/test-case.service';

@Component({
    selector: 'app-create-test-case',
    templateUrl: './create-test-case.component.html',
    styleUrls: ['./create-test-case.component.css'],
})
export class CreateTestCaseComponent {
    // Injection
    private readonly formBuilder = inject(FormBuilder);
    private readonly testCaseService = inject(TestCaseService);

    readonly isInputByMultipleLine = signal(false); // For display or remove test case input fields
    readonly semesterList = ['Spring2024', 'Spring2025', 'Spring2026'];

    // Form
    readonly createTestCaseForm = new FormGroup({
        isInputByLine: new FormControl(false),
        testCaseInputs: this.formBuilder.array<FormControl[]>([]),
        testCaseOutputs: this.formBuilder.array<FormControl[]>([]),

        mark: new FormControl(0, [
            Validators.required,
            Validators.min(1),
            Validators.max(10),
            Validators.pattern('^[1-9]+$'),
        ]),

        semester: new FormControl(0, [
            Validators.required,
            Validators.pattern(`^(${this.semesterList.join('|')})$`),
        ]),
    });

    constructor() {
        this.addTestCaseInputField();
        this.addTestCaseOutputField();
        this.clearAllTestCaseInputsByMultipleLineIsFalse();
    }

    submitForm() {
        if (this.createTestCaseForm.valid) {
            const result = window.confirm('Are you sure you want to create ?');

            if (result) {
                const { mark, isInputByLine, semester } = this.createTestCaseForm.controls;

                const createTestCase = {
                    mark: mark.value,
                    isInputByLine: isInputByLine.value,
                    semesterId: semester.value,
                    inputs: this.convertInputTestCaseFormControlToStringArray(),
                    outputs: this.convertOutputTestCaseFormControlToStringArray(),
                } as CreateTestCase;

                this.testCaseService.createNewTestCase(createTestCase);
            }
        }
    }

    convertInputTestCaseFormControlToStringArray(): string[] {
        const testCaseInputStrings = [] as string[];
        const inputControls = this.createTestCaseForm.controls.testCaseInputs.controls;

        for (const formControl of inputControls) {
            testCaseInputStrings.push(formControl.value);
        }
        return testCaseInputStrings;
    }

    convertOutputTestCaseFormControlToStringArray(): string[] {
        const testCaseOutputStrings = [] as string[];
        const outputControls = this.createTestCaseForm.controls.testCaseOutputs.controls;

        for (const formControl of outputControls) {
            testCaseOutputStrings.push(formControl.value);
        }
        return testCaseOutputStrings;
    }

    addTestCaseInputField() {
        const newFormControl = this.formBuilder.control('', [
            Validators.required,
            CustomFormValidation.checkEmptyString(),
        ]);
        this.createTestCaseForm.controls.testCaseInputs.push(newFormControl);
    }

    addTestCaseOutputField() {
        const newFormControl = this.formBuilder.control('', [
            Validators.required,
            CustomFormValidation.checkEmptyString(),
        ]);
        this.createTestCaseForm.controls.testCaseOutputs.push(newFormControl);
    }

    deleteTestCaseInputField(inputFieldIndex: number) {
        if (inputFieldIndex !== 0) {
            this.createTestCaseForm.controls.testCaseInputs.removeAt(inputFieldIndex);
        }
    }

    deleteTestCaseOutputField(outputFieldIndex: number) {
        if (outputFieldIndex !== 0) {
            this.createTestCaseForm.controls.testCaseOutputs.removeAt(outputFieldIndex);
        }
    }

    clearAllTestCaseInputsByMultipleLineIsFalse() {
        effect(() => {
            if (this.isInputByMultipleLine() == false) {
                this.createTestCaseForm.controls.testCaseInputs.controls.splice(1);
            }
        });
    }
}
