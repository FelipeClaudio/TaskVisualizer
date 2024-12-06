import { Component } from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';

@Component({
  selector: 'app-top-tab',
  templateUrl: './top-tab.component.html',
  styleUrls: ['./top-tab.component.scss'],
  standalone: true,
  imports: [MatTabsModule],
})

export class TopTabComponent {}