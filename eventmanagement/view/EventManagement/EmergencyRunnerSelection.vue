<template>
    <div id="emergencyRunner-selection">
      <el-header>
        <el-menu class="el-menu-demo" mode="horizontal" background-color="#c81623" text-color="#666" active-text-color="#ffd04b" :default-active="activeMenu">
          <el-menu-item index="1">首页</el-menu-item>
          <el-menu-item index="2">赛历</el-menu-item>
        </el-menu>
      </el-header>
      <div class="content-container">
        <div id="div-left">
          <div class="break"></div>
          <div>
            <button :class="checkClass(0)" @click="navigateTo('ParticipantLottery')">选手抽签</button>
          </div>
          <div>
            <button :class="checkClass(1)" @click="togglePacerSelection">配速员选拔</button>
          </div>
          <div v-if="showPacerOptions">
            <button class="sub-button" @click="navigateTo('PacerSelection')">待选跑者</button>
            <button class="sub-button" @click="navigateTo('PacerSubmission')">已选跑者</button>
          </div>
          <div>
            <button :class="checkClass(2)" @click="toggleEmergencyRunnerSelection">急救跑者选拔</button>
          </div>
          <div v-if="showEmergencyRunnerOptions">
            <button class="sub-button" @click="navigateTo('EmergencyRunnerSelection')">待选跑者</button>
            <button class="sub-button" @click="navigateTo('EmergencyRunnerSubmission')">已选跑者</button>
          </div>
        </div>
        <el-main class="main-content">
          <div class="right-menu">
            <el-button class="custom-button" icon="el-icon-arrow-left" @click="goBack">返回</el-button>
          </div>
          <div class="save_or_cancel">
            <el-button type="primary" @click="save">保存</el-button>
            <el-button type="primary" @click="cancel">取消</el-button>
          </div>
          <div v-if="paginatedParticipants.length>0">
            <el-table :data="paginatedParticipants" class="emergencyRunner-selection-table" style="width: 100%">
              <el-table-column prop="id" label="ID" width="100"></el-table-column>
              <el-table-column prop="name" label="姓名" width="100"></el-table-column>
              <el-table-column prop="sex" label="性别" width="100"></el-table-column>
              <el-table-column prop="age" label="年龄" width="100"></el-table-column>
              <el-table-column prop="number" label="参赛号码" width="100"></el-table-column>
              <el-table-column prop="history" label="历史成绩" width="150">
                <template slot-scope="scope">
                  <el-button @click="showHistory(scope.row)">查看详情</el-button>
                </template>
              </el-table-column>
              <el-table-column label="选为急救跑者" width="120">
                <template slot-scope="scope">
                  <el-checkbox v-model="scope.row.isEmergencyRunner"></el-checkbox>
                </template>
              </el-table-column>
            </el-table>
            <el-pagination
              class="pagination"
              background
              layout="prev, pager, next"
              :total="totalParticipantsCount"
              :page-size="pageSize"
              @current-change="handlePageChange"
            ></el-pagination>
          </div>
          <div v-else class="custom-empty">
            暂无数据
          </div>
        </el-main>
        <el-dialog :visible.sync="historyDialogVisible" title="历史成绩" width="70%">
          <div v-if="selectedParticipantHistory.length > 0">
            <el-table :data="selectedParticipantHistory" class="history-table" style="width: 100%">
              <el-table-column prop="index" label="序号" width="70"></el-table-column>
              <el-table-column prop="eventDate" label="开赛时间" width="100"></el-table-column>
              <el-table-column prop="eventName" label="赛事名称" width="250"></el-table-column>
              <el-table-column prop="eventType" label="赛事类型" width="70"></el-table-column>
              <el-table-column prop="result" label="成绩" width="90"></el-table-column>
              <el-table-column prop="ranking" label="名次" width="70"></el-table-column>
            </el-table>
          </div>
          <div v-else class="custom-empty">
            暂无数据
          </div>
          <span slot="footer" class="dialog-footer">
            <el-button @click="historyDialogVisible = false">关闭</el-button>
          </span>
        </el-dialog>
      </div>
    </div>
  </template>
