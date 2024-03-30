import { ChangePasswordComponent } from './components/auth/change-password/change-password.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/auth/login/login.component';
import { StudentComponent } from './components/roles/student/student.component';
import { TeacherComponent } from './components/roles/teacher/teacher.component';
import { UsersComponent } from './components/roles/admin/user/user-list/users.component';
import { CommonLayoutComponent } from './components/layout/common-layout/common-layout.component';
import { TestCaseComponent } from './components/roles/admin/test-case/test-case.component';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'change-password', component: ChangePasswordComponent },
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
