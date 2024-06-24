<template>
    <div class="grid grid-cols-1 mt-20 md:gap-4 md:grid-cols-2 lg:grid-cols-4 md:mt-0">
        <form @submit="onSubmit" class="flex flex-col-reverse md:flex-col cols-span-full md:col-span-2">
            <div class="flex items-center justify-end mb-4 space-y-2">
                <div class="flex space-x-4 flex-end">
                    <ButtonSpin disabled v-if="loading" class="w-24 h-24 animate-spin">
                        <Loader2 class="w-8 h-8 animate-spin" variant="destructive" />
                        <!-- <span class="text-xl font-bold text-white" style="text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);">
                            Wait
                        </span> -->
                    </ButtonSpin>
                    <ButtonSpin v-else type="submit" class="w-24 h-24">
                        <span class="text-xl font-bold text-white" style="text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);">
                            Spin!
                        </span>
                    </ButtonSpin>
                </div>
            </div>
            <div class="grid-cols-1 gap-4 md:grid col-span-full md:grid-cols-2 lg:grid-cols-2 lg:grid-rows-3">


                <!-- Category -->
                <Card class="col-span-full md:col-span-1">
                    <CardHeader class="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle class="text-2xl font-bold">Set yout game!</CardTitle>
                        <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" version="1" id="casino">
                            <g transform="translate(-126.5 -1363.862)">
                                <circle style="isolation:auto;mix-blend-mode:normal" cx="142.5" cy="1379.862" r="15.5"
                                    fill="#f55" stroke="#373748" stroke-linecap="round" stroke-linejoin="round"
                                    color="#000" overflow="visible">
                                </circle>
                                <circle style="isolation:auto;mix-blend-mode:normal" cx="142.5" cy="1379.862" r="10.875"
                                    fill="#f55" stroke="#373748" stroke-linecap="round" stroke-linejoin="round"
                                    color="#000" overflow="visible"></circle>
                                <g style="line-height:125%">
                                    <path fill="#373748"
                                        d="M485.563 507.11c0 1.5 1.612 1.552 1.612 2.632 0 .375-.165.51-.427.51-.263 0-.428-.135-.428-.51v-.412h-.78v.36c0 .712.3 1.17.892 1.29v.382h.675v-.382c.585-.12.893-.578.893-1.29 0-1.5-1.613-1.553-1.613-2.633 0-.375.15-.517.413-.517s.412.142.412.517v.218h.78v-.165c0-.713-.3-1.163-.885-1.283v-.39h-.675v.39c-.577.128-.87.578-.87 1.283z"
                                        font-family="Bebas Neue" font-size="7.5" font-weight="700" letter-spacing="0"
                                        transform="translate(-1007.675 178.579) scale(2.36287)" word-spacing="0"
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
                                            <p class="mb-2 text-sm font-normal leading-none text-muted-foreground">
                                                <strong>Straight: </strong>select a specific number & color
                                            </p>
                                        </li>
                                        <li>
                                            <p class="mb-2 text-sm font-normal leading-none text-muted-foreground">
                                                <strong>Even: </strong>any even number from a color you
                                                choose
                                            </p>
                                        </li>
                                        <li>
                                            <p class="mb-2 text-sm font-normal leading-none text-muted-foreground">
                                                <strong>Odd: </strong>any odd number from a color you choose
                                            </p>
                                        </li>
                                        <li>
                                            <p class="mb-2 text-sm font-normal leading-none text-muted-foreground">
                                                <strong>Color: </strong>choose between red or black
                                            </p>
                                        </li>

                                    </ul>

                                </FormDescription>

                            </FormItem>
                        </FormField>

                    </CardContent>
                </Card>


                <!-- Bet -->
                <Card>
                    <CardHeader class="flex flex-row items-center justify-between pb-2 space-y-0">
                        <CardTitle class="text-2xl font-bold">Place yout Bet</CardTitle>
                        <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36"
                            xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 64 64" id="money">
                            <defs>
                                <linearGradient id="a" x1="32" x2="32" y1="3" y2="37" gradientUnits="userSpaceOnUse">
                                    <stop offset="0" stop-color="#ffc033"></stop>
                                    <stop offset="1" stop-color="#ef8e2e"></stop>
                                </linearGradient>
                                <linearGradient id="b" x1="36" x2="36" y1="44" y2="52" xlink:href="#a"></linearGradient>
                                <linearGradient id="c" x1="29" x2="29" y1="36" y2="44" xlink:href="#a"></linearGradient>
                                <linearGradient id="d" x1="30" x2="30" y1="52" y2="60" xlink:href="#a"></linearGradient>
                            </defs>
                            <g>
                                <circle cx="32" cy="20" r="17" fill="url(#a)"></circle>
                                <rect width="34" height="8" x="19" y="44" fill="url(#b)" rx="1"></rect>
                                <rect width="34" height="8" x="12" y="36" fill="url(#c)" rx="1"></rect>
                                <rect width="34" height="8" x="13" y="52" fill="url(#d)" rx="1"></rect>
                            </g>
                            <g>
                                <path
                                    d="M33.29,15a2,2,0,0,1,2,2h2a4,4,0,0,0-4-4H33V10.57H31V13h-.29a4,4,0,0,0,0,8H31v4h-.29a2,2,0,0,1-2-2h-2a4,4,0,0,0,4,4H31v2.43h2V27h.29a4,4,0,0,0,0-8H33V15Zm0,6a2,2,0,0,1,0,4H33V21ZM31,19h-.29a2,2,0,0,1,0-4H31Z">
                                </path>
                                <path
                                    d="M32,7A13,13,0,1,0,45,20,13,13,0,0,0,32,7Zm0,24A11,11,0,1,1,43,20,11,11,0,0,1,32,31Z">
                                </path>
                                <path
                                    d="M11,36v8a1,1,0,0,0,1,1h6v6H13a1,1,0,0,0-1,1v8a1,1,0,0,0,1,1H47a1,1,0,0,0,1-1V53h5a1,1,0,0,0,1-1V44a1,1,0,0,0-1-1H47V36a1,1,0,0,0-1-1H40a17,17,0,1,0-16,0H12A1,1,0,0,0,11,36ZM46,59H39V56H37v3H31V56H29v3H23V56H21v3H14V53h3v3h2V53h6v3h2V53h6v3h2V53h6v3h2V53h3Zm6-8H45V48H43v3H37V48H35v3H29V48H27v3H20V45h3v3h2V45h6v3h2V45h6v3h2V45h6v3h2V45h3ZM17,20A15,15,0,1,1,32,35,15,15,0,0,1,17,20ZM13,37h3v3h2V37h6v3h2V37h6v3h2V37h6v3h2V37h3v6H38V40H36v3H30V40H28v3H22V40H20v3H13Z">
                                </path>
                            </g>
                        </svg>
                    </CardHeader>
                    <CardContent class="grid grid-cols-2 gap-6">
                        <template v-if="store.player.score">
                            <div class="flex-col col-span-full">
                                <div class="text-lg font-semibold">
                                    Your Score
                                </div>
                                <div class="text-2xl font-bold">
                                    {{ store.player.score }}
                                </div>
                            </div>
                        </template>
                        <template v-else>
                            <div class="grid gap-2 col-span-full">
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
                        <div class="grid gap-2 col-span-full">
                            <FormField v-slot="{ componentField }" name="betAmount">
                                <FormItem v-auto-animate>
                                    <FormLabel class="text-lg font-semibold">Bet Amount</FormLabel>
                                    <FormControl>
                                        <Input type="number" placeholder="100" v-model="betDataObj.betAmount"
                                            v-bind="componentField" />
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

                <!-- Posittion -->
                <Card class="col-span-2 col-start-1 row-span-2 row-start-2">
                    <CardHeader>
                        <CardTitle class="text-2xl font-bold">Choose your position</CardTitle>
                    </CardHeader>
                    <CardContent class="grid gap-6">
                        <FormField v-slot="{ componentField }" type="radio" name="color">
                            <FormItem class="space-y-3">
                                <FormLabel class="text-lg font-semibold">Color</FormLabel>
                                <FormControl>
                                    <RadioGroup v-bind="componentField" class="grid grid-cols-2 gap-4 md:grid-cols-4">
                                        <FormItem>
                                            <FormControl>
                                                <div>
                                                    <RadioGroupItem id="Red" value="Red" class="sr-only peer" />
                                                    <Label for="Red"
                                                        class="flex flex-row items-center justify-between rounded-md border-2 border-muted bg-popover p-4 hover:bg-accent hover:text-accent-foreground peer-data-[state=checked]:border-rose-600 [&:has([data-state=checked])]:border-primary">
                                                        <div className='p-3 border rounded-full bg-rose-600'>
                                                        </div>
                                                        Red
                                                    </Label>
                                                </div>
                                            </FormControl>
                                        </FormItem>
                                        <FormItem>
                                            <FormControl>
                                                <div>
                                                    <RadioGroupItem id="Black" value="Black" class="sr-only peer" />
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
                                    <RadioGroup v-bind="componentField" class="grid grid-cols-4 md:grid-cols-8">
                                        <FormItem v-for="(number, index) in numbers" :key="index">
                                            <FormControl>
                                                <div>
                                                    <RadioGroupItem :id="number.toString()" :value="number.toString()"
                                                        class="sr-only peer"
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

        <div class="flex flex-col col-span-2 gap-4">


            <Card class=" h-fit">
                <CardHeader>
                    <CardTitle class="text-2xl font-bold">Results</CardTitle>
                </CardHeader>
                <CardContent class="flex flex-col md:grid md:grid-cols-4">

                    <SaveScoreButton :responseData="responseData" />

                    <div class="relative flex-col justify-end col-span-3">
                        <div class="relative flex-col text-right justify-right">
                            <template v-if="responseData">
                                <div class="mb-4 text-xl font-bold tracking-tight md:text-3xl ">
                                    <div>
                                        {{ responseData?.isWin == true ? "You Win!" : "You Loose" }}
                                    </div>
                                </div>
                                <div
                                    class="flex-row-reverse h-10 mb-2 overflow-hidden text-xl font-bold text-green-600 md:text-4xl text-muted-foreground">
                                    {{ !responseData ? " " : (responseData.newScore) }}


                                </div>
                                <div
                                    class="relative flex justify-end h-10 text-lg font-bold text-white align-middle rounded-full flex-reverse text-muted-foreground">
                                    <div class="relative flex justify-center w-20 h-10 my-auto text-lg font-bold text-white align-middle bg-black rounded-full flex-end"
                                        :class="{ 'bg-rose-600': wheelData.color === 'Red', 'bg-black': wheelData.color === 'Black' }">
                                        <pre
                                            class="justify-center my-auto">{{ !wheelData ? " " : wheelData.number }}</pre>
                                    </div>
                                </div>
                            </template>
                            <template v-else>
                                <div class="mb-4 text-3xl font-bold tracking-tight ">
                                    <div>
                                        {{ "Waiting for your bet" }}
                                    </div>
                                </div>
                            </template>
                        </div>
                    </div>

                    <Separator class="my-4 col-span-full" />

                    <WinningHistory :wheelData="wheelData" />


                </CardContent>
            </Card>

            <Card class="col-span-2 h-96">
            </Card>
        </div>
    </div>


