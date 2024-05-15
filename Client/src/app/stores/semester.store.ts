import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GetSemester } from '../models/semester';

@Injectable({ providedIn: 'root' })
export class SemesterStore {
    private readonly semester = new BehaviorSubject({} as GetSemester);
    private readonly semesterList = new BehaviorSubject([] as GetSemester[]);

    get getSemesterList(): Observable<GetSemester[]> {
        return this.semesterList.asObservable();
    }

    updateSemesterList(semesterList: GetSemester[]) {
        this.semesterList.next(semesterList);
    }

    get getSemester(): Observable<GetSemester> {
        return this.semester.asObservable();
    }

    updateSemester(semester: GetSemester) {
        this.semester.next(semester);
    }
}
