import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangePasswordComponent } from './components/auth/change-password/change-password.component';
import { LoginComponent } from './components/auth/login/login.component';
import { CommonLayoutComponent } from './components/layout/common-layout/common-layout.component';
import { TestCaseListComponent } from './components/roles/admin/test-case/test-case-list/test-case-list.component';
import { UsersComponent } from './components/roles/admin/user/user-list/users.component';
import { StudentComponent } from './components/roles/student/student.component';
import { TeacherComponent } from './components/roles/teacher/teacher.component';
import { NotFoundComponent } from './components/shared/not-found/not-found.component';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'change-password', component: ChangePasswordComponent },

    // Admin routes
    {
        path: 'admin',
        component: CommonLayoutComponent,
        children: [
            { path: '', component: UsersComponent },
            { path: 'test-case', component: TestCaseListComponent },
        ],
    },

    // Student routes
    {
        path: 'student',
        component: CommonLayoutComponent,
        children: [{ path: '', component: StudentComponent }],
    },

    // Teacher routes
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
    { path: '**', component: NotFoundComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
