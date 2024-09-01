<template>
  <div>
    <!-- 筛选和最佳成绩栏目 -->
    <div class="pick_best">
      <span>比赛类型：</span>
      <el-select v-model="filterType" placeholder="选择比赛类型">
        <el-option label="全部" value="all"></el-option>
        <el-option label="全程马拉松" value="full"></el-option>
        <el-option label="半程马拉松" value="half"></el-option>
      </el-select>

      <!-- 搜索框 -->
      <div class="search_container">
        <el-input
          v-model="searchQuery"
          placeholder="搜索比赛..."
          class="search_input"
          @keyup.enter.native="search"
        />
        <el-button type="primary" icon="el-icon-search" @click="search">搜索</el-button>
      </div>
    </div>

    <!-- 比赛成绩表格 -->
    <div class="grades">
      <table class="result_table">
        <thead>
          <tr>
            <th>比赛名称</th>
            <th>比赛类型</th>
            <th>净成绩</th>
            <th>排名</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="result in paginatedResults" :key="result.name">
            <td>{{ result.name }}</td>
            <td>{{ result.type === 'half' ? '半马' : '全马' }}</td>
            <td :style="{ color: 'red' }">{{ result.time }}</td>
            <td :style="{ color: 'red' }">{{ result.rank }}名</td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- 分页组件 -->
    <el-pagination
      v-if="filteredResults.length > 0"
      background
      layout="prev, pager, next"
      :total="filteredResults.length"
      :page-size="pageSize"
      :current-page.sync="currentPage"
      @current-change="handlePageChange"
      class="pagination_bar"
    >
    </el-pagination>
  </div>
</template>

<script>
import { getMyResults, getHalfResults, getFullResults } from '@/api/myGrade'; // 导入API函数

export default {
  name: 'MyResults',
  data() {
    return {
      searchQuery: '', // 搜索框输入内容
      filterType: 'all', // 筛选类型：全部、全马、半马
      currentPage: 1, // 当前页码
      pageSize: 10, // 每页显示条数
      results: [], // 存储所有比赛成绩
      filteredResults: [], // 存储筛选后的比赛成绩
    };
  },

  created() {
    this.fetchAllResults(); // 页面加载时获取所有比赛成绩
  },

  methods: {
    // 获取所有比赛成绩
    async fetchAllResults() {
      try {
        const Player_ID = 1; // 设置 Player_ID 为 1
        const response = await getMyResults(Player_ID);
        this.results = response; // 假设 API 返回的数据在 data 属性中
        this.filteredResults = this.results; // 初始化筛选结果
      } catch (error) {
        console.error('获取比赛成绩失败:', error);
      }
    },

    // 搜索功能，根据比赛名称或ID进行筛选
    search() {
      const query = this.searchQuery.trim();
      if (query) {
        // 根据搜索框输入的内容搜索
        this.filteredResults = this.results.filter(result =>
          result.name.includes(query) || result.Event_ID.toString() === query
        );
      } else {
        // 如果搜索框为空，显示所有成绩
        this.filteredResults = this.results;
      }
    },

    // 根据比赛类型进行筛选
    async filterResults(type) {
      this.filterType = type;

      try {
        let response;
        const Player_ID = 1; // 保持一致的 Player_ID

        if (type === 'full') {
          response = await getFullResults(Player_ID); // 获取全马成绩
        } else if (type === 'half') {
          response = await getHalfResults(Player_ID); // 获取半马成绩
        } else {
          // 如果选择 "全部"，则显示所有成绩
          this.filteredResults = this.results;
          return;
        }
        this.filteredResults = response.data; // 更新筛选结果
      } catch (error) {
        console.error('筛选比赛成绩失败:', error);
      }
    },

    // 分页处理
    handlePageChange(page) {
      this.currentPage = page;
    }
  },

  computed: {
    // 计算当前页需要显示的比赛成绩
    paginatedResults() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = start + this.pageSize;
      return this.filteredResults.slice(start, end);
    }
  }
};
</script>



<style scoped>
.search_container {
  display: flex;
  align-items: center;
  margin-left: auto; /* 将搜索框放置在最右边 */
}

.el-input {
  width: 200px; 
}

.el-button--primary {
  background-color: #c81623; /* 酒红色 */
}

.pick_best {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin: 20px 0;
  top: 80px; /* 位置在导航栏下面 */
  background: white;
  padding: 10px 0;
  z-index: 999;
  height:50px;
}

.best_result {
  display: inline-block;
  margin-left: 20px;
  background-color: #ffffff;
  color: rgb(12, 5, 5);
  padding: 5px 8px 2px 8px;
  font-size: 20px;
  line-height: 24px; 
}

.grades {
  z-index: 1000; 
  max-height: calc(100vh - 280px); 
  overflow-y: auto; /* 滚动条 */
  text-align: center;
  position: relative;
  max-height:550px;
  scrollbar-color: #888 transparent;
}


.result_table {
  width: 100%;
  border-collapse: collapse;
  text-align: left;
  font-size: 16px;
  font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
  margin-top: 20px;
}

.result_table th, .result_table td {
  padding: 10px 20px;
  border-bottom: 1px solid #ddd;
}

.result_table th {
  background-color: #f9f9f9;
  font-weight: bold;
}

.result_table td {
  vertical-align: middle;
}

.result_table td.red {
  color: red;
}

.pagination_bar {
  background-color: #ffffff;
  text-align: center;
  padding: 10px;
  bottom: 0;
  left: 0;
  right: 0;
  z-index: 1000;
  font-size: 24px; 
  height: 50px;
}
</style>
