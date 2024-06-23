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
                <template v-if="enableSave">
                    <p class="py-2">You're logged in, don't forget to save your score.
                        <br>
                        If you don't, you'll lose your progress once window closes
                    </p>
                    <Button disabled v-if="loading">
                        <Loader2 class="w-4 h-4 mr-2 animate-spin" />
                        Please wait
                    </Button>
                    <Button v-else @click="UpdateScore" variant="secondary" class="px-10 ">
                        Save Score
                    </Button>
                </template>
                <template v-else>
                    <p class="py-2 font-semibold text-muted-foreground">Your score has been saved!</p>
                </template>

            </template>
        </div>
    </div>
</template>

<script setup lang="ts">
import RegisterForm from './RegisterForm.vue';
import { ref, watch } from 'vue';
import { usePlayerStore } from '@/stores/player'
import { Button } from './ui/button'
import { updatePlayerScore } from '@/services/scoreHandler'
import handleError from '@/provider/handleError';
import { toastMessage } from '@/provider/toastProvider';
import { Loader2 } from 'lucide-vue-next'

const loading = ref(false);
const enableSave = ref(true);

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
        loading.value = true;

        const response = await updatePlayerScore(store.player.idUsername, store.player.idScore, store.player.score, store.player.accessToken);
        toastMessage('Score Saved successful', response.data, 5000);

        enableSave.value = false;
    }
    catch (error) {
        console.error(error);
        handleError(error);
    }
    finally {
        loading.value = false;
    }
}
</script>