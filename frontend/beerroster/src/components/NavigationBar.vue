<template>
  <v-toolbar>
    <router-link to="/" style="text-decoration:none">
        <v-icon color="blue" @click="goDashboard">home</v-icon>
    </router-link>
      
    <v-toolbar-title>BEER ROSTER</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-toolbar-items class="hidden-sm-and-down">
      <v-menu offset-y>
        <v-btn flat    
          slot="activator"
          color="primary"
          dark>
          Membership
        </v-btn>

        <v-list v-for="(menu, index) in membershipMenu" :key="index">
          <router-link :to="menu.route" style="text-decoration:none">
            <v-list-tile @click="false"> 
            <v-list-tile-title>{{ menu.title }} </v-list-tile-title>
            </v-list-tile>
          </router-link>
        </v-list>
      </v-menu>

      <v-menu offset-y>
        <v-btn flat  
          slot="activator"
          color="primary"
          dark>ROSTER</v-btn>
          <v-list v-for="(menu, index) in rosterMenu" :key="index">
            <router-link :to="menu.route" style="text-decoration:none">
              <v-list-tile @click="false"> 
              <v-list-tile-title>{{ menu.title }} </v-list-tile-title>
              </v-list-tile>
            </router-link>
          </v-list>
      </v-menu>

   
      <v-menu offset-y>
       <v-btn flat  
          slot="activator"
          color="primary"
          dark @click="loginLogout">{{this.$store.getters.isUserLoggedIn ? 'LOGOUT' : 'LOGIN'}}</v-btn> 
      </v-menu>
    </v-toolbar-items>
  </v-toolbar>
</template>


<script>
  export default {
    name: "NavigationBar",
    data() {
      return {
          membershipMenu: [
              { title: 'Add New Member', route: '/Member/Add' },
              { title: 'Edit Member', route: '/Member/Edit' },
              { title: 'List Member', route: '/Member/List' },
            ],
          rosterMenu: [
            { title: 'Create New Roster', route: '/Roster/Create' },
            { title: 'Roster New Members', route: '/Roster/NewMembers' },
            { title: 'Edit Roster', route: '/Roster/Edit' },
            { title: 'Dashboard', route: '/Roster/Dashboard' },
          ],
      }
    },
    methods: {
      loginLogout() {
        if (!this.$store.getters.isUserLoggedIn) {
          this.$router.push('/Login');
        } else {
          //this.$store.commit('logout');
          this.$store.dispatch('logout', this.$router);
          //this.$router.push('/Welcome');
        }
      },
      goDashboard() {
        if (!this.$store.getters.isUserLoggedIn) {
          this.$router.push('/Login');
        } else {
          this.$router.push('/Dashboard');
        }
      }
    }
  }
</script>
