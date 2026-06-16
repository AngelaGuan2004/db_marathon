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
      <button type="primary" @click="showItemManagement">添加</button>
      <button type="primary" @click="savePackageDetails">保存</button>
    </div>
    <el-dialog :visible.sync="dialogVisible" width="80%" title="物品管理" @close="resetItemManagement">
      <ItemManagement ref="itemManagement" :show-category-select="true" @save="handleSaveItems" />
    </el-dialog>
  </div>
</template>

<script>
import ItemManagement from './ItemManagement.vue';
import { getPackageDetails, savePackageDetails } from '@/api/PackageManagement';

export default {
  components: {
    ItemManagement
  },
  data() {
    return {
      packageForm: {
        eventName: ''
      },
      items: [],
      dialogVisible: false
    };
  },
  created() {
    this.loadPackageDetails();
  },
  methods: {
    loadPackageDetails() {
      const { eventName } = this.packageForm;
      if (eventName) {
        getPackageDetails(eventName)
          .then(response => {
            this.items = response.data;
          })
          .catch(error => {
            console.error('Error loading package details:', error);
          });
      }
    },
    showItemManagement() {
      this.dialogVisible = true;
    },
    handleSaveItems(selectedItems) {
      selectedItems.forEach(selectedItem => {
        const existingItem = this.items.find(item => item.id === selectedItem.id && item.category === selectedItem.category);
        if (existingItem) {
          existingItem.quantity = parseInt(existingItem.quantity) + parseInt(selectedItem.quantity);
        } else {
          this.items.push({ 
            id: selectedItem.id, 
            name: selectedItem.name, 
            quantity: selectedItem.quantity, 
            category: selectedItem.category 
          });
        }
      });
      this.dialogVisible = false;
    },
    savePackageDetails() {
      const { eventName } = this.packageForm;
      savePackageDetails(eventName, this.items)
        .then(() => {
          this.$message.success('保存成功');
        })
        .catch(error => {
          console.error('Error saving package details:', error);
          this.$message.error('保存失败');
        });
    },
    resetItemManagement() {
      this.$refs.itemManagement.resetItems();
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
