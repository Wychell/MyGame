import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './../../../models/user.model';
import { AccountService } from './../../../services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  user: User = null;
  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.accountService.userObservable.subscribe(x => this.user = x);
  }

  logout() {
    this.user = null;
    this.accountService.logout();
    this.router.navigate(['login']);
  }

}
