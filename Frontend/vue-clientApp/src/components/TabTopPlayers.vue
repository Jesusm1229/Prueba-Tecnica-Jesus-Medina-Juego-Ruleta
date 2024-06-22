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

            <thead class="p-4 text-xs text-gray-700 uppercase bg-gray-50">
                <tr>
                    <th scope="col" class="px-6 py-3">
                        Username
                    </th>
                    <th scope="col" class="px-6 py-3">
                        Score
                    </th>
                </tr>
            </thead>
            <div v-if="loading" class="flex flex-col w-full space-y-3">
                <!-- Skeletons for loading state -->
                <!--  <Skeleton class="w-full h-full rounded-xl " /> -->
                <div class="w-full p-4 space-y-2">
                    <Skeleton class="w-full h-8" />
                    <Skeleton class="w-full h-8" />
                    <Skeleton class="w-full h-8" />
                    <Skeleton class="w-full h-8" />
                    <Skeleton class="w-full h-8" />
                    <Skeleton class="w-full h-8" />
                </div>
            </div>
            <tbody v-else>
                <tr v-for="player in players" :key="player.id"
                    class="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                    <td class="px-6 py-4">{{ player.userName }}</td>
                    <td class="px-6 py-4">{{ player.score.points }}</td>
                </tr>

            </tbody>
        </table>
    </div>

</template>

<script setup lang="ts">
import type { PlayerWithScore } from '@/lib/types';
import axios from 'axios';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { Skeleton } from './ui/skeleton';
import { onMounted, ref } from 'vue';
import handleError from '@/provider/handleError';


const players = ref<PlayerWithScore[]>([]); // Use ref for reactive variables
const loading = ref(true); // Use ref for reactive state

async function fetchTopPlayers() {
    try {
        const response = await axios.get('https://localhost:7299/api/players/top');
        players.value = response.data; // Update reactive variables with .value

    } catch (error) {
        handleError(error);
    } finally {
        loading.value = false; // Update reactive state with .value
    }
}

onMounted(fetchTopPlayers);

</script>