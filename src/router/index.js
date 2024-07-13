import Vue from 'vue';
import Router from 'vue-router';
import UserInfo from '@/view/UserCenter/UserInfo.vue'
import MyRegistrations from '@/view/UserCenter/MyRegistrations.vue'
import MyVolunteering from '@/view/UserCenter/MyVolunteering.vue'
import MyResults from '@/view/UserCenter/MyResults.vue'
Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/UserCenter/UserInfo',
      name: 'UserInfo',
      component: UserInfo
    },
    {
        path: '/UserCenter/MyRegistrations',
        name: 'MyRegistrations',
        component: MyRegistrations
      },
    {
        path:'/',
        name:'MyVolunteering',
        component:MyVolunteering
    },
    {
      path:'/UserCenter/MyResults',
      name:'MyResults',
      component:MyResults
  }
]
});