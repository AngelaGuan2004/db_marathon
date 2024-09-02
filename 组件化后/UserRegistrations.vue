<template>
  <div id="UserRegistrations">
    <div class="UserRegistrations">
      <el-main>
        <div style="width: 100%; overflow: auto ;height: 420px;margin-top: 20px;">
          <el-table :data="events" class="table"  row-class-name="clickable-row" stripe>
            <el-table-column prop="name" label="比赛名称" width="300"></el-table-column>
            <el-table-column prop="event_Date" label="时间" width="150"></el-table-column>
            <el-table-column prop="category" label="赛事类型" width="150"></el-table-column>
            <el-table-column prop="result" label="结果" width="150">
              <template slot-scope="scope">
                <div style="color: #c81623;">{{ scope.row.result }}</div>
              </template>
            </el-table-column>
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
import {getMyRegistrations} from '@/api/UserCenter.js' 
import {getEventInfo} from '@/api/UserCenter.js' 

export default {
  name: 'UserRegistrations',
  mounted() {
    getMyRegistrations(this.ID)
      .then((res) => {
        // 调用处理函数，将每个赛事的result字段进行相应处理
        this.processEvents(res);
      })
      .catch(error => {
        console.error('查询失败:', error);
        alert('查询失败');
      });
    
  },
  data() {
    return {
      ID: 1,
      activeMenu: '2',
      searchName: '',
      events: [
       
      ],
      currentPage: 1,
      pageSize: 5,
    }
  },
  methods: {
    handleRowClick(row) {
      window.alert(row.event_id);
    },
    async processEvents(events) {
      this.events = await Promise.all(events.map(async (event) => {
        /*
        if (event.is_Drawn === "否") {
          // 如果 is_Drawn 为 "否"，直接设置 result 为 "未抽签"
          event.result = "未抽签";
        } else {
          // 否则从数据库获取 result 值
          event.result = await this.getResultFromAPI(this.ID, event.id);
        }
        */
        event.result = await this.getResultFromAPI(this.ID, event.id);
        return event;
      }));

      // 打印或使用处理后的数据
      console.log('处理后的赛事数据:', this.events);
    },
    getResultFromAPI(playerId, eventId) {
      // 使用 API 查询具体的 result 值
      return getEventInfo(playerId, eventId)
        .then(response => {
          // 根据实际的 API 返回结构解析 result 值
          if(response==="未中签"||response==="尚未抽签")
            return response;
          else
            return "已中签";
        })
        .catch(error => {
          console.error(`获取赛事 ${eventId} 的结果失败:`, error);
          return "查询失败"; // 处理错误时返回一个默认值
        });
    }
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