export class PatchRequest {
  constructor(
    public value: boolean,
    public path = 'active',
    public op = 'replace'
  ) {}
}
