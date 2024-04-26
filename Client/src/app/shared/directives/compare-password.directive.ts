import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appComparePassword]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: ComparePasswordDirective,
      multi: true
    }
  ]
})
export class ComparePasswordDirective implements Validator {
  @Input('appComparePassword') public comparePassword = ''

  validate(control: AbstractControl): ValidationErrors | null {
    const passwordControl = control.root.get(this.comparePassword);
    if (passwordControl) {
      const passwordValue = passwordControl.value;
      const confirmPasswordValue = control.value;
      if (passwordValue !== confirmPasswordValue) {
        return {
          compareFailed: true
        };
      }
    }

    return null;
  }
}
