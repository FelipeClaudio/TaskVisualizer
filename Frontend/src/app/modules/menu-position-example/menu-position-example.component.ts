import {Component} from '@angular/core';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';

/**
 * @title Menu positioning
 */
@Component({
  selector: 'menu-position-example',
  templateUrl: './menu-position-example.component.html',
  standalone: true,
  imports: [MatButtonModule, MatMenuModule, MatToolbarModule],
})
export class MenuPositionExampleComponent {}