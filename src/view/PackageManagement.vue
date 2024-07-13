<template>
    <div class="management-container">
      <h2>物资包管理</h2>
      <el-form :model="packageForm" label-width="120px">
        <el-form-item label="比赛名称">
          <el-input v-model="packageForm.eventName" placeholder="比赛名称"></el-input>
        </el-form-item>
      </el-form>
      <el-table :data="items">
        <el-table-column prop="category" label="物品类别"></el-table-column>
        <el-table-column prop="id" label="物品ID"></el-table-column>
        <el-table-column prop="name" label="物品名"></el-table-column>
        <el-table-column prop="quantity" label="物品数量"></el-table-column>
      </el-table>
      <div class="button-group">
        <el-button type="primary" @click="showItemManagement">添加</el-button>
        <el-button type="primary" @click="savePackageDetails">保存</el-button>
      </div>
      <el-dialog :visible.sync="dialogVisible" width="80%" title="物品管理">
        <ItemManagement :show-category-select="true" @save="handleSaveItems" />
      </el-dialog>
    </div>
  </template>
  
  <script>
  import ItemManagement from './ItemManagement.vue';
  export default {
    name:'cyy',
    components: {
      ItemManagement
    },
    data() {
      return {
        packageForm: {
          eventName: ''
        },
        items: [
          { category: '参赛物资', id: '1', name: '号码布', quantity: '200' },
          { category: '完赛物资', id: '2', name: '奖牌', quantity: '200' }
        ],
        dialogVisible: false
      };
    },
    created() {
      this.loadPackageDetails();
    },
    methods: {
      loadPackageDetails() {
        // 从后端加载物资包数据
        // const { eventName } = this.packageForm;
        // if (eventName) {
        //   this.$http.get(`/api/event/${eventName}/packages`)
        //     .then(response => {
        //       this.items = response.data;
        //     })
        //     .catch(error => {
        //       console.error('Error loading package details:', error);
        //     });
        // }
      },
      showItemManagement() {
        this.dialogVisible = true;
      },
      handleSaveItems(selectedItems) {
        this.items = [...this.items, ...selectedItems];
        this.dialogVisible = false;
      },
      savePackageDetails() {
        // 保存物资包数据到后端
        // const { eventName } = this.packageForm;
        // this.$http.post(`/api/event/${eventName}/packages`, this.items)
        //   .then(() => {
        //     this.$message.success('保存成功');
        //   })
        //   .catch(error => {
        //     console.error('Error saving package details:', error);
        //     this.$message.error('保存失败');
        //   });
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
    justify-content: space-between;
    margin-top: 20px;
  }
  </style>
  