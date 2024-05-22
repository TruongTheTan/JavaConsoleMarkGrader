import { GoogleSigninButtonModule, SocialLoginModule } from '@abacritt/angularx-social-login';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgToastModule } from 'ng-angular-popup';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CookieService } from 'ngx-cookie-service';
import { interceptorConfig } from './api/http-interceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { TableComponent } from './components/table/table.component';
import { CommonLayoutComponent } from './layout/common-layout/common-layout.component';
import { CreateTestCaseComponent } from './pages/admin/test-case/create-test-case/create-test-case.component';
import { TestCaseListComponent } from './pages/admin/test-case/test-case-list/test-case-list.component';
import { CreateUserComponent } from './pages/admin/user/create-user/create-user.component';
import { UsersComponent } from './pages/admin/user/user-list/users.component';
import { ChangePasswordComponent } from './pages/auth/change-password/change-password.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { StudentComponent } from './pages/student/student.component';
import { TeacherComponent } from './pages/teacher/teacher.component';
import { AdminService } from './services/admin.service';
import { AuthService } from './services/auth.service';
import { StudentService } from './services/student.service';
import { TestCaseService } from './services/test-case.service';
import { SemesterStore } from './stores/semester.store';
import { TestCaseStore } from './stores/test-case.store';
import { UserStore } from './stores/user.store';
import { GlobalHttpHandler } from './utils/global-http-handler';
import socialAuthProvider from './utils/social-auth-provider';
import { UpdateTestCaseComponent } from './pages/admin/test-case/update-test-case/update-test-case.component';

// Add stores here
const storesProvider = [UserStore, TestCaseStore, SemesterStore];

// Add services here
const servicesProvider = [AdminService, AuthService, StudentService, GlobalHttpHandler, TestCaseService];

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
        TableComponent,
        UpdateTestCaseComponent,
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
        BrowserAnimationsModule,
        BsDropdownModule,
    ],
    providers: [servicesProvider, storesProvider, socialAuthProvider, CookieService, interceptorConfig],
    bootstrap: [AppComponent],
})
export class AppModule {}
