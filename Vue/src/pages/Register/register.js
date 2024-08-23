import Vue from 'vue'
import Register from './Register.vue'
//引入elm组件
import ElementUI from 'element-ui';
//引入store
//import store from './store'
//引入VueRouter
import VueRouter from 'vue-router'
//引入全局css
import '@/assets/css/Base.css'

Vue.config.productionTip = false

Vue.use(ElementUI);
Vue.use(VueRouter)

new Vue({
  render: h => h(Register),
  //store,
  //router,
  beforeCreate() {
    debugger
    Vue.prototype.$bus = this
  }
}).$mount('#register')
