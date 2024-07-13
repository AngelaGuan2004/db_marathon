import Vue from 'vue';
import Router from 'vue-router';
import myGrade from '@/view/MyResults.vue';
import rank from '@/view/LeaderBoard.vue';
import cyy from '@/view/PackageManagement.vue'
import PackageManagement from '@/view/PackageManagement.vue';
import MedicalPointManagement from '@/view/MedicalPointManagement.vue'
import SupplyPointManagemrnt from '@/view/SupplyPointManagement.vue'
import ShuttleBusManagement from '@/view/ShuttleManagement.vue'

Vue.use(Router);

export default new Router({
    // mode: 'history',
    routes: [
      {
        path: '/',
        name: 'ShuttleBusManagement',
        component:ShuttleBusManagement
      },
    ]
    })

