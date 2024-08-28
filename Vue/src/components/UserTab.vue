<template>
  <div id="UserTab">
    <div class="User">
      <div style="margin-bottom: 50px;">
        <el-breadcrumb separator-class="el-icon-arrow-right">
          <el-breadcrumb-item :to="{ path: '/Home' }">首页</el-breadcrumb-item>
          <el-breadcrumb-item>我的</el-breadcrumb-item>
        </el-breadcrumb>
      </div>
      <div class="TabRight">
        <div class="TabFlex">
          <div class="Tab">
            <el-menu ref="UserMenu" :default-active="ActiveIndexForUserTab" class="el-menu-vertical-demo"
              @select="ActiveTab" active-text-color="rgb(168, 27, 31)">
              <el-menu-item index="1">
                <span>个人信息</span>
              </el-menu-item>
              <el-menu-item index="2">
                <span>我的成绩</span>
              </el-menu-item>
              <el-menu-item index="3">
                <span>我的报名</span>
              </el-menu-item>
              <el-submenu index="6" menu-trigger="hover">
                <template slot="title">
                  <span>我的志愿</span>
                </template>
                <el-menu-item index="4">成为志愿者</el-menu-item>
                <el-menu-item index="5" style="margin-bottom: 10px;">已报名志愿</el-menu-item>
              </el-submenu>
            </el-menu>
          </div>
        </div>
        <router-view></router-view>
      </div>
    </div>
  </div>
</template>

<script>
import { Message } from 'element-ui';
export default {
  name: 'UserTab',
  data() {
    return {
      UserTabTitle: ['UserInfo', 'UserResults', 'UserRegistrations', 'UserVolunteerSignup', 'UserVolunteering'],
      ActiveIndexForUserTab: "1"
    }
  },
  methods: {
    ActiveTab(index) {

      this.$router.push({ name: this.UserTabTitle[index - 1], })
    },
    ActiveIndex(index) {
      this.ActiveIndexForUserTab = index; // 设置当前激活的菜单项
      this.$refs.UserMenu.activeIndex = index;
      if (index.includes('-')) { // 处理submenu的子项
        const [mainIndex, subIndex] = index.split('-');
        this.$router.push({ name: this.UserTabTitle[parseInt(mainIndex, 10) - 1 + parseInt(subIndex, 10) - 1] });
      } else {
        this.$router.push({ name: this.UserTabTitle[index - 1] });
      }
    }
  },
  mounted() {
    this.$router.push({ name: 'UserInfo' })
    this.$bus.$on('ActiveIndexForUserTab', this.ActiveIndex);
  },
  beforeDestroy() {
    this.$bus.$off('ActiveIndexForUserTab', this.ActiveIndex);
  },
  beforeRouteEnter(to, from, next) {
    console.log(Message)
    let role = localStorage.getItem('UserRole') || 'Visitor';
    if (role === 'Visitor') {
      Message({
        type: 'warning',
        message: '请先登录'
      });
      setTimeout(() => {
        location.href = 'login.html';
      }, 1500)
    } else if (role === 'Admin') {
      Message({
        type: 'warning',
        message: '您是管理者，没有身份信息'
      });
    }
    else {
      next(); // 允许进入
    }
  },
}
</script>

<style scoped>
@import "../assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "../assets/css/LeftTab.css";

.User {
  width: 100%;
  height: 600px;
  background-color: rgb(244, 244, 244);
  padding: 0 100px;
  padding-top: 20px;
}
</style>