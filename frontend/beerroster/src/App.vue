<template>
  <v-app>
    <navigation-bar></navigation-bar>
    <router-view></router-view>
    <footer-bar></footer-bar>
  </v-app>
</template>

<script>
  import jwtService from '@/services/jwtService';

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
          this.$store.dispatch('logout', this.$router);
     
          return;
        }

        // Ensure user is logged in and the token has not expired
        //let token =  window.localStorage.getItem('jwtToken');
        let token = jwtService.getJwtToken();

        if (!token) {
          this.$store.dispatch('logout', this.$router);
     
          return;
        }

        // let jwtToken = VueJwtDecode.decode(token);
        // let unixTimestamp = moment().unix();
        // let expiredTime = moment.utc(jwtToken.exp).valueOf();
        // if (expiredTime < unixTimestamp)
        // {
        //   this.$store.dispatch('logout');
        //   this.$router.push('/Login');
        //   return;
        // }
        if (jwtService.tokenHasExpired(token)) {
          this.$store.dispatch('logout', this.$router);
          return;
        }
      }
    }
  }
</script>
