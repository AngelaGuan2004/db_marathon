import Vue from 'vue';
import Router from 'vue-router';
import EventList from '@/view/Event/EventList.vue';
import EventDetail from '@/view/Event/EventDetails.vue';
import EventRegistration from '@/view/Event/EventRegistration.vue';
import PackageDetails from '@/view/Event/PackageDetails.vue';
import SupplypointDetails from '@/view/Event/SupplypointDetails.vue';
import MedicalDetails from '@/view/Event/MedicalDetails.vue';
import ShuttleDetails from '@/view/Event/ShuttleDetails.vue';
//import SupplypointManagement from '@/view/Event/SupplypointManagement.vue';
//import PackageManagement from '@/view/Event/PackageManagement.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    /*{
      path: '/',
      name: 'PackageManagement',
      component: PackageManagement
    },*/
    {
     path: '/',
     name: 'EventList',
     component: EventList
   },
    {
      path: '/event/:id',
      name: 'EventDetail',
      component: EventDetail,
      props: true
    },
    {
      path: '/participant-registration/:id',
      name: 'EventRegistration',
      component: EventRegistration,
      props: true
    },
    {
      path: '/package-details/:id',
      name: 'PackageDetails',
      component: PackageDetails,
      props: true
    },
    {
      path: '/supplypoint-details/:id',
      name: 'SupplypointDetails',
      component: SupplypointDetails,
      props: true
    },
    {
      path: '/medical-details/:id',
      name: 'MedicalDetails',
      component: MedicalDetails,
      props: true
    },
    {
      path: '/shuttle-details/:id',
      name: 'ShuttleDetails',
      component: ShuttleDetails,
      props: true
    }
  ]
});
