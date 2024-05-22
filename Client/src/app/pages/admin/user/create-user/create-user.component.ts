import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AdminService } from '../../../../services/admin.service';

@Component({
    selector: 'app-create-user',
    templateUrl: './create-user.component.html',
    styleUrls: ['./create-user.component.css'],
})
export class CreateUserComponent {
    //
    private adminService = inject(AdminService);
    private formBuilder = inject(FormBuilder);

    // Form
    readonly createUserForm = this.formBuilder.group({
        username: ['', Validators.required],
        email: ['', [Validators.required, Validators.pattern('^[A-Za-z0-9_.]+@gmail.com$')]],
        role: ['', [Validators.required, Validators.pattern('^(Admin|Teacher|Student)$')]],
    });

    submit() {
        if (this.createUserForm.valid) {
            const { email, username, role } = this.createUserForm.value;
            this.adminService.createNewUser({
                email: email!.trim(),
                username: username!.trim(),
                role: role!.trim(),
            });
        }
    }

    reset() {
        this.createUserForm.reset();
    }
}
