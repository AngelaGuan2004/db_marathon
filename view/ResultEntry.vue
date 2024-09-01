<template>
    <div class="results-container">
      <h2>完赛成绩</h2>
      <el-form :model="searchForm" label-width="120px" inline>
        <el-form-item label="比赛名称">
          <el-input v-model="searchForm.raceName" placeholder="输入比赛名称进行搜索"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="searchResults">搜索</el-button>
        </el-form-item>
      </el-form>
      
      <div class="filters">
        <el-select v-model="sortField" placeholder="选择排序方式">
          <el-option label="净成绩" value="netTime"></el-option>
          <el-option label="枪声成绩" value="gunTime"></el-option>
        </el-select>
  
        <el-select v-model="sortOrder" placeholder="选择排序顺序">
          <el-option label="顺序" value="asc"></el-option>
          <el-option label="逆序" value="desc"></el-option>
        </el-select>
      </div>
  
      <el-table :data="sortedResults" stripe>
        <el-table-column prop="name" label="姓名"></el-table-column>
        <el-table-column prop="netTime" label="净成绩"></el-table-column>
        <el-table-column prop="gunTime" label="枪声成绩"></el-table-column>
      </el-table>
    </div>
  </template>
  
  <script>
  import { get_all_results } from '@/api/grade'; // 导入API函数

  export default {
    name: 'ResultEntry',
    data() {
      return {
        searchForm: {
          Event_id: '10095' // 默认查询的比赛ID
        },
        sortField: 'netTime', // 排序字段（净成绩或枪声成绩）
        sortOrder: 'asc', // 排序顺序（顺序或逆序）
        results: [] // 存储比赛成绩的数组
      };
    },
    computed: {
      sortedResults() {
        return this.results.sort((a, b) => {
          const fieldA = this.sortField === 'netTime' ? a.netTime : a.gunTime;
          const fieldB = this.sortField === 'netTime' ? b.netTime : b.gunTime;
          if (this.sortOrder === 'asc') {
            return fieldA.localeCompare(fieldB);
          } else {
            return fieldB.localeCompare(fieldA);
          }
        });
      }
    },
    created() {
      this.searchResults(); // 页面加载时自动查询默认比赛ID的成绩
    },
    methods: {
      async searchResults() {
        if (this.searchForm.Event_id) {
          try {
            const response = await get_all_results(this.searchForm.Event_id);
            this.results = response.data; // 假设 API 返回的数据在 data 属性中
          } catch (error) {
            console.error('获取比赛成绩失败:', error);
          }
        }
      },
      changeSortOrder(field) {
        if (this.sortField === field) {
          this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
        } else {
          this.sortField = field;
          this.sortOrder = 'asc';
        }
      }
    }
  };
</script>

  
  
  <style scoped>
  .results-container {
    max-width: 800px;
    margin: 0 auto;
    padding: 30px;
    background-color: white;
    border: 1px solid grey;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }
  
  .filters {
    display: flex;
    justify-content: space-between;
    margin: 20px 0;
  }
  
  .el-form-item {
    margin-bottom: 0; /* 调整表单项的底部间距 */
  }
  </style>
  