import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpResponse,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import {
    delay,
    mergeMap,
    materialize,
    dematerialize
} from 'rxjs/operators';
import { User, Role } from '../_models';

/* The fake backend provider enables the example to run without a backend / backendless,
 it contains a hardcoded collection of users and provides fake implementations for the api endpoints
  "authenticate", "get user by id", and "get all users", */

@Injectable()
export class FakeBackendInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const users: User[] = [
            { id: 1, username: 'admin@mail.com', password: 'admin', firstName: 'Admin', lastName: 'User', role: Role.Admin },
            { id: 2, username: 'user@mail.com', password: 'user', firstName: 'Jenny', lastName: 'Long', role: Role.User }
        ];

        const authHeader = request.headers.get('Authorization');
        const isLoggedIn = authHeader && authHeader.startsWith('Bearer fake-jwt-token');
        const roleString = isLoggedIn && authHeader.split('.')[1];
        const role = roleString ? Role[roleString] : null;

        // wrap in delayed observable to simulate server api call.
        return of(null).pipe(mergeMap(() => {

            // Authenticate - allowAnonymous
            if (request.url.endsWith('/users/authenticate') && request.method === 'POST') {
                // get the user from the request body (username;password;)
                const user = users.find(
                    u => u.username === request.body.username &&
                        u.password === request.body.password
                );

                if (!user) { return error('Username / password is incorrect'); }
                return ok({
                    id: user.id,
                    username: user.username,
                    firstName: user.firstName,
                    lastName: user.lastName,
                    role: user.role,
                    token: `fake-jwt-token.${user.role}`
                });
            }

            // get user by id - admin or user (normal user can only access their own record)
            if (request.url.match(/\/users\/\d+$/) && request.method === 'GET') {
                if (!isLoggedIn) { return unauthorised(); }

                // get id from request url
                const urlParts = request.url.split('/');
                const id = parseInt(urlParts[urlParts.length - 1]);

                // allow normal users to access their own record.
                const currentUser = users.find(u => u.role === role);
                if (id !== currentUser.id && role !== Role.Admin) { return unauthorised(); }

                const user = users.find(u => u.id === id);
                return ok(user);

            }

            // get all users (admin only)
            if (request.url.endsWith('/users') && request.method === 'GET') {
                if (role !== Role.Admin) { return unauthorised(); }
                return ok(users);
            }

            // pass through any requests not handled above
            return next.handle(request);
        }))
            // tslint:disable-next-line: max-line-length
            // call materialize and dematerialize to ensure delay even if an error is thrown (https://github.com/Reactive-Extensions/RxJS/issues/648)
            .pipe(materialize())
            .pipe(delay(500))
            .pipe(dematerialize());

        // private helper functions

        function ok(body) {
            return of(new HttpResponse({ status: 200, body }));
        }

        function unauthorised() {
            return throwError({ status: 401, error: { message: 'Unauthorised' } });
        }

        function error(message) {
            return throwError({ status: 400, error: { message } });
        }
    }
}

export let fakeBackendProvider = {
    // use fake backend in place of an http api for backend-less development
    provide: HTTP_INTERCEPTORS,
    useClass: FakeBackendInterceptor,
    multi: true
};

