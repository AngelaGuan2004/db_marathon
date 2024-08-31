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
        <el-select v-model="selectedLocation" placeholder="请选择比赛地点" class="EventFilterItem">
          <el-option label="全部" value="ALL"></el-option>
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
          <el-table-column label="赛事管理" width="100">
            <div class="EventTableDetail" @click.prevent="handleEventManagement">详情</div>
          </el-table-column>
          <el-table-column label="志愿管理" width="100">
            <div class="EventTableDetail" @click.prevent="handleEventVolunteer">详情</div>
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
    handleEventManagement(event) {
      event.stopPropagation();
      if (localStorage.getItem('UserRole') === 'Admin')
        this.$router.push({ name: 'EventManagementTab', params: { id: event.id } });
      else
        this.$message.error('您不是管理员，没有权限')
    },
    handleEventVolunteer(event) {
      event.stopPropagation();
      if (localStorage.getItem('UserRole') === 'Admin')
        this.$router.push({ name: 'EventVolunteerTab', params: { id: event.id } });
      else
        this.$message.error('您不是管理员，没有权限')
    },
    handleRowClick(event) {
      this.$router.push({ name: 'EventDetail', params: { id: event.id } });
    },
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/EventList.css";
</style>