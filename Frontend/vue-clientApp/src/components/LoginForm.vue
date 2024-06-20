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
import { ref, watch, reactive, defineEmits } from 'vue'
import { Button } from './ui/button'
import { Select, SelectTrigger, SelectContent, SelectGroup, SelectLabel, SelectItem, SelectValue } from './ui/select'
import { Tabs, TabsList, TabsTrigger, TabsContent } from './ui/tabs'
import { Card } from './ui/card'
import { CardTitle, CardHeader } from './ui/card';
import CardContent from './ui/card/CardContent.vue';
import { Input } from '@/components/ui/input'
import { useForm, defineRule } from 'vee-validate'
import { toTypedSchema } from '@vee-validate/zod'
import * as z from 'zod'
import { FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage } from './ui/form'
import { h, type RendererElement, type RendererNode, type VNode, type VNodeArrayChildren } from 'vue';
import { useToast } from '@/components/ui/toast/use-toast'
import { vAutoAnimate } from '@formkit/auto-animate/vue'
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'
import { Separator } from '@/components/ui/separator'
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger, } from '@/components/ui/dialog'
import axios from 'axios'
import { Label } from '@/components/ui/label'
import { usePlayerStore } from '@/stores/player'

const { toast } = useToast()

interface UserData {
    username: string;
    score?: number;
}

const stateLogin = reactive({
    isUserLoggedIn: false,
    isLoginDialogOpen: false,
});

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

function closeDialog() {
    showDialog.value = false;
    emit('close-dialog');
}


const userFormSubmit = userForm.handleSubmit((values) => {

    console.log('Form submitted!', values)
    console.log(store.player, "store.player before login")

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
                title: 'Login successful',
                duration: 5000,
            })
            userObject.accessToken = response.data.accessToken;
            userObject.refreshToken = response.data.refreshToken;

            return axios.get('https://localhost:7299/api/players/' + values.username, {//get player
                headers: {
                    'Authorization': `Bearer ${userObject.accessToken}`
                }
            });
        })
        .then((response) => {
            userObject.idUsername = response.data.id;

            return axios.get('https://localhost:7299/api/players/' + userObject.idUsername + '/scores', {//get score stored in database
                headers: {
                    'Authorization': `Bearer ${userObject.accessToken}`
                }
            });
        })
        .then((response) => {
            userObject.idScore = response.data.id;
            userObject.score = response.data.points;

            localStorage.setItem('UserObject', JSON.stringify(userObject));
        })
        .then(() => {
            stateLogin.isUserLoggedIn = true;
            store.setPlayer(userObject);
            closeDialog();

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

    let userResult = JSON.parse(localStorage.getItem('UserObject') ?? 'null');

    console.log(store.player, "store.player")

    console.log(localStorage.getItem('UserObject'), "user Object")


})





</script>

<script>

</script>
