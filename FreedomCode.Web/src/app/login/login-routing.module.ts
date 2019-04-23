import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { ForgotPasswordComponent } from './forgot-password';

const routes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'forgot-password', component: ForgotPasswordComponent}
];
export const declarations: Array<any> = [
  LoginComponent,
  ForgotPasswordComponent
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginRoutingModule { }
