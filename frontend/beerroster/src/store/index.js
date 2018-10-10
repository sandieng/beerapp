import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    isLoggedIn: false
  },
  getters: {
    isUserLoggedIn(state) {
      return state.isLoggedIn;
   } 
  },
  actions: {
    login: (context) => {
      context.commit('login');
    },
    logout: (context) => {
      context.commit('logout');
    }
  },
  mutations: {
    login: state => state.isLoggedIn = true,
    logout: state => state.isLoggedIn = false
  },

});