<template>
  <div id="event_info">
    <el-header>
      <el-menu class="el-menu-demo" mode="horizontal" background-color="#c81623" text-color="#666" active-text-color="#ffd04b" :default-active="activeMenu">
        <el-menu-item index="1">首页</el-menu-item>
        <el-menu-item index="2">赛历</el-menu-item>
      </el-menu>
    </el-header>
    <el-main>
      <div class="filter-container">
        <el-select v-model="selectedType" placeholder="请选择赛事类型" class="filter-item">
          <el-option label="全部" value="马"></el-option>
          <el-option label="半马" value="半马"></el-option>
          <el-option label="全马" value="全马"></el-option>
        </el-select>
        <el-date-picker v-model="selectedDate" type="date" placeholder="请选择开赛时间" class="filter-item"></el-date-picker>
        <el-input placeholder="请输入赛事名称" v-model="searchName" class="filter-item"></el-input>
      </div>
      <div v-if="filteredEvents.length > 0">
        <el-table :data="filteredEvents" class="event-info-table" @row-click="handleRowClick" row-class-name="clickable-row">
          <el-table-column prop="date" label="开赛时间" width="150"></el-table-column>
          <el-table-column prop="name" label="比赛名称" width="300"></el-table-column>
          <el-table-column prop="type" label="赛事类型" width="150"></el-table-column>
          <el-table-column prop="scale" label="赛事规模" width="150"></el-table-column>
        </el-table>
        <el-pagination
          class="pagination"
          background
          layout="prev, pager, next"
          :total="totalEvents"
          :page-size="pageSize"
          @current-change="handlePageChange"
        ></el-pagination>
      </div>
      <div v-else class="custom-empty">
        暂无数据
      </div>
    </el-main>
  </div>
</template>

<script>
export default {
  data() {
    return {
      activeMenu: '2',
      selectedLocation: '',
      selectedType: '',
      selectedDate: null,
      searchName: '',
      events: [
{ id:'1', date: '2024-09-22', name: '2024天津团泊湖半程马拉松',type: '半马', scale: '6000人' },
{ id:'2', date: '2024-09-16', name: '2024邯郸半程马拉松', type: '半马', scale: '10000人' },
{ id:'3', date: '2024-09-08', name: '2024长治环漳泽湖马拉松赛', type: '全马', scale: '10000人' },
{ id:'4', date: '2024-09-01', name: '2024长春马拉松赛',type: '全马，半马', scale: '30000人' },
{ id:'5', date: '2024-08-31', name: '2024广元半程马拉松赛', type: '半马', scale: '10000人' },
{ id:'6', date: '2024-08-25', name: '2024鄂州环梁子湖马拉松比赛', type: '半马', scale: '3000人' },
{ id:'7', date: '2024-09-22', name: '2024天津团泊湖半程马拉松', type: '半马', scale: '6000人' },
{ id:'8', date: '2024-09-16', name: '2024邯郸半程马拉松', type: '半马', scale: '10000人' },
{ id:'9', date: '2024-09-08', name: '2024长治环漳泽湖马拉松赛', type: '全马', scale: '10000人' },
{ id:'10', date: '2024-09-01', name: '2024长春马拉松赛', type: '全马，半马', scale: '30000人' },
{ id:'11', date: '2024-08-31', name: '2024广元半程马拉松赛', type: '半马', scale: '10000人' },
{ id:'12', date: '2024-08-25', name: '2024鄂州环梁子湖马拉松比赛', type: '半马', scale: '3000人' }
],
      currentPage: 1,
      pageSize: 7
    };
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
        filtered = filtered.filter(event => event.date === this.selectedDate);
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
        filtered = filtered.filter(event => event.date === this.selectedDate);
      }
      return filtered.length;
    }
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    handleRowClick(row) {
      this.$router.push({ name: 'ParticipantLottery', params: { id: row.id } });
    }
  }
};
</script>

<style>
#event_info {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

.filter-item {
  flex: 1;
  min-width: 150px;
}

.el-menu-demo {
  flex: 1;
  margin-left: 20px;
}

.filter-container {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 10px;
  margin: 20px auto; /* 使用 auto 确保容器居中 */
  width: 600px; /* 调整宽度以适应内容 */
}


.event-info-table {
  width: 100%;
  table-layout: fixed;
  border: none;
}

.event-info-table th .cell,
.event-info-table td .cell {
  display: inline-block;
  width: 100%;
  text-align: center;
  height: 30px; /* Adjust the height as needed */
  border: none;
  vertical-align: middle; /* 垂直居中 */
  line-height: 30px; /* 确保行高与单元格高度一致 */
}

.el-table__header-wrapper,
.el-table__body-wrapper {
  display: flex;
  justify-content: center;
}

.custom-empty {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 200px;
  color: #999;
  width: 100%;
  text-align: center;
}

.pagination {
  display: flex;
  justify-content: center;
  margin: 20px 0;
}

.clickable-row {
  cursor: pointer;
}

.clickable-row:hover {
  background-color: #f5f5f5;
}
</style>