export class ClientPatchRequest {
  private op: string = 'replace'

  constructor(public value: boolean, public path: string = 'active') {}
}
