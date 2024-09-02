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
      <div class="TabRight" style="margin-right: 15%;margin-top: 5%;">
        <div class="TabFlex">
          <div class="Tab" style="margin-top: 0;">
            <el-menu ref="EventManagementTab" :default-active="ActiveIndexForEventManagementTab"
              class="el-menu-vertical-demo" @select="ActiveIndex" active-text-color="rgb(168, 27, 31)">
              <el-menu-item index="1">
                <span>选手抽签</span>
              </el-menu-item>
              <el-menu-item index="2">
                <span>配速员选拔</span>
              </el-menu-item>
              <el-menu-item index="3">
                <span>急救跑者选拔</span>
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
import { fetchEventById } from '@/api/Event.js';

export default {
  name: 'EventManagementTab',
  data() {
    return {
      ActiveIndexForEventManagementTab: '',
      IsLottery: false, // 默认情况下设置为 false
      EventManagementTabTitle: ['ParticipantLottery', 'PacerSelection', 'EmergencyRunnerSelection'],
    };
  },
  methods: {
    ActiveIndex(index) {
      if (this.IsLottery || index === '1') {
        this.ActiveIndexForEventManagementTab = index; // 设置当前激活的菜单项
        this.$router.push({ name: this.EventManagementTabTitle[index - 1] });
      } else {
        this.$message.warning('请先完成抽签');
        this.$bus.$emit('ActiveIndexForEventManagementTab', '1');
        this.$refs.EventManagementTab.activeIndex = '1';
        this.$router.push({ name: 'ParticipantLottery' });
      }
    },
    LotteryPermission() {
      this.IsLottery = true;
    },
    checkIsLottery() {
      const eventId = "10077"; // 您需要根据实际情况使用 eventId
      fetchEventById(eventId)
        .then(event => {
          // 根据 is_Drawn 的状态设置 IsLottery
          if (event.Event.Is_Drawn === '是') {
            this.IsLottery = true;
          }
        })
        .catch(error => {
          console.error('Failed to load event:', error);
          this.$message.error('加载赛事信息失败，请稍后重试。');
        });
    }
  },
  created() {
    this.checkIsLottery(); // 页面加载时检查是否已经抽签
  },
  mounted() {
    this.$router.push({ name: 'ParticipantLottery' });
    this.$bus.$on('IsLottery', this.LotteryPermission);
    this.$bus.$on('ActiveIndexForEventManagementTab', this.ActiveIndex);
    this.$bus.$emit('ActiveIndexForEventManagementTab', '1');
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
  padding: 0 100px;
  padding-top: 20px;
}
</style>
