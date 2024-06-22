<script setup lang="ts">
import { Toaster } from '@/components/ui/toast';
import WheelDashboard from './components/WheelDashboard.vue';
import GameOverDialog from './components/GameOverDialog.vue';
import { ref } from 'vue';

const componentKey = ref(0);

const updateComponentKey = (newValue: number) => {
  componentKey.value = newValue;
};

</script>

<template>
  <Toaster />
  <GameOverDialog :componentKey="componentKey" @update:componentKey="updateComponentKey" />
  <WheelDashboard :key="componentKey" />

</template>
