<script setup lang="ts">
import {
    TransitionRoot,
    TransitionChild,
    Dialog,
    DialogPanel,
    DialogTitle,
} from '@headlessui/vue'
import { ref, watch } from 'vue';
import { useGameStore } from '../stores/game';
import { defineProps, defineEmits } from 'vue';

const gameStore = useGameStore();
const isOpen = ref(gameStore.gameOver);

// Accept componentKey as a prop
const props = defineProps({
    componentKey: Number
});

const emit = defineEmits(['update:componentKey']);

const forceRerender = () => {
    const key = props.componentKey ?? 0;
    emit('update:componentKey', key + 1);
};

// Watch for changes in gameStore.gameOver and update isOpen accordingly
watch(() => gameStore.gameOver, (newVal) => {
    isOpen.value = newVal;
});


const closeModal = () => {
    isOpen.value = false;
    // Also update the store if necessary
    gameStore.setGame(false);
    forceRerender();
};

</script>


<template>
    <TransitionRoot appear :show="isOpen" as="template">
        <Dialog as="div" @close="closeModal" class="relative z-10">
            <TransitionChild as="template" enter="duration-300 ease-out" enter-from="opacity-0" enter-to="opacity-100"
                leave="duration-200 ease-in" leave-from="opacity-100" leave-to="opacity-0">
                <div class="fixed inset-0 bg-black/80" />
            </TransitionChild>

            <div class="fixed inset-0 overflow-y-auto">
                <div class="flex items-center justify-center min-h-full p-4 text-center">
                    <TransitionChild as="template" enter="duration-300 ease-out" enter-from="opacity-0 scale-95"
                        enter-to="opacity-100 scale-100" leave="duration-200 ease-in" leave-from="opacity-100 scale-100"
                        leave-to="opacity-0 scale-95">
                        <DialogPanel
                            class="w-full max-w-md p-6 overflow-hidden text-center align-middle transition-all transform bg-white shadow-xl rounded-2xl">
                            <DialogTitle as="h3" class="mb-4 text-3xl font-bold leading-6 tracking-tight text-gray-900">
                                Game Over!
                            </DialogTitle>
                            <div class="mt-8">
                                <p class="mb-4 text-base font-normal text-gray-500">
                                    Sorry you lost! Better luck next time! 🎉
                                </p>
                                <p class="mb-8 text-base font-normal leading-snug text-gray-500">
                                    We'll reset everything for you. If you were logged in you'll still keep your
                                    previous score
                                </p>
                            </div>

                            <div class="relative flex justify-center mt-4">
                                <button type="button"
                                    class="relative justify-center px-4 py-2 text-lg font-medium text-green-900 bg-green-200 border border-transparent rounded-md hover:bg-green-300 focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500 focus-visible:ring-offset-2"
                                    @click="closeModal">
                                    Got it, thanks!
                                </button>
                            </div>
                        </DialogPanel>
                    </TransitionChild>
                </div>
            </div>
        </Dialog>
    </TransitionRoot>
</template>