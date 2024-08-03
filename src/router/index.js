import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router);

import PhotoWall from '@/view/PhotoWall.vue'
import PhotographerCenter from '@/view/PhotographerCenter.vue'

export default new Router({
    routes:[
        { path:'/', component:PhotoWall},
        { path:'/photoWall', component:PhotoWall},
        { path:'/photographerCenter', component:PhotographerCenter}
    ]
})

