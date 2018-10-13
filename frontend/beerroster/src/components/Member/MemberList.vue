<template>
  <v-flex>
    <v-snackbar v-model="showSnackbar" :timeout="6000" :top="true">
                {{ showInfo }}
                <v-btn color="pink" flat @click="showSnackbar = false">
                  Close
                </v-btn>
    </v-snackbar>
    
    <v-data-table
        :headers="headers"
        :items="memberList"
        :pagination.sync="pagination"
        class="elevation-1"
    >
        <template slot="items" slot-scope="props">
        <td>{{ props.item.firstName }} {{ props.item.lastName }}</td>
        <td class="text-xs-left">{{ props.item.dateJoined.slice(0, 10) }}</td>
        <td>{{ props.item.isActive}}</td>
        </template>
    </v-data-table>
  </v-flex>
</template>

<script>
  import memberService from './../../services/memberService'

  export default {
    name: 'MemberList',
    data () {
      return {
        showSnackbar: false,
        showInfo: '',
        pagination: {
        },
        headers: [
          {
            text: 'Member\'s Name',
            align: 'left',
            sortable: false,
            value: 'id'
          },
          { text: 'Date Joined', value: 'dateJoined' },
          { text: 'Active Member', value: 'isActive' }
        ],
        memberList: [],    
      }
    },
    //cool, awesome I like BEER "awe super dooper cold ( I like cold stuff like icy pole, " yummy" ). Plus West Coast Eagles won the grand finals.
    created() {
      // Centralised user login in App.vue
      
      // if (!this.$store.getters.isUserLoggedIn) {
      //   this.$router.push('/Login');
     
      //   return;
      // }

      memberService.list()
        .then((response) => {
            this.showSnackbar = true;
            this.showInfo = 'Member list retrieved.';
            this.memberList = response.data;
          })
          .catch((error) => {
            this.showSnackbar = true;
            this.showInfo = error.response.data.message;
          })
    }
  }
</script>