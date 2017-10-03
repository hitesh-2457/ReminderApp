import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { CalendarModule } from 'primeng/primeng';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app.routing';
import { ToDoListModule } from './todolist/todolist.module';

import { NotificationService } from './Services/notification/notification.service';
import { TaskApiService } from './Services/task-api/task-api.service';
import { TodoApiService } from './Services/todo-api/todo-api.service';

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TodolistComponent } from './todolist/todolist.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    TodolistComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    AppRoutingModule,
    ToDoListModule,
    CalendarModule,
    BrowserAnimationsModule
  ],
  providers: [
    NotificationService,
    TaskApiService,
    TodoApiService
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
