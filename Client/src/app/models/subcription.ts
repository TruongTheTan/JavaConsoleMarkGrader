import { Subscription } from 'rxjs';

export abstract class Subscriptions {
    protected readonly subscriptions = [] as Subscription[];

    unsubscribeAll() {
        this.subscriptions.forEach((subscription) => subscription.unsubscribe());
        this.subscriptions.splice(0, this.subscriptions.length);
    }
}
