import { usePlayerStore } from "@/stores/player";



export async function resetUser() {
    const store = usePlayerStore()

    store.player = {
        accessToken: '',
        refreshToken: '',
        idUsername: '',
        username: '',
        idScore: '',
        score: 0
    }
    localStorage.removeItem('UserObject');
}