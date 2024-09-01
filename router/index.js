import Vue from 'vue';
import Router from 'vue-router';
import MyResults from '@/view/MyResults.vue';
import MedicalPointManagement from '@/view/MedicalPointManagement.vue'
import SupplyPointManagement from '@/view/SupplyPointManagement.vue'
import ShuttleManagement from '@/view/ShuttleManagement.vue'
import ResultEntry from '@/view/ResultEntry.vue'
import LeaderBoard from '@/view/LeaderBoard.vue'

Vue.use(Router);

export default new Router({
    mode: 'history',
    routes: [
    /*
      {
        path: '/',
        name: 'ResultEntry',
        component:ResultEntry
      },
      */
    
     /*
      {
        path: '/',
        name: 'LeaderBoard',
        component:LeaderBoard
      },
*/
   
   /*
      {
        path:'/',
        name:'MyResults',
        component:MyResults
        
      },
    */
     /*
      {
        path:'/',
        name:'MedicalPointManagement',
        component:MedicalPointManagement
      },
    */
     
      {
        path:'/',
        name:'SupplyPointManagement',
        component:SupplyPointManagement
      },
      
    /*
      {
        path:'/',
        name:'ShuttleManagement',
        component:ShuttleManagement
      },
    */
     /*
      {
        path:'/',
        name:'LeaderBoard',
        component:LeaderBoard
      },
      */
    ]
    })
  

