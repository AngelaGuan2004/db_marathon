import Vue from 'vue';
import Router from 'vue-router';
import VolunManagement from '@/view/VolunteerArrangement/VolunManagement.vue';
import SupplyManagement from '@/view/VolunteerArrangement/SupplyManagement.vue';
import MedicalManagement from '@/view/VolunteerArrangement/MedicalManagement.vue';
import CarManagement from '@/view/VolunteerArrangement/CarManagement.vue';


Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'VolunManagement',
      component: VolunManagement,
      props: true
    },
    {
      path: '/SupplyManagement',
      name: 'SupplyManagement',
      component: SupplyManagement,
      props: true
    },
    {
      path: '/MedicalManagement',
      name: 'MedicalManagement',
      component: MedicalManagement,
      props: true
    },
    {
      path: '/CarManagement',
      name: 'CarManagement',
      component: CarManagement,
      props: true
    }
  ]
});