<script>
export default {
  data() {
    return {
      activeMenu: '1',
      activeNav: 2,
      totalParticipants: '',
      historyDialogVisible: false,
      selectedParticipantHistory: [],
      showEmergencyRunnerOptions: true,
      showPacerOptions: false,
      participants: [],
      currentPage: 1,
      pageSize: 6
    };
  },
  computed: {
    totalParticipantsCount() {
      return this.participants.length;
    },
    paginatedParticipants() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.participants.slice(start, end);
    },
    isEmergencyRunnerSelectionActive() {
      return this.$route.name === 'EmergencyRunnerSelection';
    },
    isEmergencyRunnerSubmissionActive() {
      return this.$route.name === 'EmergencyRunnerSubmission';
    },
    isPacerSelectionActive() {
      return this.$route.name === 'PacerSelection';
    },
    isPacerSubmissionActive() {
      return this.$route.name === 'PacerSubmission';
    }
  },
  methods: {
    checkClass(index) {
      return index === this.activeNav ? 'active' : 'inactive';
    },
    navigateTo(page) {
      if (page === 'EmergencyRunnerSelection'){
        this.activeNav = 2;
      }else if(page === 'EmergencyRunnerSubmission') {
        this.activeNav = 2;
        this.$router.push({ name: 'EmergencyRunnerSubmission'});
      } else if (page === 'PacerSelection') {
        this.activeNav = 1;
        this.$router.push({ name: 'PacerSelection' });
      }else if (page === 'PacerSubmission') {
        this.activeNav = 1;
        this.$router.push({ name: 'PacerSubmission' });
      } else if (page === 'ParticipantLottery') {
        this.activeNav = 0;
        this.$router.push({ name: 'ParticipantLottery' });
      }
    },
    goBack() {
      this.$router.push({ name: 'EventList' });
    },
    toggleEmergencyRunnerSelection() {
      this.activeNav = 2;
      this.showEmergencyRunnerOptions = !this.showEmergencyRunnerOptions;
    },
    togglePacerSelection() {
      this.showPacerOptions = !this.showPacerOptions;
    },
    handlePageChange(page) {
      this.currentPage = page;
    },
    showHistory(participant) {
      this.selectedParticipantHistory = participant.history || [];
      this.historyDialogVisible = true;
    },
    save() {
      const selectedEmergencyRunners = this.participants.filter(p => p.isEmergencyRunner);
      this.$store.dispatch('saveSelectedParticipants', selectedEmergencyRunners)
        .then(() => {
          this.participants = this.$store.getters.getAvailableEmergencyRunners;
          this.$message.success('保存成功');
        })
    },
    cancel() {
      this.participants.forEach(p => p.isEmergencyRunner = false);
      this.$message.info('已取消所有勾选');
    }
  },
  created() {
    this.participants = this.$store.getters.getAvailableEmergencyRunners;
  }
};
</script>
<style>
#emergencyRunner-selection {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

.content-container {
  display: flex;
  background-color: #f4f4f6;
  height: 100vh;
}

#div-left {
  width: 200px;
  background-color: white;
  padding: 20px;
  box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
  height:580px;
}

#div-left button {
  width: 100%;
  margin-top: 10px;
  padding: 10px;
  font-size: 16px;
  background-color: white;
  border: none;
  cursor: pointer;
}

#div-left button:hover {
  background-color: #f5f5f5;
}

#div-left button.active {
  color: #c81623;
  background-color: #fff0f0;
}

.sub-button {
  width: 100%;
  margin-top: 5px;
  padding: 5px;
  font-size: 14px;
  background-color: #f9f9f9;
  border: none;
  cursor: pointer;
}

.sub-button:hover {
  background-color: #e6e6e6;
}

.sub-button.active {
  color: #c81623;
  background-color: #fff0f0;
}

.main-content {
  flex-grow: 1;
  padding: 20px;
  overflow-y: auto;
}

.right-menu {
  display: flex;
  justify-content: flex-start; /* 将内容对齐到左边 */
  align-items: center; /* 垂直居中对齐 */
  padding: 10px;
}

.custom-button {
  margin-right: auto; /* 确保按钮在容器的左侧 */
}


.emergencyRunner-selection-table {
  width: 100%;
  table-layout: fixed;
  border: none;
}

.emergencyRunner-selection-table th .cell,
.emergencyRunner-selection-table td .cell {
  display: inline-block;
  width: 100%;
  text-align: center;
  height: 40px; /* Adjust the height as needed */
  border: none;
  vertical-align: middle; /* 垂直居中 */
  line-height: 40px; /* 确保行高与单元格高度一致 */
}

.el-table__header-wrapper,
.el-table__body-wrapper {
  display: flex;
  justify-content: center;
}

.pagination {
  display: flex;
  justify-content: center;
  margin: 20px 0;
}

.history-table {
  width: 100%;
  table-layout: fixed;
  border: none;
}

.history-table th .cell,
.history-table td .cell {
  display: inline-block;
  width: 100%;
  text-align: center;
  height: 20px; /* Adjust the height as needed */
  border: none;
  vertical-align: middle; /* 垂直居中 */
  line-height: 20px; /* 确保行高与单元格高度一致 */
}

.custom-empty {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 200px;
  color: #999;
  width: 100%;
  text-align: center;
}

.save_or_cancel {
  display: flex;
  justify-content: flex-end;
  margin-bottom: 20px;
  padding-right: 100px;
}

.save_or_cancel el-button {
  margin-right: 10px; /* 调整这个值来控制按钮之间的距离 */
}
</style>
