import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../services/employee-service.service';
import { Employee } from '../models/models.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.css',
})
export class EmployeeFormComponent {
  employeeForm!: FormGroup;
  isEditMode = false;
  employeeId?: number;
  submitted = false;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.initForm();

    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.employeeId = +params['id'];
        this.isEditMode = true;
        this.loadEmployee(this.employeeId);
      }
    });
  }

  initForm(): void {
    this.employeeForm = this.fb.group({
      firstname: ['', [Validators.required]],
      lastname: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      position: ['', [Validators.required]],
    });
  }

  loadEmployee(id: number): void {
    this.loading = true;
    this.employeeService.getEmployeeById(id).subscribe(
      (employee: any) => {
        console.log(employee.data);
        this.employeeForm.patchValue({
          firstname: employee.data.firstName,
          lastname: employee.data.lastName,
          email: employee.data.email,
          position: employee.data.position,
        });

        this.loading = false;
      },
      () => {
        this.toaster.error('Error loading employee', 'Error');

        this.loading = false;
      }
    );
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.employeeForm.invalid) {
      return;
    }

    this.loading = true;
    const employeeData: Employee = {
      ...this.employeeForm.value,
      id: this.isEditMode ? this.employeeId! : 0,
    };

    if (this.isEditMode) {
      this.employeeService.updateEmployee(employeeData).subscribe(
        () => {
          this.toaster.success('Employee Updated Successfully', 'Success 🎉');
          this.router.navigate(['/employees']);
          this.loading = false;
        },
        () => {
          this.toaster.error('Error updating employee', 'Error');

          this.loading = false;
        }
      );
    } else {
      this.employeeService.createEmployee(employeeData).subscribe(
        () => {
          this.toaster.success('Employee Created Successfully', 'Success 🎉');

          this.router.navigate(['/employees']);
          this.loading = false;
        },
        () => {
          this.toaster.error('Dublicate Email Not Allowed', 'Error');

          console.error('Error creating employee');
          this.loading = false;
        }
      );
    }
  }
}
