import {
    Component,
    effect,
    Injectable,
    OnChanges,
    OnInit,
    signal,
    SimpleChanges,
} from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { GlobalErrorHandler } from 'src/app/utils/global-error-handler';

@Component({
    selector: 'app-modal',
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.css'],
})
export class ModalComponent implements OnInit {
    message = signal('');

    constructor(private errorHander: GlobalErrorHandler) {
        this.showDialog();
    }

    ngOnInit(): void {
        this.errorHander.getUser().subscribe((message) => this.message.set(message));
        //this.showDialog();
    }

    showDialog() {
        effect(() => {
            if (this.message()) {
                const button = document.getElementById('button') as HTMLButtonElement;
                button.click();

                setTimeout(() => this.message.set(''), 3000);
            }
        });
    }
}
