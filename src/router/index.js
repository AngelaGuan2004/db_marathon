import Vue from 'vue';
import Router from 'vue-router';
import EventList from '@/view/EventManagement/EventList.vue';
import ParticipantLottery from '@/view/EventManagement/ParticipantLottery.vue';
import PacerSelection from '@/view/EventManagement/PacerSelection.vue';
import EmergencyRunnerSelection from '@/view/EventManagement/EmergencyRunnerSelection.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'EventList',
      component: EventList
    },
    {
      path: '/participantLottery',
      name: 'ParticipantLottery',
      component: ParticipantLottery,
      props: true
    },
    {
      path: '/participantLottery/pacerSelection',
      name: 'PacerSelection',
      component: PacerSelection,
      props: true
    },
    {
      path: '/participantLottery/emergencyRunnerSelection',
      name: 'EmergencyRunnerSelection',
      component: EmergencyRunnerSelection,
      props: true
    }
  ]
});
