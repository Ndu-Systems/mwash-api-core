import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { JobsComponent } from './jobs';
import { StaffComponent } from './staff';
import { StatsComponent } from './stats';
import { TicketsComponent } from './tickets';
import { AuthGuard } from '../_guards';


const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        component: StatsComponent
      }
    ]
  },
];

export const declarations: Array<any> = [
  DashboardComponent,
  JobsComponent,
  StaffComponent,
  StatsComponent,
  TicketsComponent
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
