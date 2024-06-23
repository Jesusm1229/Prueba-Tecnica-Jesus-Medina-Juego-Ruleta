<script setup lang="ts">
import axios from 'axios';
import { RouterView } from 'vue-router'
/* import HelloWorld from './components/HelloWorld.vue' */

axios.defaults.baseURL = 'http://www.winwheel.unilink:44308/api/';

</script>

<template>
  <!-- <header>
    <img alt="Vue logo" class="logo" src="@/assets/logo.svg" width="125" height="125" />

    <div class="wrapper">
      <HelloWorld msg="You did it!" />
      this is my production
      <nav>
        <RouterLink to="/">Home</RouterLink>
        <RouterLink to="/about">About</RouterLink>
      </nav>
    </div>
  </header> -->

  <RouterView />
</template>
