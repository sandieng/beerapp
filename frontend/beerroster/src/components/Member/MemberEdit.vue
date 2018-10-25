<template>
   <v-content>
      <v-container fluid>
        <v-layout align-center justify-center>
          <v-flex >
            <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
              {{ showInfo }}
              <v-btn color="pink" flat @click="showSnackbar = false">
                Close
              </v-btn>
            </v-snackbar>

            <v-card>
              <v-card-text>
                <v-form>
                  <v-text-field required name="email" label="Email" type="text" id="email" v-model="email"></v-text-field>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="primary" @click="searchMember(email)">Search</v-btn>
                  </v-card-actions>
                </v-form>
              </v-card-text>
            </v-card>

            <v-card class="elevation-12" v-if="memberFound">
              <v-toolbar dark color="primary">
                <v-toolbar-title>Edit member</v-toolbar-title>
                <v-spacer></v-spacer>
             
              </v-toolbar>
              <v-card-text>
                <v-form>
                  <v-text-field required name="firstName" label="First name" type="text" id="firstName" v-model="firstName"></v-text-field>
                  <v-text-field required name="lastName" label="Last name" type="text" id="lastName" v-model="lastName"></v-text-field>
                  <v-text-field name="email" label="Email" type="email" v-model="email"></v-text-field>
                  <label v-if="emailError" color="red">Email is required</label>
                  <label v-if="validEmailError" color="red">Valid email is required</label>
                  <input type="checkbox" id="isActive" v-model="isActive">
                  <label for="isActive">Member is active?</label>

                  <v-menu
                    ref="menu"
                    :close-on-content-click="false"
                    v-model="menu"
                    :nudge-right="40"
                    :return-value.sync="dateJoined"
                    lazy
                    transition="scale-transition"
                    offset-y
                    full-width
                    min-width="290px"
                  >
                    <v-text-field
                      slot="activator"
                      v-model="dateJoined"
                      label="Date joined"
                      prepend-icon="event"
                      readonly
                    ></v-text-field>
                    <v-date-picker v-model="dateJoined" no-title scrollable>
                      <v-spacer></v-spacer>
                      <v-btn flat color="primary" @click="menu = false">Cancel</v-btn>
                      <v-btn flat color="primary" @click="$refs.menu.save(dateJoined)">OK</v-btn>
                    </v-date-picker>
                  </v-menu>

                  <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="primary" @click="updateMember">Save</v-btn>
                  </v-card-actions>
                </v-form>
              </v-card-text>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
</template>

<script>
  import jwtService from '@/services/jwtService';
  import memberService from './../../services/memberService';
  import emailService from './../../services/emailService';

  export default {
    name: 'MemberEdit',
    data() {
      return {
        id: null,
        firstName: '',
        lastName: '',
        email: '',
        password: 'secret',
        dateJoined: new Date().toISOString().slice(0, 10),
        isActive: null,
        menu: false,
        emailError: false,
        validEmailError: false,
        showSnackbar: false,
        showInfo: '',
        memberFound: false,
        alreadySaved: false
      }
    },
    methods: {
      searchMember(email) {
        // Ensure email is supplied
        if (!email) {
          this.showSnackbar = true;
          this.showInfo = 'Can\'t search without a valid email.';
          this.memberFound = false;
          return;
        }

        emailService.search(this.email)
           .then((response) => {
            this.showSnackbar = true;
            this.showInfo = 'Member\'s data retrieved.';
            this.id = response.data.payload[0].id;
            this.firstName = response.data.payload[0].firstName;
            this.lastName = response.data.payload[0].lastName;
            this.email = response.data.payload[0].email;
            this.password = response.data.payload[0].password;
            this.dateJoined = response.data.payload[0].dateJoined;
            this.isActive = response.data.payload[0].isActive;
            this.memberFound = true;
            this.alreadySaved = false;
          })
          .catch((error) => {
            this.showSnackbar = true;
            this.showInfo = error.response.data.message;
            this.memberFound = false;
          })
      },

      updateMember() {
        let member = {id: this.id, firstName: this.firstName, lastName: this.lastName, email: this.email, password: this.password, dateJoined: this.dateJoined, isActive: this.isActive};
        memberService.update(member)
          .then(() => {
              this.showSnackbar = true;      
              this.showInfo = 'Member\'s data updated.';
              this.alreadySaved = true;
            })
            .catch((error) => {
              this.showSnackbar = true;
              this.showInfo = error.response.data.message;
            });
      },
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

    beforeRouteLeave(to, from, next) {
      if (this.memberFound && !this.alreadySaved) {
        const response = confirm('You have not saved your changes. Are you sure?');
        next(response);
      } else {
        next(true);
      }
    }
  }
</script>

