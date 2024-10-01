import { HttpClient, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, retry, switchMap, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const errorsInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService)

  return next(req).pipe(
    catchError(error => {
      switch (error.status){
        case 401:
          return authService.refreshToken().pipe(
            switchMap(token => {
              localStorage.setItem('token', token)  

              return next(req)
            }),
            catchError(error => {
              console.log(error)
              // fix

              throw error
            })
          )
        
        default:
          throw error
      }
    })
  );
};
