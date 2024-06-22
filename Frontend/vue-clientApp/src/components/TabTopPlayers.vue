<template>
    <div class="relative mt-8 overflow-x-auto border-2 shadow-md sm:rounded-lg">
        <table class="w-full text-sm text-left text-gray-500 rtl:text-right ">
            <caption class="p-5 text-lg font-semibold text-left text-gray-900 bg-white rtl:text-right ">
                Our Top Winners
                <p class="mt-1 text-sm font-normal text-gray-500 ">
                    Check the top 10 players with the highest scores. Create an account and start playing to get on the
                    list!
                </p>
            </caption>
            <thead class="text-xs text-gray-700 uppercase bg-gray-50 ">
                <tr>
                    <th scope="col" class="px-6 py-3">
                        Username
                    </th>
                    <th scope="col" class="px-6 py-3">
                        Score
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="player in players" :key="player.id"
                    class="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                    <td class="px-6 py-4">{{ player.userName }}</td>
                    <td class="px-6 py-4">{{ player.score.points }}</td>
                </tr>

            </tbody>
        </table>
    </div>

</template>

<script lang="ts">
import axios from 'axios'
import { onMounted, ref } from 'vue'
import type { PlayerWithScore } from '@/lib/types'

export default {
    data() {
        return {
            players: [] as PlayerWithScore[],
        }
    },
    methods: {
        async fetchTopPlayers() {
            try {
                const response = await axios.get('https://localhost:7299/api/players/top')
                this.players = response.data
            } catch (error) {
                console.error(error)
            }
        }
    },
    mounted() {
        console.log("mounted")
        this.fetchTopPlayers()
    }
}
</script>