import { Component } from '@angular/core';
import {
  Employee,
  PaginatedResult,
  PaginatedSearch,
} from '../models/models.model';
import { EmployeeService } from '../services/employee-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css',
})
export class EmployeeListComponent {
  employees: Employee[] = [];
  paginatedResult: PaginatedResult<Employee> = {
    data: [],
    totalCount: 0,
    pageNumber: 1,
    pageSize: 10,
  };
  paginatedSearch: PaginatedSearch = {
    pageNumber: 1,
    pageSize: 10,
    key: '',
  };
  loading = false;
  searchTerm = '';

  constructor(
    private employeeService: EmployeeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.loading = true;
    this.employeeService.getPaginated(this.paginatedSearch).subscribe(
      (result: PaginatedResult<Employee>) => {
        console.log(result);
        this.paginatedResult.data = result.data;
        this.paginatedResult.totalCount = result.data.length;

        this.employees = result.data;
        console.log(this.employees);
        this.loading = false;
      },
      () => {
        console.error('Error loading employees');
        this.loading = false;
      }
    );
  }

  onSearch(): void {
    this.paginatedSearch.key = this.searchTerm;
    this.paginatedSearch.pageNumber = 1;
    this.employeeService.search(this.searchTerm).subscribe({
      next: (result: any) => {
        this.paginatedResult.data = result.data;
        this.paginatedResult.totalCount = result.data.length;

        this.employees = result.data;
        console.log(this.employees);
        this.loading = false;
      },
      error: () => {},
    });
    this.loadEmployees();
  }

  onPageChange(page: number): void {
    this.paginatedSearch.pageNumber = page;
    this.loadEmployees();
  }

  viewEmployee(id: number): void {
    this.router.navigate(['/employees', id]);
  }

  editEmployee(id: number): void {
    this.router.navigate(['/employees/edit', id]);
  }

  deleteEmployee(id: number): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(id).subscribe(
        () => {
          this.loadEmployees();
        },
        () => {
          console.error('Error deleting employee');
        }
      );
    }
  }
}
