export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  position: string;
}

export interface PaginatedSearch {
  pageNumber: number;
  pageSize: number;
  key?: string;
}

export interface Search {
  searchTerm: string;
}

export interface PaginatedResult<T> {
  data: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
