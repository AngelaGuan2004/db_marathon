<template>
  <div id="ShuttleManagement">
    <div class="ManagementContainer">
      <div style="margin-bottom: 50px;font-weight: bold;font-size: 26px;">{{ this.$route.params.name }}</div>
      <el-table v-if="shuttleBuses" :data="shuttleBuses" class='DeleteButton'>
        <el-table-column prop="event_Id" label="赛事ID"></el-table-column>
        <el-table-column prop="id" label="接驳车ID"></el-table-column>
        <el-table-column prop="departure_Time" label="出发时间"></el-table-column>
        <el-table-column prop="arrival_Time" label="到达时间"></el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button size="mini" @click="confirmDelete(scope.$index)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <div v-else style="text-align: center;height: 10vh;line-height: 10vh;font-size: 18px;">暂无数据</div>
      <div class="button-group">
        <el-button type="primary" @click="showShuttleForm">添加</el-button>
      </div>
      <el-dialog :visible.sync="dialogVisible" width="40%" title="添加接驳车" class="Dialog">
        <el-form :model="newShuttle" label-width="150px">
          <el-form-item label="赛事ID">
            <el-input v-model="newShuttle.eventId" placeholder="输入赛事ID"></el-input>
          </el-form-item>
          <el-form-item label="接驳车ID">
            <el-input v-model="newShuttle.shuttleId" placeholder="输入接驳车ID"></el-input>
          </el-form-item>
          <el-form-item label="出发时间">
            <el-input v-model="newShuttle.departureTime" placeholder="输入出发时间"></el-input>
          </el-form-item>
          <el-form-item label="到达时间">
            <el-input v-model="newShuttle.arrivalTime" placeholder="输入到达时间"></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="addShuttleBus">添加</el-button>
        </div>
      </el-dialog>
    </div>
  </div>
</template>

<script>
import { get_shuttleInfo, add_shuttleInfo, delete_shuttleInfo } from '@/api/shuttle'; // 导入API函数
export default {
  name: 'ShuttleManagement',
  data() {
    return {
      searchForm: {
        Event_id: '10001'
      },
      shuttleBuses: [
        //{ eventId: '1', shuttleId: '101', departureTime: '08:00', arrivalTime: '09:00' },
        //  { eventId: '2', shuttleId: '102', departureTime: '09:00', arrivalTime: '10:00' }
      ],
      dialogVisible: false,
      confirmDialogVisible: false,
      deleteIndex: -1,
      newShuttle: {
        Event_id: '10001',
        shuttleId: '',
        departureTime: '',
        arrivalTime: ''
      }
    };
  },
  methods: {
    async fetchShuttleBuses() {
      if (this.searchForm.Event_id) {
        try {
          const response = await get_shuttleInfo(this.searchForm.Event_id);
          this.shuttleBuses = response; // 假设API返回的数据在data属性中
        } catch (error) {
          console.error('获取接驳车信息失败:', error);
        }
      }
    },
    showShuttleForm() {
      this.dialogVisible = true;
    },
    async addShuttleBus() {
      if (this.newShuttle.Event_id && this.newShuttle.shuttleId) {
        try {
          await add_shuttleInfo({
            Event_id: this.newShuttle.Event_id,
            shuttlecar_id: this.newShuttle.shuttleId,
            departtime: this.newShuttle.departureTime,
            arrivetime: this.newShuttle.arrivalTime
          });
          this.shuttleBuses.push({ ...this.newShuttle });
          this.dialogVisible = false;
          this.newShuttle = { Event_id: '10001', shuttleId: '', departureTime: '', arrivalTime: '' };
          this.$message.success('添加接驳车成功!');
        } catch (error) {
          console.error('添加接驳车失败:', error);
          this.$message.error('添加接驳车失败');
        }
      } else {
        this.$message.error('请填写完整信息');
      }
    },
    confirmDelete(index) {
      this.$confirm('确定要删除此接驳车？', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.deleteShuttleBus(this.shuttleBuses[index].shuttleId);
        this.fetchShuttleBuses();
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        });
      });
    },
    async deleteShuttleBus() {
      if (this.deleteIndex !== -1) {
        try {
          const shuttlecar_ID = this.shuttleBuses[this.deleteIndex].shuttleId;
          await delete_shuttleInfo(shuttlecar_ID);
          this.shuttleBuses.splice(this.deleteIndex, 1);
          this.confirmDialogVisible = false;
          this.$message.success('删除成功!');
        } catch (error) {
          console.error('删除接驳车失败:', error);
          this.$message.error('删除接驳车失败');
        }
      }
    },
    searchShuttleBuses() {
      this.fetchShuttleBuses(); // 搜索时重新获取接驳车信息
    }
  },
  created() {
    this.fetchShuttleBuses(); // 页面加载时默认获取接驳车信息
  }
};
</script>
<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Management.css";
</style>