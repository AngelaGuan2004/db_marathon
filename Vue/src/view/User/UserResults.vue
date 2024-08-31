<template>
  <div id="UserResults">
    <div class="UserResultsContent">
      <div>
        <!-- 筛选和最佳成绩栏目 -->
        <div class="UserResultsRaceType">
          <span style="margin-right: 5px;">比赛类型：</span>
          <el-select v-model="filterType" placeholder="选择比赛类型" @change="showBest">
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
          </div>
        </div>

        <!-- 比赛成绩表格 -->
        <div>
          <el-table :data="paginatedResults" class="table" row-class-name="clickable-row" stripe>
            <el-table-column prop="name" label="比赛名称" width="350"></el-table-column>
            <el-table-column prop="type" label="比赛类型" width="150">
              <template slot-scope="scope">
                <div>{{ getRaceType(scope.row.type) }}</div>
              </template>
            </el-table-column>
            <el-table-column prop="time" label="净成绩" width="200">
              <template slot-scope="scope">
                <div style="font-weight: bold;">{{ scope.row.time }}</div>
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
export default {
  name: 'UserResults',
  data() {
    return {
      searchQuery: '',
      bestResultSelected: false,
      bestResult: {},
      filterType: 'all',
      currentPage: 1,
      pageSize: 8,
      results: [
        { name: '上海国际马拉松', type: 'full', time: '3:45:30', rank: 123 },
        { name: '杭州半程马拉松', type: 'half', time: '1:45:20', rank: 56 },
        { name: '北京马拉松', type: 'full', time: '3:50:10', rank: 150 },
        { name: '广州半程马拉松', type: 'half', time: '1:50:00', rank: 78 },
        { name: '深圳国际马拉松', type: 'full', time: '3:55:40', rank: 200 },
        { name: '成都半程马拉松', type: 'half', time: '1:55:30', rank: 90 },
        { name: '重庆国际马拉松', type: 'full', time: '4:05:20', rank: 250 },
        { name: '南京半程马拉松', type: 'half', time: '2:00:10', rank: 100 },
        { name: '武汉国际马拉松', type: 'full', time: '4:15:00', rank: 300 },
        { name: '西安半程马拉松', type: 'half', time: '2:05:50', rank: 120 },
        { name: '长沙国际马拉松', type: 'full', time: '4:25:30', rank: 350 },
        { name: '青岛半程马拉松', type: 'half', time: '2:10:40', rank: 140 },
        { name: '苏州国际马拉松', type: 'full', time: '4:35:20', rank: 400 },
        { name: '天津半程马拉松', type: 'half', time: '2:15:30', rank: 160 },
        { name: '哈尔滨国际马拉松', type: 'full', time: '4:45:50', rank: 450 }
      ]
    }
  },
  methods: {
    search() {
      console.log('Searching for:', this.searchQuery);
    },
    showBest(type) {
      if (type !== 'all') {
        this.bestResultSelected = true;
        const bestResult = this.results
          .filter(result => result.type === type)
          .reduce((best, result) => best.time < result.time ? best : result, {});
        this.bestResult = bestResult;
      }
      else {
        this.bestResultSelected = false;
        this.bestResult = ''
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
    filteredResults() {
      if (this.filterType === 'all') {
        return this.results;
      }
      return this.results.filter(result => result.type === this.filterType);
    },
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