import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { AuthenticationService } from './core/authentication.service';
import { ConfigurationService } from './core/configuration.service';
import { NavigationService } from './core/navigation.service';
import { NotificationService } from './core/notification.service';
import { RegistrationService } from './core/registration.service';
import { TranslationService } from './i18n/translation.service';

@NgModule({
  imports: [CommonModule],
})
export class SdkModule {
  static forRoot(): ModuleWithProviders<SdkModule> {
    return {
      ngModule: SdkModule,
      providers: [
        AuthenticationService,
        ConfigurationService,
        NavigationService,
        NotificationService,
        RegistrationService,
        TranslationService,
      ],
    };
  }
  static forChild(): ModuleWithProviders<SdkModule> {
    return {
      ngModule: SdkModule,
      providers: [
        AuthenticationService,
        ConfigurationService,
        NavigationService,
        NotificationService,
        RegistrationService,
        TranslationService,
      ],
    };
  }
}
