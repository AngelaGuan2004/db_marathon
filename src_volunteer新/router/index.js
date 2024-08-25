import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

//导入需要的组件
//我的
// import ManagerPage from '@/view/my_register_and_loginpage/ManagerPage.vue'
// import LoginPage from '@/view/my_register_and_loginpage/LoginPage.vue'
// import PhotographerPage from '@/view/my_register_and_loginpage/PhotographerPage.vue'
// import RegisterPage from '@/view/my_register_and_loginpage/RegisterPage.vue'
import VolunteerDetail1 from '@/components/volunteerDetail1.vue';
import VolunteerDetail2 from '@/components/volunteerDetail2.vue';


export default new Router({
    //在这里声明路由规则
    routes: [
        // {
        //     path: '/',
        //     name: 'LoginPage',
        //     component: LoginPage
        // },
        // {
        //     path: '/regist',
        //     name: 'RegisterPage',
        //     component: RegisterPage
        // },
        // {
        //     path: '/photo',
        //     name: 'PhotographerPage',
        //     component: PhotographerPage
        // },
        // {
        //     path: '/manage',
        //     name: 'ManagerPage',
        //     component: ManagerPage
        // },
        {
            path: '/volunteer1',
            name: 'VolunteerDetail1',
            component: VolunteerDetail1 ,
          },
        {
            path: '/volunteer2',
            name: 'VolunteerDetail2',
            component: VolunteerDetail2 ,
        }
        
    ]
}
)
