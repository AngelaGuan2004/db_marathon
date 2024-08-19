import Vue from 'vue';
import Router from 'vue-router';
import PhotoUserInfo from '@/view/UserCenter/PhotoUserInfo.vue'
import PlayerUserInfo from '@/view/UserCenter/PlayerUserInfo.vue'
import VolunteerUserInfo from '@/view/UserCenter/VolunteerUserInfo.vue'
import MyRegistrations from '@/view/UserCenter/MyRegistrations.vue'
import MyVolunteering from '@/view/UserCenter/MyVolunteering.vue'
import MyResults from '@/view/UserCenter/MyResults.vue'
Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/UserCenter/PhotoUserInfo',
      name: 'PhotoUserInfo',
      component: PhotoUserInfo
    },
    {
      path: '/UserCenter/VolunteerUserInfo',
      name: 'VolunteerUserInfo',
      component: VolunteerUserInfo
    },
    {
      path: '/UserCenter/PlayerUserInfo',
      name: 'PlayerUserInfo',
      component: PlayerUserInfo
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
    },
    
]
});