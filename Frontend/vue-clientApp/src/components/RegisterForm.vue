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
import { defineEmits, h, ref } from 'vue'
import * as z from 'zod'
import { Button } from './ui/button'
import { FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage } from './ui/form'


const { toast } = useToast()

interface UserData {
    username: string;
    score?: number;
}

/* const stateLogin = reactive({
    isUserLoggedIn: false,
    isLoginDialogOpen: false,
}); */

const userDataObj = ref<UserData>({
    username: '',
});

/* console.log(betDataObj.value, "betDataObj") */

let userObject = JSON.parse(localStorage.getItem('UserObject') ?? 'null') ?? {};

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

const showDialog = ref(false);
const emit = defineEmits(['close-dialog']);

function closeRegisterDialog() {
    showDialog.value = false;
    emit('close-dialog');
}


const userFormSubmit = userForm.handleSubmit((values) => {

    console.log('Register Form submitted!', values)

    const userObject = {
        accessToken: '',
        refreshToken: '',
        idUsername: '',
        username: values.username,
        idScore: '',
        score: 0
    }

    const newuserObject = {
        username: values.username,
        score: {
            points: store.player?.score ?? 0,
        }
    }


    axios.post('https://localhost:7299/api/authentication', newuserObject)
        .then(
            (response) => {
                toast({
                    title: 'Register successful. Wait a moment while we do our magic!',
                    duration: 5000,
                })
                userObject.accessToken = response.data.accessToken;
                userObject.refreshToken = response.data.refreshToken;

                return axios.post('https://localhost:7299/api/authentication/login', values)
                    .then((response) => {
                        /* toast({
                            title: 'Login successful',
                            duration: 5000,
                        }) */
                        userObject.accessToken = response.data.accessToken;
                        userObject.refreshToken = response.data.refreshToken;

                        return axios.get('https://localhost:7299/api/players/' + values.username, {
                            headers: {
                                'Authorization': `Bearer ${userObject.accessToken}`
                            }
                        });
                    })
                    .then((response) => {
                        userObject.idUsername = response.data.id;

                        return axios.get('https://localhost:7299/api/players/' + userObject.idUsername + '/scores', {
                            headers: {
                                'Authorization': `Bearer ${userObject.accessToken}`
                            }
                        });
                    })
                    .then((response) => {
                        userObject.idScore = response.data.id;
                        userObject.score = response.data.points;

                        localStorage.setItem('UserObject', JSON.stringify(userObject));
                        toast({
                            title: 'Done!',
                            duration: 5000,
                        })
                    })

            }
        ).then(() => {
            /*    stateLogin.isUserLoggedIn = true; */
            closeRegisterDialog();

        })

        /* .then((response) => {
            userObject.idScore = response.data.id;
            userObject.score = response.data.points;

            localStorage.setItem('UserObject', JSON.stringify(userObject));
        })
        .then((response) => {
            stateLogin.isUserLoggedIn = true;
            closeDialog();

        }) */
        .catch((error) => {
            console.log(error.response.data);
            console.log(error.response.status);
            console.log(error.response.headers);

            console.log(Object.values(error.response.data).flat().join());
            console.log(error.response.data.errors);
            toast({
                title: "An error occurred",
                description: h('div', { class: ' text-wrap' }, error.response ? error.response.status + ": " + Object.values(error.response.data).flat().join() : error),
                duration: 6000,
                variant: "destructive"
            });
            console.log(error);

        });

    /*  let userResult = JSON.parse(localStorage.getItem('UserObject') ?? 'null'); */

    store.setPlayer(userObject);

    console.log(localStorage.getItem('UserObject'), "user Object")


})





</script>
