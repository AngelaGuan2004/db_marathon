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
          <el-menu-item index="3">照片墙</el-menu-item>
          <el-menu-item index="4">我的</el-menu-item>
        </el-menu>
      </div>
      <div class="HeaderLogin">
        <el-menu class="el-menu-demo" mode="horizontal" @select="handleSelectLogin" background-color="transparent"
          text-color="white" active-text-color="white" v-if="GetRole() === 'Visitor'">
          <el-menu-item index="1">登录</el-menu-item>
        </el-menu>
        <div v-else>
          <div
            style="display: inline-block;height: 100px;vertical-align: top;text-align: center;margin-top: 35px;margin-right: 35px;">
            <i class="el-icon-user-solid"></i>
            <div style="font-size: 16px;color: white;margin-top: 5px;">{{ TranslateRoleToChinese(GetRole()) }}</div>
          </div>
          <div style="display: inline-block;">
            <el-menu class="el-menu-demo" mode="horizontal" @select="handleSelectExit" background-color="transparent"
              text-color="white" active-text-color="white">
              <el-menu-item index="7">退出登录</el-menu-item>
            </el-menu>
          </div>
        </div>
      </div>
    </div>
    <router-view></router-view>
  </div>
</template>


<script>
import { fetchAllEvents } from '@/api/Event'
export default {
  name: 'Header',
  data() {
    return {
      TabHerf: ['Home', 'EventList', 'PhotoWall', 'UserTab'],
      Role: '',
      events: [],
      ActiveIndexForHeader: "1"
    };
  },
  mounted() {
    this.loadAllEvents();  // 在组件创建时加载所有赛事信息

  },
  methods: {
    loadAllEvents() {
      fetchAllEvents().then(response => {
        console.log(response);
        // 映射 eventList 数组
        this.events = response.map(Event => ({
          id: Event.id,
          date: this.formatDate(Event.event_Date),
          name: Event.name,
          type: Event.category,
          scale: Event.scale
        }))
        this.$router.push({ name: 'Home', params: { events: this.events } });
      }).catch(error => {
        // 错误处理
        this.$message.error('加载赛事失败，请稍后重试。');
        console.error('Failed to load events:', error);
      });
    },
    formatDate(dateString) {
      const date = new Date(dateString); // 创建一个Date对象
      const year = date.getFullYear(); // 获取年份
      const month = (date.getMonth() + 1).toString().padStart(2, '0'); // 获取月份，月份从0开始计数，所以加1
      const day = date.getDate().toString().padStart(2, '0'); // 获取日期

      return `${year}-${month}-${day}`; // 返回格式化的日期字符串
    },
    handleSelect(key) {
      if (key === '1' || key === '2')
        this.$router.push({ name: this.TabHerf[key - 1], params: { events: this.events } })
      else
        this.$router.push({ name: this.TabHerf[key - 1], })
    },
    handleSelectLogin() {
      this.$router.push({ name: 'Home' })
      location.href = "login.html"
    },
    handleSelectExit() {
      localStorage.clear()
      location.href = "login.html"
    },
    GetRole() {
      return localStorage.getItem('UserRole') || 'Visitor'
    },
    TranslateRoleToChinese(role) {
      if (role === 'Visitor')
        return '访客'
      else if (role === 'Athlete')
        return '跑者'
      else if (role === 'Photographer')
        return '摄影师'
      else if (role === 'Admin')
        return '管理员'
    }
  },
}
</script>

<style scoped>
@import "../assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "../assets/css/Header.css";
</style>