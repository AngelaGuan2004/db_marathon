<template>
  <div id="Header">
    <div class="Header">
      <div class="HeaderLogo">
        <img src="../assets/images/JiMaIndex.png" alt="">
      </div>
      <div class="HeaderTab">
        <el-menu :default-active="ActiveIndexForHeader" class="el-menu-demo" mode="horizontal" @select="handleSelect"
          background-color="transparent" text-color="white" active-text-color="white">
          <el-menu-item index="1">首页</el-menu-item>
          <el-menu-item index="2">赛历</el-menu-item>
          <el-menu-item index="3" :disabled="!(GetRole() === 'Admin')">赛事管理</el-menu-item>
          <el-menu-item index=" 4" :disabled="!(GetRole() === 'Admin' || GetRole() === 'Volunteer')">志愿服务</el-menu-item>
          <el-menu-item index=" 5">照片墙</el-menu-item>
          <el-menu-item index=" 6">我的</el-menu-item>
        </el-menu>
      </div>
      <div class=" HeaderTab HeaderLogin">
        <el-menu class="el-menu-demo" mode="horizontal" @select="handleSelectLogin" background-color="transparent"
          text-color="white" active-text-color="white">
          <el-menu-item index="1">登录</el-menu-item>
        </el-menu>
      </div>
    </div>
    <router-view></router-view>
  </div>

</template>

<script>
export default {
  name: 'Header',
  data() {
    return {
      TabHerf: ['Home', 'EventList', '', '', '', 'UserTab'],
      Role: '',
      ActiveIndexForHeader: "1"
    };
  },
  methods: {
    handleSelect(key) {
      this.$router.push({ name: this.TabHerf[key - 1], })
    },
    handleSelectLogin() {
      this.$router.push({ name: 'Home' })
      location.href = "login.html"
    },
    GetRole() {
      return localStorage.getItem('UserRole') || 'Visitor'
    },
  },
  created() {
    this.$router.push({ name: 'Home' })
  },
}
</script>

<style scoped>
@import "../assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "../assets/css/Header.css";
</style>