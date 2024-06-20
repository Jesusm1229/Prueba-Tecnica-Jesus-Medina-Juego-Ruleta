<template>
  <div></div>
</template>

<script>
import axios from 'axios';
import { h } from 'vue';
import { useStore } from 'vuex';

export default {
  name: 'TokenValidator',
  methods: {
    refreshToken() {
      const store = useStore();

      const token = {
        accessToken: store.player.accessToken,
        refreshToken: store.player.refreshToken,
      };

      axios
        .post('https://localhost:7299/api/authentication/refresh', token, {
          headers: {},
        })
        .then((response) => {
          store.player.accessToken = response.data.accessToken;
          store.player.refreshToken = response.data.refreshToken;

          localStorage.setItem('UserObject', JSON.stringify(store.player));
        })
        .catch((error) => {
          console.error(error);
          /*  this.logOut(); */ // If refreshing the token fails, log out the user
        });

    },

    isTokenExpired(token) {
      try {
        const { exp } = JSON.parse(atob(token.split('.')[1]));
        const expirationDate = new Date(exp * 1000);

        return expirationDate < new Date();

      } catch (error) {
        console.error('Invalid token', error);
        return true;
      }
    },


    /* logOut() {
      const store = useStore();
      const token = {
        accessToken: store.player.accessToken,
        refreshToken: store.player.refreshToken,
      };

      axios
        .post('https://localhost:7299/api/authentication/logout', token, {
          headers: {},
        })
        .then((response) => {
          store.player = {
            accessToken: '',
            refreshToken: '',
            idUsername: '',
            username: '',
            idScore: '',
            score: 0,
          };
          localStorage.removeItem('UserObject');
          this.$toast({
            title: 'Logout successful',
            description: h('div', { class: ' text-wrap' }, 'You have been logged out'),
            duration: 5000,
          });
          console.log(response);
        })
        .catch((error) => {
          console.error(error);
          console.error(error.response.data.Message);
          this.$toast({
            title: 'An error occurred',
            description: h(
              'div',
              { class: ' text-wrap' },
              error.response ? error.response.status + ': ' + error.response.data.Message : error
            ),
            duration: 6000,
            variant: 'destructive',
          });
        });
    }, */


    scheduleTokenRefresh() {
      const store = useStore();
      const { exp } = JSON.parse(atob(store.player.accessToken.split('.')[1]));
      const expirationDate = new Date(exp * 1000);
      const refreshTime = expirationDate.getTime() - Date.now() - 2 * 60 * 1000; // 2 minutes before expiration

      if (refreshTime > 0) {
        setTimeout(this.refreshToken, refreshTime);
      } else {
        this.refreshToken(); // If the token is already expired or will expire in less than 2 minutes, refresh it immediately
      }
    }
  },
  created() {
    const store = useStore();

    /* if (this.isTokenExpired(store.player.accessToken)) {
      this.logOut();
    } */
  },
};
</script>