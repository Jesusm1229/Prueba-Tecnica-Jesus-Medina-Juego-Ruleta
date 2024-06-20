<script setup lang="ts">
import { ref, watch, reactive } from 'vue'
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
import LoginForm from './LoginForm.vue'
import RegisterForm from './RegisterForm.vue'
import { usePlayerStore } from '@/stores/player'
import { CategoryTypes, type Bet, type BetResponse } from '@/lib/types'
import { LogOutIcon } from 'lucide-vue-next'
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger
} from '@/components/ui/tooltip'

const { toast } = useToast()

const store = usePlayerStore();

interface UserData {
    username: string;
    score?: number;
}

const state = reactive({
    isUserLoggedIn: false,
    isLoginDialogOpen: false,
    showDialog: false,
    showRegisterDialog: false,
    showSaveScoreDialog: false,
});



const responseData = ref<BetResponse | null>(null);

const initialBetData = {
    category: null,
    score: store.player.score ? store.player.score : 0,
    betAmount: 0,
    color: null,
    number: '0',
};

const betDataObj = ref<Bet>({
    ...initialBetData
});

const userDataObj = ref<UserData>({
    username: '',
});



console.log(store.player.username, "store")

console.log(betDataObj.value, "betDataObj")

let userObject = JSON.parse(localStorage.getItem('UserObject') ?? 'null');

/* let userFormSchema = toTypedSchema(z.object({
    username: z.string({
        required_error: 'Username is required',
        invalid_type_error: 'Username must be a string',
    }).refine(value => {
        if (value.includes(' ')) {
            return 'Username cannot contain spaces';
        }
        return true;
    }),
})); */

const formSchema = toTypedSchema(z.object({
    category: z.enum(Object.keys(CategoryTypes) as [string, ...string[]], {
        required_error: 'Category is required',
        invalid_type_error: 'Category must be a string',
    }),

    score: store.player.score ? z.number({
        invalid_type_error: 'Score must be a number',
    }).int().min(1, 'Score must be at least 1').optional() : z.number({
        required_error: 'Score is required',
        invalid_type_error: 'Score must be a number',
    }).int().min(1, 'Score must be at least 1'),

    betAmount: z.number({
        required_error: 'Bet amount is required',
        invalid_type_error: 'Bet amount must be a number',
    }).int().min(1, 'Bet amount must be at least 1'),

    color: z.enum(['Red', 'Black'], {
        required_error: 'Color is required',
        invalid_type_error: 'Color must be a string',
    }),

    number: z.string().refine(value => value !== '', {
        message: 'Number is required',
        path: ['number'],
    }).optional(),

}).refine(data => (data.category !== CategoryTypes.Straight || data.number !== undefined), {
    message: 'Number is required when category is Straight',
    path: ['number'],
}))


console.log(betDataObj.value, "betDataObj")

const form = useForm({
    validationSchema: formSchema,
})
/* 
const userForm = useForm({
    validationSchema: userFormSchema,
}) */

const postBet = async (values: { category: string; betAmount: number; color: "Red" | "Black"; number?: string | undefined; score?: number | undefined }) => {
    try {
        const response = await axios.post('https://localhost:7299/api/bets', values)
        responseData.value = response.data
        console.log(response.data)
        toast({
            title: 'Submission successful',
            description: h('pre', { class: 'mt-2 w-[340px] rounded-md bg-slate-950 p-4' }, h('code', { class: 'text-white' }, JSON.stringify(response.data, null, 2))),
            duration: 5000,
        })
        store.player.score = response.data.newScore;
    } catch (error) {
        handleError(error)
    }
}

const handleError = (error: any) => {
    toast({
        title: "An error occurredaaaa",
        description: h('div', { class: ' text-wrap' }, error.response.status + ": " + error.response.data),
        duration: 6000,
        variant: "destructive"
    })
    if (error.response && error.response.data instanceof z.ZodError) {
        error.response.data.issues.forEach((issue: { message: string | number | boolean | VNodeArrayChildren | { [name: string]: unknown; $stable?: boolean } | VNode<RendererNode, RendererElement, { [key: string]: any }> | (() => any) | undefined }) => {
            toast({
                title: 'An erroreeeee occurred',
                description: h('div', { class: '' }, h('text', { class: 'text-white' }, issue.message)),
                duration: 5000,
            })
        });
    }
}

