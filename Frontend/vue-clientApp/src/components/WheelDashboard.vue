<script setup lang="ts">
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


import { h } from 'vue';

import { toast } from '@/components/ui/toast'
import { vAutoAnimate } from '@formkit/auto-animate/vue'
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group'

import { Separator } from '@/components/ui/separator'

const formSchema = toTypedSchema(z.object({
    category: z.string({
        required_error: 'Category is required',
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
    })
}))

/* const formSchema = toTypedSchema(z.object({
    email: z
        .string({
            required_error: 'Please select an email to display.',
        })
        .email(),
}))
 */
const { handleSubmit } = useForm({
    validationSchema: formSchema,
})

const onSubmit = handleSubmit((values) => {
    toast({
        title: 'You submitted the following values:',
        description: h('pre', { class: 'mt-2 w-[340px] rounded-md bg-slate-950 p-4' }, h('code', { class: 'text-white' }, JSON.stringify(values, null, 2))),
    })
})
</script>

<script lang="ts">
export default {
    data() {
        return {
            numbers: Array.from({ length: 37 }, (_, i) => i)
        }
    }
}
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
                    <div class="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
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
                                            <p class="mb-2 text-base text-muted-foreground">
                                                Straight: select a number & color
                                            </p>
                                        </FormDescription>

                                    </FormItem>
                                </FormField>

                            </CardContent>
                        </Card>
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
                                            <FormLabel>Score</FormLabel>
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
                                            <FormLabel>Bet Amount</FormLabel>
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
                            </CardContent>
                        </Card>
                        <div class="col-span-2"></div>
                        <Card class="col-span-2">
                            <CardHeader>
                                <CardTitle class="text-2xl font-bold">Choose your position</CardTitle>
                            </CardHeader>
                            <CardContent class="grid gap-6">
                                <FormField v-slot="{ componentField }" type="radio" name="color">
                                    <FormItem class="space-y-3">
                                        <FormLabel>Color</FormLabel>
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
                                <FormField v-slot="{ componentField }" name="number">
                                    <FormItem class="space-y-3">
                                        <FormLabel>Number</FormLabel>
                                        <FormControl>
                                            <RadioGroup v-bind="componentField" class="grid grid-cols-6">
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