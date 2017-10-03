import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TodolistComponent } from './todolist/todolist.component';

const APP_ROUTES: Routes = [

    { path: '', component: DashboardComponent },
    { path: 'todolist', component: TodolistComponent },
    // { path: '**', redirectTo: ''},
    // {path:'', redirectTo:'/home', pathMatch:'full'}
];

export const AppRoutingModule = RouterModule.forRoot(APP_ROUTES);
