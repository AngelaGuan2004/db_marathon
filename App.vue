<!-- src/App.vue -->
<template>
  <div id="app">
    <UserRegister />
  </div>
</template>

<script>
import UserRegister from './components/LoginPage.vue';  // 导入注册组件

export default {
  name: 'App',
  components: {
    UserRegister
  }
}
</script>

<style>
/* 全局样式 */
</style>
