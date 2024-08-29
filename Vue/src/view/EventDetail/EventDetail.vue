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
            <span><span>物资：</span><a @click="navigateTo('supplies')" class="EventDetailLink">查看详情>></a></span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img src="@/assets/images/location.png" alt="Event Image" class="EventDetailContainerIcon">
            <span><span>补给点：</span><a @click="navigateTo('refreshments')" class="EventDetailLink">查看详情>></a></span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img src="@/assets/images/location.png" alt="Event Image" class="EventDetailContainerIcon">
            <span><span>医疗点：</span><a @click="navigateTo('medical')" class="EventDetailLink">查看详情>></a></span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img src="@/assets/images/location.png" alt="Event Image" class="EventDetailContainerIcon">
            <span><span>接驳车：</span><a @click="navigateTo('shuttle')" class="EventDetailLink">查看详情>></a></span>
          </div>
          <div class="EventDetailContainerInfoItem">
            <img src="@/assets/images/location.png" alt="Event Image" class="EventDetailContainerIcon">
            <span><span>天气：</span><a @click="openWeatherModal" class="EventDetailLink">查看详情>></a></span>
          </div>
        </div>
      </div>
      <!-- 天气详情弹窗 -->
      <WeatherDetailsModal v-if="showWeatherModal" :weatherDetails="weatherDetails" @close="closeWeatherModal" />
      <div class="EventDetailContainerButton">
        <el-button @click="navigateTo('participant-registration')">选手报名</el-button>
        <el-button @click="navigateTo('volunteer-registration')">志愿者报名</el-button>
      </div>
    </div>
  </div>

</template>

<script>
import { fetchEventDetails } from '@/api/EventDetails';
import WeatherDetailsModal from './WeatherDetail.vue';
import { getWeatherDetails } from '@/api/WeatherDetails';

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
      showWeatherModal: false
    };
  },
  components: {
    WeatherDetailsModal
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
      // this.loadWeatherDetails(); // 暂时注释掉接口调用
      this.showWeatherModal = true;
    },
    closeWeatherModal() {
      this.showWeatherModal = false;
    },
    navigateTo(page) {
      if (page === 'participant-registration') {
        this.$router.push({ name: 'EventRegistration', params: { id: this.$route.params.id } });
      }
      else if (page === 'supplies') {
        this.$router.push({ name: 'PackageDetail', params: { id: this.$route.params.id } });
      }
      else if (page === 'refreshments') {
        this.$router.push({ name: 'SupplypointDetail', params: { id: this.$route.params.id } });
      }
      else if (page === 'medical') {
        this.$router.push({ name: 'MedicalDetail', params: { id: this.$route.params.id } });
      }
      else if (page === 'shuttle') {
        this.$router.push({ name: 'ShuttleDetail', params: { id: this.$route.params.id } });
      }
      else if (page === 'volunteer-registration') {
        this.$router.push({ name: 'VolunteerSignup', params: { id: this.$route.params.id } });
      }
      else {
        this.$router.push({ name: page });
      }
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