<template>
  <v-app>
    <navigation-bar></navigation-bar>
    <router-view></router-view>
    <footer-bar></footer-bar>
  </v-app>
</template>

<script>
  import VueJwtDecode from 'vue-jwt-decode';
  import moment from 'moment';

  import NavigationBar from './components/NavigationBar';
  import FooterBar from './components/Footer';

  export default {
    name: 'App',
    components: {
      NavigationBar,
      FooterBar
    },
    data () {
      return {
      }
    },
    watch: {
      '$route'(to, from) {
        // Ensure user is logged in
        if (!this.$store.getters.isUserLoggedIn) {
          this.$router.push('/Login');
     
          return;
        }

        // Ensure user is logged in and the token has not expired
        let token =  window.localStorage.getItem('jwtToken');
        let jwtToken = VueJwtDecode.decode(token);
        let utc = moment.utc().valueOf();
        let expiredTime = moment.utc(jwtToken.expiryToken).valueOf();
        if (expiredTime < utc)
        {
          this.$store.dispatch('logout');
          this.$router.push('/Login');
          return;
        }
      }
    }
  }
</script>
