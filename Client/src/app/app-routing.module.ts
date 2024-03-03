import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { StudentComponent } from './components/student/student.component';
import { TeacherComponent } from './components/teacher/teacher.component';
import { UsersComponent } from './components/admin/user/user-list/users.component';
import { CommonLayoutComponent } from './components/layout/common-layout/common-layout.component';
import { TestCaseComponent } from './components/admin/test-case/test-case.component';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    {
        path: 'admin',
        component: CommonLayoutComponent,
        children: [
            { path: '', component: UsersComponent },
            { path: 'test-case', component: TestCaseComponent },
        ],
    },
    {
        path: 'student',
        component: CommonLayoutComponent,
        children: [{ path: '', component: StudentComponent }],
    },
    {
        path: 'teacher',
        component: CommonLayoutComponent,
        children: [{ path: '', component: TeacherComponent }],
    },
    {
        path: '',
        redirectTo: '/login',
        pathMatch: 'full',
    },
    { path: '**', redirectTo: '' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
