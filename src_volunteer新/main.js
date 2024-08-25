import Vue from 'vue';
import App from './App.vue';
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import router from './router'; 
import axios from 'axios';

Vue.prototype.$http = axios; // 将 axios 绑定到 Vue 实例的原型上


Vue.use(ElementUI);

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');



