import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { CustomFormValidation } from 'src/app/utils/custom-form-validation';

@Component({
    selector: 'app-change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.css'],
})
export class ChangePasswordComponent {
    changePasswordForm = new FormGroup(
        {
            email: new FormControl('', [
                Validators.required,
                Validators.pattern('^[A-Za-z0-9_.]+@gmail.com$'),
            ]),
            oldPassword: new FormControl('', Validators.required),
            newPassword: new FormControl('', Validators.required),
            confirmPassword: new FormControl('', Validators.required),
        },
        {
            validators: [CustomFormValidation.match('newPassword', 'confirmPassword')],
        }
    );

    constructor(private authService: AuthService) {}

    submitForm() {
        if (!this.changePasswordForm.invalid) {
            const { email, oldPassword, newPassword } = this.changePasswordForm.controls;

            this.authService.changePassword(email.value!, oldPassword.value!, newPassword.value!);
        }
    }
}
