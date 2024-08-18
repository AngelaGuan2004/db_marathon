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
import { fetchEvents } from '@/api/Event' 

export default {
  data() {
    return {
      activeMenu: '2',
      selectedLocation: '',
      selectedType: '',
      selectedDate: null,
      searchName: '',
      events: [],
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
        filtered = filtered.filter(event => {
          const eventDate = new Date(event.Event_Date); // 确保是 Date 对象
          return eventDate.toDateString() === this.selectedDate.toDateString();
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
        filtered = filtered.filter(event => {
          const eventDate = new Date(event.Event_Date);
          return eventDate.toDateString() === this.selectedDate.toDateString();
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
        fetchEvents().then(response => {
          this.events = response.data.events.map(event => {
          return {
            id: event.Id,                        // 直接映射
            date: this.parseDate(event.Event_Date), // 日期字段处理
            name: event.Name,                    // 直接映射
            type: event.Category,                // 将后端的 category 字段映射为前端的 type
            scale: event.Scale                   // 直接映射
          };
        });
        }).catch(error => {
            console.error('Failed to load events:', error);
        });
    },
    parseDate(dateString) {
      if (!dateString) return null;  // 如果字符串为空，返回 null
      const parts = dateString.split('-');
      const year = parseInt(parts[0], 10);
      const month = parseInt(parts[1], 10) - 1; // 月份需要减 1
      const day = parseInt(parts[2], 10);
      return new Date(year, month, day);
    },
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