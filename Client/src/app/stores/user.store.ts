import { BehaviorSubject, map } from 'rxjs';
import { GetUser } from '../models/user';
import { Injectable } from '@angular/core';
import { action, computed, makeObservable, observable } from 'mobx';

@Injectable({ providedIn: 'root' })
export class UserStore {
    loggedUserSubject = new BehaviorSubject<GetUser>({} as GetUser);

    setUser(user: GetUser) {
        this.loggedUserSubject.next(user);
    }
}
