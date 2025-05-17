import { Component } from '@angular/core';
import {
  Employee,
  PaginatedResult,
  PaginatedSearch,
} from '../models/models.model';
import { EmployeeService } from '../services/employee-service.service';
import { Router } from '@angular/router';
import { NgbPaginationConfig } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css',
  providers: [NgbPaginationConfig],
})
export class EmployeeListComponent {
  employees: Employee[] = [];
  paginatedResult: PaginatedResult<Employee> = {
    data: [],
    Total: 0,
    pageNumber: 1,
    pageSize: 5,
  };
  paginatedSearch: PaginatedSearch = {
    pageNumber: 1,
    pageSize: 5,
    key: '',
  };
  loading = false;
  searchTerm = '';

  constructor(
    private employeeService: EmployeeService,
    private router: Router,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.loading = true;
    this.employeeService.getPaginated(this.paginatedSearch).subscribe(
      (result: any) => {
        console.log(result);
        this.paginatedResult.data = result.data.readEmployeeDTOs;
        this.paginatedResult.Total = result.data.total;

        this.employees = result.data.readEmployeeDTOs;
        console.log(this.employees, this.paginatedResult.Total);
        this.loading = false;
      },
      () => {
        console.error('Error loading employees');
        this.loading = false;
      }
    );
  }
  isSearching = false;

  onSearch(): void {
    this.isSearching = !!this.searchTerm;

    this.paginatedSearch.key = this.searchTerm;

    this.employeeService.search(this.paginatedSearch).subscribe({
      next: (result: any) => {
        console.log(result);
        this.paginatedResult.data = result.data.readEmployeeDTOs;
        this.paginatedResult.Total = result.data.total;

        this.employees = result.data.readEmployeeDTOs;
        console.log('this is seacrh', this.employees);
        this.loading = false;
      },
      error: () => {},
    });
    this.loadEmployees();
  }

  onPageChange(page: number): void {
    this.paginatedSearch.pageNumber = page;
    this.paginatedResult.pageNumber = page;
    if (this.isSearching) {
      this.onSearch();
    } else {
      this.loadEmployees();
    }
    // this.loadEmployees();
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
          this.toaster.success('Employee Deleted Successfully', 'Success 🎉');
          this.loadEmployees();
        },
        () => {
          this.toaster.success('Error deleting employee', 'Error 😁');
        }
      );
    }
  }
}
