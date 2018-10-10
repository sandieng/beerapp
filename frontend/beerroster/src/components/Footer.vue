<template>
 <v-footer :fixed="fixed" app>
    <img src="@/assets/logo.png" alt="Vuetify.js" class="vue-img">
    <span>Johns Lyng Group &copy; {{new Date().getFullYear()}}</span>
    <v-spacer></v-spacer>
    <blockquote >
      &#8220;First, solve the problem then write the code.&#8221;
      <em>&mdash;John Johnson</em>        
    </blockquote>
  </v-footer>
</template>

<script>
  export default {
    name: "FooterBar",
    data() {
      return {
          fixed: false
      }
    },
    methods: {
    }
  }
</script>

<style scoped>
  .vue-img {
    width: 30px;
  }
</style>