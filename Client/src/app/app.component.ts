import { Component, effect, signal } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { GlobalHttpHandler } from './utils/global-http-handler';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent {
    private responseType = true;
    private readonly message = signal('');

    constructor(private toast: NgToastService, private globalHttpHandler: GlobalHttpHandler) {
        this.setupErrorMessage();
        this.displayToast();
    }

    private setupErrorMessage() {
        this.globalHttpHandler.getResponseType.subscribe((type) => (this.responseType = type));
        this.globalHttpHandler.getErrorMessage.subscribe((message) => this.message.set(message));
    }

    private displayToast() {
        effect(() => {
            if (this.message().trim()) {
                this.showToastMessage();

                // Remove error message
                setTimeout(() => {
                    this.message.set('');
                }, 3200);
            }
        });
    }

    private showToastMessage() {
        if (this.responseType) {
            this.toast.success({
                detail: 'Success',
                summary: this.message(),
                duration: 3000,
            });
        } else {
            this.toast.error({ detail: 'Error', summary: this.message(), duration: 3000 });
        }
    }
}
