<template>
  <div>
    <div id="UserVolunteering">
      <el-main>
        <div style="width: 100%; overflow: auto ;height: 420px;margin-top: 20px;">
          <el-table :data="events" class="table" row-class-name="clickable-row" stripe>
            <el-table-column prop="name" label="比赛名称" width="300"></el-table-column>
            <el-table-column prop="event_Date" label="时间" width="150"></el-table-column>
            <el-table-column prop="category" label="赛事类型" width="150"></el-table-column>
            <el-table-column prop="scale" label="志愿规模" width="150"></el-table-column>
            <el-table-column label="操作" width="150">
              <template slot-scope="scope">
                <el-button type="text" @click="handleRowClick(scope.row)">详情</el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
      </el-main>
    </div>
  </div>
</template>
<script>
import {getMyVolunteering} from '@/api/UserCenter.js' 
export default {
  name: 'UserVolunteering',
  mounted() {
    getMyVolunteering(this.ID)
      .then((res) => {
        this.events=res;
      })
      .catch(error => {
        console.error('查询失败:', error);
        alert('查询失败');
      });
  },
  data() {
    return {
      ID: 1,
      events: [
        
      ],
    }
  },
  methods: {
    handleRowClick(row) {
      window.alert(row.id);
    }
  }
}
</script>

<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "../assets/css/Base.css";
@import "../assets/css/UserTable.css";

#UserVolunteering {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-end;
  margin-right: 60px;
  background-color: white;
  margin-top: 80px;
  width: 95%;
  font-size: 15px;
}

.el-main {
  margin-left: 0;
}
</style>