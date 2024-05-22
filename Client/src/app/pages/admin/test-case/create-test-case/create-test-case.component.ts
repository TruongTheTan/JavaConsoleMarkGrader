import { Component, effect, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { GetSemester } from 'src/app/models/semester';
import { SemesterService } from 'src/app/services/semester.service';
import { SemesterStore } from 'src/app/stores/semester.store';
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
    private readonly semesterStore = inject(SemesterStore);
    private readonly testCaseService = inject(TestCaseService);
    private readonly semesterService = inject(SemesterService);

    semesterList = [] as GetSemester[];
    readonly isInputByMultipleLine = signal(false); // For display or remove test case input fields

    // Form
    readonly createTestCaseForm = this.formBuilder.group({
        isInputByLine: [false],
        testCaseInputs: this.formBuilder.array<FormControl[]>([]),
        testCaseOutputs: this.formBuilder.array<FormControl[]>([]),

        mark: [0, [Validators.required, Validators.min(1), Validators.max(10), Validators.pattern('^[1-9]+$')]],
        semester: [0, [Validators.required]],
    });

    constructor() {
        this.addTestCaseInputField();
        this.addTestCaseOutputField();
        this.clearAllTestCaseInputsByMultipleLineIsFalse();

        this.semesterService.getSemesterList();
        this.semesterStore.getSemesterList
            .pipe(takeUntilDestroyed())
            .subscribe((data) => (this.semesterList = data));
    }

    submitForm() {
        if (this.createTestCaseForm.valid) {
            const result = window.confirm('Are you sure you want to create ?');

            if (result) {
                const { mark, isInputByLine, semester } = this.createTestCaseForm.controls;

                const semeserId = semester.value as number;

                const createTestCase = {
                    mark: mark.value,
                    isInputByLine: isInputByLine.value,
                    semesterId: semeserId === 0 ? this.semesterList[0].id : +semeserId,
                    inputs: this.convertInputTestCaseFormControlToStringArray(),
                    outputs: this.convertOutputTestCaseFormControlToStringArray(),
                } as CreateTestCase;

                this.testCaseService.createNewTestCase(createTestCase);
                this.createTestCaseForm.reset();
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
