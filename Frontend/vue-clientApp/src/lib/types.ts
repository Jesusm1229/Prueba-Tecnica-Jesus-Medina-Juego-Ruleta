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
    score: number | null | undefined;
    betAmount: number | null;
    color: ColorTypes | null;
    number?: string;
    spinWheelColor?: string;
    spinWheelNumber?: number;
}

export interface BetResponse {
    newScore: number;
    /*  winnerNumber: number;
     winnerColor: string; */
    isWin: boolean;
    isGameOver: boolean;
}

export interface WheelResponse {
    number: number;
    color: string;
}

export interface PlayerWithScore {
    id?: string;
    userName: string;
    score: { points: number };
}