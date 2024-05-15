import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthenticationUser, GetUser } from '../models/user';

@Injectable({ providedIn: 'root' })
export class UserStore {
    private readonly userList = new BehaviorSubject([] as GetUser[]);
    private readonly authenticationUser = new BehaviorSubject({} as AuthenticationUser);

    setUser(user: AuthenticationUser) {
        this.authenticationUser.next(user);
    }

    get getUser() {
        return this.authenticationUser.asObservable();
    }

    updateList(list: GetUser[]) {
        this.userList.next(list);
    }

    get getUserList(): Observable<GetUser[]> {
        return this.userList;
    }
}
