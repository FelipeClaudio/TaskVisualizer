import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {IUsersResponse } from '../models/IUsersResponse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HomePageServiceService {
  constructor(private http: HttpClient) { }

  // This needs to be moved to a configuration.
  private taskVisualizerBackend = environment.backendAddress;

  getUsers(): Observable<IUsersResponse[]> {
    return this.http.get<IUsersResponse[]>(this.taskVisualizerBackend + '/users');
  }
}
