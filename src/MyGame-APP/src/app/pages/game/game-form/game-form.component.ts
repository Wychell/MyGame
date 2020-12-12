import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FriendService } from 'src/app/services/friend.service';
import { NotificacaService } from 'src/app/services/notificaca.service';
import { GameService } from './../../../services/game.service';

@Component({
  selector: 'app-game-form',
  templateUrl: './game-form.component.html',
  styleUrls: ['./game-form.component.css']
})
export class GameFormComponent implements OnInit {
  form: FormGroup;
  isNew: boolean = true;
  id: string;
  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private gameService: GameService,
    private notificacaoService: NotificacaService) { }

  ngOnInit(): void {
    this.buildForm();
    this.activatedRoute.params.subscribe(async params => {
      this.isNew = params["id"] === "new";

      if (!this.isNew) {
        this.id = params["id"];
        var result = await this.gameService.get(params["id"]);
        this.form.patchValue(result);
      }
    });


  }
  private buildForm() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      gender: ['', Validators.required]
    });
  }


  onSubimit() {
    if (this.form.valid) {
      if (this.isNew)
        this.gameService.create(this.form.value);
      else
        this.gameService.update(this.id, this.form.value);

      this.notificacaoService.showSuccess(`Jogo ${this.isNew ? 'Cadastrado' : 'Atualizado'} com sucesso.`, `Game`);
      this.router.navigate(["/game"]);

    }
    else {
      this.notificacaoService.showWarning(`Formulario invalido`, `Game`);
    }
  }
}
