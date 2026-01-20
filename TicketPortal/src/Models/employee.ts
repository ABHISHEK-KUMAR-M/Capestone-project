export class Employee {
  empId: string;
  name: string;
  email: string;
  password: string;
  role: string;
  departmentId?: string;

  constructor(
    empId: string = '',
    name: string = '',
    email: string = '',
    password: string = '',
    role: string = '',
    departmentId: string = ''
  ) {
    this.empId = empId;
    this.name = name;
    this.email = email;
    this.password = password;
    this.role = role;
    this.departmentId = departmentId;
  }
}
