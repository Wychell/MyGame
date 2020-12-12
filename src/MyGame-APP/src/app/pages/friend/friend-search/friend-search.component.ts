import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IGRidColunm } from 'src/app/components/shared/simple-grid/simple-grid.component';
import { Friend } from 'src/app/models/friend.model';
import { NotificacaService } from 'src/app/services/notificaca.service';
import { FriendService } from './../../../services/friend.service';

@Component({
  selector: 'app-friend-search',
  templateUrl: './friend-search.component.html',
  styleUrls: ['./friend-search.component.css']
})
export class FriendSearchComponent implements OnInit {
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
      id: 'email',
      label: 'Email',
    },
    ,
    {
      id: 'action',
      label: '',
      action: {
        edit: true,
        delete: true
      }
    }
  ];
  data: Friend[];
  constructor(private friendService: FriendService, private router: Router, private notificacaoService: NotificacaService) { }

  ngOnInit(): void {
    this.get();
  }

  onCliclAdd() {
    this.router.navigate([`/friend/new`])
  }

  async onClickDelete(row: Friend) {
    await this.friendService.delete(row.id);
    this.notificacaoService.showSuccess(`Amigo deletado com sucesso.`, `Friend`);
    this.get();
  }

  onClickEdit(row: Friend) {
    this.router.navigate([`/friend/${row.id}`])
  }
  async get() {
    this.data = await this.friendService.all();
  }
}
