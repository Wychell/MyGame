import { Component, OnInit } from '@angular/core';
import { IGRidColunm } from 'src/app/components/shared/simple-grid/simple-grid.component';
import { GameService } from './../../../services/game.service';
import { Game } from './../../../models/game.model';
import { Router } from '@angular/router';
import { NotificacaService } from 'src/app/services/notificaca.service';

@Component({
  selector: 'app-game-search',
  templateUrl: './game-search.component.html',
  styleUrls: ['./game-search.component.css']
})
export class GameSearchComponent implements OnInit {

  colunas: IGRidColunm[] = [
    {
      isKey: true,
      id: 'id',
      label: 'Id'
    },
    {
      id: 'name',
      label: 'Nome',
    },
    {
      id: 'gender',
      label: 'Genero',
    },
    {
      id: 'action',
      label: '',
      action: {
        edit: true,
        delete: true
      }
    }
  ];
  data: Game[];
  constructor(private gameService: GameService, private router: Router,
    private notificacaoService: NotificacaService) { }

  ngOnInit(): void {
    this.get();
  }

  onCliclAdd() {
    this.router.navigate([`/game/new`])
  }

  async onClickDelete(row: Game) {
    await this.gameService.delete(row.id);
    this.notificacaoService.showSuccess(`Jogo deletado com sucesso.`, `Game`);
    this.get();
  }
  
  onClickEdit(row: Game) {
    this.router.navigate([`/game/${row.id}`])
  }

  async get() {
    this.data = await this.gameService.all();
  }

}
