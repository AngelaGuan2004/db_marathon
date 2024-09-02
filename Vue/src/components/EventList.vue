<template>
  <div id="EventList">
    <div style="margin-left: 6%;margin-top: 20px;">
      <el-breadcrumb separator-class="el-icon-arrow-right">
        <el-breadcrumb-item :to="{ path: '/Home' }">首页</el-breadcrumb-item>
        <el-breadcrumb-item>赛历</el-breadcrumb-item>
      </el-breadcrumb>
    </div>
    <el-main>
      <div class="EventFilterContainer">
        <el-select v-model="selectedType" placeholder="请选择赛事类型" class="EventFilterItem">
          <el-option label="全部" value="马"></el-option>
          <el-option label="半马" value="半马"></el-option>
          <el-option label="全马" value="全马"></el-option>
        </el-select>
        <el-date-picker v-model="selectedDate" type="date" placeholder="请选择开赛时间"
          class="EventFilterItem"></el-date-picker>
        <el-input placeholder="请输入赛事名称" v-model="searchName" class="EventFilterItem"></el-input>
      </div>
      <div v-if="filteredEvents.length > 0">
        <el-table :data="filteredEvents" class="EventInfoTable" @row-click="handleRowClick"
          row-class-name="clickable-row">
          <el-table-column prop="date" label="开赛时间" width="150"></el-table-column>
          <el-table-column prop="name" label="比赛名称" width="250"></el-table-column>
          <el-table-column prop="type" label="赛事类型" width="150"></el-table-column>
          <el-table-column prop="scale" label="赛事规模" width="150"></el-table-column>
          <el-table-column label="赛事成绩" width="100">
            <template slot-scope="scope">
              <div class="EventTableDetail" @click.prevent="handleLeaderBoard(scope.row, $event)">查看</div>
            </template>
          </el-table-column>
          <el-table-column label="赛事管理" width="100">
            <template slot-scope="scope">
              <div class="EventTableDetail" @click.prevent="handleEventManagement(scope.row, $event)">详情</div>
            </template>
          </el-table-column>
          <el-table-column label="志愿管理" width="100">
            <template slot-scope="scope">
              <div class="EventTableDetail" @click.prevent="handleEventVolunteer(scope.row, $event)">详情</div>
            </template>
          </el-table-column>
        </el-table>
        <el-pagination class="Pagination" background layout="prev, pager, next" :total="totalEvents"
          :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
      </div>
      <div v-else class="EventEmpty">
        暂无数据
      </div>
    </el-main>
  </div>
</template>

<script>

export default {
  name: 'EventList',
  data() {
    return {
      activeMenu: '2',
      selectedLocation: '',
      selectedType: '',
      selectedDate: null,
      searchName: '',
      events: [],
      currentPage: 1,
      pageSize: 7,
      my_id: '',
    };
  },
  created() {
    this.events = this.$route.params.events
  },
  computed: {
    filteredEvents() {
      let filtered = this.events;
      if (this.searchName) {
        filtered = filtered.filter(event => event.name.includes(this.searchName));
      }
      if (this.selectedType) {
        filtered = filtered.filter(event => event.type.includes(this.selectedType));
      }
      if (this.selectedDate) {
        // 将前端选择的日期转换为只有年月日的格式
        const selectedDateString = this.formatDate(this.selectedDate);
        // 筛选日期
        filtered = filtered.filter(event => {
          const eventDateString = this.formatDate(event.date);
          return eventDateString === selectedDateString;
        });
      }
      return filtered.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize);
    },
    totalEvents() {
      let filtered = this.events;
      if (this.searchName) {
        filtered = filtered.filter(event => event.name.includes(this.searchName));
      }
      if (this.selectedType) {
        filtered = filtered.filter(event => event.type.includes(this.selectedType));
      }
      if (this.selectedDate) {
        // 将前端选择的日期转换为只有年月日的格式
        const selectedDateString = this.formatDate(this.selectedDate);
        // 筛选日期
        filtered = filtered.filter(event => {
          const eventDateString = this.formatDate(event.date);
          return eventDateString === selectedDateString;
        });
      }
      return filtered.length;
    }
  },

  methods: {
    formatDate(dateString) {
      const date = new Date(dateString); // 创建一个Date对象
      const year = date.getFullYear(); // 获取年份
      const month = (date.getMonth() + 1).toString().padStart(2, '0'); // 获取月份，月份从0开始计数，所以加1
      const day = date.getDate().toString().padStart(2, '0'); // 获取日期

      return `${year}-${month}-${day}`; // 返回格式化的日期字符串
    },
    handlePageChange(page) {
      this.currentPage = page;
    },
    handleEventManagement(row, event) {
      event.stopPropagation();
      if (localStorage.getItem('UserRole') === 'Admin')
        this.$router.push({ name: 'EventManagementTab', params: { event_id: row.id, name: row.name } });
      else
        this.$message.error('您不是管理员，没有权限')
    },
    handleEventVolunteer(row, event) {
      event.stopPropagation();
      if (localStorage.getItem('UserRole') === 'Admin')
        this.$router.push({ name: 'EventVolunteerTab', params: { event_id: row.id, name: row.name } });
      else
        this.$message.error('您不是管理员，没有权限')
    },
    handleLeaderBoard(row, event) {
      event.stopPropagation();
      this.$router.push({ name: 'LeaderBoard', params: { event_id: row.id, name: row.name } });
    },
    handleRowClick(event) {
      console.log(event)
      this.$router.push({ name: 'EventDetail', params: { event_id: event.id } });
    },
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/EventList.css";
</style>