<template>
  <div>
    <v-carousel class="carousel" hide-controls=true hide-delimiters=true>
      <v-carousel-item
          v-for="(beer, index) in beers" :key="index" :src="beer.src"
      >
      </v-carousel-item>
    </v-carousel>
    <v-data-table
        :headers="headers"
        :items="roster"
        :pagination.sync="pagination"
        class="elevation-1"
    >
        <template slot="items" slot-scope="props">
        <td>{{ props.item.firstName }} {{ props.item.lastName }}</td>
        <td class="text-xs-left">{{ props.item.rosteredDate.slice(0, 10) }}</td>
        </template>
    </v-data-table>
  </div>
</template>

<script>
  import jwtService from '@/services/jwtService';

  export default {
    name: 'Dashboard',
    data () {
      return {
        pagination: {

        },
        beers: [
          { 
            src: 'https://cdn.vuetifyjs.com/images/carousel/sky.jpg' 
          },
           {
            src: 'https://cdn.vuetifyjs.com/images/cards/desert.jpg'
          },
           {
            src: 'https://cdn.vuetifyjs.com/images/carousel/planet.jpg'
          },
        ],
        headers: [
          {
            text: 'Who\'s buying beer?',
            align: 'left',
            sortable: false,
            value: 'id'
          },
          { text: 'Rostered Date', value: 'rosteredDate' },
        ],
        roster: [],    
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

    beforeMount() {
      let uri = 'http://localhost:60908/api/roster';

      this.axios.get(uri)
        .then((response) => {
          this.error = false;      
          this.roster = response.data.payload;
        })
        .catch((error) => {
          this.error = true;
          this.errorMessage = error.response.data.message;
        });
      }
  }
</script>

<style scoped>
  .carousel {
    height: 120px !important;
  }
</style>