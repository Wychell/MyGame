import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, LOCALE_ID, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './components/shared/nav/nav.component';
import { LoginComponent } from './pages/login/login.component';
import { FriendSearchComponent } from './pages/friend/friend-search/friend-search.component';
import { FriendFormComponent } from './pages/friend/friend-form/friend-form.component';
import { GameSearchComponent } from './pages/game/game-search/game-search.component';
import { SimpleGridComponent } from './components/shared/simple-grid/simple-grid.component';
import { MatTableModule } from '@angular/material/table';
import { FriendService } from './services/friend.service';
import { GameService } from './services/game.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { GameFormComponent } from './pages/game/game-form/game-form.component';
import { ErrorInterceptor } from './services/erro.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { LoanFormComponent } from './pages/friend/loan-form/loan-form.component';
import {MatSelectModule} from '@angular/material/select';
import { JwtInterceptor } from './services/jwt.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    FriendSearchComponent,
    FriendFormComponent,
    GameSearchComponent,
    SimpleGridComponent,
    GameFormComponent,
    LoanFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    ToastrModule.forRoot(),
    MatSelectModule
  ],
  providers: [FriendService, GameService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    {
      provide: LOCALE_ID,
      useValue: "pt-BR"
    }],

  bootstrap: [AppComponent]
})
export class AppModule { }
