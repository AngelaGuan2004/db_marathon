<template>
  <div id="UserVolunteering">
    <div class='NotTable' v-if="this.loading && !this.Flag">
      <div style=" display: table-cell;vertical-align: middle;width: 100vh;height: 300px;">
        未查询到志愿活动！
      </div>
    </div>
    <div style="width: 100%;vertical-align: top;" v-else>
      <el-main>
        <div style="width: 100%; overflow: auto ;height:100%;margin-top: 20px;">
          <el-table :data="paginatedResults" v-loading='loading' class="table" row-class-name="clickable-row" stripe>
            <el-table-column prop="name" label="比赛名称" width="300"></el-table-column>
            <el-table-column prop="event_Date" label="时间" width="150"></el-table-column>
            <el-table-column prop="category" label="赛事类型" width="150"></el-table-column>
            <el-table-column prop="scale" label="志愿规模" width="150"></el-table-column>
            <el-table-column label="操作" width="150">
              <template slot-scope="scope">
                <span style="font-size: 14px;color: rgb(64, 158, 255);font-weight: bold;"
                  @click="details(scope.row)">详情</span>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination v-if="events.length > 0" background layout="prev, pager, next" :total="events.length"
            :page-size="pageSize" :current-page.sync="currentPage" @current-change="handlePageChange"
            class="pagination">
          </el-pagination>
        </div>
      </el-main>
    </div>

  </div>
</template>
<script>
import { getMyVolunteering } from '@/api/UserCenter.js'
export default {
  name: 'UserVolunteering',
  mounted() {
    this.ID = localStorage.getItem('UserId')
    getMyVolunteering(this.ID)
      .then((res) => {
        this.events = res;
        this.loading = false
      })
      .catch(error => {
        console.error(error)
        this.$message.error('查询失败');
        this.Flag = false
      });
  },
  data() {
    return {
      ID: '',
      events: [],
      currentPage: 1,
      pageSize: 8,
      loading: true,
      Flag: true
    }
  },
  methods: {
    details(row) {
      this.$router.push({
        name: 'VolunteerDetailForAddStation',
        params: {
          event_id: row.event_id,
        }
      });
    },
    handlePageChange(page) {
      this.currentPage = page;
    },
  },
  computed: {
    paginatedResults() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = start + this.pageSize;
      return this.events.slice(start, end);
    }
  }
}
</script>

<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";
@import "@/assets/css/UserTable.css";

#UserVolunteering {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  margin-right: 5%;
  background-color: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  font-size: 15px;
  width: 110vh;
  height: 65vh;
  margin-top: -25px;
  margin-bottom: 75px;
}

.el-main {
  margin-left: 0;
}
</style>