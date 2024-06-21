<script setup lang="ts">
import { Toaster } from '@/components/ui/toast';
import { onMounted, ref } from 'vue';
import WheelDashboard from './components/WheelDashboard.vue';


const userObjectExists = ref(false);

onMounted(() => {
  const userObject = localStorage.getItem('UserObject');
  userObjectExists.value = userObject !== null;
});

</script>

<template>
  <Toaster />
  <WheelDashboard />

</template>


