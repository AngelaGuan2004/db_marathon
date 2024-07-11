import Vue from 'vue'
import Router from 'vue-router'
import ManagerPage from '@/view/ManagerPage.vue'
import LoginPage from '@/view/LoginPage.vue'
import PhotographerPage from '@/view/PhotographerPage.vue'
import RegisterPage from '@/view/RegisterPage.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'LoginPage',
      component: LoginPage
    },
    {
      path: '/regist',
      name: 'RegisterPage',
      component: RegisterPage
    },
    {
      path: '/photo',
      name: 'PhotographerPage',
      component: PhotographerPage
    },
    {
      path: '/manage',
      name: 'ManagerPage',
      component: ManagerPage
    }
  ]
})
