import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { UserStore } from './stores/user.store';
import { StudentComponent } from './components/student/student.component';
import { TeacherComponent } from './components/teacher/teacher.component';
import { StudentService } from './services/student.service';
import { UsersComponent } from './components/admin/user/user-list/users.component';
import { ModalComponent } from './components/modal/modal.component';
import { GlobalErrorHandler } from './utils/global-error-handler';
import { TestCaseComponent } from './components/admin/test-case/test-case.component';
import { CommonLayoutComponent } from './components/layout/common-layout/common-layout.component';
import { CreateUserComponent } from './components/admin/user/create-user/create-user.component';
import { AdminService } from './services/admin.service';
import {
    SocialLoginModule,
    SocialAuthServiceConfig,
    GoogleLoginProvider,
    GoogleSigninButtonModule,
} from '@abacritt/angularx-social-login';
import { interceptorConfig } from './utils/http-interceptor';

// Add services here
const servicesProvider = [AdminService, AuthService, StudentService, GlobalErrorHandler];

// Add stores here
const storesProvider = [UserStore];

const SocialAuthProvider = {
    provide: 'SocialAuthServiceConfig',
    useValue: {
        autoLogin: false,
        providers: [
            {
                id: GoogleLoginProvider.PROVIDER_ID,
                provider: new GoogleLoginProvider(
                    '256438874185-qp91u851or88s8plr1p8ku8nv28vp0jh.apps.googleusercontent.com'
                ),
            },
        ],
    } as SocialAuthServiceConfig,
};

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        StudentComponent,
        TeacherComponent,
        UsersComponent,
        ModalComponent,
        TestCaseComponent,
        CommonLayoutComponent,
        CreateUserComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        SocialLoginModule,
        GoogleSigninButtonModule,
    ],
    providers: [
        ...servicesProvider,
        ...storesProvider,
        SocialAuthProvider,
        CookieService,
        interceptorConfig,
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
