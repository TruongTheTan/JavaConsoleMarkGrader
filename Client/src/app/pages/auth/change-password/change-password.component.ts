import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { CustomFormValidation } from 'src/app/utils/custom-form-validation';

@Component({
    selector: 'app-change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.css'],
})
export class ChangePasswordComponent {
    // Form
    readonly changePasswordForm = this.formBuilder.group(
        {
            email: ['', [Validators.required, Validators.pattern('^[A-Za-z0-9_.]+@gmail.com$')]],
            oldPassword: ['', Validators.required],
            newPassword: ['', Validators.required],
            confirmPassword: ['', Validators.required],
        },
        {
            validators: [CustomFormValidation.match('newPassword', 'confirmPassword')],
        }
    );

    constructor(private authService: AuthService, private formBuilder: FormBuilder) {}

    submitForm() {
        if (this.changePasswordForm.valid) {
            const { email, oldPassword, newPassword } = this.changePasswordForm.controls;
            this.authService.changePassword(email.value!, oldPassword.value!, newPassword.value!);
        }
    }
}
