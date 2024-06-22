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
        <Separator class="my-4" />
        <div class="flex items-center my-4 space-x-4 text-sm">
            <ul class="flex-row w-full gap-2">
                <li v-for="(win, index) in winningHistory" :key="index"
                    class="relative flex-row justify-center inline-block h-10 mx-2 my-auto space-x-2">
                    <div class="relative flex justify-center w-10 h-10 text-lg font-bold text-white align-middle rounded-full"
                        :class="{ 'bg-rose-600': win.color === 'Red', 'bg-black': win.color === 'Black' }">
                        <div class="relative flex justify-center my-auto">
                            {{ win.number }}
                        </div>
                    </div>
                    <Separator orientation="vertical" />
                </li>
                <Separator orientation="vertical" />
            </ul>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { WheelResponse } from '@/lib/types';
import { ref, watch, type Ref } from 'vue';

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
        // Keep only the last 10 entries
        winningHistory.value = winningHistory.value.slice(0, 10);
    }
}, { deep: true });
</script>
