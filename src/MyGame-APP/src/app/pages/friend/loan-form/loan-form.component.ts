import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Friend } from 'src/app/models/friend.model';
import { Game } from 'src/app/models/game.model';
import { GameService } from 'src/app/services/game.service';
import { NotificacaService } from 'src/app/services/notificaca.service';
import { FriendService } from './../../../services/friend.service';

@Component({
  selector: 'app-loan-form',
  templateUrl: './loan-form.component.html',
  styleUrls: ['./loan-form.component.css']
})
export class LoanFormComponent implements OnInit {
  form: FormGroup;
  isNew: boolean = true;
  games: Game[] = [];
  friend: Friend;
  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private friendService: FriendService,
    private gameService: GameService,
    private notificacaoService: NotificacaService,
  ) { }

  ngOnInit(): void {
    this.buildForm();
    this.activatedRoute.params.subscribe(async params => {
      this.isNew = params["id"] === "novo";
      var result = await this.friendService.get(params["id"]);
      this.friend = result;
      this.games = await this.gameService.all();
      this.form.patchValue(result);
    });


  }
  private buildForm() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      game: [null, Validators.required],

    });
  }


  async onSubimit() {
    console.log(this.form.value)
    if (this.form.valid) {
      await this.friendService.lend(this.friend.id, this.form.value.game.id);
      this.notificacaoService.showSuccess(`Emprestimoo feito com sucesso.`, `Loan`);
      this.router.navigate([`/friend/${this.friend.id}`]);
    } else {
      this.notificacaoService.showWarning(`Formulario invalido`, `Loan`);
    }
  }
}
