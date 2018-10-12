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
              
            <v-card class="elevation-12" v-if="showLogin">
              <v-toolbar dark color="primary">
                <v-toolbar-title>Login form</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form>
                  <v-text-field prepend-icon="person" name="email" label="Email" type="email" v-model="email"></v-text-field>
                  <label v-if="emailError" color="red">Email is required</label>
                  <label v-if="validEmailError" color="red">Valid email is required</label>

                  <v-text-field prepend-icon="lock" name="password" label="Password" id="password" type="password" v-model="password"></v-text-field>
                  <label v-if="passwordError" color="red">Password is required</label>
                </v-form>
              </v-card-text>

              <v-card-actions>
                <v-btn color="primary" @click="signup">Sign Up</v-btn>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="login">Login</v-btn>
              </v-card-actions>
            </v-card>

            <v-card class="elevation-12" v-if="showSignup">
              <v-toolbar dark color="primary">
                <v-toolbar-title>Sign up form</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form>
                  <v-text-field name="firstName" label="First name" type="text" id="firstName" v-model="firstName"></v-text-field>
                  <label v-if="firstNameError" color="red">First name is required</label>

                  <v-text-field name="lastName" label="Last name" type="text" id="lastName" v-model="lastName"></v-text-field>
                  <label v-if="lastNameError" color="red">Last name is required</label>

                  <v-text-field name="email" label="Email" type="email" v-model="email"></v-text-field>
                  <label v-if="emailError" color="red">Email is required</label>
                  <label v-if="validEmailError" color="red">Valid email is required</label>

                  <v-text-field name="password" label="Password" id="password" type="password" v-model="password"></v-text-field>
                  <label v-if="passwordError" color="red">Password is required</label>

                  <v-text-field name="confirmPassword" label="Confirm Password" id="confirmPassword" type="password" v-model="confirmPassword"></v-text-field>
                  <label v-if="confirmPasswordError" color="red">Confirm password is required</label>
                  <label v-if="passwordMismatchError" color="red">Confirm password mismatch</label>
                </v-form>
              </v-card-text>

              <v-card-actions>
                <v-btn color="primary" @click="cancelSignup">Cancel</v-btn>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="signupMember">Sign Up</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
</template>

<script>
  import loginService from './../services/loginService'
  import memberService from './../services/memberService'

  export default {
      name: "LoginSignup",
      data() {
        return {
            fixed: false,
            showLogin: true,
            showSignup: false,
            firstName: null,
            lastName: null,
            email: null,
            password: null,
            confirmPassword: null,
            showSnackbar: false,
            showInfo: '',
            emailError: false,
            validEmailError: false,
            passwordError: false,
            confirmPasswordError: false,
            passwordMismatchError: false,
            firstNameError: false,
            lastNameError: false
        }
      },
      methods: {
        checkLoginForm() {
          this.emailError = false;
          this.passwordError = false;

          if (this.email && this.password)
            return true;

          if (!this.email)
            this.emailError = true;
          else if (!this.validEmail(this.email)) {
            this.validEmailError = true;
          }
          
          if (!this.password)
            this.passwordError = true;

          return false;
        },

        checkSignupForm() {
          let loginDetailsValid = this.checkLoginForm();

          this.firstNameError = false;
          this.lastNameError = false;
          this.passwordError = false;
          this.confirmPasswordError = false;
          this.passwordMismatchError = false;

          if (!this.firstName)
            this.firstNameError = true;

          if (!this.lastName)
            this.lastNameError = true;

          if (!this.password)
            this.passwordError = true;

          if (!this.confirmPassword)
            this.confirmPasswordError = true;

          if (this.password !== this.confirmPassword)
            this.passwordMismatchError = true;

          if (this.firstNameError || this.lastNameError || this.emailError || this.passwordError || this.confirmPasswordError || this.passwordMismatchError)
            return false;

          return true;
        },

        validEmail: function (email) {
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
          },
        signup() {
          this.showLogin = false;
          this.showSignup = true;
        },

        signupMember(e) {
          let formValid = this.checkSignupForm();
          if (!formValid) {
            e.preventDefault();
            return;
          }

          //let uri = 'http://localhost:60908/api/member/signup';
          let signup = {firstName: this.firstName, lastName: this.lastName, email: this.email, password: this.password};
          memberService.save(signup)
            .then((response) => {
            window.localStorage.setItem('jwtToken', response.data);

              this.showSnackbar = true; 
              this.showInfo = 'Sign up successful.';
              
              this.$store.commit('login');
              this.$router.push('/Dashboard');
            })
            .catch((error) => {
              this.showSnackbar = true;
              this.showInfo = error.response.data.message;
            });
        },

        cancelSignup() {
          this.$router.push('/Welcome');
        },

        login(e) {
          let formValid = this.checkLoginForm();
          if (!formValid) {
            e.preventDefault();
            return;
          }

          let credentials = {email: this.email, password: this.password};

          loginService.login(credentials)
             .then((response) => {
              window.localStorage.setItem('jwtToken', response.data);
              //this.$store.commit('login');
              this.$store.dispatch('login');
              this.showSnackbar = false;      
              this.$router.push('/Dashboard');
            })
            .catch((error) => {
              this.showSnackbar = true;
              this.showInfo = error.response.data.message;
            });
        }
    }
  }
</script>
