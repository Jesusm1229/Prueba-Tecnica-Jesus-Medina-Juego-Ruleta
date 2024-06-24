<template>
    <div class="col-span-full">
        <div class="space-y-1">
            <h4 class="text-base font-medium leading-none">
                Winning History (Last 10)
            </h4>
            <p class="text-sm text-muted-foreground">
                Check the last 10 winning positions
            </p>
        </div>

        <div v-if="loading" class="flex flex-row flex-wrap w-full space-y-3">
            <div class="flex flex-row flex-wrap w-full p-4 md:space-x-2">
                <Skeleton v-for="n in 10" :key="n"
                    class="relative flex justify-center w-10 h-10 my-auto rounded-full" />
            </div>
        </div>
        <div v-else class="flex flex-wrap items-center my-4 space-x-4 text-sm">
            <ul class="flex-row flex-wrap w-full gap-2">
                <li v-for="(win, index) in winningHistory" :key="index"
                    class="relative flex-row flex-wrap justify-center inline-block h-10 mx-2 my-2 space-x-2">
                    <div class="relative flex justify-center w-10 h-10 text-lg font-bold text-white align-middle rounded-full"
                        :class="{ 'bg-rose-600': win.color === 'Red', 'bg-black': win.color === 'Black' }">
                        <div class="relative flex justify-center my-auto">
                            {{ win.number }}
                        </div>
                    </div>
                </li>
            </ul>
        </div>

    </div>
</template>

<script setup lang="ts">
import type { WheelResponse } from '@/lib/types';
import { ref, watch, type Ref } from 'vue';
import { Skeleton } from './ui/skeleton';

const loading = ref(true);

const props = defineProps({
    wheelData: Object as () => WheelResponse | null | undefined,
});


const winningHistory: Ref<WheelResponse[]> = ref([]);

watch(() => props.wheelData, (newVal) => {
    if (newVal) {

        // Add the new winning number and color to the start of the array
        winningHistory.value.unshift({
            number: newVal.number,
            color: newVal.color,
        });
        loading.value = false;
        // Keep only the last 10 entries
        winningHistory.value = winningHistory.value.slice(0, 10);
    }
}, { deep: true });
</script>
