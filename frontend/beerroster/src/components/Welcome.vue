<template>
  <v-container grid-list-sm fluid>
  <v-layout row wrap>
    <v-flex
      v-for="(block, i) in blocks"
      :key="i"
      xs4
      d-flex
    >

    <v-card >
      <v-img
        :src="`https://picsum.photos/500/300?image=${i * 5 + 10}`"
        :lazy-src="`https://picsum.photos/10/6?image=${i * 5 + 10}`"
        class="white--text"
        height="200px"
      >
      </v-img>

      <v-card-title>
        <div>
          <v-spacer></v-spacer>
          <blockquote>
            &#8220;{{block.quote.title}}&#8221;
            <em>&mdash;{{block.quote.by}}</em>        
          </blockquote>
        </div>
      </v-card-title>
    </v-card>
    </v-flex>
  </v-layout>
</v-container>
</template>

<script>
   export default {
      name: "Welcome",
       data() {
        return {
            blocks: [],
            quotes: [ 
              {title: 'Milk is for babies. When you grow up you have to drink beer.', by: 'Arnold Schwarzenegger'},
              {title: 'Give me a woman who loves beer and I will conquer the world.', by: 'Kaiser Wilhelm'},
              {title: 'Some people wanted champagne and caviar when they should have had beer and hot dogs.', by: 'Dwight Eisenhower'},
              {title: 'I\'m gaining weight the right way: I\'m drinking beer.', by: 'Johnny Damon'}, 
              {title: 'I look like the kind of guy who has a bottle of beer in his hand.', by: 'Charles Bronson'}, 
              {title: 'Beer is the best damn drink in the world.', by: 'Jack Nicholson'}, 
              {title: 'My goal is to hit the gym every day I\'m on vacation. Usually I just end up sleeping and drinking beer.', by: 'Gary Allan'},
              {title: 'Beer makes you feel the way you ought to feel without beer.', by: 'Henry Lawson'},
              {title: 'I\'m an old-fashioned guy. I want to be an old man with a beer belly sitting on a porch, looking at a lake or something.', by: 'Johnny Depp'}, 
            ]
        }
      },
      methods: {
        generateQuotes: function() {
          for (let i = 0; i < 9; i++) {
            this.blocks.push({
              quote: this.quotes[i]
            })
          }
        },
      },
      mounted: function () {
        this.generateQuotes();
      }
   }
</script>


