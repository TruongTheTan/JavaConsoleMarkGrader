import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthenticationUser, GetUser } from '../models/user';

@Injectable({ providedIn: 'root' })
export class UserStore {
    private readonly userList = new BehaviorSubject([] as GetUser[]);
    private readonly authenticationUser = new BehaviorSubject({} as AuthenticationUser);

    private readonly a = new BehaviorSubject(10);

    setUser(user: AuthenticationUser) {
        this.authenticationUser.next(user);
    }

    getUser() {
        return this.authenticationUser.asObservable();
    }

    updateList(list: GetUser[]) {
        this.userList.next(list);
    }

    getUserList(): Observable<GetUser[]> {
        return this.userList;
    }

    getA() {
        return this.a.getValue();
    }

    getGGG() {
        return this.a.asObservable();
    }

    addA() {
        this.a.getValue();
    }
}
