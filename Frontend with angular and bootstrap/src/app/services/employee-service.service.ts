import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  Employee,
  PaginatedResult,
  PaginatedSearch,
} from '../models/models.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private apiUrl = 'https://localhost:7290/api/Employee';

  constructor(private http: HttpClient) {}

  getAllEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.apiUrl);
  }

  getEmployeeById(id: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.apiUrl}/GetEmployee/${id}`);
  }
  createEmployee(employee: Employee): Observable<any> {
    return this.http.post(`${this.apiUrl}/CreateEmployee`, employee);
  }

  updateEmployee(employee: Employee): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateEmployee`, employee);
  }

  deleteEmployee(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/DeleteEmpoyee/${id}`);
  }

  search(request: PaginatedSearch): Observable<Employee[]> {
    let params = new HttpParams()
      .set('key', request.key!)
      .set('pageNumber', request.pageNumber.toString())
      .set('pageSize', request.pageSize.toString());
    return this.http.get<Employee[]>(`${this.apiUrl}/Search`, { params });
  }

  getPaginated(
    request: PaginatedSearch
  ): Observable<PaginatedResult<Employee>> {
    let params = new HttpParams()
      .set('pageNumber', request.pageNumber.toString())
      .set('pageSize', request.pageSize.toString());

    return this.http.get<PaginatedResult<Employee>>(
      `${this.apiUrl}/Paginated`,
      { params }
    );
  }
}
