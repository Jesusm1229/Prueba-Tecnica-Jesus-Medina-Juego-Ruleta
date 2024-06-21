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
  <!--   <div class="fixed inset-0 flex items-center justify-center">
    <button type="button" @click="openModal"
      class="px-4 py-2 text-sm font-medium text-white rounded-md bg-black/20 hover:bg-black/30 focus:outline-none focus-visible:ring-2 focus-visible:ring-white/75">
      Open dialog
    </button>
  </div> -->
  <GameOverDialog :componentKey="componentKey" @update:componentKey="updateComponentKey" />
  <WheelDashboard :key="componentKey" />

</template>
