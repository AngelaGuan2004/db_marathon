<template>
  <div class="container" v-if="medicalStation">
    <div class="header">{{ medicalStation.eventTitle }}</div>
    <div class="info-container">
      <div class="info">
        <div class="info-item">
          <span><b>赛事名称：</b>{{ medicalStation.eventTitle }}</span>
        </div>
        <div class="info-item">
          <span><b>医疗点 ID：</b>{{ medicalStation.id }}</span>
        </div>
        <div class="info-item">
          <span><b>医疗点位置：</b>{{ medicalStation.location }}</span>
        </div>
      </div>
    </div>
    <div class="buttons-container">
      <button class="large-button" @click="navigateTo('medical-registration')">返回医疗点报名</button>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      medicalStation: null
    };
  },
  created() {
    this.loadMedicalStation();
  },
  watch: {
    '$route.params.id': 'loadMedicalStation'
  },
  methods: {
    loadMedicalStation() {
      const medicalStationId = this.$route.params.id;
      axios.get(`/api/medical-stations/${medicalStationId}`)
        .then(response => {
          this.medicalStation = response.data;
        })
        .catch(error => {
          console.error('Error loading medical station info:', error);
          this.$message.error('加载医疗点信息失败');
        });
    },
    navigateTo(page) {
      this.$router.push({ name: page });
    }
  }
}
</script>

<style scoped>
.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 30px;
  font-family: Arial, sans-serif;
  background-color: white;
  border: 1px solid grey;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}
.header {
  font-size: 28px;
  font-weight: bold;
  margin-bottom: 40px;
}
.info-container {
  display: flex;
  justify-content: center;
}
.info {
  width: 100%;
}
.info-item {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  font-size: 18px;
}
.buttons-container {
  margin-top: 40px;
  display: flex;
  justify-content: center;
}
button {
  padding: 10px 20px;
  font-size: 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  background-color: rgb(224, 128, 128);
  color: #fff;
}
button:hover {
  background-color: rgb(200, 112, 112);
}
.large-button {
  font-size: 20px;
  padding: 15px 25px;
}
</style>
