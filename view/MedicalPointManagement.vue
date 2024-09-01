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

import {get_medical_point,add_medical_point,delete_medical_point  } from '@/api/MedicalPoint'; // 导入API函数*/

  export default {
    name: 'MedicalPointManagement',
    data() {
      return {
        packageForm: {
          eventName: ''
        },
        medicalPoints: [
         // { id: '1', position: '起点处' },
         // { id: '2', position: '终点处' }
        ],
        newMedicalPoint: {
          id: '',
          position: ''
        },
        dialogVisible: false
      };
    },
    created() {
    this.fetchMedicalPoints(); // 页面加载时获取所有医疗点
  },
  methods: {
    // 获取所有医疗点
    async fetchMedicalPoints() {
      try {
        const Event_id = 1; // 假设 Event_id 为 1，可根据实际需求调整
        const response = await get_medical_point(Event_id);
        this.medicalPoints = response.data; // 假设 API 返回的数据在 data 属性中
      } catch (error) {
        console.error('获取医疗点失败:', error);
      }
    },

    // 显示添加医疗点对话框
    showAddDialog() {
      this.dialogVisible = true;
    },

    // 添加医疗点
    async addMedicalPoint() {
      if (this.newMedicalPoint.id && this.newMedicalPoint.position) {
        try {
          const Event_id = 1; // 假设 Event_id 为 1，可根据实际需求调整
          await add_medical_point({
            Event_id,
            id: this.newMedicalPoint.id,
            place: this.newMedicalPoint.position
          });

          // 将新医疗点添加到数组中
          this.medicalPoints.push({
            id: this.newMedicalPoint.id,
            position: this.newMedicalPoint.position
          });

          this.dialogVisible = false;
          this.clearForm();
          this.$message.success('添加医疗点成功');
        } catch (error) {
          console.error('添加医疗点失败:', error);
          this.$message.error('添加医疗点失败');
        }
      } else {
        this.$message.error('请填写完整信息');
      }
    },

    // 删除医疗点确认
    confirmDelete(index, medicalPointID) {
      this.$confirm('确定要删除此医疗点？', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.deleteMedicalPoint(index, medicalPointID);
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        });
      });
    },

    // 删除医疗点
    async deleteMedicalPoint(index, medicalPointID) {
      try {
        await delete_medical_point(medicalPointID);
        this.medicalPoints.splice(index, 1);
        this.$message.success('删除成功!');
      } catch (error) {
        console.error('删除医疗点失败:', error);
        this.$message.error('删除医疗点失败');
      }
    },

    // 清空表单
    clearForm() {
      this.newMedicalPoint.id = '';
      this.newMedicalPoint.position = '';
    },

    // 点击行事件
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
  