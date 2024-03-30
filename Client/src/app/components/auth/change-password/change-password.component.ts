import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.css'],
})
export class ChangePasswordComponent {
    changePasswordForm = new FormGroup({
        email: new FormControl('', [
            Validators.required,
            Validators.pattern('^[A-Za-z0-9_.]+@gmail.com$'),
        ]),
        oldPassword: new FormControl('', Validators.required),
        newPassword: new FormControl('', Validators.required),
        ReEnterNewpassword: new FormControl('', Validators.required),
    });

    constructor(private authService: AuthService) {}

    submitForm() {
        if (this.changePasswordForm.invalid) {
            alert('Must fill in email and password');
        } else {
            const newPassword = this.changePasswordForm.controls.newPassword.value!;
            const confirmPassword = this.changePasswordForm.controls.ReEnterNewpassword.value!;

            if (newPassword !== confirmPassword) {
                alert('new password and confirm password are not equal');
            } else {
                const email = this.changePasswordForm.controls.email.value!;
                const oldPassword = this.changePasswordForm.controls.oldPassword.value!;

                this.authService.changePassword(email, oldPassword, newPassword);
            }
        }
    }
}
