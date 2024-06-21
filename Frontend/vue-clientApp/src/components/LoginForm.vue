<template>
    <Dialog>
        <DialogTrigger as-child>
            <Button>
                Login
            </Button>
        </DialogTrigger>

        <DialogContent class="sm:max-w-[425px]">
            <form @submit.prevent="userFormSubmit">
                <DialogHeader>
                    <DialogTitle>Login</DialogTitle>
                    <DialogDescription>
                        You'll use your stored score
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
                        Login
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
import { jwtDecode } from "jwt-decode";
import { scheduleTokenRefresh } from '@/services/tokenHandler'
import { loginUser } from '@/services/authHandler'

const { toast } = useToast()

interface UserData {
    username: string | null;
    score?: number;
}

const userDataObj = ref<UserData>({
    username: null,
});

/* console.log(betDataObj.value, "betDataObj") */

const store = usePlayerStore()


let userFormSchema = toTypedSchema(z.object({
    username: z.string({
        required_error: 'Username is required',
        invalid_type_error: 'Username must be a string',
    }).refine(value => {
        if (value.includes(' ')) {
            return 'Username cannot contain spaces';
        }
        return true;
    }),
}));

const userForm = useForm({
    validationSchema: userFormSchema,
})


async function getPlayer(username: string, accessToken: string) {
    const response = await axios.get(`https://localhost:7299/api/players/${username}`, {
        headers: {
            'Authorization': `Bearer ${accessToken}`
        }
    });
    return response.data;
}

async function getPlayerScore(idUsername: string, accessToken: string) {
    const response = await axios.get(`https://localhost:7299/api/players/${idUsername}/scores`, {
        headers: {
            'Authorization': `Bearer ${accessToken}`
        }
    });
    return response.data;
}

const userFormSubmit = userForm.handleSubmit(async (values) => {
    try {
        const userObject = {
            accessToken: '',
            refreshToken: '',
            idUsername: '',
            username: values.username,
            idScore: '',
            score: 0
        }

        const loginData = await loginUser(values);
        userObject.accessToken = loginData.accessToken;
        userObject.refreshToken = loginData.refreshToken;

        const playerData = await getPlayer(values.username, userObject.accessToken);
        userObject.idUsername = playerData.id;

        const scoreData = await getPlayerScore(userObject.idUsername, userObject.accessToken);
        userObject.idScore = scoreData.id;
        userObject.score = scoreData.points;

        toast({
            title: 'Data retrieved',
            description: h('div', { class: ' text-wrap' }, 'Welcome back ' + userObject.username + '. Your score is ' + userObject.score),
            duration: 5000,
        });

        store.setPlayer(userObject);
        localStorage.setItem('UserObject', JSON.stringify(userObject));
        console.log(store.player, "store.player")
        scheduleTokenRefresh();
    } catch (error: any) {
        let errorMessage = error;

        if (error.response) {
            errorMessage = error.response.status + ": Player doesn't exist";
        }

        toast({
            title: "An error login occurred",
            description: h('div', { class: ' text-wrap' }, errorMessage),
            duration: 6000,
            variant: "destructive"
        });
        console.log(error);
    }
});
</script>
