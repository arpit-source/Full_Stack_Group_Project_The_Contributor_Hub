import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const userStr = localStorage.getItem('currentUser');
  if (userStr) {
    const user = JSON.parse(userStr);
    req = req.clone({
      setHeaders: { 'X-User-Id': user.id.toString() }
    });
  }
  return next(req);
};
