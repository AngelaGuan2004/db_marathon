<template>
  <div id="PacerSelection">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content" v-if="paginatedParticipants.length > 0">
        <div class="Button">
          <div style="margin-left: 65%;">
            <el-button type="primary" @click="openConfirmDialog" style="margin: 0 10px;">提交</el-button>
            <el-button type="primary" @click="cancel">取消</el-button>
          </div>
        </div>
        <div>
          <el-table :data="paginatedParticipants" class="Table" style="width: 100%">
            <el-table-column prop="id" label="ID" width="100"></el-table-column>
            <el-table-column prop="name" label="姓名" width="120"></el-table-column>
            <el-table-column prop="sex" label="性别" width="80"></el-table-column>
            <el-table-column prop="age" label="年龄" width="80"></el-table-column>
            <el-table-column prop="number" label="参赛号码" width="110"></el-table-column>
            <el-table-column prop="history" label="历史成绩" width="120">
              <template slot-scope="scope">
                <el-button @click="showHistory(scope.row)">查看详情</el-button>
              </template>
            </el-table-column>
            <el-table-column label="选为配速员" width="120">
              <template slot-scope="scope">
                <el-checkbox v-model="scope.row.isPacer"></el-checkbox>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="Pagination" background layout="prev, pager, next" :total="totalParticipantsCount"
            :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
        </div>
      </el-main>
      <div v-else class="Empty">暂无数据</div>
      
      <!-- 配速员确认弹窗 -->
      <el-dialog :visible.sync="confirmDialogVisible" title="确定配速员名单为" width="50%">
        <div>
          <el-table :data="selectedPacers" style="width: 100%">
            <el-table-column prop="id" label="ID" width="100"></el-table-column>
            <el-table-column prop="name" label="姓名" width="120"></el-table-column>
            <el-table-column prop="sex" label="性别" width="80"></el-table-column>
            <el-table-column prop="age" label="年龄" width="80"></el-table-column>
            <el-table-column prop="number" label="参赛号码" width="110"></el-table-column>
          </el-table>
        </div>
        <span slot="footer" class="dialog-footer">
          <el-button @click="confirmDialogVisible = false">取消</el-button>
          <el-button type="primary" @click="confirmSelection">确定</el-button>
        </span>
      </el-dialog>

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
        <div v-else class="Empty">暂无数据</div>
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
  name: 'PacerSelection',
  data() {
    return {
      totalParticipants: '',
      historyDialogVisible: false,
      confirmDialogVisible: false, // 新增确认对话框的显示状态
      selectedParticipantHistory: '',
      selectedPacers: [], // 保存选中的配速员信息
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
    openConfirmDialog() {
      this.selectedPacers = this.participants.filter(p => p.isPacer);
      if (this.selectedPacers.length > 0) {
        this.confirmDialogVisible = true; // 打开确认对话框
      } else {
        this.$message.warning('请至少选择一个配速员');
      }
    },
    async confirmSelection() {
      try {
        const payload = this.selectedPacers.map(p => ({
          Number_: p.number,
          Role_: 'pacer',
          Player_Id: p.id,
          Event_Id: this.$route.params.event_id,
        }));
        
        const response = await choosePacer(payload);

        if (response === true) {
          this.$message.success('提交成功');
        } else {
          this.$message.error('提交失败，请稍后重试');
        }
        
        this.confirmDialogVisible = false; // 关闭确认对话框
      } catch (error) {
        this.$message.error('提交过程中发生错误');
      }
    },
    cancel() {
      this.participants.forEach(p => p.isPacer = false);
      this.$message.info('已取消所有勾选');
    },
    async loadInitialData() {
      try {
        const eventId = this.$route.params.event_id;
        const players = await fetchPlayersByEvent(eventId);
        const pacers = players.filter(player => player.role_ === 'pacer' && player.number_ !== null);

        const pacerDetailsPromises = pacers.map(async (pacer) => {
          const details = await fetchPlayerDetails(pacer.player_Id);
          const history = await fetchPlayerHistory(pacer.player_Id);

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
            number: pacer.number_,
            isPacer: false,
            history: detailedHistory,
          };
        });

        this.participants = await Promise.all(pacerDetailsPromises);
      } catch (error) {
        console.error('Failed to load initial pacers data:', error);
      }
    }
  },
  created() {
    this.loadInitialData().then(() => {
      console.log('Rendering pacers:', this.participants);
    });
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/PacerAndEmergencyTable.css";

#PacerSelection {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  background-color: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: 105vh;
  height: 72vh;
  margin-right: 35px;
  margin-bottom: 75px;
  font-size: 15px;
}
</style>
