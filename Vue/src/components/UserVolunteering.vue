<template>
  <div>
    <div id="UserVolunteering">
      <el-main>
        <div style="width: 100%; overflow: auto ;height: 420px;margin-top: 20px;">
          <el-table :data="events" class="table" row-class-name="clickable-row" stripe>
            <el-table-column prop="name" label="比赛名称" width="300"></el-table-column>
            <el-table-column prop="event_date" label="时间" width="150"></el-table-column>
            <el-table-column prop="type" label="赛事类型" width="150"></el-table-column>
            <el-table-column prop="voltype" label="志愿类型" width="150">
              <template slot-scope="scope">
                <div style="color: #c81623;">{{ scope.row.voltype }}</div>
              </template>
            </el-table-column>
            <el-table-column label="操作" width="150">
              <template slot-scope="scope">
                <el-button type="text" @click="details(scope.row)">详情</el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
      </el-main>
    </div>
  </div>
</template>
<script>
/*import {getMyVolunteering} from '@/api/UserCenter.js' */
export default {
  name: 'UserVolunteering',
  mounted() {
    /* 
    getMyVolunteering(this.ID)
      .then((res) => {
        
      })
      .catch(error => {
        console.error('查询失败:', error);
        alert('查询失败');
      });
    */
  },
  data() {
    return {
      ID: '1212131',
      events: [
        { event_id: '1212', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '半马', voltype: '补给' },
        { event_id: '2313', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '全马', voltype: '医疗' },
        { event_id: '1212', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '半马', voltype: '补给' },
        { event_id: '2313', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '全马', voltype: '医疗' },
        { event_id: '1212', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '半马', voltype: '补给' },
        { event_id: '2313', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '全马', voltype: '医疗' },
        { event_id: '1212', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '半马', voltype: '补给' },
        { event_id: '2313', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '全马', voltype: '医疗' },
        { event_id: '1212', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '半马', voltype: '补给' },
        { event_id: '2313', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '全马', voltype: '医疗' },
      ],
    }
  },
  methods: {
    details(row) {
      window.alert(row.event_id);
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