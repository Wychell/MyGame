import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from './../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  constructor(private formBuilder: FormBuilder, private accountService: AccountService,
    private _: ActivatedRoute,
    private router: Router) {

    this.accountService.userObservable.subscribe(user => {
      if (user)
        this.router.navigate(["/friend"]);
    });

  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }


  onSubmit() {
    this.accountService
      .login(this.form.controls.username.value, this.form.controls.password.value)
      .subscribe(_ => {
        this.router.navigate(["/friend"]);
      });
  }

}
