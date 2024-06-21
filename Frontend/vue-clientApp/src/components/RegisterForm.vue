<template>
    <Dialog>
        <DialogTrigger as-child>
            <Button>
                Register
            </Button>
        </DialogTrigger>

        <DialogContent class="sm:max-w-[425px]">
            <form @submit.prevent="userFormSubmit">
                <DialogHeader>
                    <DialogTitle>Register Profile</DialogTitle>
                    <DialogDescription>
                        We'll save your score for you. The next time you play, you'll be
                        able to continue with your score progress.
                    </DialogDescription>
                </DialogHeader>
                <div class="grid gap-4 py-4">
                    <FormField v-slot="{ componentField }" name="username">
                        <FormItem v-auto-animate>
                            <FormLabel class="text-lg font-semibold">Username
                            </FormLabel>
                            <FormControl>
                                <Input type="text" placeholder="" v-bind="componentField"
                                    v-model="userDataObj.username" />
                            </FormControl>
                            <FormDescription>
                                This is your score in the game.
                            </FormDescription>
                            <FormMessage />
                        </FormItem>
                    </FormField>
                </div>
                <DialogFooter>
                    <Button type="submit">
                        Register
                    </Button>
                </DialogFooter>
            </form>
        </DialogContent>
    </Dialog>
</template>

<script setup lang="ts">
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger, } from '@/components/ui/dialog'
import { Input } from '@/components/ui/input'
import { useToast } from '@/components/ui/toast/use-toast'
import { usePlayerStore } from '@/stores/player'
import { vAutoAnimate } from '@formkit/auto-animate/vue'
import { toTypedSchema } from '@vee-validate/zod'
import axios from 'axios'
import { useForm } from 'vee-validate'
import { h, ref } from 'vue'
import * as z from 'zod'
import { Button } from './ui/button'
import { FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage } from './ui/form'
import { toastMessage } from '@/provider/toastProvider'
import { loginUser, registerUser } from '@/services/authHandler'
import { scheduleTokenRefresh } from '@/services/tokenHandler'
import { fetchPlayerById } from '@/services/playerHandler'
import { fetchScoresById } from '@/services/scoreHandler'

const toast = useToast();
const store = usePlayerStore();

const userDataObj = ref({
    username: '',
});

const userFormSchema = toTypedSchema(z.object({
    username: z.string().refine(value => !value.includes(' '), 'Username cannot contain spaces'),
}));

const userForm = useForm({
    validationSchema: userFormSchema,
});

const userFormSubmit = userForm.handleSubmit(async (values) => {
    try {
        const newuserObject = {
            username: values.username,
            score: { points: store.player?.score ?? 0 },
        };

        const registerResponse = await registerUser(newuserObject);
        console.log(registerResponse, 'registerResponse');
        toastMessage(
            'Register successful.',
            h('div', { class: 'text-sm' }, 'We are retrieving your data.'),
            500
        );

        const loginResponse = await loginUser(values);
        console.log(loginResponse)
        const accessToken = loginResponse.accessToken;

        const playerResponse = await fetchPlayerById(values.username, accessToken);
        console.log(playerResponse, 'playerResponse')
        const scoresResponse = await fetchScoresById(playerResponse.data.id, accessToken);
        console.log(scoresResponse, 'scoresResponse')

        const userObject = {
            accessToken: loginResponse.accessToken,
            refreshToken: loginResponse.refreshToken,
            idUsername: playerResponse.data.id,
            username: values.username,
            idScore: scoresResponse.data.id,
            score: scoresResponse.data.points,
        };

        localStorage.setItem('UserObject', JSON.stringify(userObject));

        toastMessage(
            'Done!',
            h('div', { class: 'text-wrap' }, `Welcome ${userObject.username}. Your score is ${userObject.score}`),
            5000);

        store.setPlayer(userObject);
        scheduleTokenRefresh();

    } catch (error: unknown) {
        console.error(error);
        let errorMessage = 'An unexpected error occurred';
        // Check if error is an instance of Error
        if (error instanceof Error) {
            errorMessage = error.message;
        }
        // Further type check if error has a 'response' property
        const axiosError = error as { response?: { status: number; data: any } };
        if (axiosError.response) {
            const { status, data } = axiosError.response;
            errorMessage = `${status}: ${Object.values(data).flat().join()}`;
        }
        // Use toastMessage with the determined errorMessage
        toastMessage(
            "An error occurred",
            h('div', { class: 'text-wrap' }, errorMessage),
            6000,
            "destructive"
        );
    }
});




</script>
