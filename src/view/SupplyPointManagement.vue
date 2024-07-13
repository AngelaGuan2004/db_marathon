<template>
    <div class="management-container">
      <h2>补给点管理</h2>
      <el-form :model="packageForm" label-width="120px">
        <el-form-item label="比赛名称">
          <el-input v-model="packageForm.eventName" placeholder="比赛名称"></el-input>
        </el-form-item>
      </el-form>
      <el-table :data="supplyPoints">
        <el-table-column prop="id" label="补给点ID"></el-table-column>
        <el-table-column prop="place" label="补给位置"></el-table-column>
        <el-table-column prop="type" label="类型"></el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button type="danger" size="mini" @click="confirmDelete(scope.$index)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <div class="button-group">
        <el-button type="primary" @click="showSupplyPointForm">添加补给点</el-button>
      </div>
      <el-dialog :visible.sync="dialogVisible" width="50%" title="添加补给点">
        <el-form :model="newSupplyPoint" label-width="80px">
          <el-form-item label="补给位置">
            <el-input v-model="newSupplyPoint.place" placeholder="输入补给位置"></el-input>
          </el-form-item>
          <el-form-item label="类型">
            <el-select v-model="newSupplyPoint.type" placeholder="选择类型">
              <el-option label="水站" value="水站"></el-option>
              <el-option label="功能饮料站" value="功能饮料站"></el-option>
              <el-option label="能量补给点" value="能量补给点"></el-option>
            </el-select>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="addSupplyPoint">添加</el-button>
        </div>
      </el-dialog>
  
      <!-- Confirmation Dialog -->
      <el-dialog :visible.sync="confirmDialogVisible" width="30%" title="确认删除">
        <span>确定要删除此补给点？</span>
        <div slot="footer" class="dialog-footer">
          <el-button @click="confirmDialogVisible = false">取消</el-button>
          <el-button type="danger" @click="deleteSupplyPoint">确定</el-button>
        </div>
      </el-dialog>
    </div>
  </template>
  
  <script>
  export default {
    name: 'SupplyPointManagement',
    data() {
      return {
        packageForm: {
          eventName: ''
        },
        supplyPoints: [
          { id: '1', place: '起点', type: '水站' },
          { id: '2', place: '终点', type: '功能饮料站' }
        ],
        dialogVisible: false,
        confirmDialogVisible: false,
        deleteIndex: -1,
        newSupplyPoint: {
          id: '',
          place: '',
          type: ''
        }
      };
    },
    methods: {
      showSupplyPointForm() {
        this.dialogVisible = true;
      },
      addSupplyPoint() {
        // Generate unique ID for new supply point
        this.newSupplyPoint.id = String(this.supplyPoints.length + 1);
        this.supplyPoints.push({ ...this.newSupplyPoint });
        this.dialogVisible = false;
        // Optionally clear form fields
        this.newSupplyPoint = { id: '', place: '', type: '' };
      },
      confirmDelete(index) {
        this.deleteIndex = index;
        this.confirmDialogVisible = true;
      },
      deleteSupplyPoint() {
        if (this.deleteIndex !== -1) {
          this.supplyPoints.splice(this.deleteIndex, 1);
          this.confirmDialogVisible = false;
          this.$message({
            type: 'success',
            message: '删除成功!'
          });
        }
      }
    }
  };
  </script>
  
  <style scoped>
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
  </style>
  