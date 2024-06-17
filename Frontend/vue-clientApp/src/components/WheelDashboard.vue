<script setup lang="ts">
import { ref } from 'vue'
import { Button } from './ui/button'
import { Select, SelectTrigger, SelectContent, SelectGroup, SelectLabel, SelectItem, SelectValue } from './ui/select'
import { Tabs, TabsList, TabsTrigger, TabsContent } from './ui/tabs'
import { Card } from './ui/card'
import { CardTitle, CardHeader } from './ui/card';
import CardContent from './ui/card/CardContent.vue';
import { Input } from '@/components/ui/input'
import { useForm } from 'vee-validate'
import { toTypedSchema } from '@vee-validate/zod'
import * as z from 'zod'
import {
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from './ui/form'
import { h, type RendererElement, type RendererNode, type VNode, type VNodeArrayChildren } from 'vue';
import { useToast } from '@/components/ui/toast/use-toast'
import { vAutoAnimate } from '@formkit/auto-animate/vue'
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'
import { Separator } from '@/components/ui/separator'
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
} from '@/components/ui/dialog'

import { Label } from '@/components/ui/label'

const { toast } = useToast()

interface ResponseData {
    newScore: number;
    winnerNumber: number;
    winnerColor: string;
    didIWin: boolean;
}

