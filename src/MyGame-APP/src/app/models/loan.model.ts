import { Game } from './game.model';

export class Loan {
    id: string | null;
    gameId: string;
    friendId: string;
    game: Game;
    endDate: string | null;
}