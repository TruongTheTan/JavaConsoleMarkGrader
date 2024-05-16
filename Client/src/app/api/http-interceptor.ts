import {
    HTTP_INTERCEPTORS,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';

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

export const interceptorConfig = {
    provide: HTTP_INTERCEPTORS,
    useClass: InterceptorConfig,
    multi: true,
};
