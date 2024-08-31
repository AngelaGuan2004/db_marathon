<template>
  <div id="EventDetail" :key="$route.fullPath">
    <div style="margin-left: 6%;margin-top: 20px;">
      <el-breadcrumb separator-class="el-icon-arrow-right">
        <el-breadcrumb-item :to="{ path: '/Home' }">首页</el-breadcrumb-item>
        <el-breadcrumb-item :to="{ path: '/EventList' }">赛历</el-breadcrumb-item>
        <el-breadcrumb-item>赛事详情</el-breadcrumb-item>
      </el-breadcrumb>
    </div>
    <div class="EventDetailContainer" v-if="event">
      <div class="EventDetailTitle">{{ event.title }}</div>
      <div class="EventDetailContainerInfo">
        <div class="EventDetailContainerInfoOne">
          <div class="EventDetailContainerInfoItem">
            <img :src="require(`@/assets/images/location.png`)" alt="Event Image" class="EventDetailContainerIcon">
            <span><span>赛事名称：</span>{{ event.title }}</span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img :src="require(`@/assets/images/location.png`)" alt="Event Image" class="EventDetailContainerIcon">
            <span><span>赛事类型：</span>{{ event.type }}</span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img :src="require(`@/assets/images/location.png`)" alt="Event Image" class="EventDetailContainerIcon">
            <span><span>报名开始时间：</span>{{ event.registration_start }}</span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img :src="require(`@/assets/images/location.png`)" alt="Event Image" class="EventDetailContainerIcon">
            <span><span>报名结束时间：</span>{{ event.registration_end }}</span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img :src="require(`@/assets/images/location.png`)" alt="Event Image" class="EventDetailContainerIcon">
            <span><span>正式比赛时间：</span>{{ event.date }}</span>
          </div>
        </div>
        <div class="EventDetailContainerInfoTwo">
          <div class="EventDetailContainerInfoItem">
            <img src="@/assets/images/location.png" alt="Event Image" class="EventDetailContainerIcon">
            <span>
              <span>物资：</span>
              <a @click="openPackageModal">详情>></a>
            </span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img src="@/assets/images/location.png" alt="Event Image" class="EventDetailContainerIcon">
            <span>
              <span>补给点：</span>
              <a @click="openSupplypointModal">详情>></a>
            </span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img src="@/assets/images/location.png" alt="Event Image" class="EventDetailContainerIcon">
            <span>
              <span>医疗点：</span>
              <a @click="openMedicalModal">详情>></a>
            </span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img src="@/assets/images/location.png" alt="Event Image" class="EventDetailContainerIcon">
            <span>
              <span>接驳车：</span>
              <a @click="openShuttleModal">详情>></a>
            </span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img src="@/assets/images/location.png" alt="Event Image" class="EventDetailContainerIcon">
            <span>
              <span>天气：</span>
              <a @click="openWeatherModal">详情>></a>
            </span>
          </div>
        </div>
      </div>
      <div class="EventDetailContainerButton">
        <el-button @click="GoToEventRegistration">选手报名</el-button>
        <el-button @click="openVolunteerSignupModal">志愿者报名</el-button>
      </div>
      <!-- 详情弹窗 -->
      <WeatherDetailsModal v-if="showWeatherModal" :weatherDetails="weatherDetails" @close="closeWeatherModal" />
      <MedicalDetail v-if="showMedicalModal" :medicalPoints="medicalPoints" @close="closeMedicalModal" />
      <PackageDetail v-if="showPackageModal" :packages="packages" @close="closePackageModal" />
      <ShuttleDetail v-if="showShuttleModal" :shuttles="shuttles" @close="closeShuttleModal" />
      <SupplypointDetail v-if="showSupplypointModal" :supplypoints="supplypoints" @close="closeSupplypointModal" />
      <VolunteerSignup v-if="showVolunteerSignupModal" @close="closeVolunteerSignupModal" />
    </div>
  </div>

</template>

<script>
import { fetchEventDetails } from '@/api/EventDetails';
import { getWeatherDetails } from '@/api/WeatherDetails';
import WeatherDetailsModal from './WeatherDetail.vue';
import MedicalDetail from './MedicalDetail.vue';
import PackageDetail from './PackageDetail.vue';
import ShuttleDetail from './ShuttleDetail.vue';
import SupplypointDetail from './SupplypointDetail.vue';
import VolunteerSignup from './VolunteerSignup.vue';


