import { Country } from "../enums/country.enum";

export class MethodHandler {
  private static calculateParaguayanRuc = (document: string) => {
    let checkDigit = 0;
    if(document && document.length >= 6 && !isNaN(+document)) {
      let multiplier = 2;
      const module = 11;
      const reverseDocument = document.split('').reverse();
      let result = 0;
      reverseDocument.forEach(value => {
        result += multiplier * +value;
        multiplier++;
        if (multiplier > 11) {
          multiplier = 2;
        }
      });
      const rest = result % module;
      checkDigit = rest > 1 ? module - rest : 0;
    }
    return checkDigit
  }

  public static calculateCheckDigit = (document: string, country: Country): number => {
    switch (country) {
      case Country.Paraguay: return MethodHandler.calculateParaguayanRuc(document);
      case Country.Argentina:
      default: return 0;
    }
  }
}
