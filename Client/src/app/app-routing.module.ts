import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { CommonLayoutComponent } from './layout/common-layout/common-layout.component';
import { TestCaseListComponent } from './pages/admin/test-case/test-case-list/test-case-list.component';
import { UsersComponent } from './pages/admin/user/user-list/users.component';
import { ChangePasswordComponent } from './pages/auth/change-password/change-password.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { StudentComponent } from './pages/student/student.component';
import { TeacherComponent } from './pages/teacher/teacher.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/login',
        pathMatch: 'full',
    },
    { path: 'login', component: LoginComponent },
    { path: 'change-password', component: ChangePasswordComponent },
    {
        path: 'app',
        component: CommonLayoutComponent,
        children: [
            {
                path: 'admin',
                children: [
                    { path: 'user', component: UsersComponent },
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
        ],
    },
    // Admin routes
    // {
    //     path: 'admin',
    //     component: CommonLayoutComponent,
    //     children: [
    //         { path: '', component: UsersComponent },
    //         { path: 'test-case', component: TestCaseListComponent },
    //     ],
    // },

    // // Student routes
    // {
    //     path: 'student',
    //     component: CommonLayoutComponent,
    //     children: [{ path: '', component: StudentComponent }],
    // },

    // // Teacher routes
    // {
    //     path: 'teacher',
    //     component: CommonLayoutComponent,
    //     children: [{ path: '', component: TeacherComponent }],
    // },
    { path: '**', component: NotFoundComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
