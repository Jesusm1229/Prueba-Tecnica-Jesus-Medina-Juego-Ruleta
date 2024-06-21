export enum CategoryTypes {
    Straight = 'Straight',
    Even = 'Even',
    Odd = 'Odd',
    Color = 'Color'
}

export enum ColorTypes {
    Red = 'Red',
    Black = 'Black',
}
export interface Player {
    accessToken?: string | null;
    refreshToken?: string | null;
    idUsername?: string | null;
    username?: string | null;
    idScore?: string | null;
    score?: number | null;
}
export interface Bet {
    category: CategoryTypes | null;
    score: number | null;
    betAmount: number | null;
    color: ColorTypes | null;
    number?: string;
}

export interface BetResponse {
    newScore: number;
    winnerNumber: number;
    winnerColor: string;
    didIWin: boolean;
    isGameOver: boolean;
}

export interface PlayerWithScore {
    username: string;
    score: { points: number };
}