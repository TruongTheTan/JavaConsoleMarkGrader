import { Component, Injectable, signal } from '@angular/core';
import { GlobalErrorHandler } from 'src/app/utils/global-error-handler';

@Component({
    selector: 'app-modal',
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.css'],
})
export class ModalComponent {
    message = '';

    constructor(private errorHander: GlobalErrorHandler) {}
}