</template>

<script setup lang="ts">
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'
import { Separator } from '@/components/ui/separator'
import { useToast } from '@/components/ui/toast/use-toast'
import { CategoryTypes, type Bet, type WheelResponse } from '@/lib/types'
import handleError from '@/provider/handleError'
import { postBet } from '@/services/betHandler'
import { getCategoryType, getColorType } from '@/services/categoryHandler'
import { spinWheel } from '@/services/wheelHandler'
import { useGameStore } from '@/stores/game'
import { usePlayerStore } from '@/stores/player'
import { vAutoAnimate } from '@formkit/auto-animate/vue'
import { toTypedSchema } from '@vee-validate/zod'
import { useForm } from 'vee-validate'
import { h, reactive, ref } from 'vue'
import * as z from 'zod'
import SaveScoreButton from './SaveScoreButton.vue'
import { Button } from './ui/button'
import { Card, CardHeader, CardTitle } from './ui/card'
import CardContent from './ui/card/CardContent.vue'
import { FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage } from './ui/form'
import { Select, SelectContent, SelectGroup, SelectItem, SelectLabel, SelectTrigger, SelectValue } from './ui/select'
import WinningHistory from './WinningHistory.vue'
import { Loader2 } from 'lucide-vue-next'
import { ButtonSpin } from './ui/buttonSpin'

