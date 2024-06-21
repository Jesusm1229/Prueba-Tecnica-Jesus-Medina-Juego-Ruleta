import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import type { Player } from '@/lib/types'

export const usePlayerStore = defineStore('player', () => {

    const player = ref<Player>({
        accessToken: null,
        refreshToken: null,
        idUsername: null,
        username: '',
        idScore: null,
        score: null
    })


    function setPlayer(newPlayer: Player) {
        player.value = newPlayer
    }

    return { player, setPlayer }
}
)