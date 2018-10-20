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
                <v-toolbar-title>Create roster</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form>
                   <v-text-field prepend-icon="event" name="cycle" label="Number of Cycles" type="number" v-model="cycle" min=1 max=10></v-text-field>
                  <blockquote>
                    To create a brand new roster or to extend the existing roster, please click the 'Create' button.
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
  import rosterService from './../../services/rosterService'
   export default {
      name: "RosterCreate",
      data() {
        return {
          cycle: 1,
          showInfo: false,
          showSnackbar: false
        }
      },

      methods: {
        createRoster() {
          rosterService.create(this.cycle)
            .then((response) => {
                window.localStorage.setItem('jwtToken', response.data.token);

                this.showSnackbar = true; 
                this.showInfo = 'Beer roster created successful.';
                this.$router.push('/dashboard');
              })
              .catch((error) => {
                this.showSnackbar = true;
                this.showInfo = error.response.data.message;
              });
           
        }
      },

      created() {
        // Centralised user login in App.vue
      
        // if (!this.$store.getters.isUserLoggedIn) {
        //   this.$router.push('/Login');
        //   return;
        // }
    }
   }
</script>