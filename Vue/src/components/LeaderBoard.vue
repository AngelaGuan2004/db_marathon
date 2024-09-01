<template>
  <div>
    <!-- 顶部导航栏 -->
    <nav class="topBar">
      <!-- <div class="logo">
        <img 
          src="@/assets/logo.png" 
          alt="Logo" 
          class="index_logo" 
          style="height: 55px;"
        >
      </div> -->
      <div class="back">
        <router-link to="/" class="nav_link">返回</router-link>
      </div>
      <!-- 搜索框 -->
      <div class="search_container">
        <form @submit.prevent="search" class="search_form">
          <input type="text" v-model="searchQuery" placeholder="搜索比赛..." class="search_input">
          <button type="submit" class="search_button">搜索</button>
        </form>
      </div>
    </nav>

    <!-- 筛选区域 -->
    <div class="set">
      <el-row :gutter="20" class="filter-container">
        <el-col :span="8">
          <el-select v-model="selectedGender" placeholder="性别">
            <el-option label="全部" value="全部"></el-option>
            <el-option label="男" value="男"></el-option>
            <el-option label="女" value="女"></el-option>
          </el-select>
        </el-col>
        <el-col :span="8">
          <el-select v-model="sortType" placeholder="排序方式">
            <el-option label="顺序排列" value="顺序排列"></el-option>
            <el-option label="逆序排列" value="逆序排列"></el-option>
          </el-select>
        </el-col>
      </el-row>

      <!-- 成绩排行榜 -->
      <el-table :data="paginatedResults" :border="true" :style="{ width: '100%' }">
        <el-table-column label="姓名" header-align="center" align="center">
          <template v-slot:default="scope">
            <span>{{ scope.row.name }}</span>
          </template>
        </el-table-column>
        <el-table-column label="性别" header-align="center" align="center">
          <template v-slot:default="scope">
            <span>{{ scope.row.gender }}</span>
          </template>
        </el-table-column>
        <el-table-column label="比赛名称" header-align="center" align="center">
          <template v-slot:default="scope">
            <span>{{ scope.row.race }}</span>
          </template>
        </el-table-column>
        <el-table-column label="比赛成绩" header-align="center" align="center">
          <template v-slot:default="scope">
            <span :style="{ color: 'red' }">{{ scope.row.netTime }}</span>
          </template>
        </el-table-column>
        <el-table-column label="比赛排名" header-align="center" align="center">
          <template v-slot:default="scope">
            <span :style="{ color: 'red' }">{{ scope.row.rank }}</span>
          </template>
        </el-table-column>
      </el-table>

      <!-- 分页组件 -->
      <el-pagination background layout="prev, pager, next" :total="filteredResults.length" :page-size="10"
        @current-change="handlePageChange" class="pagination-bar"></el-pagination>
    </div>
  </div>
</template>

<script>
import { getRank } from '@/api/rank'; // 导入API函数*/
export default {
  name: 'LeaderBoard',
  data() {
    return {
      selectedGender: '全部',
      sortType: '顺序排列',
      searchQuery: '', // 用于输入 event ID 的搜索框
      currentPage: 1,
      results: [], // 存储比赛成绩的数组
      filteredResults: [], // 筛选后的比赛成绩数组
      itemsPerPage: 10, // 每页显示的结果数
    };
  },
  created() {
    this.fetchRaceResults(); // 页面加载时获取默认数据
  },
  computed: {
    paginatedResults() {
      // 根据当前页和每页显示的数量，计算分页后的数据
      const startIndex = (this.currentPage - 1) * this.itemsPerPage;
      const endIndex = startIndex + this.itemsPerPage;
      return this.filteredResults.slice(startIndex, endIndex);
    },
    totalPages() {
      // 计算总页数
      return Math.ceil(this.filteredResults.length / this.itemsPerPage);
    }
  },
  methods: {
    async fetchRaceResults() {
      try {
        const Event_id = this.searchQuery || 10095; // 默认 event_id 为 10001，用户输入的为优先
        const topN = 1;
        const gender = this.selectedGender === '全部' ? '女' : this.selectedGender;

        // 调用 API 获取数据
        const response = await getRank(Event_id, topN, gender);
        this.results = response;
        this.filterResults(); // 获取数据后进行初步筛选
      } catch (error) {
        console.error('获取比赛成绩失败:', error);
      }
    },
    filterResults() {
      let filtered = [...this.results];

      // 根据性别筛选
      if (this.selectedGender !== '全部') {
        filtered = filtered.filter(item => item.gender === this.selectedGender);
      }

      // 更新筛选后的数据
      this.filteredResults = filtered;

      // 进行排序
      this.sortResults();
    },
    sortResults() {
      // 根据排序方式进行排序
      if (this.sortType === '顺序排列') {
        this.filteredResults.sort((a, b) => a.rank - b.rank);
      } else {
        this.filteredResults.sort((a, b) => b.rank - a.rank);
      }
    },
    handlePageChange(currentPage) {
      this.currentPage = currentPage;
    },
    search() {
      this.fetchRaceResults(); // 在用户输入 event ID 后重新获取数据
    }
  }
};
</script>

<style scoped>
.topBar {
  background-color: #c81623;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 10px;
  position: fixed;
  top: 0;
  width: 100%;
  z-index: 1000;
}


.logo img {
  height: 40px;
}

.back {
  font-size: 16px;
}

.search_container {
  flex-grow: 1;
  display: flex;
  justify-content: center;
  margin-right: 120px;
}

.search_form {
  display: flex;
  align-items: center;
}

.search_input {
  padding: 8px;
  border: none;
  height: 30px;
  border-radius: 5px;
  font-size: 15px;
  border: 1px solid grey;
}

.search_button {
  padding: 8px 15px;
  background-color: rgb(250, 242, 242);
  border: none;
  height: 48px;
  border-radius: 5px;
  cursor: pointer;
  font-size: 15px;
}

.results_container {
  margin-top: 20px;
}

.filter_container {
  background-color: #f0f0f0;
  padding: 10px;
  width: 100%;
  box-sizing: border-box;
  position: fixed;
  top: 60px;
  z-index: 900;
  border-bottom: 1px solid #ccc;
}

.filter_label {
  font-weight: bold;
}

.select_box {
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.results_container {
  margin-top: 100px;
}

.table_container {
  margin-top: 20px;
}

.race_table {
  width: 100%;
  border-collapse: collapse;
}

.race_table th,
.race_table td {
  padding: 10px;
  text-align: center;
}

.race_table th {
  background-color: #f0f0f0;
  border-bottom: 2px solid #ccc;
}

.race_table td {
  border-bottom: 1px solid #ccc;
}

.pagination_container {
  text-align: center;
  padding: 10px;
  bottom: 0;
  left: 0;
  right: 0;
  z-index: 1000;
  font-size: 24px;
  height: 50px;
}

.pagination_button {
  cursor: pointer;
  padding: 8px 12px;
  margin: 0 5px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.pagination_button.active {
  background-color: #007bff;
  color: white;
  border-color: #007bff;
}

@media (max-width: 600px) {
  .topBar {
    flex-direction: column;
    align-items: flex-start;
  }

  .search_container {
    margin-top: 12px;
  }
}

.set {
  margin-top: 100px;
  text-align: center;
}
</style>