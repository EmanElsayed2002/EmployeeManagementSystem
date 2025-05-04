import { Component } from '@angular/core';
import { Employee } from '../models/models.model';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../services/employee-service.service';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrl: './employee-detail.component.css',
})
export class EmployeeDetailComponent {
  employee?: Employee;
  loading = false;
  error = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.loadEmployee(+params['id']);
      }
    });
  }

  loadEmployee(id: number): void {
    this.loading = true;
    this.employeeService.getEmployeeById(id).subscribe(
      (employee: any) => {
        console.log(employee);
        this.employee = employee.data;
        this.loading = false;
      },
      () => {
        console.error('Error loading employee');
        this.loading = false;
        this.error = true;
      }
    );
  }

  editEmployee(): void {
    if (this.employee) {
      this.router.navigate(['/employees/edit', this.employee.id]);
    }
  }

  deleteEmployee(): void {
    if (
      this.employee &&
      confirm('Are you sure you want to delete this employee?')
    ) {
      this.employeeService.deleteEmployee(this.employee.id).subscribe(
        () => {
          this.router.navigate(['/employees']);
        },
        () => {
          console.error('Error deleting employee');
        }
      );
    }
  }

  goBack(): void {
    this.router.navigate(['/employees']);
  }
}
