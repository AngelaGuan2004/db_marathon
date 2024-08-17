<template>
    <div>
      <nav class="navbar">
        <img src="@/assets/logo.png" alt="Logo" class="logo" style="height: 55px;">
        <button @click="navigateTo('/')">首页</button>
        <button @click="navigateTo('/profile')">个人空间</button>
        <button @click="navigateTo('/profile')">赛事摄影</button>
        <button @click="navigateTo('/injuryEntry')">伤员</button>
      </nav>       
      
      <div class="injury-list">
        <div style="margin-left: 45px;margin-top: 0px;height: 40px;">
          <h2 style="font-size: 24px; color: #565656;">伤员名单</h2>
        </div>
      
        <div class="table-container">
          <el-table :data="injuredPlayers" stripe height="400" :default-sort = "{prop: 'date', order: 'descending'}">
            <el-table-column
              prop="date"
              label="日期"
              sortable
              width="120">
            </el-table-column>
            <el-table-column
              prop="name"
              label="姓名"
              sortable
              width="120">
              <template slot-scope="scope">
                <div slot="reference" class="name-wrapper">
                  <el-tag size="medium">{{ scope.row.name }}</el-tag>
                </div>
              </template>
            </el-table-column>
            <el-table-column
              prop="ID"
              label="编号"
              sortable
              width="120">
            </el-table-column>
            <el-table-column
              prop="medicalPoint"
              label="医疗点"
              :formatter="formatter">
            </el-table-column>
            <el-table-column label="操作" width="180">
             <template slot-scope="scope">
                <el-button
                  size="small"
                  @click="handleEdit(scope.$index, scope.row)"
                  style=" background-color: #f3d5d7; 
                          color:white;
                          font-size: 14px;       
                          border-width: 2px;
                          border-color: black;">编辑</el-button>
                <el-button
                  size="small"
                  type="danger"
                  @click="handleDelete(scope.$index, scope.row)"
                  style="background-color: #d2313c;
                         font-size: 14px;">删除</el-button>
              </template>
            </el-table-column>
          </el-table>

          <div class="add-button-container">
            <el-button
              type="primary"
              @click="handleAddRow"
              style="margin-top: 20px; font-size: 25px; font-weight: bold; background-color: #409effc7;">
              +
            </el-button>
          </div>

          <el-dialog :visible.sync="formVisible">
            <el-form  ref="form" :model="form" :rules="rules" label-width="80px">
              <el-form-item label="录入日期" prop="date1">
                <el-col :span="15">
                  <el-date-picker type="date" placeholder="选择日期" v-model="form.date1" style="width: 100%;"></el-date-picker>
                </el-col>              
              </el-form-item>
              <el-form-item label="伤员姓名" prop="name">
                <el-input v-model="form.name"></el-input>
              </el-form-item>
              <el-form-item label="伤员编号" prop="ID">
                <el-input v-model="form.ID"></el-input>
              </el-form-item>
              <el-form-item label="医疗点" prop="medicalPoint">
                <el-input v-model="form.medicalPoint"></el-input>
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="onSubmit">上传</el-button>
                <el-button @click="handleCancel">取消</el-button>
              </el-form-item>
            </el-form>
          </el-dialog>

        </div>
      </div>
    </div>
  </template>
  
  
  <script>
  
  export default {
      name:'InjuryEntry',
      data() {
        return {
          injuredPlayers: [
          { date: '2016-05-02', name: '周一', ID: '1234561', medicalPoint: '上海市普陀区金沙江路 1518 弄' }, 
          { date: '2016-05-02', name: '钱二', ID: '1234562', medicalPoint: '上海市普陀区金沙江路 1500 弄' }, 
          { date: '2016-05-02', name: '张三', ID: '1234563', medicalPoint: '上海市普陀区金沙江路 1522 弄' }, 
          { date: '2016-05-04', name: '李四', ID: '1234564', medicalPoint: '上海市普陀区金沙江路 1517 弄' }, 
          { date: '2016-05-01', name: '王五', ID: '1234565', medicalPoint: '上海市普陀区金沙江路 1519 弄' }, 
          { date: '2016-05-03', name: '赵六', ID: '1234566', medicalPoint: '上海市普陀区金沙江路 1516 弄' }, 
          { date: '2016-05-12', name: '何七', ID: '1234567', medicalPoint: '上海市普陀区金沙江路 1516 弄' }, 
          { date: '2016-05-07', name: '郑八', ID: '1234568', medicalPoint: '上海市普陀区金沙江路 1547 弄' }, 
          { date: '2016-05-03', name: '郭九', ID: '1234569', medicalPoint: '上海市普陀区金沙江路 1538 弄' }
          ],

          formVisible: false,
          editingIndex: null,
          editingMode: false,

          form: {
            date1: '',
            name: '',
            ID: '',
            medicalPoint: ''
          },

          rules: {
            name: [
              { required: true, message: '请输入伤员姓名', trigger: 'blur' }
            ],
            ID:[
              {required: true, message: '请输入伤员编号', trigger: 'blur'}
            ],
            medicalPoint:[
              {required: true,message: '请输入医疗点', trigger: 'blur'}
            ],
            date1: [
              { required: true, message: '请选择录入时间', trigger: 'change' }
            ]
          }
        }
      },
      methods:{
        navigateTo(_path) {
          this.$router.push({path:_path},()=>{})
        },
        handleEdit(index, row) {
          this.editingIndex = index;
          this.form = { ...row };
          this.editingMode = true;
          this.formVisible = true;     
        },
        handleDelete(index, row) {
          this.$confirm(`确认删除选手 ${row.name} 吗？`, '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning',
          }).then(() => {
            this.injuredPlayers.splice(index, 1);
            this.$message({
              type: 'success',
              message: '删除成功!',
            });
          }).catch(() => {
            this.$message({
              type: 'info',
              message: '已取消删除',
            });
          });
        },
        handleAddRow() {
          this.formVisible = true;
          this.resetForm();
        },
        onSubmit() {
          this.$refs.form.validate((valid) => { 
            if (valid) {
              if (this.editingMode) {
                this.injuredPlayers.splice(this.editingIndex, 1, this.form);
                this.$message.success('编辑成功');
              } else {
                this.injuredPlayers.push({
                  date: this.form.date1,
                  name: this.form.name,
                  ID: this.form.ID,
                  medicalPoint: this.form.medicalPoint
                });
                this.$message.success('上传成功');
              }
              this.formVisible = false;
              this.resetForm();
            } else {
              this.$message.error('表单验证失败');
              return false;
            }
          });
        },
        resetForm() {
          this.$refs.form.resetFields();
        },
        handleCancel() {
          this.formVisible = false;
          this.resetForm();
        }
        }
    }
  </script>
  
  
  <style scoped>
   * {
      margin: 0;
      padding: 0;
      box-sizing: border-box;
    }
    
    html, body {
      height: 100%;
    }
    
    .navbar {
      background-color: #c81623; 
      padding: 20px;
      display: flex;
      justify-content: space-around;
      position: fixed;
      top: 0;
      left: 0;
      width: 100%;
      z-index: 1000; /* 确保 navbar 在最上层 */
    }
  
    button {
      background-color: #c81623;
      color:white;
      border: none;
      padding: 10px 20px;
      cursor: pointer;
      font-size: 16px;
    }
    
    .injury-list{
      display: flex;  
      flex-direction: column;
      justify-content: center; 
      border-radius: 5px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
      margin-top: 120px;
      margin-left: 150px; 
      width: 80%;
      height: 600px;
      background-color: white;
    }
    
    .table-container{
      display: flex;
      justify-content: center; 
      border-radius: 5px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
      margin-top: 10px;
      margin-left: 50px;
      width: 90%;
      height: 470px;
      background-color: white;
      position: relative;
      padding-bottom: 60px;
    }

    .el-table{
      background-color:aliceblue;
      width: 50%; 
      max-width: 800px;
    }
    
    .add-button-container {
      position: absolute;
    bottom: 10px;
    left: 50%;
    transform: translateX(-50%);
    }
  </style>
  