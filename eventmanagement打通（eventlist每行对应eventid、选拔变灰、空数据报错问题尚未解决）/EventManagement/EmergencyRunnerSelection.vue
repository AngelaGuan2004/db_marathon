<template>
  <div id="EmergencyRunnerSelection">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content">
        <div class="Button">
          <div style="margin-left: 65%;">
            <el-button type="primary" @click="save" style="margin: 0 10px;">提交</el-button>
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
import {
  fetchPlayersByEvent,
  fetchPlayerDetails,
  fetchPlayerHistory,
  choosePacer,
} from '@/api/EventManagement';
import { fetchEventById } from '@/api/Event';

export default {
  name: 'EmergencyRunnerSelection',
  data() {
    return {
      totalParticipants: '',
      historyDialogVisible: false,
      selectedParticipantHistory: '',
      participants: [],
      currentPage: 1,
      pageSize: 6,
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
    formatDate(dateString) {
      const date = new Date(dateString);
      const year = date.getFullYear();
      const month = (date.getMonth() + 1).toString().padStart(2, '0');
      const day = date.getDate().toString().padStart(2, '0');
      return `${year}-${month}-${day}`;
    },
    handlePageChange(page) {
      this.currentPage = page;
    },
    showHistory(participant) {
      this.selectedParticipantHistory = participant.history || [];
      this.historyDialogVisible = true;
    },
    async save() {
      try {
        // 构建要发送到后端的数据
        const payload = this.participants
          .filter(p => !p.isEmergencyRunner)  // 过滤出 isEmergencyRunner 为 false 的选手
          .map(p => ({
            Number_: p.number,
            Role_: 'first_aid',
            Player_Id: p.id,
            Event_Id: "10077",  // 固定的 event_Id
          }));

        const response = await choosePacer(payload);

        // 处理响应
        if (response === true) {
          this.$message.success('提交成功');
        } else {
          this.$message.error('提交失败，请稍后重试');
        }
      } catch (error) {
        this.$message.error('提交过程中发生错误');
      }
    },
    cancel() {
      this.participants.forEach(p => p.isEmergencyRunner = false);
      this.$message.info('已取消所有勾选');
    },
    async loadInitialData() {
      try {
        const eventId = '10077';
        const players = await fetchPlayersByEvent(eventId);
        const emergencyRunners = players.filter(player => player.role_ === 'first_aid' && player.number_ !== null);

        const emergencyRunnerDetailsPromises = emergencyRunners.map(async (runner) => {
          const details = await fetchPlayerDetails(runner.player_Id);
          const history = await fetchPlayerHistory(runner.player_Id);

          const historyWithEventDetailsPromises = history.map(async (h, index) => {
            const eventDetails = await fetchEventById(h.event_Id);
            if (!eventDetails || !eventDetails.Event) {
              console.error(`Failed to fetch event details for event_Id: ${h.event_Id}`);
              return {
                index: index + 1,
                eventDate: 'N/A',
                eventName: 'N/A',
                eventType: 'N/A',
                result: h.net_Result,
                ranking: h.rank,
              };
            }
            
            const event = eventDetails.Event;
            return {
              index: index + 1,
              eventDate: this.formatDate(event.Event_Date),
              eventName: event.Name,
              eventType: event.Category,
              result: h.net_Result,
              ranking: h.rank,
            };
          });

          const detailedHistory = await Promise.all(historyWithEventDetailsPromises);

          return {
            id: details.Id,
            name: details.Name,
            sex: details.Gender,
            age: details.Age,
            number: runner.number_,
            isEmergencyRunner: false,
            history: detailedHistory,
          };
        });

        this.participants = await Promise.all(emergencyRunnerDetailsPromises);
      } catch (error) {
        console.error('Failed to load initial emergency runners data:', error);
      }
    }
  },
  created() {
    this.loadInitialData().then(() => {
      console.log('Rendering emergency runners:', this.participants);
    });
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
