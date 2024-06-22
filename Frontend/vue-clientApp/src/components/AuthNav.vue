<template>
    <template v-if="store.player.accessToken">
        <div class="relative flex flex-row justify-center gap-2 text-lg text-center col-span-full">
            <p class="relative my-auto">Enjoy your game,
                <strong class="font-semibold">
                    {{ store.player.username }}
                </strong>
            </p>
            <Separator orientation="vertical" class="bg-rose-500" />
            <TooltipProvider>
                <Tooltip>
                    <TooltipTrigger>
                        <Button @click="LogOut" variant="outline" size="icon">
                            <LogOutIcon class="w-4 h-4" />
                        </Button>
                    </TooltipTrigger>
                    <TooltipContent>
                        <p>LogOut</p>
                    </TooltipContent>
                </Tooltip>
            </TooltipProvider>

        </div>

    </template>
    <template v-else>
        <div class="relative flex flex-col self-end justify-end gap-4 text-sm text-right justify-right">
            <div class="">
                <LoginForm />
            </div>
            <Separator />
            <p class="relative flex justify-end w-fit text-muted-foreground">
                Logged players can save their scores to keep track of their progress.
            </p>
            <p class="relative flex justify-end w-fit text-muted-foreground">
                Place your first bet and you will be prompted to create an account!.
            </p>
        </div>

    </template>
</template>

<script setup lang="ts">
import { Separator } from '@/components/ui/separator'
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger
} from '@/components/ui/tooltip'
import { logOutUser } from '@/services/authHandler'
import { usePlayerStore } from '@/stores/player'
import { LogOutIcon } from 'lucide-vue-next'
import LoginForm from './LoginForm.vue'
import { Button } from './ui/button'


const store = usePlayerStore();

const LogOut = logOutUser;

</script>