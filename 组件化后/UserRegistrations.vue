<template>
  <div id="UserRegistrations">
    <div class="UserRegistrations">
      <el-main>
        <div style="width: 100%; overflow: auto ;height: 420px;margin-top: 20px;">
          <el-table :data="events" class="table"  row-class-name="clickable-row" stripe>
            <el-table-column prop="name" label="比赛名称" width="300"></el-table-column>
            <el-table-column prop="event_date" label="时间" width="150"></el-table-column>
            <el-table-column prop="type" label="赛事类型" width="150"></el-table-column>
            <el-table-column prop="result" label="结果" width="150">
              <template slot-scope="scope">
                <div style="color: #c81623;">{{ scope.row.result }}</div>
              </template>
            </el-table-column>
            <el-table-column prop="number" label="号码" width="150"></el-table-column>
            <el-table-column label="操作" width="100">
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
/*import {getMyRegistrations} from '@/api/UserCenter.js' */
export default {
  name: 'UserRegistrations',
  mounted() {
    /* 
    getMyRegistrations(this.ID)
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
      activeMenu: '2',
      searchName: '',
      events: [
        { event_id: '1', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '半马', result: '已中签', number: '22222' },
        { event_id: '2', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '全马', result: '已中签', number: '22222' },
        { event_id: '3', name: '2024上海马拉松', event_date: '2024 7 12', type: '半马', result: '已中签', number: '22222' },
        { event_id: '4', name: '2024上海马拉松', event_date: '2024 7 12', type: '全马', result: '已中签', number: '22222' },
        { event_id: '1', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '半马', result: '已中签', number: '22222' },
        { event_id: '2', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '全马', result: '已中签', number: '22222' },
        { event_id: '3', name: '2024上海马拉松', event_date: '2024 7 12', type: '半马', result: '已中签', number: '22222' },
        { event_id: '4', name: '2024上海马拉松', event_date: '2024 7 12', type: '全马', result: '已中签', number: '22222' },
        { event_id: '1', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '半马', result: '已中签', number: '22222' },
        { event_id: '2', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '全马', result: '已中签', number: '22222' },
        { event_id: '3', name: '2024上海马拉松', event_date: '2024 7 12', type: '半马', result: '已中签', number: '22222' },
        { event_id: '4', name: '2024上海马拉松', event_date: '2024 7 12', type: '全马', result: '已中签', number: '22222' },
        { event_id: '1', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '半马', result: '已中签', number: '22222' },
        { event_id: '2', name: '2024天津团泊湖半程马拉松', event_date: '2024 7 12', type: '全马', result: '已中签', number: '22222' },
        { event_id: '3', name: '2024上海马拉松', event_date: '2024 7 12', type: '半马', result: '已中签', number: '22222' },
        { event_id: '4', name: '2024上海马拉松', event_date: '2024 7 12', type: '全马', result: '已中签', number: '22222' },
      ],
      currentPage: 1,
      pageSize: 5,
    }
  },
  methods: {
    handleRowClick(row) {
      window.alert(row.event_id);
    },
  },
  computed: {

  },
}
</script>

<style>
@import 'element-ui/lib/theme-chalk/index.css';
@import "../assets/css/Base.css";
@import "../assets/css/UserTable.css";

#UserRegistrations {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-end;
  margin-right: 60px;
  background-color: white;
  margin-top: 80px;
  width: 66%;
  font-size: 15px;
}
</style>