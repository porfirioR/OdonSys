export class UploadFileRequest {
  constructor(
    public referenceId: string,
    public files: File[]
  ) { }
}
