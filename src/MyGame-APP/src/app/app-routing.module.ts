import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FriendSearchComponent } from './pages/friend/friend-search/friend-search.component';
import { LoginComponent } from './pages/login/login.component';
import { FriendFormComponent } from './pages/friend/friend-form/friend-form.component';
import { GameSearchComponent } from './pages/game/game-search/game-search.component';
import { GameFormComponent } from './pages/game/game-form/game-form.component';
import { LoanFormComponent } from './pages/friend/loan-form/loan-form.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [{
  path: '',
  pathMatch: 'full',
  redirectTo: 'login'
},
{
  path: 'login',
  component: LoginComponent,
  pathMatch: 'full',
},
{
  path: 'friend',
  component: FriendSearchComponent,
  canActivate: [AuthGuardService],

},
{
  path: 'friend/:id',
  component: FriendFormComponent,
  canActivate: [AuthGuardService],
},
{
  path: 'friend/:id/loan',
  component: LoanFormComponent,
  canActivate: [AuthGuardService],
},
{
  path: 'game',
  component: GameSearchComponent,
  canActivate: [AuthGuardService],
},
{
  path: 'game/:id',
  component: GameFormComponent,
  canActivate: [AuthGuardService],
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
