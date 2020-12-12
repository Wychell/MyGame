import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NotificacaService } from './notificaca.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router, private notificacaService: NotificacaService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(catchError(err => {
                if (err.status === 401) {
                    this.router.navigate(['login']);
                    return;
                }
                if (err.status == 400 && err.error.messages) {
                    this.notificacaService.showError(err.error.messages.map(x => x.message).join(', '), "Critica");
                    return
                }

                const error = err?.error?.message || err.statusText;
                this.notificacaService.showError(error !== "OK" ? error : "erro ao realizar request", "Erro");

                return throwError(error ?? "erro ao realizar request");
            }))
    }
}