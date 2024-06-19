import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useScoreStore = defineStore({
    id: 'score',
    state: () => ({
        score: 0,
    }),
    actions: {
        setScore(newScore: number) {
            this.score = newScore;
        },
        /* async fetchScore() {
          try {
            const response = await axios.post('https://localhost:7299/api/score');
            this.setScore(response.data.score);
          } catch (error) {
            console.error(error);
          }
        }, */
    },
});