<template>
    <div class="flex items-center justify-center w-24 h-24 transition-transform duration-200 rounded-full cursor-pointer hover:rotate-[360deg]"
        @click="spin"
        style="background-image: conic-gradient(#000 0% 12.5%, #ff0000 12.5% 25%, #000 25% 37.5%, #ff0000 37.5% 50%, #000 50% 62.5%, #ff0000 62.5% 75%, #000 75% 87.5%, #ff0000 87.5% 100%);">
        <span class="font-bold text-white" style="text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);">Spin!</span>
    </div>
</template>

<script>
export default {
    methods: {
        spin() {
            console.log('Spinning the wheel!');
        },
    },
};
</script>