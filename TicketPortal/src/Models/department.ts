export class Department {
  departmentId: string;
  departmentName: string;
  description?: string;

  constructor(
    departmentId: string = '',
    departmentName: string = '',
    description: string = ''
  ) {
    this.departmentId = departmentId;
    this.departmentName = departmentName;
    this.description = description;
  }
}
