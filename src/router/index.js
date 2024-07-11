import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

import AddUser from '@/view/Login.vue';


export default new Router({
    routes: [
        { path: '/', redirect: '/login' },   //定义路径，添加组件
        { path: '/login', component: AddUser }
    ]

}
);