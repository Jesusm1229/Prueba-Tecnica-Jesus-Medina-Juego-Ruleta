import { defineStore } from 'pinia'

export const useGameStore = defineStore({
    id: 'game',

    state: () => ({
        gameOver: false,
    }),

    actions: {
        setGame(newGameState: boolean) {
            this.gameOver = newGameState;
        },
    },
});