const onSubmit = form.handleSubmit((values) => {
    console.log(values, "values")

    /* if (state.isUserLoggedIn) {
        values.score = userObject.score;
    }
 */
    if (store.player.score) {
        values.score = store.player.score;
    }

    postBet(values)
})




/* const userFormSubmit = userForm.handleSubmit((values) => {

    const userObject = {
        accessToken: '',
        refreshToken: '',
        idUsername: '',
        username: values.username,
        idScore: '',
        score: 0
    }

    axios.post('https://localhost:7299/api/authentication/login', values)
        .then((response) => {
            toast({
                title: 'Login successful',
                duration: 5000,
            })
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
        })
        .then((response) => {
            state.isUserLoggedIn = true;
        })
        .catch((error) => {
            toast({
                title: "An error occurred",
                description: h('div', { class: ' text-wrap' }, error.response ? error.response.status + ": " + error.response.data : error),
                duration: 6000,
                variant: "destructive"
            });
            state.isUserLoggedIn = false;
        });



    console.log(localStorage.getItem('UserObject'), "user Object")})
 */



</script>

<script lang="ts">

export default {
    data() {


        return {
            numbers: Array.from({ length: 37 }, (_, i) => i),
            players: [],

        }
    },
    methods: {
        async fetchPlayers() {
            try {
                const response = await axios.get('https://localhost:7299/api/players')
                this.players = response.data
                console.log(this.players)
            } catch (error) {
                console.error(error)
            }
        },

        updateScore(event: { stopPropagation: () => void }) {

            event.stopPropagation();

            console.log(store.player.idUsername, store.player.idScore, store.player.score)

            const score = {
                points: store.player.score
            }

            axios.put('https://localhost:7299/api/players/' + store.player.idUsername + '/scores/' + store.player.idScore, {
                score
            }, {
                headers: {
                    'Authorization': `Bearer ${store.player.accessToken}`
                }
            })
                .then((response) => {
                    console.log(response.data)
                    toast({
                        title: 'Score saved',
                        description: h('div', { class: ' text-wrap' }, response.data),
                        duration: 5000,
                    })
                })
                .catch((error) => {
                    console.error(error)
                    toast({
                        title: "An error occurred",
                        description: h('div', { class: ' text-wrap' }, error.response ? error.response.status + ": " + error.response.data : error),
                        duration: 6000,
                        variant: "destructive"
                    });
                });
        },



        /* async UpdateScore() {
            try {
                const response = await axios.put('https://localhost:7299/api/players/' + store.player.idUsername + '/scores' + store.player.idScore, {
                    points: store.player.score
                }, {
                    headers: {
                        'Authorization': `Bearer ${store.player.accessToken}`
                    }
                })
                console.log(response.data)
            } catch (error) {
                console.error(error)
            }
        } ,*/


        closeDialog() {
            state.showDialog = false
        },

        closeRegisterDialog() {
            state.showDialog = false
        }

    },
    mounted() {
        this.fetchPlayers()
    },
}

</script>