const numbers = Array.from({ length: 37 }, (_, i) => i);

const { toast } = useToast()

const store = usePlayerStore();
const gameStore = useGameStore();

const loading = ref(false); // Use ref for reactive state


const responseData = ref<Record<string, any> | undefined>(undefined);
const wheelData = ref<WheelResponse | null | undefined>(undefined);

const initialBetData = {
    category: null,
    score: store.player.score || null,
    betAmount: 0,
    color: null,
    number: '0',
};

const betDataObj = ref(ref<Bet>({
    ...initialBetData
}));


/* const winningHistory = ref<Array<WheelResponse>>([]);
 */
const maxValue = 2147483647 - 1;

const formSchema = reactive(toTypedSchema(z.object({
    category: z.enum(Object.keys(CategoryTypes) as [string, ...string[]], {
        required_error: 'Category is required',
        invalid_type_error: 'Category must be a string',
    }),

    score: z.number({
        invalid_type_error: 'Score must be a number',
    }).int().min(1, 'Score must be at least 1').max(maxValue, "Score limit reached").optional(),

    betAmount: z.number({
        required_error: 'Bet amount is required',
        invalid_type_error: 'Bet amount must be a number',
    }).int().min(1, 'Bet amount must be at least 1').max(maxValue, "Bet amount limit reached"),

    color: z.enum(['Red', 'Black'], {
        required_error: 'Color is required',
        invalid_type_error: 'Color must be a string',
    }),

    number: z.string().refine(value => value !== '', {
        message: 'Number is required',
        path: ['number'],
    }).optional(),

})
    .refine(data => (data.category !== CategoryTypes.Straight || data.number !== undefined), {
        message: 'Number is required when category is Straight',
        path: ['number'],
    })
    .refine(data => {

        if (data.category === CategoryTypes.Straight) {
            // Ensure betAmount is not greater than maxValue / 3
            return data.betAmount < Math.floor(maxValue / 3);
        }
        // Check if the category is Even or Odd
        if (data.category === CategoryTypes.Even || data.category === CategoryTypes.Odd) {
            // Ensure betAmount is not greater than maxValue / 2
            return data.betAmount < Math.floor(maxValue / 2);
        }

        return true;
    }, {
        message: 'You exceeded the maximum bet amount for this category',
        path: ['betAmount'],
    })
    .refine(data => {
        if (data.score) {
            return data.score >= data.betAmount;
        }
        return true;
    }, {
        message: 'You do not have enough score to place this bet',
        path: ['betAmount'],
    })

))


