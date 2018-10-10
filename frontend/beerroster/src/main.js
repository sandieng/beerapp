import '@babel/polyfill'
import Vue from 'vue'
import VueRouter from 'vue-router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import VeeValidate from 'vee-validate'
import './plugins/vuetify'
import App from './App.vue'
import store from './store';
import router from './routes/router'

Vue.use(VueRouter);
Vue.use(VueAxios, axios);
Vue.use(VeeValidate);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
