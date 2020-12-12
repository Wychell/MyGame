import { Loan } from './loan.model';

export class Friend {
    id?: string;
    name: string;
    email: string;
    loans: Loan[]
}