<template>
 <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
              {{ showInfo }}
              <v-btn color="pink" flat @click="showSnackbar = false">
                Close
              </v-btn>
            </v-snackbar>
              
            <v-card class="elevation-12">
              <v-toolbar dark color="primary">
                <v-toolbar-title>Create roster for new members</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form>
                  <blockquote>
                    To create a roster for newly joined members, please click the 'Create' button.
                  </blockquote>
                </v-form>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="createRoster">Create</v-btn>
              </v-card-actions>
            </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script>
  import rosterService from './../../services/rosterService';

   export default {
      name: "RosterNewMembers",
      data() {
        return {
          showInfo: false,
          showSnackbar: false
        }
      },

      methods: {
        createRoster() {
            rosterService.createForNewMembers()
             .then(() => {
                this.showSnackbar = true; 
                this.showInfo = 'Beer roster created successful for new members.';
                this.$router.push('/dashboard');
              })
              .catch((error) => {
                this.showSnackbar = true;
                this.showInfo = error.response.data.message;
              });
        }
      },

      created() {
      // Page is created through navigation or F5
      // If the page is refreshed through F5, the Vuex state is lost
      // The effect of that is that the user is no longer logged in despite the token has not expired
      // We will resintate the state here
      var jwtToken = jwtService.getJwtToken();
      if (!jwtService.tokenHasExpired(jwtToken)) {
        // Reestablished user's
        this.$store.dispatch('login');
      } else {
        this.$store.dispatch('logout', this.$router);
        return;
      }
    },
   }
</script>