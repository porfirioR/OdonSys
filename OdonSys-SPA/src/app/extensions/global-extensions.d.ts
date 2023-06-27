export { }

declare global {
  interface String {
    /**
     * Compare two string case insensitive
     * @param this Current string that is compared to the argument
     * @param arg Argument to compare
     */
    compareString(this: string, arg: string): boolean
  }
}