const formSchema = toTypedSchema(z.object({
    category: z.enum(['Straight', 'Even', 'Odd', 'Red', 'Black'], {
        required_error: 'Category is required',
        invalid_type_error: 'Category must be a string',
    }),

    score: z.number({
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

    number: z.string({
    }),

}))



/* const { handleSubmit } = useForm({
    validationSchema: formSchema,
})
 */

const form = useForm({
    validationSchema: formSchema,
})

const responseData = ref<ResponseData | null>(null);


const onSubmit = form.handleSubmit((values) => {
    console.log(values)
    axios.post('https://localhost:7299/api/bets', values)
        .then((response) => {
            responseData.value = response.data
            console.log(response.data)
            toast({
                title: 'Submission successful',
                description: h('pre', { class: 'mt-2 w-[340px] rounded-md bg-slate-950 p-4' }, h('code', { class: 'text-white' }, JSON.stringify(response.data, null, 2))),
                duration: 5000,

            })
        })
        .catch((error) => {
            toast({
                title: 'An error occurred',
                description: h('pre', { class: 'mt-2 w-[340px] rounded-md  p-4' }, h('code', { class: 'text-white' }, error.message)),
                duration: 5000,
                variant: "destructive"

            })
            if (error.response && error.response.data instanceof z.ZodError) {
                error.response.data.issues.forEach((issue: { message: string | number | boolean | VNodeArrayChildren | { [name: string]: unknown; $stable?: boolean; } | VNode<RendererNode, RendererElement, { [key: string]: any; }> | (() => any) | undefined; }) => {
                    toast({
                        title: 'An error occurred',
                        description: h('pre', { class: 'mt-2 w-[340px] rounded-md bg-slate-950 p-4' }, h('code', { class: 'text-white' }, issue.message)),
                        duration: 5000,

                    })
                });
            }
        });
})

/* const onSubmit = handleSubmit((values) => {
    try {
        toast({
            title: 'You submitted the following values:',
            description: h('pre', { class: 'mt-2 w-[340px] rounded-md bg-slate-950 p-4' }, h('code', { class: 'text-white' }, JSON.stringify(values, null, 2))),
        })
    } catch (error) {
        if (error instanceof z.ZodError){
            toast({
                title: 'An error occurred',
                description: h('pre', { class: 'mt-2 w-[340px] rounded-md bg-slate-950 p-4' }, h('code', { class: 'text-white' }, error.message)),
                duration: 5000,

            })
        }
    }
}) */

</script>

<script lang="ts">
import axios from 'axios'

export default {
    data() {
        return {
            posts: [],
            numbers: Array.from({ length: 37 }, (_, i) => i)
        }
    },
    mounted() {
        axios
            .get('https://localhost:7299/api/players')
            .then((response) => {
                console.log(response.data)
            })
    }
}

/* data() {
        return {
            numbers: Array.from({ length: 37 }, (_, i) => i)
        }
    } */

</script>



<template>
    <div class="flex-1 p-8 pt-6 mx-4 space-y-4 border">
        <div class="flex items-center justify-between space-y-2">
            <h2 class="text-3xl font-bold">Unilink Win Wheel</h2>
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

            <TabsContent value="play" class="space-y-4">
                <form @submit="onSubmit">
                    <div class="flex items-center justify-end mb-4 space-y-2">
                        <div class="flex space-x-4 flex-end">
                            <Button type="submit" class="px-10 bg-green-600">Spin Wheel!</Button>
                        </div>
                    </div>
                    <div class="grid gap-4 md:grid-cols-2 lg:grid-cols-4 lg:grid-rows-3">

                        <!-- Category -->
                        <Card>
                            <CardHeader>
                                <CardTitle class="text-2xl font-bold">Set yout game!</CardTitle>
                            </CardHeader>
                            <CardContent>
                                <!-- <div class="text-lg font-semibold">
                                    Category
                                </div> -->
                                <FormField v-slot="{ componentField }" name="category">
                                    <FormItem v-auto-animate>
                                        <FormLabel class="text-lg font-semibold">Category</FormLabel>
                                        <Select v-bind="componentField">
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
                                                    <SelectLabel>Colors</SelectLabel>
                                                    <SelectItem value="Red">
                                                        Red
                                                    </SelectItem>
                                                    <SelectItem value="Black">
                                                        Black
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
                                                        <strong>Even: </strong>any even number from a color you choose
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
                                                        <strong>Red: </strong>any red number
                                                    </p>
                                                </li>
                                                <li>
                                                    <p
                                                        class="mb-2 text-sm font-normal leading-none text-muted-foreground">
                                                        <strong>Black: </strong>any black number
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
                                    stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2"
                                    class="w-4 h-4 text-muted-foreground">
                                    <path d="M12 2v20M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6" />
                                </svg>
                            </CardHeader>
                            <CardContent class="grid grid-cols-2 gap-6">
                                <div class="grid gap-2">
                                    <FormField v-slot="{ componentField }" name="score">
                                        <FormItem v-auto-animate>
                                            <FormLabel class="text-lg font-semibold">Score</FormLabel>
                                            <FormControl>
                                                <Input type="number" placeholder="200" v-bind="componentField" />
                                            </FormControl>
                                            <FormDescription>
                                                This is your score in the game.
                                            </FormDescription>
                                            <FormMessage />
                                        </FormItem>
                                    </FormField>
                                </div>
                                <div class="grid gap-2">
                                    <FormField v-slot="{ componentField }" name="betAmount">
                                        <FormItem v-auto-animate>
                                            <FormLabel class="text-lg font-semibold">Bet Amount</FormLabel>
                                            <FormControl>
                                                <Input type="number" placeholder="100" v-bind="componentField" />
                                            </FormControl>
                                            <FormDescription>
                                                The amount you want to bet.
                                            </FormDescription>
                                            <FormMessage />
                                        </FormItem>
                                    </FormField>
                                </div>

                                <div class="relative col-span-full">
                                    <div class="absolute inset-0 flex items-center">
                                        <span class="w-full border-t" />
                                    </div>
                                    <div class="relative flex justify-center text-xs uppercase">
                                        <span class="px-2 bg-background text-muted-foreground">
                                            Or continue with your account
                                        </span>
                                    </div>
                                </div>

                                <div class="grid gap-2">
                                    <Label for="username">Username</Label>
                                    <Input id="username" type="username" placeholder="TomCruise" />
                                </div>

                                <Button class="self-end w-full">
                                    Login
                                </Button>

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
                                                            <RadioGroupItem id="Red" value="Red" class="sr-only peer" />
                                                            <Label for="Red"
                                                                class="flex flex-row items-center justify-between rounded-md border-2 border-muted bg-popover p-4 hover:bg-accent hover:text-accent-foreground peer-data-[state=checked]:border-rose-800 [&:has([data-state=checked])]:border-primary">
                                                                <div className='p-3 border rounded-full bg-rose-800'>
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
                                <FormField v-slot="{ componentField }" name="number" v-if="true">
                                    <FormItem class="space-y-3">
                                        <FormLabel>Number</FormLabel>
                                        <FormControl>
                                            <RadioGroup v-bind="componentField" class="grid grid-cols-8">
                                                <FormItem v-for="(number, index) in numbers" :key="index">
                                                    <FormControl>
                                                        <div>
                                                            <RadioGroupItem :id="number.toString()"
                                                                :value="number.toString()" class="sr-only peer" />
                                                            <Label :for="number.toString()"
                                                                class="flex flex-row items-center justify-center rounded-md border-2 border-muted bg-popover p-2 hover:bg-accent hover:text-accent-foreground peer-data-[state=checked]:border-primary [&:has([data-state=checked])]:border-primary">
                                                                {{ number }}
                                                            </Label>
                                                        </div>
                                                    </FormControl>
                                                </FormItem>
                                            </RadioGroup>
                                            <FormMessage />
                                        </FormControl>
                                    </FormItem>
                                </FormField>
                            </CardContent>

                        </Card>

                        <!--  Result -->
                        <Card class="col-span-2 col-start-3 row-span-3 row-start-1">
                            <CardHeader>
                                <CardTitle class="text-2xl font-bold">Results</CardTitle>
                            </CardHeader>
                            <CardContent class="grid grid-cols-2">
                                <div class="flex-col">
                                    <template v-if="responseData == null">
                                        <p class="py-2">Save your score with an account.
                                            <br>
                                            If you don't you'll lose your
                                            score once closed
                                        </p>
                                        <Dialog>
                                            <DialogTrigger as-child>
                                                <Button>
                                                    Register
                                                </Button>
                                            </DialogTrigger>
                                            <DialogContent class="sm:max-w-[425px]">
                                                <DialogHeader>
                                                    <DialogTitle>Register Profile</DialogTitle>
                                                    <DialogDescription>
                                                        Make changes to your profile here. Click save when you're done.
                                                    </DialogDescription>
                                                </DialogHeader>
                                                <div class="grid gap-4 py-4">
                                                    <div class="grid items-center grid-cols-4 gap-4">
                                                        <Label for="name" class="text-right">
                                                            Name
                                                        </Label>
                                                        <Input id="name" value="Pedro Duarte" class="col-span-3" />
                                                    </div>
                                                    <div class="grid items-center grid-cols-4 gap-4">
                                                        <Label for="username" class="text-right">
                                                            Username
                                                        </Label>
                                                        <Input id="username" value="@peduarte" class="col-span-3" />
                                                    </div>
                                                </div>
                                                <DialogFooter>
                                                    <Button type="submit">
                                                        Save changes
                                                    </Button>
                                                </DialogFooter>
                                            </DialogContent>
                                        </Dialog>


                                    </template>
                                    <!-- save score  -->


                                </div>
                                <div class="flex justify-end col-span-1">
                                    <div class="flex-col text-right justify-right">
                                        <div class="mb-4 text-3xl font-bold tracking-tight ">
                                            <pre>
                {{ !responseData ? "Waiting your bet" : responseData?.didIWin == true ? "You Win!" : "You Lose" }}
            </pre>
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
                </form>
            </TabsContent>
            <TabsContent value="password">
                Change your password here.
            </TabsContent>

        </Tabs>
    </div>

</template>

<style></style>