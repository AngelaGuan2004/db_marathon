<template>
    <div class="management-container">
      <h2>物品管理</h2>
      <el-table :data="items" @row-click="handleRowClick">
        <el-table-column prop="id" label="物品ID"></el-table-column>
        <el-table-column prop="name" label="物品名"></el-table-column>
        <el-table-column label="选择">
          <template slot-scope="scope">
            <el-checkbox v-model="scope.row.selected" @change="handleSelectChange(scope.row)">
            </el-checkbox>
            <div v-if="scope.row.selected" class="input-container">
              <el-input v-model="scope.row.quantity" placeholder="输入数量" style="margin-left: 10px; width: 150px;"></el-input>
              <el-select v-if="showCategorySelect" v-model="scope.row.category" placeholder="选择类别" style="margin-left: 10px; width: 150px;">
                <el-option label="参赛物资" value="参赛物资"></el-option>
                <el-option label="完赛物资" value="完赛物资"></el-option>
              </el-select>
            </div>
          </template>
        </el-table-column>
      </el-table>
      <div class="button-group">
        <el-button type="primary" @click="saveItems">保存</el-button>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    props: {
      showCategorySelect: {
        type: Boolean,
        default: false
      }
    },
    data() {
      return {
        items: [
          { id: '1', name: '水', selected: false, quantity: '', category: '' },
          { id: '2', name: '能量棒', selected: false, quantity: '', category: '' },
          { id: '3', name: '运动饮料', selected: false, quantity: '', category: '' }
        ]
      };
    },
    created() {
      this.loadItems();
    },
    methods: {
      loadItems() {
        // 从后端加载物品数据
        // this.$http.get(`/api/items`)
        //   .then(response => {
        //     this.items = response.data.map(item => ({ ...item, selected: false, quantity: '', category: '' }));
        //   })
        //   .catch(error => {
        //     console.error('Error loading items:', error);
        //   });
      },
      handleSelectChange(row) {
        if (!row.selected) {
          row.quantity = '';
          row.category = '';
        }
      },
      saveItems() {
        const selectedItems = this.items.filter(item => item.selected);
        this.$emit('save', selectedItems);
      }
    }
  };
  </script>
  
  <style>
  .management-container {
    max-width: 800px;
    margin: 0 auto;
    padding: 30px;
    background-color: white;
    border: 1px solid grey;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  }
  .button-group {
    display: flex;
    justify-content: flex-end;
    margin-top: 20px;
  }
  .input-container {
    display: flex;
    align-items: center;
  }
  </style>
  