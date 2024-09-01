<template>
    <div class="management-container">
      <h2>接驳车管理</h2>
      <el-form :model="searchForm" label-width="120px" inline>
        <el-form-item label="赛事ID">
          <el-input v-model="searchForm.eventId" placeholder="输入赛事ID进行搜索"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="searchShuttleBuses">搜索</el-button>
        </el-form-item>
      </el-form>
      <el-table :data="filteredShuttleBuses">
        <el-table-column prop="eventId" label="赛事ID"></el-table-column>
        <el-table-column prop="shuttleId" label="接驳车ID"></el-table-column>
        <el-table-column prop="departureTime" label="出发时间"></el-table-column>
        <el-table-column prop="arrivalTime" label="到达时间"></el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button type="danger" size="mini" @click="confirmDelete(scope.$index)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <div class="button-group">
        <el-button type="primary" @click="showShuttleForm">添加接驳车</el-button>
      </div>
      <el-dialog :visible.sync="dialogVisible" width="50%" title="添加接驳车">
        <el-form :model="newShuttle" label-width="80px">
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
  
      <el-dialog :visible.sync="confirmDialogVisible" width="30%" title="确认删除">
        <span>确定要删除此接驳车？</span>
        <div slot="footer" class="dialog-footer">
          <el-button @click="confirmDialogVisible = false">取消</el-button>
          <el-button type="danger" @click="deleteShuttleBus">确定</el-button>
        </div>
      </el-dialog>
    </div>
  </template>
  
  <script>
  import { get_shuttleInfo,add_shuttleInfo,delete_shuttleInfo } from '@/api/shuttle'; // 导入API函数
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
    computed: {
    filteredShuttleBuses() {
      if (this.searchForm.Event_id) {
        return this.shuttleBuses.filter(bus => bus.Event_id === this.searchForm.Event_id);
      } else {
        return this.shuttleBuses;
      }
    }
  },
  methods: {
    async fetchShuttleBuses() {
      if (this.searchForm.Event_id) {
        try {
          const response = await get_shuttleInfo(this.searchForm.Event_id);
          this.shuttleBuses = response.data; // 假设API返回的数据在data属性中
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
      this.deleteIndex = index;
      this.confirmDialogVisible = true;
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
</script>
  <style scoped>
  .management-container {
    max-width: 800px;
    margin: 0 auto;
    padding: 30px;
    background-color: white;
    border: 1px solid grey;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  }
  .button-group {
    display: flex;
    justify-content: flex-end;
    margin-top: 20px;
  }
  </style>
  