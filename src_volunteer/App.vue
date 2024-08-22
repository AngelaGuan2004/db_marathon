<template>
  <div id="app">
    <VolunteerDetail />
  </div>
</template>

<script>
// 从 ./components/volunteerSignup.vue 导入组件
import VolunteerDetail from './components/volunteerDetail.vue'

export default {
  name: 'App',
  components: {
    // 注册组件
    VolunteerDetail
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
