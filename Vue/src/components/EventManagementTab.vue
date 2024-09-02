<template>
  <div id="EventManagementTab">
    <div class="Event">
      <div style="margin-bottom: 50px;">
        <el-breadcrumb separator-class="el-icon-arrow-right">
          <el-breadcrumb-item :to="{ path: '/Home' }">首页</el-breadcrumb-item>
          <el-breadcrumb-item :to="{ path: '/EventList' }">赛历</el-breadcrumb-item>
          <el-breadcrumb-item>赛事管理</el-breadcrumb-item>
        </el-breadcrumb>
      </div>
      <div class="TabRight" style="margin-right: 15%;margin-top: 10%;">
        <div class="TabFlex">
          <div class="Tab" style="margin-top: 0;">
            <el-menu ref="EventManagementTab" :default-active="ActiveIndexForEventManagementTab"
              class="el-menu-vertical-demo" @select="ActiveIndex" active-text-color="rgb(168, 27, 31)">
              <el-menu-item index="1">
                <span>选手抽签</span>
              </el-menu-item>
              <el-submenu index="111" menu-trigger="hover">
                <template slot="title">
                  <span>配速员选拔</span>
                </template>
                <el-menu-item index="2">待选跑者</el-menu-item>
                <el-menu-item index="3">已选跑者</el-menu-item>
              </el-submenu>
              <el-submenu index="222" menu-trigger="hover">
                <template slot="title">
                  <span>急救跑者选拔</span>
                </template>
                <el-menu-item index="4">待选跑者</el-menu-item>
                <el-menu-item index="5" style="margin-bottom: 10px;">已选跑者</el-menu-item>
              </el-submenu>
              <el-submenu index="333" menu-trigger="hover">
                <template slot="title">
                  <span>赛事信息管理</span>
                </template>
                <el-menu-item index="6">物资包管理</el-menu-item>
                <el-menu-item index="7" style="margin-bottom: 10px;">补给点管理</el-menu-item>
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
  name: 'EventManagementTab',
  data() {
    return {
      ActiveIndexForEventManagementTab: '',
      IsLottery: false,
      EventManagementTabTitle: ['ParticipantLottery', 'PacerSelection', 'PacerSubmission', 'EmergencyRunnerSelection', 'EmergencyRunnerSubmission', 'PackageManagement', 'SupplypointManagement'],
    };
  },
  methods: {
    ActiveIndex(index) {
      if (this.IsLottery || index === '1' || index === '6' || index === '7') {
        this.ActiveIndexForEventManagementTab = index; // 设置当前激活的菜单项
        this.$router.push({ name: this.EventManagementTabTitle[index - 1], params: { event_id: this.$route.params.event_id, name: this.$route.params.name } });
      }
      else {
        this.$message.warning('请先完成抽签');
        this.$bus.$emit('ActiveIndexForEventManagementTab', '1')
        this.$refs.EventManagementTab.activeIndex = '1';
        this.$router.push({ name: 'ParticipantLottery', params: { event_id: this.$route.params.event_id, name: this.$route.params.name } });
      }
    },
    LotteryPermission() {
      this.IsLottery = true
    }
  },
  created() {
    this.$store.dispatch('loadInitialData');
  },
  mounted() {
    this.$router.push({ name: 'ParticipantLottery' })
    this.$bus.$on('IsLottery', this.LotteryPermission);
    // 全局总线备用
    this.$bus.$on('ActiveIndexForEventManagementTab', this.ActiveIndex);
    this.$bus.$emit('ActiveIndexForEventManagementTab', '1')
  },
  beforeDestroy() {
    this.$bus.$off('ActiveIndexForEventManagementTab', this.ActiveIndex);
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
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 0 100px;
  padding-top: 20px;
  margin-top: 100px;
}
</style>
