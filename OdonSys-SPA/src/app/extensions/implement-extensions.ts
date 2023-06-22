String.prototype.compareString = function (arg: string) {
  const areEquals = this.localeCompare(arg, undefined, { sensitivity: 'accent' })
  return areEquals === 0
}