import {
    GoogleLoginProvider,
    GoogleSigninButtonModule,
    SocialAuthServiceConfig,
    SocialLoginModule,
} from '@abacritt/angularx-social-login';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgToastModule } from 'ng-angular-popup';
import { CookieService } from 'ngx-cookie-service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ChangePasswordComponent } from './components/auth/change-password/change-password.component';
import { LoginComponent } from './components/auth/login/login.component';
import { CommonLayoutComponent } from './components/layout/common-layout/common-layout.component';
import { CreateTestCaseComponent } from './components/roles/admin/test-case/create-test-case/create-test-case.component';
import { TestCaseListComponent } from './components/roles/admin/test-case/test-case-list/test-case-list.component';
import { CreateUserComponent } from './components/roles/admin/user/create-user/create-user.component';
import { UsersComponent } from './components/roles/admin/user/user-list/users.component';
import { StudentComponent } from './components/roles/student/student.component';
import { TeacherComponent } from './components/roles/teacher/teacher.component';
import { NotFoundComponent } from './components/shared/not-found/not-found.component';
import { AdminService } from './services/admin.service';
import { AuthService } from './services/auth.service';
import { StudentService } from './services/student.service';
import { TestCaseService } from './services/test-case.service';
import { SemesterStore } from './stores/semester.store';
import { TestCaseStore } from './stores/test-case.store';
import { UserStore } from './stores/user.store';
import { GlobalHttpHandler } from './utils/global-http-handler';
import { interceptorConfig } from './utils/http-interceptor';

// Add services here
const servicesProvider = [
    AdminService,
    AuthService,
    StudentService,
    GlobalHttpHandler,
    TestCaseService,
];

// Add stores here
const storesProvider = [UserStore, TestCaseStore, SemesterStore];

const clientId = '256438874185-qp91u851or88s8plr1p8ku8nv28vp0jh.apps.googleusercontent.com';
const socialAuthProvider = {
    provide: 'SocialAuthServiceConfig',
    useValue: {
        autoLogin: true,
        providers: [
            {
                id: GoogleLoginProvider.PROVIDER_ID,
                provider: new GoogleLoginProvider(clientId, {
                    oneTapEnabled: true,
                    prompt: 'select_account',
                }),
            },
        ],
        onError: (err) => {
            console.error(err);
        },
    } as SocialAuthServiceConfig,
};

@NgModule({
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    declarations: [
        AppComponent,
        LoginComponent,
        StudentComponent,
        TeacherComponent,
        UsersComponent,
        CommonLayoutComponent,
        CreateUserComponent,
        ChangePasswordComponent,
        TestCaseListComponent,
        CreateTestCaseComponent,
        NotFoundComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        SocialLoginModule,
        GoogleSigninButtonModule,
        NgToastModule,
    ],
    providers: [
        servicesProvider,
        storesProvider,
        socialAuthProvider,
        CookieService,
        interceptorConfig,
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
