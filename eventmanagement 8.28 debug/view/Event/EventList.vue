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
      events: [
        { id: '1', date: '2024-09-22', name: '2024天津团泊湖半程马拉松', type: '半马', scale: '6000人' },
        { id: '2', date: '2024-09-16', name: '2024邯郸半程马拉松', type: '半马', scale: '10000人' },
        { id: '3', date: '2024-09-08', name: '2024长治环漳泽湖马拉松赛', type: '全马', scale: '10000人' },
        { id: '4', date: '2024-09-01', name: '2024长春马拉松赛', type: '全马，半马', scale: '30000人' },
        { id: '5', date: '2024-08-31', name: '2024广元半程马拉松赛', type: '半马', scale: '10000人' },
        { id: '6', date: '2024-08-25', name: '2024鄂州环梁子湖马拉松比赛', type: '半马', scale: '3000人' },
        { id: '7', date: '2024-09-22', name: '2024天津团泊湖半程马拉松', type: '半马', scale: '6000人' },
        { id: '8', date: '2024-09-16', name: '2024邯郸半程马拉松', type: '半马', scale: '10000人' },
        { id: '9', date: '2024-09-08', name: '2024长治环漳泽湖马拉松赛', type: '全马', scale: '10000人' },
        { id: '10', date: '2024-09-01', name: '2024长春马拉松赛', type: '全马，半马', scale: '30000人' },
        { id: '11', date: '2024-08-31', name: '2024广元半程马拉松赛', type: '半马', scale: '10000人' },
        { id: '12', date: '2024-08-25', name: '2024鄂州环梁子湖马拉松比赛', type: '半马', scale: '3000人' }
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
      this.$router.push({ name: 'EventTab', params: { id: row.id } });
    }
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/EventList.css";
</style>