export class PatchRequest {
  constructor(
    public value: boolean,
    public path: string = 'active',
    public op: string = 'replace'
  ) {}
}
