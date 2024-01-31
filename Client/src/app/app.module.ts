import { Injectable, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {
    HTTP_INTERCEPTORS,
    HttpClientModule,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { Observable } from 'rxjs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { UserStore } from './stores/user.store';
import { StudentComponent } from './components/user/student/student.component';
import { TeacherComponent } from './components/user/teacher/teacher.component';
import { AdminComponent } from './components/user/admin/admin.component';
import { StudentService } from './services/student.service';

@Injectable()
export class InterceptorConfig implements HttpInterceptor {
    constructor(private cookieService: CookieService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const jwtToken = this.cookieService.get('token');

        const modifiedRequest = request.clone({
            url: `https://localhost:7254/api/${request.url}`,
            headers: request.headers.set('Authorization', `Bearer ${jwtToken}`),
        });

        return next.handle(modifiedRequest);
    }
}

// Add services here
const servicesProvider = [AuthService, StudentService];

// Add stores here
const storesProvider = [UserStore];

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        StudentComponent,
        TeacherComponent,
        AdminComponent,
    ],
    imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule, ReactiveFormsModule],
    providers: [
        ...servicesProvider,
        ...storesProvider,
        CookieService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: InterceptorConfig,
            multi: true,
        },
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
