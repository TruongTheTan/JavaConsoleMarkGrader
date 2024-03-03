import { AdminService } from './../../../../services/admin.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateUser } from 'src/app/models/user';

@Component({
    selector: 'app-create-user',
    templateUrl: './create-user.component.html',
    styleUrls: ['./create-user.component.css'],
})
export class CreateUserComponent {
    //

    createUserForm = new FormGroup({
        username: new FormControl('', Validators.required),
        email: new FormControl('', [
            Validators.required,
            Validators.pattern('^[A-Za-z0-9_.]+@gmail.com$'),
        ]),
        role: new FormControl('', Validators.required),
    });

    constructor(private adminService: AdminService) {}

    submit() {
        if (this.createUserForm.invalid) {
            alert('Invalid info');
        } else {
            this.adminService.createNewUser({
                name: this.createUserForm.value.username!,
                email: this.createUserForm.value.email!,
                role: this.createUserForm.value.role!,
            });
        }
    }

    reset() {
        this.createUserForm.reset();
    }
}
