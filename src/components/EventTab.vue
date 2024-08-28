<template>
  <div id="EventTab">
    <div class="Event">
      <div style="margin-bottom: 50px;">
        <el-breadcrumb separator-class="el-icon-arrow-right">
          <el-breadcrumb-item :to="{ path: '/Home' }">首页</el-breadcrumb-item>
          <el-breadcrumb-item :to="{ path: '/EventList' }">赛历</el-breadcrumb-item>
          <el-breadcrumb-item>赛事管理</el-breadcrumb-item>
        </el-breadcrumb>
      </div>
      <div class="TabRight" style="margin-right: 15%;margin-top: 5%;">
        <div class="TabFlex">
          <div class="Tab" style="margin-top: 0;">
            <el-menu :default-active="ActiveIndexForEventTab" class="el-menu-vertical-demo" @select="ActiveTab"
              active-text-color="rgb(168, 27, 31)">
              <el-menu-item index="1">
                <span>选手抽签</span>
              </el-menu-item>
              <el-submenu index="6" menu-trigger="hover">
                <template slot="title">
                  <span>配速员选拔</span>
                </template>
                <el-menu-item index="2">待选跑者</el-menu-item>
                <el-menu-item index="3">已选跑者</el-menu-item>
              </el-submenu>
              <el-submenu index="7" menu-trigger="hover">
                <template slot="title">
                  <span>急救跑者选拔</span>
                </template>
                <el-menu-item index="4">待选跑者</el-menu-item>
                <el-menu-item index="5" style="margin-bottom: 10px;">已选跑者</el-menu-item>
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
export default {
  name: 'EventTab',
  data() {
    return {
      ActiveIndexForEventTab: '',
      EventTabTitle: ['ParticipantLottery', 'PacerSelection', 'PacerSubmission', 'EmergencyRunnerSelection', 'EmergencyRunnerSubmission'],
    };
  },
  methods: {
    ActiveTab(index) {
      this.$router.push({ name: this.EventTabTitle[index - 1], })
    },
    ActiveIndex(index) {
      this.ActiveIndexForEventTab = index; // 设置当前激活的菜单项
      if (index.includes('-')) { // 处理submenu的子项
        const [mainIndex, subIndex] = index.split('-');
        this.$router.push({ name: this.EventTabTitle[parseInt(mainIndex, 10) - 1 + parseInt(subIndex, 10) - 1] });
      } else {
        this.$router.push({ name: this.EventTabTitle[index - 1] });
      }
    }
  },
  created() {
    this.$store.dispatch('loadInitialData');
  },
  mounted() {
    this.$router.push({ name: 'ParticipantLottery' })
    // 全局总线备用
    this.$bus.$on('ActiveIndexForEventTab', this.ActiveIndex);
    this.$bus.$emit('ActiveIndexForEventTab', '1')
  },
  beforeDestroy() {
    this.$bus.$off('ActiveIndexForEventTab', this.updateActiveIndex);
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