const form = useForm({
    validationSchema: formSchema,
})

const onSubmit = form.handleSubmit(async (values) => {
    try {

        loading.value = true;


        const betValues: Bet = ({
            category: getCategoryType(values.category),
            betAmount: values.betAmount,
            color: getColorType(values.color),
            number: values.number,
            score: store.player.score || values.score,
            /*   spinWheelColor: spinWheelResponse.color,
              spinWheelNumber: spinWheelResponse.number */
        });

        submitBet(betValues);

    }
    catch (error) {
        handleError(error);
    }

})

const submitBet = async (values: Bet) => {
    try {

        const spinWheelResponse = await spinWheel();

        values.spinWheelColor = spinWheelResponse.color;
        values.spinWheelNumber = spinWheelResponse.number;

        const response = await postBet(values);

        toast({
            title: 'Submission successful. Good luck',
            description: h('pre', { class: 'mt-2 w-[340px] rounded-md bg-slate-950 p-4' }, h('code', { class: 'text-white' }, JSON.stringify(response, null, 2))),
            duration: 5000,
        });

        wheelData.value = spinWheelResponse;

        responseData.value = response;
        store.player.score = response.newScore;

        gameStore.gameOver = response.isGameOver;


    } catch (error) {
        handleError(error);
    }

    loading.value = false;
}




</script>
