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
            <button :class="checkClass(1)" @click="navigateTo('PacerSelection')">配速员选拔</button>
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
      participants: [
        { id: "1", name: "张三", sex: "男", age: "30", number: "A1234", isEmergencyRunner: false, history: [{ index: 1, eventDate: '2024-09-22', eventName: "天津塘沽马拉松", eventType: "全马", result: "3:35:00", ranking: 7 }]  },
        { id: "2", name: "李四", sex: "女", age: "22", number: "B2345", isEmergencyRunner: false },
        { id: "3", name: "王五", sex: "男", age: "45", number: "C3456", isEmergencyRunner: false},
        { id: "4", name: "赵六", sex: "女", age: "28", number: "D4567", isEmergencyRunner: false},
        { id: "5", name: "孙七", sex: "男", age: "36", number: "E5678", isEmergencyRunner: false},
        { id: "6", name: "周八", sex: "女", age: "25", number: "A6789", isEmergencyRunner: false},
        { id: "7", name: "吴九", sex: "男", age: "40", number: "B7890", isEmergencyRunner: false},
        { id: "8", name: "郑十", sex: "女", age: "33", number: "C8901", isEmergencyRunner: false},
        { id: "9", name: "冯十一", sex: "男", age: "27", number: "D9012", isEmergencyRunner: false},
        { id: "10", name: "陈十二", sex: "女", age: "31", number: "E0123", isEmergencyRunner: false},
        { id: "11", name: "褚十三", sex: "男", age: "29", number: "A1235", isEmergencyRunner: false},
        { id: "12", name: "卫十四", sex: "女", age: "24", number: "B2346", isEmergencyRunner: false},
        { id: "13", name: "蒋十五", sex: "男", age: "35", number: "C3457", isEmergencyRunner: false},
        { id: "14", name: "沈十六", sex: "女", age: "26", number: "D4568", isEmergencyRunner: false},
        { id: "15", name: "韩十七", sex: "男", age: "38", number: "E5679", isEmergencyRunner: false},
        { id: "16", name: "杨十八", sex: "女", age: "32", number: "A6780", isEmergencyRunner: false},
        { id: "17", name: "朱十九", sex: "男", age: "37", number: "B7891", isEmergencyRunner: false},
        { id: "18", name: "秦二十", sex: "女", age: "28", number: "C8902", isEmergencyRunner: false},
        { id: "19", name: "尤二十一", sex: "男", age: "39", number: "D9013", isEmergencyRunner: false},
        { id: "20", name: "许二十二", sex: "女", age: "30", number: "E0124", isEmergencyRunner: false}
      ],
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
      } else if (page === 'ParticipantLottery') {
        this.activeNav = 0;
        this.$router.push({ name: 'ParticipantLottery' });
      }
    },
    toggleEmergencyRunnerSelection() {
      this.activeNav = 2;
      this.showEmergencyRunnerOptions = !this.showEmergencyRunnerOptions;
    },
    handlePageChange(page) {
      this.currentPage = page;
    },
    showHistory(participant) {
      this.selectedParticipantHistory = participant.history || [];
      this.historyDialogVisible = true;
    },
    save() {
      this.participants = this.participants.filter(p => !p.isEmergencyRunner);
      this.$message.success('保存成功');
    },
    cancel() {
      this.participants.forEach(p => p.isEmergencyRunner = false);
      this.$message.info('已取消所有勾选');
    }
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

.input-container {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 10px;
  margin: 20px auto;
  width: 50%;
}

.input-total-participants {
  flex-grow: 1;
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
