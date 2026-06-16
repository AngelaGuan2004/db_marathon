<template>
  <div class="container" v-if="event">
    <div class="header">{{ event.title }}</div>
    <div class="info-container">
      <div class="info">
        <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>赛事名称：</b>{{ event.location }}</span>
        </div>
        <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>赛事类型：</b>{{ event.registration }}</span>
        </div>
        <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>报名开始时间：</b>{{ event.date }}</span>
        </div>
        <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>报名结束时间：</b>{{ event.type }}</span>
        </div>
        <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>正式比赛时间：</b>{{ event.project }}</span>
        </div>
        <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>物资：</b><a @click="navigateTo('supplies')" class="detail-link">查看详情>></a></span>
        </div>
        <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>补给点：</b><a @click="navigateTo('refreshments')" class="detail-link">查看详情>></a></span>
        </div>
        <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>医疗点：</b><a @click="navigateTo('medical')" class="detail-link">查看详情>></a></span>
        </div>
        <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>接驳车：</b><a @click="navigateTo('shuttle')" class="detail-link">查看详情>></a></span>
        </div>
      </div>
      <div class="info-item">
          <img :src="require(`@/assets/${event.image}`)" alt="Event Image" class="event-icon">
          <span><b>天气：</b><a @click="openWeatherModal" class="detail-link">查看详情>></a></span>
        </div>
    </div>

    <!-- 天气详情弹窗 -->
    <WeatherDetailsModal
      v-if="showWeatherModal"
      :weatherDetails="weatherDetails"
      @close="closeWeatherModal"
    />

    <div class="buttons-container">
      <button class="large-button" @click="navigateTo('participant-registration')">选手报名</button>
      <button class="large-button" @click="navigateTo('volunteer-registration')">志愿者报名</button>
    </div>
  </div>

</template>

<script>
import { fetchEventDetails } from '@/api/EventDetails';
import WeatherDetailsModal from './WeatherDetails.vue';
import {getWeatherDetails} from '@/api/WeatherDetails';

export default {
  data() {
    return {
      events: {
        1: {
          title: '2024拉萨半程马拉松',
          image: 'location.png',
          location: '西藏自治区-拉萨市',
          registration: '2024.05.20 10:00:00 - 2024.07.26 10:00:00',
          date: '2024.08.17',
          type: '公路',
          project: '半程, 10公里, 5.6公里',
          scale: '5000人',
          organizer: '拉萨市人民政府',
          promoter: '成都中体大健康体育文化传播有限公司',
          coorganizer: '拉萨市文化局, 拉萨市旅游发展局'
        },
        2: {
          title: '2024长春马拉松赛',
          image: 'location.png',
          location: '吉林省-长春市',
          registration: '2024.07.01 10:00:00 - 2024.07.31 18:00:00',
          date: '2024.09.01',
          type: '公路',
          project: '全程, 半程, 7公里',
          scale: '30000人',
          organizer: '长春市人民政府, 吉林省体育局',
          promoter: '厦门文广体育有限公司',
          coorganizer: '吉林省田径协会'
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
  created() {
    this.loadEvent();
  },
  watch: {
    '$route.params.id': 'loadEvent'
  },
  methods: {
    async loadEvent() {
      const eventId = this.$route.params.id;
      this.event = this.events[eventId];
      
      try {
        const res = await fetchEventDetails(eventId);
        this.event = res.data;
      } catch (error) {
        console.error('加载赛事详情失败:', error);
        alert('加载赛事详情失败');
      }
    },
    async loadWeatherDetails() {
      try {
        const res = await getWeatherDetails(this.$route.params.id);
        this.weatherDetails = res.data;
      } catch (error) {
        console.error('加载天气详情失败:', error);
        alert('加载天气详情失败');
      }
    },
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
      this.$router.push({ name: 'PackageDetails', params: { id: this.$route.params.id } });
    }
    else if (page === 'refreshments') {
      this.$router.push({ name: 'SupplypointDetails', params: { id: this.$route.params.id } });
    }
    else if (page === 'medical') {
      this.$router.push({ name: 'MedicalDetails', params: { id: this.$route.params.id } });
    }
    else if (page === 'shuttle') {
      this.$router.push({ name: 'ShuttleDetails', params: { id: this.$route.params.id } });
    }
    else {
      this.$router.push({ name: page });
    }
  }
}
}
</script>

<style>
.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 30px;
  font-family: Arial, sans-serif;
  background-color: white;
  border: 1px solid grey;
  border-radius: 8px;
  box-shadow: 0 2px 4px white;
}
.header {
  font-size: 28px;
  font-weight: bold;
  margin-bottom: 40px;
}
.info-container {
  display: flex;
  justify-content: space-between;
}
.info, .info-2 {
  width: 48%;
}
.info-item {
  display: flex;
  align-items: left;
  margin-bottom: 30px;
  font-size: 20px;
}
.event-icon {
  width: 32px;
  height: 32px;
  margin-right: 10px;
}
.detail-link {
  color: black;
  text-decoration: none;
  cursor: pointer;
}
.detail-link:hover {
  color: red;
}
.buttons-container {
  margin-top: 40px;
  display: flex;
  justify-content: space-around;
}
button {
  padding: 10px 20px;
  font-size: 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  background-color: rgb(201, 64, 64);
  color: #fff;
}
button:hover {
  background-color: rgb(201, 64, 64);
}
.large-button {
  font-size: 20px;
  padding: 15px 25px;
}
</style>
