<template>
  <v-app>
    <navigation-bar></navigation-bar>
    <router-view></router-view>
    <footer-bar></footer-bar>
  </v-app>
</template>

<script>
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
  }
</script>
