import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appCompareDates]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: CompareDatesDirective,
      multi: true
    }
  ]
})
export class CompareDatesDirective implements Validator {
  @Input('appCompareDates') public compareDate = ''

  validate(control: AbstractControl): ValidationErrors | null {
    const startDateControl = control.root.get(this.compareDate);
    if (startDateControl) {
      const startDate = startDateControl.value;
      const endDate = control.value;
      if (endDate < startDate) {
        return {
          compareFailed: true
        };
      }
    }

    return null;
  }
}
