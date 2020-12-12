import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Game } from '../models/game.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class GameService extends BaseService<Game>{

    constructor(http: HttpClient) {
        super(http);
    }

    all(): Promise<Game[]> {
        return this.searchRootAsync("game");
    }

    get(id: string): Promise<Game> {
        return this.getRootAsync("game", id);
    }

    update(id: string, model: Game): Promise<Game> {
        return this.putRootAsync("game", id, model);
    }

    create(model: Game): Promise<Game> {
        return this.postRootAsync("game", model);
    }

    delete(id: string): Promise<Game> {
        return this.deleteRootAsync("game", id);
    }
}