import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IGRidColunm } from 'src/app/components/shared/simple-grid/simple-grid.component';
import { Loan } from 'src/app/models/loan.model';
import { FriendService } from './../../../services/friend.service';
import { NotificacaService } from './../../../services/notificaca.service';
import * as moment from 'moment';

@Component({
  selector: 'app-friend-form',
  templateUrl: './friend-form.component.html',
  styleUrls: ['./friend-form.component.css']
})
export class FriendFormComponent implements OnInit {
  form: FormGroup;
  isNew: boolean = true;
  id: string;
  colunas: IGRidColunm[] = [
    {
      isKey: true,
      id: 'id',
      label: 'Id',
      format: (data: Loan) => data.game.id
    },
    {
      id: 'name',
      label: 'Nome',
      format: (data: Loan) => data.game.name
    },
    {
      id: 'gender',
      label: 'Genero',
      format: (data: Loan) => data.game.gender
    },
    ,
    {
      id: 'endDate',
      label: 'Fim Emprestimo',
      format: (data: Loan) => data.endDate ? moment(data.endDate).format('DD/MM/YYYY') : ''
    },
    {
      id: 'action',
      label: '',
      action: {
        delete: true
      }
    }
  ];
  data: Loan[];
  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private friendService: FriendService,
    private notificacaoService: NotificacaService) { }

  ngOnInit(): void {
    this.buildForm();
    this.activatedRoute.params.subscribe(async params => {
      this.isNew = params["id"] === "new";

      if (!this.isNew) {
        this.id = params["id"];
        await this.get();
      }
    });


  }
  private async get() {
    var result = await this.friendService.get(this.id);
    this.form.patchValue(result);
    this.data = result.loans;
  }

  private buildForm() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      phone: ['', Validators.required]
    });
  }

  async onClickEdit(row: Loan) {
    await this.friendService.finalize(this.id, row.id)
    this.get();
  }


  addLoan() {
    this.router.navigate([`/friend/${this.id}/loan`])
  }

  async onSubimit() {
    if (this.form.valid) {
      if (this.isNew)
        await this.friendService.create(this.form.value);
      else
        await this.friendService.update(this.id, this.form.value);

      this.notificacaoService.showSuccess(`Amigo ${this.isNew ? 'Cadastrado' : 'Atualizado'} com sucesso.`, `Friend`);
      this.router.navigate(["/friend"]);
    }
    else {
      this.notificacaoService.showWarning(`Formulario invalido`, `Friend`);
    }
  }
}
