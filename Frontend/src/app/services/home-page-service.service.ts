import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {IUsersResponse } from '../models/IUsersResponse';

@Injectable({
  providedIn: 'root'
})
export class HomePageServiceService {
  constructor(private http: HttpClient) { }

  // This needs to be moved to a configuration.
  private taskVisualizerBackend = 'http://localhost:32123/users';

  getUsers(): Observable<IUsersResponse[]> {
    return this.http.get<IUsersResponse[]>(this.taskVisualizerBackend);
  }
}
