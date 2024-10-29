import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { MainPageComponent } from './main-page/main-page.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenuPositionExampleComponent } from './menu-position-example/menu-position-example.component';
import { ExampleTableComponent } from './example-table/example-table.component';

@NgModule({
  declarations: [
    MainPageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MenuPositionExampleComponent,
    ExampleTableComponent
  ],
  providers: [],
  bootstrap: [MainPageComponent]
})
export class AppModule { }
