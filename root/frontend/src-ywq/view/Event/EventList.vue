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
          <el-select v-model="selectedLocation" placeholder="请选择比赛地点" class="filter-item">
            <el-option label="全部" value=""></el-option>
            <el-option label="北京市" value="北京"></el-option>
            <el-option label="天津市" value="天津"></el-option>
            <el-option label="河北省" value="河北"></el-option>
            <el-option label="山西省" value="山西"></el-option>
            <el-option label="内蒙古自治区" value="内蒙古"></el-option>
            <el-option label="辽宁省" value="辽宁"></el-option>
            <el-option label="吉林省" value="吉林"></el-option>
            <el-option label="黑龙江省" value="黑龙江"></el-option>
            <el-option label="上海市" value="上海"></el-option>
            <el-option label="江苏省" value="江苏"></el-option>
            <el-option label="浙江省" value="浙江"></el-option>
            <el-option label="安徽省" value="安徽"></el-option>
            <el-option label="福建省" value="福建"></el-option>
            <el-option label="江西省" value="江西"></el-option>
            <el-option label="河南省" value="河南"></el-option>
            <el-option label="山东省" value="山东"></el-option>
            <el-option label="湖北省" value="湖北"></el-option>
            <el-option label="湖南省" value="湖南"></el-option>
            <el-option label="广东省" value="广东"></el-option>
            <el-option label="广西壮族自治区" value="广西"></el-option>
            <el-option label="海南省" value="海南"></el-option>
            <el-option label="重庆市" value="重庆"></el-option>
            <el-option label="四川省" value="四川"></el-option>
            <el-option label="贵州省" value="贵州"></el-option>
            <el-option label="云南省" value="云南"></el-option>
            <el-option label="西藏自治区" value="西藏"></el-option>
            <el-option label="陕西省" value="陕西"></el-option>
            <el-option label="甘肃省" value="甘肃"></el-option>
            <el-option label="青海省" value="青海"></el-option>
            <el-option label="宁夏回族自治区" value="宁夏"></el-option>
            <el-option label="新疆维吾尔自治区" value="新疆"></el-option>
            <el-option label="台湾省" value="台湾"></el-option>
            <el-option label="香港特别行政区" value="香港"></el-option>
            <el-option label="澳门特别行政区" value="澳门"></el-option>
          </el-select>
          <el-select v-model="selectedType" placeholder="请选择赛事类型" class="filter-item">
            <el-option label="全部" value=""></el-option>
            <el-option label="半马" value="半马"></el-option>
            <el-option label="全马" value="全马"></el-option>
          </el-select>
          <el-date-picker v-model="selectedDate" type="date" placeholder="请选择开赛时间" class="filter-item"></el-date-picker>
          <el-input placeholder="请输入赛事名称" v-model="searchName" class="filter-item"></el-input>
        </div>
        <el-table :data="filteredEvents" style="width: 100%" @row-click="handleRowClick" row-class-name="clickable-row">
          <el-table-column prop="date" label="开赛时间" width="180"></el-table-column>
          <el-table-column prop="name" label="比赛名称" width="180"></el-table-column>
          <el-table-column prop="location" label="比赛地点" width="180"></el-table-column>
          <el-table-column prop="type" label="赛事类型" width="180"></el-table-column>
          <el-table-column prop="scale" label="赛事规模" width="180"></el-table-column>
        </el-table>
        <el-pagination
          class="pagination"
          background
          layout="prev, pager, next"
          :total="totalEvents"
          :page-size="pageSize"
          @current-change="handlePageChange"
        ></el-pagination>
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
          { id:'1', date: '2024-09-22', name: '2024天津团泊湖半程马拉松', location: '天津市/静海区', type: '半马', scale: '6000人' },
          { id:'2', date: '2024-09-16', name: '2024邯郸半程马拉松', location: '河北省/邯郸市/丛台区', type: '半马', scale: '10000人' },
          { id:'3', date: '2024-09-08', name: '2024长治环漳泽湖马拉松赛', location: '山西省/长治市/潞州区', type: '全马', scale: '10000人' },
          { id:'4', date: '2024-09-01', name: '2024长春马拉松赛', location: '吉林省/长春市/南关区', type: '全马，半马', scale: '30000人' },
          { id:'5', date: '2024-08-31', name: '2024广元半程马拉松赛', location: '四川省/广元市/利州区', type: '半马', scale: '10000人' },
          { id:'6', date: '2024-08-25', name: '2024鄂州环梁子湖马拉松比赛', location: '湖北省/鄂州市/梁子湖区', type: '半马', scale: '3000人' },
          { id:'7', date: '2024-09-22', name: '2024天津团泊湖半程马拉松', location: '天津市/静海区', type: '半马', scale: '6000人' },
          { id:'8', date: '2024-09-16', name: '2024邯郸半程马拉松', location: '河北省/邯郸市/丛台区', type: '半马', scale: '10000人' },
          { id:'9', date: '2024-09-08', name: '2024长治环漳泽湖马拉松赛', location: '山西省/长治市/潞州区', type: '全马', scale: '10000人' },
          { id:'10', date: '2024-09-01', name: '2024长春马拉松赛', location: '吉林省/长春市/南关区', type: '全马，半马', scale: '30000人' },
          { id:'11', date: '2024-08-31', name: '2024广元半程马拉松赛', location: '四川省/广元市/利州区', type: '半马', scale: '10000人' },
          { id:'12', date: '2024-08-25', name: '2024鄂州环梁子湖马拉松比赛', location: '湖北省/鄂州市/梁子湖区', type: '半马', scale: '3000人' }
          ],
        currentPage: 1,
        pageSize: 6
      };
    },
    computed: {
      filteredEvents() {
        let filtered = this.events;
        if (this.searchName) {
          filtered = filtered.filter(event => event.name.includes(this.searchName));
        }
        if (this.selectedLocation) {
          filtered = filtered.filter(event => event.location.includes(this.selectedLocation));
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
        if (this.selectedLocation) {
          filtered = filtered.filter(event => event.location.includes(this.selectedLocation));
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
      this.$router.push({ name: 'EventDetail', params: { id: row.id } });
      }
    }
  };
  </script>
  
  <style>
  #app {
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
    margin: 20px 0;
  }
  
  .input-with-select {
    width: 300px;
  }
  
  .el-table {
    width: 100%;
    table-layout: fixed;
    border: none; /* 移除表格的边框 */
  }
  
  .el-table th .cell,
  .el-table td .cell {
    display: inline-block;
    width: 100%;
    text-align: center;
    height: 43px;
    border: none; /* 移除单元格的边框 */
  }
  
  .el-table__header-wrapper,
  .el-table__body-wrapper {
    display: flex;
    justify-content: center;
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
  