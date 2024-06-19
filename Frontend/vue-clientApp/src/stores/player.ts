import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const usePlayerStore = defineStore('player', () => {
    const player = ref({
        userName: '',
    })
    function setUserName(userName: string) {
        player.value.userName = userName
    }
    return { player, setUserName }
}
)