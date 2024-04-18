import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthenticationUser, GetUser } from '../models/user';

@Injectable({ providedIn: 'root' })
export class UserStore {
    private userList = new BehaviorSubject([] as GetUser[]);
    private authenticationUser = new BehaviorSubject({} as AuthenticationUser);

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
}
