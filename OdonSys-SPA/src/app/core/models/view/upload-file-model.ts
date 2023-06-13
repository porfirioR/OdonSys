export class UploadFileModel {
  constructor(
    public title: string,
    public multiple: boolean = true,
    public accept: string = '*',
    public removable: boolean = true,
    public maxFileSize: number
  ) { }
}
