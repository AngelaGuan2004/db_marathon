<template>
  <div id="UserTab">
    <div class="User">
      <div class="breadcrumb">
        <el-breadcrumb separator-class="el-icon-arrow-right">
          <el-breadcrumb-item :to="{ path: '/Home' }">首页</el-breadcrumb-item>
          <el-breadcrumb-item>我的</el-breadcrumb-item>
        </el-breadcrumb>
      </div>
      <div class="UserTabRight">
        <div class="UserTabFlex">
          <div class="UserTab">
            <el-menu default-active="1" class="el-menu-vertical-demo" @select="ActiveTab"
              active-text-color="rgb(168, 27, 31)">
              <el-menu-item index="1">
                <span slot="title">个人信息</span>
              </el-menu-item>
              <el-menu-item index="2">
                <span slot="title">我的成绩</span>
              </el-menu-item>
              <el-menu-item index="3">
                <span slot="title">我的报名</span>
              </el-menu-item>
              <el-menu-item index="4">
                <span slot="title">我的志愿</span>
              </el-menu-item>
            </el-menu>
          </div>
        </div>
        <router-view></router-view>
      </div>
    </div>
  </div>

</template>

<script>
export default {
  name: 'UserTab',
  data() {
    return {
      UserTabTitle: ['UserInfo', 'UserResults', 'UserRegistrations', 'UserVolunteering']
    }
  },
  methods: {
    ActiveTab(index) {
      this.$router.push({ name: this.UserTabTitle[index - 1], })
    }
  },
  mounted() {
    this.$router.push({ name: 'UserInfo' })
  }
}
</script>

<style scoped>
@import "../assets/css/Home.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "../assets/css/UserTab.css";
</style>