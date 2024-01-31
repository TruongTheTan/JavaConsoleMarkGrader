import { StudentService } from './../../../services/student.service';
import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-student',
    templateUrl: './student.component.html',
    styleUrls: ['./student.component.css'],
})
export class StudentComponent {
    file: File | null = null;

    constructor(private studentService: StudentService) {}

    submit() {
        if (this.file == null) alert('Please choose a file');
        else this.studentService.submitFile(this.file!);
    }

    onFileSelected(event: Event): void {
        const inputElement = event.target as HTMLInputElement;
        if (inputElement.files && inputElement.files.length) {
            this.file = inputElement.files[0];
        }
    }
}
