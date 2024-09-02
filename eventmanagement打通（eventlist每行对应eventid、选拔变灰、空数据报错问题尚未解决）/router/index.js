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

import EventManagementTab from '@/components/EventManagementTab.vue'
import EventVolunteerTab from '@/components/EventVolunteerTab.vue'

import EventList from '@/view/EventManagement/EventList.vue'

import ParticipantLottery from '@/view/EventManagement/ParticipantLottery.vue'
import PacerSelection from '@/view/EventManagement/PacerSelection.vue'
import EmergencyRunnerSelection from '@/view/EventManagement/EmergencyRunnerSelection.vue'

import CarManagement from '@/view/EventVolunteer/CarManagement.vue'
import MedicalManagement from '@/view/EventVolunteer/MedicalManagement.vue'
import SupplyManagement from '@/view/EventVolunteer/SupplyManagement.vue'
import VolunManagement from '@/view/EventVolunteer/VolunManagement.vue'
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
      path: '/EventManagementTab',
      name: 'EventManagementTab',
      component: EventManagementTab,
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
          path: 'EmergencyRunnerSelection',
          name: 'EmergencyRunnerSelection',
          component: EmergencyRunnerSelection
        },
      ]
    }, {
      path: '/EventVolunteerTab',
      name: 'EventVolunteerTab',
      component: EventVolunteerTab,
      children: [
        {
          path: '/CarManagement',
          name: 'CarManagement',
          component: CarManagement
        }, {
          path: '/MedicalManagement',
          name: 'MedicalManagement',
          component: MedicalManagement
        }, {
          path: '/SupplyManagement',
          name: 'SupplyManagement',
          component: SupplyManagement
        }, {
          path: '/VolunManagement',
          name: 'VolunManagement',
          component: VolunManagement
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