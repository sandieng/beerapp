<template>
  <div>
    <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
      {{ showInfo }}
      <v-btn color="pink" flat @click="showSnackbar = false">
        Close
      </v-btn>
    </v-snackbar>

    <v-toolbar flat color="white">
      <v-dialog v-model="dialog" max-width="500px">
        <v-card>
          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                  <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.firstName" label="First Name"></v-text-field>
                </v-flex>
                  <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.lastName" label="Last Name"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.dateJoined" label="Date Joined"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.rosteredDate" label="Rostered Date"></v-text-field>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" flat @click.native="close">Cancel</v-btn>
            <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-toolbar>
    <v-data-table
      :headers="headers"
      :items="rosterList"
      hide-actions
      class="elevation-1"
    >
      <template slot="items" slot-scope="props">
        <td>{{ props.item.firstName }} {{ props.item.lastName }}</td>

        <td class="text-xs-left">{{ props.item.dateJoined.slice(0, 10) }}</td>
        <td class="text-xs-left">{{ props.item.rosteredDate.slice(0, 10) }}</td>
        <td class="justify-center layout px-0">
          <v-icon
            small
            class="mr-2"
            @click="editRoster(props.item)"
          >
            edit
          </v-icon>
          <v-icon
            small
            @click="deleteRoster(props.item)"
          >
            delete
          </v-icon>
        </td>
      </template>
     
    </v-data-table>
  </div>
</template>


<script>
  import rosterService from './../../services/rosterService'
  import memberService from './../../services/memberService'


  export default {
    name: 'RosterEdit',
    data () {
      return {
        showSnackbar: false,
        showInfo: '',
        dialog: false,
        headers: [
          {
            text: 'Member\'s Name',
            align: 'left',
            sortable: false,
            value: 'id'
          },
          { text: 'Date Joined', value: 'dateJoined' },
          { text: 'Rostered Date', value: 'rosteredDate' },
        ],
        rosterList: [],   
        editedIndex: -1,
        editedItem: {
          dateJoined: null,
          rosteredDate: null
        } 
      }
    },
    computed: {
    },

    watch: {
      dialog (val) {
        val || this.close()
      }
    },

    //cool, awesome I like BEER "awe super dooper cold ( I like cold stuff like icy pole, " yummy" ). Plus West Coast Eagles won the grand finals.
     created() {
      if (!this.$store.getters.isUserLoggedIn) {
        this.$router.push('/Login');
        return;
      }

      rosterService.list()
         .then((response) => {
          this.error = false;      
          this.rosterList = response.data;
        })
        .catch((error) => {
          this.error = true;
          this.errorMessage = error.response.data.message;
        });
    },

    methods: {
      editRoster(roster) {
        roster.dateJoined = roster.dateJoined.slice(0, 10);
        roster.rosteredDate = roster.rosteredDate.slice(0, 10);
        this.editedIndex = this.rosterList.indexOf(roster);
        this.editedItem = Object.assign({}, roster);
        this.dialog = true;
      },

      deleteRoster(roster) {
        const index = this.rosterList.indexOf(roster)
        let ok = confirm('Are you sure you want to delete this roster?') && this.rosterList.splice(index, 1);
        if (ok) {
          rosterService.delete(roster.id)
            .then(() => {
                this.showSnackbar = true;      
                this.showInfo = 'Roster\'s deleted.';
              })
              .catch((showSnackbar) => {
                this.showSnackbar = true;
                this.showInfo = showSnackbar.response.data.message;
          });
        }
      },

      save () {
        if (this.editedIndex > -1) {
          Object.assign(this.rosterList[this.editedIndex], this.editedItem);

          // Update change in the backend
          let member = {id: this.editedItem.memberID, firstName: this.editedItem.firstName, lastName: this.editedItem.lastName, dateJoined: this.editedItem.dateJoined};
          memberService.update(member)
           .then(() => {
                this.showSnackbar = true;      
                this.showInfo = 'Member\'s data updated.';
              })
              .catch((showSnackbar) => {
                this.showSnackbar = true;
                this.showInfo = showSnackbar.response.data.message;
              });

          let roster = {id: this.editedItem.id, memberID: this.editedItem.memberID, rosteredDate: this.editedItem.rosteredDate};
          rosterService.update(roster)
            .then(() => {
                this.showSnackbar = true;      
                this.showInfo = 'Roster\'s data updated.';
              })
              .catch((showSnackbar) => {
                this.showSnackbar = true;
                this.showInfo = showSnackbar.response.data.message;
              });
        } else {
          this.rosterList.push(this.editedItem);
        }
        this.close()
      },

      close () {
        this.dialog = false
        setTimeout(() => {
          this.editedItem = Object.assign({}, this.defaultItem);
          this.editedIndex = -1;
        }, 300)
      },
    }
  }
</script>