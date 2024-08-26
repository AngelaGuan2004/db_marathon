// 该文件专门用于创建整个应用的路由器
import VueRouter from 'vue-router'
import Home from '@/view/Home.vue'
import UserTab from '@/components/UserTab.vue'
import UserRegistrations from '@/components/UserRegistrations.vue'
import UserInfo from '@/components/UserInfo.vue'
import UserVolunteering from '@/components/UserVolunteering.vue'
import UserResults from '@/components/UserResults.vue'
import UserVolunteerSignup from '@/components/UserVolunteerSignup.vue';
import VolunteerDetailForAddStation from '@/components/VolunteerDetailForAddStation.vue'
//创建并暴露一个路由器
export default new VueRouter({
  mode: 'history',
  routes: [
    {
      path: '/Home',
      name: 'Home',
      component: Home
    },
    {
      path: '/UserTab',
      name: 'UserTab',
      component: UserTab,
      children: [
        {
          path: 'UserRegistrations',
          name: 'UserRegistrations',
          component: UserRegistrations
        },
        {
          path: 'UserInfo',
          name: 'UserInfo',
          component: UserInfo
        }, {
          path: 'UserVolunteering',
          name: 'UserVolunteering',
          component: UserVolunteering,

        }, {
          path: 'VolunteerDetailForAddStation',
          name: 'VolunteerDetailForAddStation',
          component: VolunteerDetailForAddStation,
          path: 'VolunteerDetailForAddStation/:id/:voltype',
        }, {
          path: 'UserVolunteerSignup',
          name: 'UserVolunteerSignup',
          component: UserVolunteerSignup
        }, {
          path: 'UserResults',
          name: 'UserResults',
          component: UserResults
        }
      ]
    },
  ],
})

//全局前置路由守卫————初始化的时候被调用、每次路由切换之前被调用
//一般用于鉴权
// router.beforeEach((to, from, next) => {

// })

//全局后置路由守卫————初始化的时候被调用、每次路由切换之后被调用
//一般用于跳转后修改页面标题
// router.afterEach((to, from) => {

// })