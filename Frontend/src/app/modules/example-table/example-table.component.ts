import { Component } from '@angular/core';
import { HomePageServiceService } from '../../core/services/home-page-service.service';
import { IUsersResponse } from '../../core/models/IUsersResponse';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-example-table',
  templateUrl: './example-table.component.html',
  styleUrls: ['./example-table.component.scss'],
  imports: [MatTableModule],
  standalone: true
})
export class ExampleTableComponent {
  private _displayedColumns: string[];
  private _users: IUsersResponse[] = [
    {name: "", email: "", status: 1}
  ];

  constructor(private homePageService: HomePageServiceService) {
    this.updateUsersList()
    this._displayedColumns = Object.keys( this._users[0]);
  }

  updateUsersList(): void {
    this.homePageService.getUsers().subscribe((response) => {
      this._users = response;
    });
  }

  get usersList() : IUsersResponse[]{
    return this._users
  }

  get displayedColumns(): string[] {
    return this._displayedColumns
  }
}
