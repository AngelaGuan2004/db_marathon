<template>
  <div class="results-container">
    <h2>完赛成绩</h2>
    <el-form :model="searchForm" label-width="120px" inline>
      <el-form-item label="比赛名称">
        <el-input v-model="searchForm.Event_id" placeholder="输入比赛ID进行搜索"></el-input>
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

    <div class="add">
      <el-button type="primary" @click="openDialog">
        {{ isDialogVisible ? '收起' : '录入选手成绩' }}
      </el-button>
    </div>

    <el-dialog
      title="录入选手成绩"
      :visible.sync="isDialogVisible"
      width="50%"
      @close="handleClose"
    >
      <el-form :model="resultForm" label-width="120px" inline>
        <el-form-item label="选手ID">
          <el-input v-model="resultForm.Player_id" placeholder="输入选手ID"></el-input>
        </el-form-item>
        <el-form-item label="枪声成绩">
          <el-input v-model="resultForm.gunResults" placeholder="输入枪声成绩"></el-input>
        </el-form-item>
        <el-form-item label="净成绩">
          <el-input v-model="resultForm.netResults" placeholder="输入净成绩"></el-input>
        </el-form-item>
        <el-form-item label="排名">
          <el-input v-model="resultForm.Rank" placeholder="输入排名"></el-input>
        </el-form-item>
        <el-form-item label="成绩ID">
          <el-input v-model="resultForm.grade_id" placeholder="输入成绩ID"></el-input>
        </el-form-item>
        <el-form-item label="性别排名">
          <el-input v-model="resultForm.gender_Rank" placeholder="输入性别排名"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="addResults">录入成绩</el-button>
          <el-button @click="isDialogVisible = false">取消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>

<script>
import { get_all_results, add_result } from '@/api/grade'; // 导入API函数

export default {
  name: 'ResultEntry',
  data() {
    return {
      searchForm: {
        Event_id: '10095'
      },
      sortField: 'netTime', // 排序（净成绩或枪声成绩）
      sortOrder: 'asc', // 排序（顺序或逆序）
      results: [],
      resultForm: {
        Player_id: '',
        gunResults: '',
        netResults: '',
        Rank: '',
        grade_id: '', // 新增字段
        gender_Rank: '' // 新增字段
      }, // 存储录入成绩的表单数据
      isDialogVisible: false // 控制弹出框的显示
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
    async addResults() {
      if (!this.searchForm.Event_id) {
        alert('请先选择比赛ID');
        return;
      }
      
      try {
        const response = await add_result({
          grade_id: this.resultForm.grade_id,
          gunResults: this.resultForm.gunResults,
          netResults: this.resultForm.netResults,
          Player_id: this.resultForm.Player_id,
          Event_id: this.searchForm.Event_id,
          Rank: this.resultForm.Rank,
          gender_Rank: this.resultForm.gender_Rank
        });
        if (response.data.success) {
          alert('成绩录入成功');
          this.searchResults(); // 录入成绩后重新加载成绩
          this.resultForm = {
            Player_id: '',
            gunResults: '',
            netResults: '',
            Rank: '',
            grade_id: '',
            gender_Rank: ''
          }; // 清空表单数据
          this.isDialogVisible = false; // 关闭弹出框
        } else {
          alert('成绩录入失败');
        }
      } catch (error) {
        console.error('录入成绩失败:', error);
      }
    },
    openDialog() {
      this.isDialogVisible = true;
    },
    handleClose() {
      this.resultForm = {
        Player_id: '',
        gunResults: '',
        netResults: '',
        Rank: '',
        grade_id: '',
        gender_Rank: ''
      }; 
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
  margin-bottom: 0; 
}

.add {
  margin: 0 auto;
  width: 200px; 
}
</style>
