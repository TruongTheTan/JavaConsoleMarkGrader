import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonLayoutComponent } from './layout/common-layout/common-layout.component';
import { TestCaseListComponent } from './pages/admin/test-case/test-case-list/test-case-list.component';
import { UsersComponent } from './pages/admin/user/user-list/users.component';
import { ChangePasswordComponent } from './pages/auth/change-password/change-password.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { StudentComponent } from './pages/student/student.component';
import { TeacherComponent } from './pages/teacher/teacher.component';
import { NotFoundComponent } from './shared/not-found/not-found.component';

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
