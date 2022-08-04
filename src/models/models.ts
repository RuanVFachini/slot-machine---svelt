export interface ScoreItem {
    name: string;
    score: number;
}

export interface DiceResult {
    dice1Steps: number;
    dice2Steps: number;
    dice3Steps: number;
    winner: boolean;
}