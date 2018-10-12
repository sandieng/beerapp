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
  import memberService from './../../services/memberService'
  import emailService from './../../services/emailService'

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
        memberFound: false
      }
    },
    methods: {
      searchMember(email) {
        // Ensure email is supplied
        if (!this.email) {
          this.showSnackbar = true;
          this.showInfo = 'Can\'t search without a valid email.';
          this.memberFound = false;
          return;
        }

        emailService.search(this.email)
           .then((response) => {
            this.showSnackbar = true;
            this.showInfo = 'Member\'s data retrieved.';
            this.id = response.data.id;
            this.firstName = response.data.firstName;
            this.lastName = response.data.lastName;
            this.email = response.data.email;
            this.password = response.data.password;
            this.dateJoined = response.data.dateJoined;
            this.isActive = response.data.isActive;
            this.memberFound = true;
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
          .then((response) => {
              this.showSnackbar = true;      
              this.showInfo = 'Member\'s data updated.';
            })
            .catch((showSnackbar) => {
              this.showSnackbar = true;
              this.showInfo = showSnackbar.response.data.message;
            });
      },
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

