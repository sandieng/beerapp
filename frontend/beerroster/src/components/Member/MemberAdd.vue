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

            <v-card class="elevation-12">
              <v-toolbar dark color="primary">
                <v-toolbar-title>Add new member</v-toolbar-title>
                <v-spacer></v-spacer>
             
              </v-toolbar>
              <v-card-text>
                <v-form>
                  <v-text-field required name="firstName" label="First name" type="text" id="firstName" v-model="firstName"></v-text-field>
                  <v-text-field required name="lastName" label="Last name" type="text" id="lastName" v-model="lastName"></v-text-field>
                  <v-text-field name="email" label="Email" type="email" v-model="email"></v-text-field>
                  <!-- <label v-if="emailError" color="red">Email is required</label>
                  <label v-if="validEmailError" color="red">Valid email is required</label> -->

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
                  <v-btn color="primary" @click="saveMember">Save</v-btn>
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
    name: 'MemberAdd',
    data() {
      return {
        firstName: '',
        lastName: '',
        email: '',
        password: 'secret',
        dateJoined: new Date().toISOString().slice(0, 10),
        menu: false,
        showSnackbar: false,
        showInfo: ''
      }
    },
    methods: {
      saveMember() {
        let member = {firstName: this.firstName, lastName: this.lastName, email: this.email, password: this.password, dateJoined: this.dateJoined};
        memberService.save(member)
          .then(() => {
            this.showSnackbar = true;
            this.showInfo = 'New member added successfully.'
          })
          .catch((error) => {
            this.showSnackbar = true;
            this.showInfo = error.response.data.message;
          })

        // Send welcome email
        let email = {toEmail: 'targetEmail@beerlover.com.au', subject: 'Hi new member', message: 'Welcome to Beer Roster'};
        emailService.send(email)
         .then(() => {
            this.showSnackbar = true;
            this.showInfo = 'Welcome email sent to the new member successfully.'
          })
          .catch(() => {
            this.showSnackbar = true;
            this.showInfo = 'Failed to send welcome email to the new member.';
          })
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

