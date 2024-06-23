<script setup lang="ts">
import { resetUser } from '@/services/localStorageHandler'
import { isTokenExpired, scheduleTokenRefresh } from '@/services/tokenHandler'
import { usePlayerStore } from '@/stores/player'
import AuthNav from './AuthNav.vue'
import TabPlay from './TabPlay.vue'
import TabTopPlayers from './TabTopPlayers.vue'
import { Tabs, TabsContent, TabsList, TabsTrigger } from './ui/tabs'

const store = usePlayerStore();

function handleUserSession() {
    const userObject = JSON.parse(localStorage.getItem('UserObject') ?? 'null');

    if (userObject) {
        if (userObject.accessToken && !isTokenExpired(userObject.accessToken)) {
            // Token is valid, refresh it and set user object to state
            scheduleTokenRefresh();
            store.player = userObject;
        } else {
            // Token is expired or not present, reset user
            resetUser();
        }
    }
}


handleUserSession();
</script>


<template>
    <div class="flex-1 p-2 pt-6 mx-4 mt-8 space-y-4 md:border-2 md:p-8 rounded-2xl">
        <div class="flex-col items-center justify-between space-y-2 md:flex-row md:flex">
            <h2 class="flex flex-row justify-center text-3xl font-bold">
                <img alt="Unilink logo" class="w-20 h-full md:w-60 logo" src="@/assets/unilink_logo.png" />
                <div class="relative flex justify-center m-auto tracking-tighter text-[#00308F]">
                    Win Wheel
                </div>
            </h2>
            <div>
                <AuthNav />
            </div>
        </div>

        <Tabs default-value="play" class="w-full">
            <TabsList>
                <TabsTrigger value="play">
                    Play
                </TabsTrigger>
                <TabsTrigger value="topPlayers">
                    Top Winners
                </TabsTrigger>
            </TabsList>

            <TabsContent value="play" class="space-y-4 ">
                <TabPlay />
            </TabsContent>
            <TabsContent value="topPlayers">
                <TabTopPlayers />
            </TabsContent>

        </Tabs>
    </div>

</template>