import Vue from 'vue'
import App from './App.vue'
import router from './router'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import axios from 'axios';
import store from './store';
import qs from 'qs';
//import { createApp } from 'vue'
import VueParticles from 'vue-particles'


// 将 qs 库添加到 Vue 实例的原型属性中，便于在组件中使用
Vue.prototype.$qs = qs;

// 设置 Vue 的生产提示为 false，即在生产环境中关闭 Vue 的提示
Vue.config.productionTip = false;

// 使用 ElementUI 组件库
Vue.use(ElementUI);

// 将 axios 库添加到 Vue 实例的原型属性中，便于在组件中使用
Vue.prototype.$http = axios;

// 使用 VueParticles 插件
Vue.use(VueParticles);

// 创建并挂载一个新的 Vue 实例
new Vue({
    // 注入路由对象，使整个应用都有路由功能
    router,
    // 注入 Vuex store 对象，使整个应用都有状态管理功能
    store,
    // 渲染 App 组件并挂载到 id 为 'app' 的元素上
    render: h => h(App),
}).$mount('#app')