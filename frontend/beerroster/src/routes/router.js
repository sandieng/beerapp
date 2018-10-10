import Vue from 'vue';
import VueRouter from 'vue-router'

import Welcome from './../components/Welcome'
import LoginSignup from './../components/LoginSignup'
import Dashboard from './../components/Dashboard'
import MemberAdd from './../components/Member/MemberAdd'
import MemberEdit from './../components/Member/MemberEdit'
import MemberList from './../components/Member/MemberList'
import RosterCreate from './../components/Roster/RosterCreate'
import RosterNewMembers from './../components/Roster/RosterNewMembers'
import RosterEdit from './../components/Roster/RosterEdit'

// Lazy loading for RosterEdit component
//const RosterEdit = () => System.import('./../components/Roster/RosterEdit')

Vue.use(VueRouter);

const routes = [
    {
      path: '/Welcome',
      component: Welcome
    },
    {
      path: '/Login',
      component: LoginSignup
    },
    {
      path: '/Dashboard',
      component: Dashboard
    },
    {
      path: '/Member/Add',
      component: MemberAdd,
    },
    {
      path: '/Member/Edit',
      component: MemberEdit
    },
    {
      path: '/Member/List',
      component: MemberList
    },
    {
      path: '/Roster/Create',
      component: RosterCreate
    },
    {
      path: '/Roster/NewMembers',
      component: RosterNewMembers
    },
    {
      path: '/Roster/Edit',
      component: RosterEdit
    },
    {
      path: '/Roster/Dashboard',
      component: Dashboard
    },
    {
      path: '*',
      component: Welcome
    }
  ]

// const router = new VueRouter({
//   routes
// })

export default new VueRouter({
    mode: 'history',    // Remove # http://localhost:8080/#/Dashboard after URL
    routes
});