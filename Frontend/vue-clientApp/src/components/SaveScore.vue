<template>
    <div class="flex-col">
        <div v-if="responseData">
            <template v-if="!store.player.accessToken">
                <p class="py-2">Save your score with an account.
                    <br>
                    If you don't, you'll lose it once window closes
                </p>
                <RegisterForm />
            </template>
            <template v-if="store.player.accessToken">
                <p class="py-2">You're logged in, don't forget to save your score.
                    <br>
                    If you don't, you'll lose your progress once window closes
                </p>
                <Button @click="UpdateScore" variant="secondary" class="px-10 ">
                    Save Score
                </Button>
            </template>
        </div>
    </div>
</template>

<script setup>


import RegisterForm from './RegisterForm.vue';
import { ref, watch } from 'vue';
import { usePlayerStore } from '@/stores/player'
import { Button } from './ui/button'
import { updatePlayerScore } from '@/services/scoreHandler'
import handleError from '@/provider/handleError';
import { toastMessage } from '@/provider/toastProvider';

const store = usePlayerStore();

const props = defineProps({
    responseData: Object,
});

const responseData = ref(props.responseData);

watch(() => props.responseData, (newValue) => {
    responseData.value = newValue;
}, { deep: true });


const UpdateScore = async () => {
    try {
        const response = await updatePlayerScore(store.player.idUsername, store.player.idScore, store.player.score, store.player.accessToken);
        toastMessage('Score Saved successful', response.data, 5000);
    }
    catch (error) {
        console.error(error);
        handleError(error);
    }
}
</script>