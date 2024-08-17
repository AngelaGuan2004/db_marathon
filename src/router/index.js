import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router);

import PhotoWall from '@/view/PhotoWall.vue'
import PhotographerCenter from '@/view/PhotographerCenter.vue'
import MyPhotographyWorks from '@/view/MyPhotographyWorks.vue'
import InjuryEntry from '@/view/InjuryEntry.vue'

export default new Router({
    routes:[
        { path:'/', component:PhotoWall},
        { path:'/photoWall', component:PhotoWall},
        { path:'/photographerCenter', component:PhotographerCenter},
        { path:'/photographerCenter/myPhotographyWorks', component:MyPhotographyWorks},
        { path:'/injuryEntry',component:InjuryEntry},
    ]
})



