import { isPlatformBrowser } from '@angular/common';
import { HttpInterceptorFn } from '@angular/common/http';
import { inject, PLATFORM_ID } from '@angular/core';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const platformId: Object = inject(PLATFORM_ID)
  
  if (isPlatformBrowser(platformId)) {
    const authRequest = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${localStorage.getItem('token')}`),
    })
    
    return next(authRequest)
  }
  
  return next(req);
};
