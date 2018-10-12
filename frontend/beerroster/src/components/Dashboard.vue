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
    beforeMount() {
      if (!this.$store.getters.isUserLoggedIn) {
            // this.$router.push('/login');
      } else {
        let uri = 'http://localhost:60908/api/roster';

        this.axios.get(uri)
          .then((response) => {
            this.error = false;      
            this.roster = response.data;
          })
          .catch((error) => {
            this.error = true;
            this.errorMessage = error.response.data.message;
          });
      }
    }
  }
</script>

<style scoped>
  .carousel {
    height: 120px !important;
  }
</style>