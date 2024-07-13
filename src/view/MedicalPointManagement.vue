<template>
    <div class="management-container">
      <h2>医疗点管理</h2>
      <el-form :model="packageForm" label-width="120px">
        <el-form-item label="比赛名称">
          <el-input v-model="packageForm.eventName" placeholder="比赛名称"></el-input>
        </el-form-item>
      </el-form>
      <el-table :data="medicalPoints" @row-click="handleRowClick">
        <el-table-column prop="id" label="医疗点ID"></el-table-column>
        <el-table-column prop="position" label="位置"></el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-button type="danger" size="mini" @click="confirmDelete(scope.$index)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <div class="button-group">
        <el-button type="primary" @click="showAddDialog">添加</el-button>
      </div>
      <el-dialog
        :visible.sync="dialogVisible"
        width="40%"
        title="添加医疗点"
      >
        <el-form :model="newMedicalPoint" label-width="100px">
          <el-form-item label="医疗点ID">
            <el-input v-model="newMedicalPoint.id" placeholder="医疗点ID"></el-input>
          </el-form-item>
          <el-form-item label="位置">
            <el-input v-model="newMedicalPoint.position" placeholder="位置"></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="addMedicalPoint">保存</el-button>
        </div>
      </el-dialog>
    </div>
  </template>
  
  <script>
  export default {
    name: 'MedicalPointManagement',
    data() {
      return {
        packageForm: {
          eventName: ''
        },
        medicalPoints: [
          { id: '1', position: '起点处' },
          { id: '2', position: '终点处' }
        ],
        newMedicalPoint: {
          id: '',
          position: ''
        },
        dialogVisible: false
      };
    },
    methods: {
      showAddDialog() {
        this.dialogVisible = true;
      },
      addMedicalPoint() {
        if (this.newMedicalPoint.id && this.newMedicalPoint.position) {
          this.medicalPoints.push({
            id: this.newMedicalPoint.id,
            position: this.newMedicalPoint.position
          });
          this.dialogVisible = false;
          this.clearForm();
        } else {
          this.$message.error('请填写完整信息');
        }
      },
      confirmDelete(index) {
        this.$confirm('确定要删除此医疗点？', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.deleteMedicalPoint(index);
          this.$message({
            type: 'success',
            message: '删除成功!'
          });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          });
        });
      },
      deleteMedicalPoint(index) {
        this.medicalPoints.splice(index, 1);
      },
      clearForm() {
        this.newMedicalPoint.id = '';
        this.newMedicalPoint.position = '';
      },
      handleRowClick(row) {
        console.log('Clicked on row:', row);
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
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }
  .button-group {
    display: flex;
    justify-content: flex-end;
    margin-top: 20px;
  }
  </style>
  