export class UploadFileModel {
  constructor(
    public title: string,
    public multiple = true,
    public accept = '*',
    public removable = true,
    public maxFileSize: number,
    public height = 'auto',
    public formGroupClass = 'm-b-16'
  ) { }
}
