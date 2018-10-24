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
    logout: (context, router) => {
      window.localStorage.removeItem('jwtToken');
      context.commit('logout');
      router.push('/Login');
    }
  },
  mutations: {
    login: state => state.isLoggedIn = true,
    logout: state => state.isLoggedIn = false
  },

});