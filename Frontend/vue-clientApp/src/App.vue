<script setup lang="ts">
import { ref, onMounted } from 'vue';
import WheelDashboard from './components/WheelDashboard.vue'
import { Toaster } from '@/components/ui/toast'
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from '@/components/ui/alert-dialog'


const userObjectExists = ref(false);

onMounted(() => {
  const userObject = localStorage.getItem('UserObject');
  userObjectExists.value = userObject !== null;
});

</script>

<template>
  <Toaster />
  <!-- <template v-if="userObjectExists">
    <AlertDialog>
      <AlertDialogTrigger>Open</AlertDialogTrigger>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
          <AlertDialogDescription>
            This action cannot be undone. This will permanently delete your account
            and remove your data from our servers.
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>Cancel</AlertDialogCancel>
          <AlertDialogAction>Continue</AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  </template> -->
  <WheelDashboard />

</template>

<script lang="ts">

export default {
  data() {
    return {
    }
  },
  methods: {
    checkUserObject() {
      const userObject = localStorage.getItem('UserObject');
      /* if (userObject) {
        alert('UserObject exists in localStorage');
      } */
    }
  },
  mounted() {
    this.checkUserObject();
  },
}

</script>

<style scoped></style>
