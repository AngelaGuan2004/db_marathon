// 该文件专门用于创建整个应用的路由器
import VueRouter from 'vue-router'

import Home from '@/view/Home/Home.vue'

import UserTab from '@/components/UserTab.vue'
import UserRegistrations from '@/view/User/UserRegistrations.vue'
import UserInfo from '@/view/User/UserInfo.vue'
import UserVolunteering from '@/view/User/UserVolunteering.vue'
import UserResults from '@/view/User/UserResults.vue'
import UserVolunteerSignup from '@/view/User/UserVolunteerSignup.vue';
import VolunteerDetailForAddStation from '@/view/User/VolunteerDetailForAddStation.vue'

import EventList from '@/view/Event/EventList.vue'
import EventTab from '@/components/EventTab.vue'
import ParticipantLottery from '@/view/Event/ParticipantLottery.vue'
import PacerSelection from '@/view/Event/PacerSelection.vue'
import PacerSubmission from '@/view/Event/PacerSubmission.vue'
import EmergencyRunnerSelection from '@/view/Event/EmergencyRunnerSelection.vue'
import EmergencyRunnerSubmission from '@/view/Event/EmergencyRunnerSubmission.vue'
//创建并暴露一个路由器
export default new VueRouter({
  mode: 'history',
  routes: [
    {
      path: '/Home',
      name: 'Home',
      component: Home
    }, {
      path: '/EventList',
      name: 'EventList',
      component: EventList
    }, {
      path: '/EventTab',
      name: 'EventTab',
      component: EventTab,
      children: [
        {
          path: 'ParticipantLottery',
          name: 'ParticipantLottery',
          component: ParticipantLottery
        }, {
          path: 'PacerSelection',
          name: 'PacerSelection',
          component: PacerSelection
        }, {
          path: 'PacerSubmission',
          name: 'PacerSubmission',
          component: PacerSubmission
        },
        {
          path: 'EmergencyRunnerSelection',
          name: 'EmergencyRunnerSelection',
          component: EmergencyRunnerSelection
        },
        {
          path: 'EmergencyRunnerSubmission',
          name: 'EmergencyRunnerSubmission',
          component: EmergencyRunnerSubmission
        },
      ]
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