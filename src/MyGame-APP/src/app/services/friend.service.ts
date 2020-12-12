import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Friend } from './../models/friend.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class FriendService extends BaseService<Friend>{

    constructor(http: HttpClient) {
        super(http);
    }

    all(): Promise<Friend[]> {
        return this.searchRootAsync("friend");
    }

    get(id: string): Promise<Friend> {
        return this.getRootAsync("friend", id);
    }

    update(id: string, model: Friend): Promise<Friend> {
        return this.putRootAsync("friend", id, model);
    }

    create(model: Friend): Promise<Friend> {
        return this.postRootAsync("friend", model);
    }

    delete(id: string): Promise<Friend> {
        return this.deleteRootAsync("friend", id);
    }

    finalize(id: string, loanId: string): Promise<Friend> {
        return this.putRootAsync(`friend/${id}/lend/finalize`, null, { loanId });
    }

    lend(id: string, gameId: string): Promise<Friend> {
        return this.postRootAsync(`friend/${id}/lend`, { gameId });
    }
}