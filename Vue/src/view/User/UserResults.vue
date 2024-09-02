<template>
  <div id="UserResults">
    <div class='NotTable' v-if="this.loading && !this.Flag">
      <div style=" display: table-cell;vertical-align: middle;width: 100vh;height: 300px;">
        未查询到志愿活动！
      </div>
    </div>
    <div class="UserResultsContent">
      <div>
        <!-- 筛选和最佳成绩栏目 -->
        <div class="UserResultsRaceType">
          <span style="margin-right: 5px;">比赛类型：</span>
          <el-select v-model="filterType" placeholder="选择比赛类型">
            <el-option label="全部" value="all"></el-option>
            <el-option label="全程马拉松" value="full"></el-option>
            <el-option label="半程马拉松" value="half"></el-option>
          </el-select>
          <!-- 
          <div class="UserResultsBestGrade" v-if="bestResultSelected">
            <span>最好成绩：</span>
            <span :class="['result_item', bestResult.type, 'best_result']">
              {{ bestResult.name }}
              <span class="highlight">{{ bestResult.time }}</span>
              <span>&nbsp;</span>
              <span class="highlight">{{ bestResult.rank }}名</span>
            </span>
          </div> -->

          <!-- 搜索框 -->
          <div class="UserResultsSearch">
            <el-input v-model="searchQuery" placeholder="搜索比赛..." class="search_input" @keyup.enter.native="search" />
            <el-button type="primary" icon="el-icon-search" @click="search">搜索</el-button>
          </div>
        </div>

        <!-- 比赛成绩表格 -->
        <div>
          <el-table :data="paginatedResults" v-loading='loading' class="table" row-class-name="clickable-row" stripe>
            <el-table-column prop="event_Name" label="比赛名称" width="350"></el-table-column>
            <el-table-column prop="type" label="比赛类型" width="150">
              <template slot-scope="scope">
                <div>{{ getRaceType(scope.row.type) }}</div>
              </template>
            </el-table-column>
            <el-table-column prop="net_Result" label="净成绩" width="200">
              <template slot-scope="scope">
                <div style="font-weight: bold;">{{ scope.row.net_Result }}</div>
              </template>
            </el-table-column>
            <el-table-column prop="rank" label="排名" width="150">
              <template slot-scope="scope">
                <div style="color: #c81623;font-weight: bold;">{{ scope.row.rank }}</div>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination v-if="filteredResults.length > 0" background layout="prev, pager, next"
            :total="filteredResults.length" :page-size="pageSize" :current-page.sync="currentPage"
            @current-change="handlePageChange" class="Pagination">
          </el-pagination>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
import { getMyResults, getHalfResults, getFullResults } from '@/api/myGrade'; // 导入API函数
export default {
  name: 'UserResults',
  data() {
    return {
      searchQuery: '',
      filterType: 'all',
      currentPage: 1,
      pageSize: 8,
      results: [],
      filteredResults: [],
      loading: true,
      Flag: true
    }
  },

  created() {
    this.fetchAllResults(); // 页面加载时获取所有比赛成绩
  },

  methods: {
    // 获取所有比赛成绩
    async fetchAllResults() {
      try {
        const Player_ID = localStorage.getItem('UserId');
        const response = await getMyResults(Player_ID);
        this.results = response; // 假设 API 返回的数据在 data 属性中
        this.filteredResults = this.results; // 初始化筛选结果
        this.loading = false
      } catch (error) {
        console.error('获取比赛成绩失败:', error);
        this.Flag = false
      }
    },
    search() {
      const query = this.searchQuery.trim();
      if (query) {
        // 根据搜索框输入的内容搜索
        this.filteredResults = this.results.filter(result =>
          result.event_Name.includes(query) || result.event_ID === query
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
    handlePageChange(page) {
      this.currentPage = page;
    },
    getRaceType(type) {
      return type === 'full' ? '全马' : '半马';
    }

  },
  computed: {
    paginatedResults() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = start + this.pageSize;
      return this.filteredResults.slice(start, end);
    }
  }

}
</script>

<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";
@import "@/assets/css/UserResults.css";
</style>