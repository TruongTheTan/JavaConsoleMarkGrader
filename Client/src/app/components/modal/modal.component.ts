import { Component, effect, OnInit, signal } from '@angular/core';
import { GlobalErrorHandler } from 'src/app/utils/global-error-handler';

@Component({
    selector: 'app-modal',
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.css'],
})
export class ModalComponent implements OnInit {
    message = signal('');

    private openModalButton = {} as HTMLButtonElement | null;
    private closeModalButton = {} as HTMLButtonElement | null;

    constructor(private errorHander: GlobalErrorHandler) {
        this.showDialog();
    }

    ngOnInit(): void {
        this.errorHander.getErrorMessage().subscribe((message) => this.message.set(message));
        this.openModalButton = document.getElementById('openModalButton') as HTMLButtonElement;
        this.closeModalButton = document.getElementById('closeModalButton') as HTMLButtonElement;
    }

    showDialog() {
        effect(() => {
            if (this.message()) {
                this.openModalButton?.click();

                setTimeout(() => {
                    this.message.set('');
                    this.closeModalButton?.click();
                }, 2300);
            }
        });
    }
}
