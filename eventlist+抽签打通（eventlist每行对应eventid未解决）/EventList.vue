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
        <el-table :data="filteredEvents" class="EventInfoTable" row-class-name="clickable-row">
          <el-table-column prop="date" label="开赛时间" width="150"></el-table-column>
          <el-table-column prop="name" label="比赛名称" width="250"></el-table-column>
          <el-table-column prop="type" label="赛事类型" width="150"></el-table-column>
          <el-table-column prop="scale" label="赛事规模" width="150"></el-table-column>
          <el-table-column label="赛事管理" width="100">
            <div class="EventTableDetail" @click.prevent="handleEventManagement">详情</div>
          </el-table-column>
          <el-table-column label="志愿详情" width="100">
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
import { fetchAllEvents } from '@/api/Event'

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
      my_id: 1,
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
  created() {
    this.loadAllEvents();  // 在组件创建时加载所有赛事信息
  },
  methods: {
    loadAllEvents() {
    fetchAllEvents().then(response => {
      console.log(response);
      // 映射 eventList 数组
      this.events = response.map(Event => ({
        id: Event.id,
        date: this.formatDate(Event.event_Date),
        name: Event.name,
        type: Event.category,
        scale: Event.scale
      }));
    }).catch(error => {
      // 错误处理
      this.$message.error('加载赛事失败，请稍后重试。');
      console.error('Failed to load events:', error);
    });
  },
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
    handleEventManagement(event) {
      this.$router.push({ name: 'EventManagementTab', params: { id: event.id } });
    },
    handleEventVolunteer() {
      this.$router.push({ name: 'EventVolunteerTab', });
    }
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/EventList.css";
</style>