<template>
    <div class="flex-1 p-8 pt-6 mx-4 space-y-4 border">
        <div class="flex items-center justify-between space-y-2">
            <h2 class="text-3xl font-bold">Unilink Win Wheel</h2>
            <div>
                <template v-if="store.player.accessToken">
                    <div class="relative flex flex-row justify-center gap-2 text-lg text-center col-span-full">
                        <p class="relative my-auto">Enjoy your game,
                            <strong class="font-semibold">
                                {{ store.player.username }}
                            </strong>
                        </p>
                        <Separator orientation="vertical" class="bg-rose-500" />
                        <TooltipProvider>
                            <Tooltip>
                                <TooltipTrigger> <Button variant="outline" size="icon">
                                        <LogOutIcon class="w-4 h-4" />
                                    </Button></TooltipTrigger>
                                <TooltipContent>
                                    <p>LogOut</p>
                                </TooltipContent>
                            </Tooltip>
                        </TooltipProvider>

                    </div>

                </template>
                <template v-else>
                    <div class="relative flex flex-col self-end justify-end gap-4 text-sm text-right justify-right">
                        <div class="">
                            <LoginForm @close-dialog="closeDialog" />
                        </div>
                        <Separator />
                        <p class="relative flex justify-end w-fit text-muted-foreground">
                            Logged players can save their scores to keep track of their progress.
                        </p>
                    </div>

                </template>


            </div>
        </div>

        <Tabs default-value="play" class="w-full">
            <TabsList>
                <TabsTrigger value="play">
                    Play
                </TabsTrigger>
                <TabsTrigger value="password">
                    Top Winners
                </TabsTrigger>
            </TabsList>

            <TabsContent value="play" class="space-y-4 ">
                <div class="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
                    <form @submit.prevent="onSubmit" class="col-span-2">
                        <div class="flex items-center justify-end mb-4 space-y-2">
                            <div class="flex space-x-4 flex-end">
                                <Button type="submit" class="px-10 bg-green-600">Spin Wheel!</Button>
                            </div>
                        </div>
                        <div class="grid gap-4 col-span-full md:grid-cols-2 lg:grid-cols-2 lg:grid-rows-3">

                            <!-- Category -->
                            <Card>
                                <CardHeader class="flex flex-row items-center justify-between pb-2 space-y-0">
                                    <CardTitle class="text-2xl font-bold">Set yout game!</CardTitle>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" version="1"
                                        id="casino">
                                        <g transform="translate(-126.5 -1363.862)">
                                            <circle style="isolation:auto;mix-blend-mode:normal" cx="142.5"
                                                cy="1379.862" r="15.5" fill="#f55" stroke="#373748"
                                                stroke-linecap="round" stroke-linejoin="round" color="#000"
                                                overflow="visible"></circle>
                                            <circle style="isolation:auto;mix-blend-mode:normal" cx="142.5"
                                                cy="1379.862" r="10.875" fill="#f55" stroke="#373748"
                                                stroke-linecap="round" stroke-linejoin="round" color="#000"
                                                overflow="visible"></circle>
                                            <g style="line-height:125%">
                                                <path fill="#373748"
                                                    d="M485.563 507.11c0 1.5 1.612 1.552 1.612 2.632 0 .375-.165.51-.427.51-.263 0-.428-.135-.428-.51v-.412h-.78v.36c0 .712.3 1.17.892 1.29v.382h.675v-.382c.585-.12.893-.578.893-1.29 0-1.5-1.613-1.553-1.613-2.633 0-.375.15-.517.413-.517s.412.142.412.517v.218h.78v-.165c0-.713-.3-1.163-.885-1.283v-.39h-.675v.39c-.577.128-.87.578-.87 1.283z"
                                                    font-family="Bebas Neue" font-size="7.5" font-weight="700"
                                                    letter-spacing="0"
                                                    transform="translate(-1007.675 178.579) scale(2.36287)"
                                                    word-spacing="0"
                                                    style="-inkscape-font-specification:'Bebas Neue Bold'">
                                                </path>
                                            </g>
                                            <path fill="#373748" fill-rule="evenodd" stroke="#373748"
                                                d="M140 1394.962v-4.1h5v4.1zm0-26.1v-4.1h5v4.1zM127.4 1377.362h4.1v5h-4.1zm26.1 0h4.1v5h-4.1zM130.055 1388.772l2.899-2.9 3.535 3.536-2.899 2.9zm18.455-18.456 2.9-2.899 3.535 3.536-2.9 2.899zM154.945 1388.772l-2.9-2.9-3.535 3.536 2.9 2.9zm-18.456-18.456-2.899-2.899-3.535 3.536 2.899 2.899z">
                                            </path>
                                        </g>
                                    </svg>
                                </CardHeader>
                                <CardContent>
                                    <!-- <div class="text-lg font-semibold">
                                    Category
                                </div> -->
                                    <FormField v-slot="{ componentField }" name="category">
                                        <FormItem>
                                            <FormLabel class="text-lg font-semibold">Category</FormLabel>
                                            <Select v-bind="componentField" v-model="betDataObj.category">
                                                <FormControl>
                                                    <SelectTrigger>
                                                        <SelectValue placeholder="Select a category" />
                                                    </SelectTrigger>
                                                </FormControl>
                                                <SelectContent>
                                                    <SelectGroup>
                                                        <SelectItem value="Straight">
                                                            Straight
                                                        </SelectItem>
                                                        <SelectLabel>Number</SelectLabel>
                                                        <SelectItem value="Even">
                                                            Even
                                                        </SelectItem>
                                                        <SelectItem value="Odd">
                                                            Odd
                                                        </SelectItem>
                                                        <SelectLabel>Full color</SelectLabel>
                                                        <SelectItem value="Color">
                                                            Color
                                                        </SelectItem>

                                                    </SelectGroup>
                                                </SelectContent>
                                            </Select>
                                            <FormMessage />
                                            <FormDescription>
                                                <ul class="pt-4">
                                                    <li>
                                                        <p
                                                            class="mb-2 text-sm font-normal leading-none text-muted-foreground">
                                                            <strong>Straight: </strong>select a specific number & color
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p
                                                            class="mb-2 text-sm font-normal leading-none text-muted-foreground">
                                                            <strong>Even: </strong>any even number from a color you
                                                            choose
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p
                                                            class="mb-2 text-sm font-normal leading-none text-muted-foreground">
                                                            <strong>Odd: </strong>any odd number from a color you choose
                                                        </p>
                                                    </li>
                                                    <li>
                                                        <p
                                                            class="mb-2 text-sm font-normal leading-none text-muted-foreground">
                                                            <strong>Color: </strong>choose between red or black
                                                        </p>
                                                    </li>

                                                </ul>

                                            </FormDescription>

                                        </FormItem>
                                    </FormField>

                                </CardContent>
                            </Card>


                            <!-- Bet configuration -->
                            <Card>
                                <CardHeader class="flex flex-row items-center justify-between pb-2 space-y-0">
                                    <CardTitle class="text-2xl font-bold">Place yout Bet</CardTitle>
                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none"
                                        stroke="currentColor" strokeLinecap="round" strokeLinejoin="round"
                                        strokeWidth="2" class="w-4 h-4 text-muted-foreground">
                                        <path d="M12 2v20M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6" />
                                    </svg>
                                </CardHeader>
                                <CardContent class="grid grid-cols-2 gap-6">
                                    <template v-if="store.player.score">
                                        <div class="flex-col">
                                            <div class="text-lg font-semibold">
                                                Your Score
                                            </div>
                                            <div class="text-2xl font-bold">
                                                {{ store.player.score }}
                                            </div>
                                        </div>
                                    </template>
                                    <template v-else>
                                        <div class="grid gap-2">
                                            <FormField v-slot="{ componentField }" name="score">
                                                <FormItem v-auto-animate>
                                                    <FormLabel class="text-lg font-semibold">Score</FormLabel>
                                                    <FormControl>
                                                        <Input type="number" placeholder="200" v-bind="componentField"
                                                            v-model="betDataObj.score" />
                                                    </FormControl>
                                                    <FormDescription>
                                                        This is your score in the game.
                                                    </FormDescription>
                                                    <FormMessage />
                                                </FormItem>
                                            </FormField>
                                        </div>
                                    </template>
                                    <div class="grid gap-2">
                                        <FormField v-slot="{ componentField }" name="betAmount">
                                            <FormItem v-auto-animate>
                                                <FormLabel class="text-lg font-semibold">Bet Amount</FormLabel>
                                                <FormControl>
                                                    <Input type="number" placeholder="100"
                                                        v-model="betDataObj.betAmount" v-bind="componentField" />
                                                </FormControl>
                                                <FormDescription>
                                                    The amount you want to bet.
                                                </FormDescription>
                                                <FormMessage />
                                            </FormItem>
                                        </FormField>
                                    </div>

                                </CardContent>
                            </Card>




                            <!-- Choose your position -->

                            <Card class="col-span-2 col-start-1 row-span-2 row-start-2">
                                <CardHeader>
                                    <CardTitle class="text-2xl font-bold">Choose your position</CardTitle>
                                </CardHeader>
                                <CardContent class="grid gap-6">
                                    <FormField v-slot="{ componentField }" type="radio" name="color">
                                        <FormItem class="space-y-3">
                                            <FormLabel class="text-lg font-semibold">Color</FormLabel>
                                            <FormControl>
                                                <RadioGroup v-bind="componentField" class="grid grid-cols-4 gap-4">
                                                    <FormItem>
                                                        <FormControl>
                                                            <div>
                                                                <RadioGroupItem id="Red" value="Red"
                                                                    class="sr-only peer" />
                                                                <Label for="Red"
                                                                    class="flex flex-row items-center justify-between rounded-md border-2 border-muted bg-popover p-4 hover:bg-accent hover:text-accent-foreground peer-data-[state=checked]:border-rose-800 [&:has([data-state=checked])]:border-primary">
                                                                    <div
                                                                        className='p-3 border rounded-full bg-rose-800'>
                                                                    </div>
                                                                    Red
                                                                </Label>
                                                            </div>
                                                        </FormControl>
                                                    </FormItem>
                                                    <FormItem>
                                                        <FormControl>
                                                            <div>
                                                                <RadioGroupItem id="Black" value="Black"
                                                                    class="sr-only peer" />
                                                                <Label for="Black"
                                                                    class="flex flex-row items-center justify-between rounded-md border-2 border-muted bg-popover p-4 hover:bg-accent hover:text-accent-foreground peer-data-[state=checked]:border-primary [&:has([data-state=checked])]:border-primary">
                                                                    <div className='p-3 border rounded-full bg-black'>
                                                                    </div>
                                                                    Black
                                                                </Label>
                                                            </div>
                                                        </FormControl>
                                                    </FormItem>
                                                </RadioGroup>
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    </FormField>
                                    <Separator class="my-4" />
                                    <FormField v-slot="{ componentField }" name="number">
                                        <FormItem class="space-y-3">
                                            <FormLabel class="text-lg font-semibold">Number</FormLabel>
                                            <FormControl>
                                                <RadioGroup v-bind="componentField" class="grid grid-cols-8">
                                                    <FormItem v-for="(number, index) in numbers" :key="index">
                                                        <FormControl>
                                                            <div>
                                                                <RadioGroupItem :id="number.toString()"
                                                                    :value="number.toString()" class="sr-only peer"
                                                                    :disabled="betDataObj.category !== CategoryTypes.Straight" />
                                                                <Label :for="number.toString()"
                                                                    class="flex flex-row items-center justify-center rounded-md border-2 border-muted bg-popover p-2 hover:bg-accent hover:text-accent-foreground peer-data-[state=checked]:border-primary [&:has([data-state=checked])]:border-primary">
                                                                    {{ number }}
                                                                </Label>
                                                            </div>
                                                        </FormControl>
                                                    </FormItem>
                                                </RadioGroup>
                                                <FormMessage v-show="betDataObj.category === CategoryTypes.Straight" />
                                            </FormControl>
                                        </FormItem>
                                    </FormField>
                                </CardContent>

                            </Card>


                        </div>
                    </form>
                    <!--  Result -->
                    <Card class="col-span-2">
                        <CardHeader>
                            <CardTitle class="text-2xl font-bold">Results</CardTitle>
                        </CardHeader>
                        <CardContent class="grid grid-cols-2">
                            <div class="flex-col">
                                <template v-if="responseData">
                                    <template v-if="!store.player.accessToken">
                                        <p class="py-2">Save your score with an account.
                                            <br>
                                            If you don't, you'll lose your
                                            score once window closes
                                        </p>
                                        <RegisterForm @close-dialog="closeRegisterDialog" />
                                    </template>
                                    <template v-if="store.player.accessToken">
                                        <p class="py-2">You're logged in, don't forget to save your score.
                                            <br>
                                            If you don't, you'll lose your
                                            score once window closes
                                        </p>
                                        <Button @click="updateScore($event)" variant="secondary" class="px-10 ">Save
                                            Score</Button>
                                    </template>
                                </template>
                                <!-- save score  -->


                            </div>
                            <div class="flex-col justify-end col-span-1">
                                <div class="flex-col text-right justify-right">
                                    <div class="mb-4 text-3xl font-bold tracking-tight ">
                                        <div>
                                            {{ !responseData ? "Waiting your bet" :
                                                responseData?.didIWin == true ? "You Win!" : "You Lose" }}
                                        </div>
                                    </div>
                                    <div class="text-2xl flex-end text-muted-foreground ">

                                        <pre>{{ !responseData ? " " : responseData.newScore + " :New Score" }}</pre>
                                    </div>
                                    <div class="text-lg flex-end text-muted-foreground ">

                                        <pre>{{ !responseData ? " " : responseData.winnerNumber + " :Winning Number" }}</pre>
                                    </div>
                                    <div class="text-lg flex-end text-muted-foreground ">
                                        <pre>{{ !responseData ? " " : responseData.winnerColor + " :Winning Color" }}</pre>
                                    </div>
                                </div>
                            </div>

                            <Separator class="my-4 col-span-full" />

                        </CardContent>
                    </Card>
                </div>
            </TabsContent>
            <TabsContent value="top Winners">
                Top Winners
            </TabsContent>

        </Tabs>
    </div>

</template>





<style></style>