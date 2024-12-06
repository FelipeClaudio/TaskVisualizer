import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenuPositionExampleComponent } from './modules/menu-position-example/menu-position-example.component';
import { ExampleTableComponent } from './modules/example-table/example-table.component';
import { MainPageComponent } from './modules/main-page/main-page.component';
import { TopTabComponent } from './shared/components/top-tab/top-tab.component';

@NgModule({ declarations: [
        MainPageComponent,
    ],
    bootstrap: [MainPageComponent], imports: [BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        MenuPositionExampleComponent,
        ExampleTableComponent,
        TopTabComponent], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
