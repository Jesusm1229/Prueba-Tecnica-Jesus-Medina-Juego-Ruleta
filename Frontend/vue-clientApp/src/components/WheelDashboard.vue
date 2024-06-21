<script setup lang="ts">
import { resetUser } from '@/services/localStorageHandler'
import { isTokenExpired, scheduleTokenRefresh } from '@/services/tokenHandler'
import { usePlayerStore } from '@/stores/player'
import AuthNav from './AuthNav.vue'
import TabPlay from './TabPlay.vue'
import TabTopPlayers from './TabTopPlayers.vue'
import { Tabs, TabsContent, TabsList, TabsTrigger } from './ui/tabs'

</script>

<script lang="ts">
export default {

    methods: {
        //refresh access token if it has not expired yet
        refresStorageUser() {
            const userObject = JSON.parse(localStorage.getItem('UserObject') ?? 'null');

            if (userObject && userObject.accessToken) {
                if (isTokenExpired(userObject.accessToken)) {
                    //if token is expired, reset
                    resetUser();
                } else {
                    //if token is not expired, refresh
                    scheduleTokenRefresh();
                }
            }
        },
        //check if userObject exists in local storage and store it in the store
        setStorageUserToState() {
            const store = usePlayerStore();

            const userObject = JSON.parse(localStorage.getItem('UserObject') ?? 'null');

            if (userObject) {
                store.player = userObject;
            }
        }

    },
    created() {
        this.setStorageUserToState();
        this.refresStorageUser();
    }

}

</script>

<template>


    <div class="flex-1 p-8 pt-6 mx-4 space-y-4 border">
        <div class="flex items-center justify-between space-y-2">
            <h2 class="text-3xl font-bold">Unilink Win Wheel</h2>
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





<style></style>