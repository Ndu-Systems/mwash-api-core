import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// user the full namespace so that it compiles with no warnings make sure code is tree shakeable friendly before shakin
import { fakeBackendProvider } from './_helpers/fake-backend';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

@NgModule({
   declarations: [
      AppComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule
   ],
   providers: [
      { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
      fakeBackendProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