export default {
  name: 'EventDetail',
  data() {
    return {
      events: {
        1: {
          title: '2024拉萨半程马拉松',
          type: '公路',
          registration_start: '2024.05.20 10:00:00',
          registration_end: '2024.07.26 10:00:00',
          date: '2024.09.01',
        },
        2: {
          title: '2024长春马拉松赛',
          type: '公路',
          registration_start: '2024.05.20 10:00:00',
          registration_end: '2024.07.26 10:00:00',
          date: '2024.09.01',
        }
      },
      event: null,
      weatherDetails: [
        {
          time: '2024-08-17 08:00',
          temperature: '15°C',
          condition: '晴',
          canProceed: '是'
        },
        {
          time: '2024-08-17 12:00',
          temperature: '20°C',
          condition: '多云',
          canProceed: '是'
        },
        {
          time: '2024-08-17 16:00',
          temperature: '18°C',
          condition: '小雨',
          canProceed: '是'
        }
      ],
      medicalPoints: [],
      packages: [],
      shuttles: [],
      supplypoints: [],
      showWeatherModal: false,
      showMedicalModal: false,
      showPackageModal: false,
      showShuttleModal: false,
      showSupplypointModal: false,
      showVolunteerSignupModal: false,
    };
  },
  components: {
    WeatherDetailsModal,
    MedicalDetail,
    PackageDetail,
    ShuttleDetail,
    SupplypointDetail,
    VolunteerSignup,
  },
  watch: {
    '$route.params.id': {
      handler: 'loadEvent',
      immediate: true
    }
  },
  methods: {
    async loadEvent() {
      const event = localStorage.getItem('eventid')
      this.event = this.events[event];

      // try {
      //   const res = await fetchEventDetails(eventId);
      //   this.event = res.data;
      // } catch (error) {
      //   console.error('加载赛事详情失败:', error);
      //   alert('加载赛事详情失败');
      // }
    },
    // async loadWeatherDetails() {
    //   try {
    //     const res = await getWeatherDetails(this.$route.params.id);
    //     this.weatherDetails = res.data;
    //   } catch (error) {
    //     console.error('加载天气详情失败:', error);
    //     alert('加载天气详情失败');
    //   }
    // },
    openWeatherModal() {
      this.showWeatherModal = true;
    },
    closeWeatherModal() {
      this.showWeatherModal = false;
    },
    openMedicalModal() {
      this.showMedicalModal = false;  // 先设置为 false
      this.$nextTick(() => {
        this.showMedicalModal = true; // 再设置为 true，确保弹窗能正确打开
      });
    },
    closeMedicalModal() {
      this.showMedicalModal = false;
    },
    openPackageModal() {
      // 加载物资详情逻辑
      this.showPackageModal = false;  // 先设置为 false
      this.$nextTick(() => {
        this.showPackageModal = true; // 再设置为 true，确保弹窗能正确打开
      });
    },
    closePackageModal() {
      this.showPackageModal = false;
    },
    openShuttleModal() {
      // 加载接驳车详情逻辑
      this.showShuttleModal = false;  // 先设置为 false
      this.$nextTick(() => {
        this.showShuttleModal = true; // 再设置为 true，确保弹窗能正确打开
      });
    },
    closeShuttleModal() {
      this.showShuttleModal = false;
    },
    openSupplypointModal() {
      // 加载补给点详情逻辑
      this.showSupplypointModal = false;  // 先设置为 false
      this.$nextTick(() => {
        this.showSupplypointModal = true; // 再设置为 true，确保弹窗能正确打开
      });
    },
    closeSupplypointModal() {
      this.showSupplypointModal = false;
    },
    openVolunteerSignupModal() {
      this.showVolunteerSignupModal = false
      this.$nextTick(() => {
        this.showVolunteerSignupModal = true; // 再设置为 true，确保弹窗能正确打开
      });
    },
    closeVolunteerSignupModal() {
      this.showVolunteerSignupModal = false
    },
    GoToEventRegistration() {
      this.$router.push({ name: 'EventRegistration', params: { id: this.$route.params.id } });
    }
  },
  created() {
    const id = this.$route.params.id || localStorage.getItem('eventid')
    localStorage.setItem('eventid', id)
    this.loadEvent();
  },
}
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/EventDetail.css";
</style>