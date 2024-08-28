<template>
  <div id="EmergencyRunnerSelection">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content">
        <div class="Button">
          <div style="margin-left: 65%;">
            <el-button type="primary" @click="save" style="margin: 0 10px;">保存</el-button>
            <el-button type="primary" @click="cancel">取消</el-button>
          </div>
        </div>
        <div v-if="paginatedParticipants.length > 0">
          <el-table :data="paginatedParticipants" class="Table" style="width: 100%">
            <el-table-column prop="id" label="ID" width="100"></el-table-column>
            <el-table-column prop="name" label="姓名" width="120"></el-table-column>
            <el-table-column prop="sex" label="性别" width="100"></el-table-column>
            <el-table-column prop="age" label="年龄" width="100"></el-table-column>
            <el-table-column prop="number" label="参赛号码" width="120"></el-table-column>
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
          <el-pagination class="Pagination" background layout="prev, pager, next" :total="totalParticipantsCount"
            :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
        </div>
        <div v-else class="Empty">
          暂无数据
        </div>
      </el-main>
      <el-dialog :visible.sync="historyDialogVisible" title="历史成绩" width="70%">
        <div v-if="selectedParticipantHistory.length > 0">
          <el-table :data="selectedParticipantHistory" class="HistoryTable" style="width: 100%">
            <el-table-column prop="index" label="序号" width="70"></el-table-column>
            <el-table-column prop="eventDate" label="开赛时间" width="120"></el-table-column>
            <el-table-column prop="eventName" label="赛事名称" width="225"></el-table-column>
            <el-table-column prop="eventType" label="赛事类型" width="120"></el-table-column>
            <el-table-column prop="result" label="成绩" width="100"></el-table-column>
            <el-table-column prop="ranking" label="名次" width="70"></el-table-column>
          </el-table>
        </div>
        <div v-else class="Empty">
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
  name: 'EmergencyRunnerSelection',
  data() {
    return {
      totalParticipants: '',
      historyDialogVisible: false,
      selectedParticipantHistory: [],
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
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    showHistory(participant) {
      this.selectedParticipantHistory = participant.history || [];
      this.historyDialogVisible = true;
    },
    save() {
      const selectedEmergencyRunners = this.participants.filter(p => p.isEmergencyRunner);
      this.$store.dispatch('saveSelectedEmergencyRunners', selectedEmergencyRunners)
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
<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/PacerAndEmergencyTable.css";

#EmergencyRunnerSelection {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: flex-end;
  background-color: white;
  margin-top: 80px;
  width: 72%;
  font-size: 15px;
}
</style>
