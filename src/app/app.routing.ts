import { Routes, RouterModule } from '@angular/router';
import { TodolistComponent } from './todolist/todolist.component';

const APP_ROUTES: Routes = [

    { path: '', component: TodolistComponent },
    // { path: '**', redirectTo: ''},
    // {path:'', redirectTo:'/home', pathMatch:'full'}
];

export const AppRoutingModule = RouterModule.forRoot(APP_ROUTES);
