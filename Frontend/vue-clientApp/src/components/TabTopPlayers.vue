<template>
    <div>
        <h1>Top Players</h1>
        <!-- <ul>
            <li v-for="player in players" :key="player.id">
                {{ player.name }} - {{ player.score }}
            </li>
        </ul> -->
    </div>
</template>

<script setup lang="ts">
import { resetUser } from '@/services/localStorageHandler'
import { isTokenExpired, scheduleTokenRefresh } from '@/services/tokenHandler'
import { usePlayerStore } from '@/stores/player'
import axios from 'axios'

</script>

<script lang="ts">

export default {

    data() {
        return {
            numbers: Array.from({ length: 37 }, (_, i) => i),
            players: [],

        }
    },
    methods: {
        async fetchTopPlayers() {
            try {
                const response = await axios.get('https://localhost:7299/api/players')
                this.players = response.data
                console.log(this.players)
            } catch (error) {
                console.error(error)
            }
        },   

    },
    mounted() {
      
        this.fetchTopPlayers();

    }

}

</script>

