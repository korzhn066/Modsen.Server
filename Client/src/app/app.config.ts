import { ApplicationConfig, provideZoneChangeDetection, importProvidersFrom } from '@angular/core';
import { provideRouter, RouterModule, withComponentInputBinding } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { icons } from './icons-provider';
import { provideNzIcons } from 'ng-zorro-antd/icon';
import { ru_RU, provideNzI18n } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import ru from '@angular/common/locales/ru';
import { FormsModule } from '@angular/forms';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { errorsInterceptor } from './interceptors/errors.interceptor';
import { authInterceptor } from './interceptors/auth.interceptor';

registerLocaleData(ru);

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(
      routes,
      withComponentInputBinding(),
    ), 
    provideClientHydration(), 
    provideHttpClient(
      withFetch(),
      withInterceptors([errorsInterceptor, authInterceptor]),
    ), 
    provideNzIcons(icons), 
    provideNzI18n(ru_RU), 
    importProvidersFrom(FormsModule), 
    provideAnimationsAsync()
  ]
};
