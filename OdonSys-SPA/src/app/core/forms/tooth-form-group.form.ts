import { FormControl } from "@angular/forms";
import { Jaw } from "../enums/jaw.enum";
import { Quadrant } from "../enums/quadrant.enum";

export interface ToothFormGroup {
  id: FormControl<string | null>,
  name: FormControl<string | null>,
  number: FormControl<number | null>,
  value: FormControl<boolean | null>
  jaw: FormControl<Jaw | null>
  quadrant: FormControl<Quadrant | null>
}
