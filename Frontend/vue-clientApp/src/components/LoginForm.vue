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

const { toast } = useToast()

interface UserData {
    username: string;
    score?: number;
}

const userDataObj = ref<UserData>({
    username: '',
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


const userFormSubmit = userForm.handleSubmit((values) => {

    const userObject = {
        accessToken: '',
        refreshToken: '',
        idUsername: '',
        username: values.username,
        idScore: '',
        score: 0
    }

    axios.post('https://localhost:7299/api/authentication/login', values)//login
        .then((response) => {
            toast({
                title: 'Login successful. We are retrieving your data.',
                duration: 5000,
            })
            userObject.accessToken = response.data.accessToken;
            userObject.refreshToken = response.data.refreshToken;

            return axios.get('https://localhost:7299/api/players/' + values.username, {//get player
                headers: {
                    'Authorization': `Bearer ${userObject.accessToken}`
                }
            }).then((response) => {
                userObject.idUsername = response.data.id;
                console.log(response.data, "response.data")

                return axios.get('https://localhost:7299/api/players/' + userObject.idUsername + '/scores', {//get score stored in database
                    headers: {
                        'Authorization': `Bearer ${userObject.accessToken}`
                    }
                }).then((response) => {
                    userObject.idScore = response.data.id;
                    userObject.score = response.data.points;

                    toast({
                        title: 'Data retrieved',
                        description: h('div', { class: ' text-wrap' }, 'Welcome back ' + userObject.username + '. Your score is ' + userObject.score),
                        duration: 5000,

                    })
                })

            })
        }).then(() => {

            store.setPlayer(userObject);

            localStorage.setItem('UserObject', JSON.stringify(userObject));
            console.log(store.player, "store.player")
            scheduleTokenRefresh();
        })
        .catch((error) => {
            toast({
                title: "An error login occurred",
                description: h('div', { class: ' text-wrap' }, error.response ? error.response.status + ": Player doesn't exist" : error),
                duration: 6000,
                variant: "destructive"
            });
            console.log(error);
        });



})



const RefreshToken = () => {
    const token = {
        AccessToken: store.player.accessToken,
        RefreshToken: store.player.refreshToken
    }

    if (typeof token.AccessToken === 'string' && isTokenExpired(token.AccessToken)) {
        /* LogOut(); */
        return;
    }

    axios.post('https://localhost:7299/api/token/refresh', token, {
        headers: {

        }
    }).then((response) => {
        store.player.accessToken = response.data.accessToken;
        store.player.refreshToken = response.data.refreshToken;

        console.log(response.data, "response.data")

        localStorage.setItem('UserObject', JSON.stringify(store.player));


    }).catch((error) => {
        console.error(error)
        /* LogOut(); */
    });

}

const isTokenExpired = (token: string) => {
    try {
        const { exp } = JSON.parse(atob(token.split('.')[1]));
        const expirationDate = new Date(exp * 1000);

        return expirationDate < new Date();

    } catch (error) {
        console.error('Invalid token', error);
        return true;
    }
}

const scheduleTokenRefresh = () => {
    if (store.player.accessToken) {
        const isExpired = isTokenExpired(store.player.accessToken);
        if (!isExpired) {
            const { exp } = JSON.parse(atob(store.player.accessToken.split('.')[1]));
            const expirationDate = new Date(exp * 1000);
            const refreshTime = expirationDate.getTime() - Date.now() - 2 * 60 * 1000; // 2 minutes before expiration

            console.log('Token will be refreshed in', refreshTime, 'ms')
            console.log('Token will expire at', expirationDate)

            if (refreshTime > 0) {
                setTimeout(RefreshToken, refreshTime);
            } else {
                RefreshToken(); // If the token is already expired or will expire in less than 2 minutes, refresh it immediately
            }
        } else {
            console.error('Token is already expired.');
        }
    } else {
        console.error('No access token found.');
    }

}


</script>
