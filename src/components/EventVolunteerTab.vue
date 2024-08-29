<template>
  <div id="EventVolunteerTab">
    <div class="Event">
      <div style="margin-bottom: 50px;">
        <el-breadcrumb separator-class="el-icon-arrow-right">
          <el-breadcrumb-item :to="{ path: '/Home' }">首页</el-breadcrumb-item>
          <el-breadcrumb-item :to="{ path: '/EventList' }">赛历</el-breadcrumb-item>
          <el-breadcrumb-item>志愿管理</el-breadcrumb-item>
        </el-breadcrumb>
      </div>
      <div class="TabRight" style="margin-right: 15%;margin-top: 5%;">
        <div class="TabFlex">
          <div class="Tab" style="margin-top: 0;">
            <el-menu ref="EventVolunteerTab" :default-active="ActiveIndexForVolunteerTab" class="el-menu-vertical-demo"
              @select="ActiveIndex" active-text-color="rgb(168, 27, 31)">
              <el-menu-item index="1">
                <span>志愿者分工</span>
              </el-menu-item>
              <el-menu-item index="2">
                <span>补给点排班</span>
              </el-menu-item>
              <el-menu-item index="3">
                <span>医疗点排班</span>
              </el-menu-item>
              <el-menu-item index="4">
                <span>摆渡车排班</span>
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
  name: 'EventVolunteerTab',
  data() {
    return {
      ActiveIndexForVolunteerTab: '',
      EventTabTitle: ['VolunManagement', 'SupplyManagement', 'MedicalManagement', 'CarManagement'],
    };
  },
  methods: {
    ActiveIndex(index) {
      this.ActiveIndexForVolunteerTab = index; // 设置当前激活的菜单项
      this.$router.push({ name: this.EventTabTitle[index - 1] });
    },
  },
  created() {
    this.$store.dispatch('loadInitialData');
  },
  mounted() {
    // this.$router.push({ name: 'ParticipantLottery' })
    this.$bus.$on('IsLottery', this.LotteryPermission);
    // 全局总线备用
    this.$bus.$on('ActiveIndexForVolunteerTab', this.ActiveIndex);
    this.$bus.$emit('ActiveIndexForVolunteerTab', '1')
  },
  beforeDestroy() {
    this.$bus.$off('ActiveIndexForVolunteerTab', this.ActiveIndex);
    this.$bus.$off('IsLottery', this.LotteryPermission);
  }
};
</script>

<style>
@import 'element-ui/lib/theme-chalk/index.css';
@import "../assets/css/Base.css";
@import "../assets/css/LeftTab.css";

.Event {
  width: 100%;
  height: 750px;
  background-color: rgb(244, 244, 244);
  padding: 0 100px;
  padding-top: 20px;
}
</style>
