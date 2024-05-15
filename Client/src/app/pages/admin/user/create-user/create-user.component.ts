import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../../../../services/admin.service';

@Component({
    selector: 'app-create-user',
    templateUrl: './create-user.component.html',
    styleUrls: ['./create-user.component.css'],
})
export class CreateUserComponent {
    //
    private adminService = inject(AdminService);

    // Form
    readonly createUserForm = new FormGroup({
        username: new FormControl('', Validators.required),
        email: new FormControl('', [
            Validators.required,
            Validators.pattern('^[A-Za-z0-9_.]+@gmail.com$'),
        ]),
        role: new FormControl('', [
            Validators.required,
            Validators.pattern('^(Admin|Teacher|Student)$'),
        ]),
    });

    submit() {
        if (this.createUserForm.valid) {
            const { email, username, role } = this.createUserForm.value;
            this.adminService.createNewUser({ email: email!, username: username!, role: role! });
        }
    }

    reset() {
        this.createUserForm.reset();
    }
}