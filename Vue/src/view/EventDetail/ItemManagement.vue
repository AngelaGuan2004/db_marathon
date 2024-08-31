<template>
  <div id="ItemManagement">
    <el-dialog title="物资管理" :visible.sync="dialogVisible" width="40%">
      <el-table :data="items">
        <el-table-column prop="id" label="物品ID" width="125"></el-table-column>
        <el-table-column prop="name" label="物品名" width="125"></el-table-column>
        <el-table-column label="选择" width="325">
          <template slot-scope="scope">
            <div style="display: inline-block;">
              <el-checkbox v-model="scope.row.selected" @change="handleSelectChange(scope.row)"></el-checkbox>
            </div>
            <div v-if="scope.row.selected" class="input-container">
              <el-input v-model="scope.row.quantity" placeholder="输入数量"
                style="margin-left: 20px; width: 150px;"></el-input>
              <el-select v-if="showCategorySelect" v-model="scope.row.category" placeholder="选择类别"
                style="margin-left: 20px; width: 150px;">
                <el-option label="参赛物资" value="参赛物资"></el-option>
                <el-option label="完赛物资" value="完赛物资"></el-option>
              </el-select>
            </div>
          </template>
        </el-table-column>
      </el-table>
      <div style=" text-align: right;margin-top: 35px;margin-right: 10px;">
        <button type="primary" @click="saveItems">保存</button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getItems } from '@/api/ItemManagement';

export default {
  name: 'ItemManagement',
  props: {
    showCategorySelect: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      items: [
        {
          id: "1",
          name: '156454',
        }, {
          id: "1",
          name: '156454',
        }, {
          id: "1",
          name: '156454',
        }
      ],
      dialogVisible: true,
    };
  },
  created() {
    this.loadItems();
  },
  methods: {
    loadItems() {
      getItems()
        .then(response => {
          this.items = response.data.map(item => ({ ...item, selected: false, quantity: '', category: '' }));
        })
        .catch(error => {
          console.error('Error loading items:', error);
        });
    },
    handleSelectChange(row) {
      if (!row.selected) {
        row.quantity = '';
        row.category = '';
      }
    },
    saveItems() {
      const selectedItems = this.items.filter(item => item.selected).map(item => ({
        id: item.id,
        name: item.name,
        quantity: item.quantity,
        category: this.showCategorySelect ? item.category : ''
      }));
      this.$emit('save', selectedItems);
    },
    resetItems() {
      this.items.forEach(item => {
        item.selected = false;
        item.quantity = '';
        item.category = '';
      });
    }
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';

.input-container {
  display: inline-block;
  text-align: center;
}

#ItemManagement .el-dialog__body {
  margin-bottom: 15px;
}

#ItemManagement .el-input {
  width: 90px !important;
}

#ItemManagement .el-select,
#ItemManagement .el-select .el-input {
  width: 120px !important;
}
</style>