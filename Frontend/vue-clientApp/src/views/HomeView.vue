<script setup lang="ts">
import Toaster from '@/components/ui/toast/Toaster.vue';
import { ref } from 'vue';
import GameOverDialog from '../components/GameOverDialog.vue';
import WheelDashboard from '../components/WheelDashboard.vue';
/* import SpinWheelButton from '../components/SpinWheelButton.vue';
 */
const componentKey = ref(0);

const updateComponentKey = (newValue: number) => {
  componentKey.value = newValue;
};
</script>

<template>
  <main>
    <Toaster />
    <GameOverDialog :componentKey="componentKey" @update:componentKey="updateComponentKey" />
    <!--  <SpinWheelButton /> -->
    <WheelDashboard :key="componentKey" />
  </main>
</template>
