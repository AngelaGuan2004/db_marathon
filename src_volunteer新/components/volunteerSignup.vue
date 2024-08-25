<template>
  <div class="registration-container-wrapper">
    <!-- 添加 logo 图片 -->
    <img class="logo" src="@/assets/jima_logo.png" alt="Logo">
    <div class="registration-container">
      <h2>志愿者报名</h2>
      <el-form :model="volunteer" label-width="120px">
        <el-form-item label="姓名">
          <el-input v-model="volunteer.name"></el-input>
        </el-form-item>
        <el-form-item label="手机号">
          <el-input v-model="volunteer.phone"></el-input>
        </el-form-item>
      </el-form>
      <div class="button-group">
        <el-button type="primary" @click="modifyInfo">修改信息</el-button>
        <el-button type="success" @click="submitRegistration">提交报名</el-button>
      </div>
    </div>
  </div>
</template>

<script>
import { registerVolunteer } from '@/api/volunteer';
/* 从 API 工具文件中引入了 registerVolunteer 函数 */

export default {
  data() {
    return {
      volunteer: {
        name: '',
        phone: ''
      }
    };
  },
  created() {
    this.loadVolunteerInfo();
  },
  methods: {
    loadVolunteerInfo() {
      const volunteerId = this.$route.params.id;
      this.$http.get(`/api/volunteer/${volunteerId}`)
        .then(response => {
          this.volunteer = response.data;
        })
        .catch(error => {
          console.error('Error loading volunteer info:', error);
          this.$message.error('加载志愿者信息失败');
        });
    },
    submitRegistration() {
      registerVolunteer(this.volunteer)
        .then(() => {
          this.$message.success('报名成功');
        })
        .catch(error => {
          console.error('Error submitting registration:', error);
          this.$message.error('报名失败');
        });
    },
    modifyInfo() {
      // Modify volunteer information logic here
    }
  }
};
</script>

<style>
.registration-container-wrapper {
  text-align: center; 
  padding: 20px; 
}

.logo {
  display: block;
  margin: 0 auto 20px; 
  max-width: 300px;
  height: auto; 
}

.registration-container {
  max-width: 600px;
  margin: 0 auto;
  padding: 30px;
  background-color: rgba(255, 255, 255, 0.9); 
  border: 1px solid grey;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.button-group {
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
}

.el-button {
  background-color: #c63939 !important; 
  /*important是为了覆盖原有的Element UI 的按钮组件有默认的样式优先级*/
  color: white; 
  border: none !important; /* 去掉按钮边框 */ 
}

.el-button--primary {
  background-color: #c63939 !important; 
}

.el-button--success {
  background-color: #c63939 !important; 
}

.el-button:hover {
  background-color: #a62828 !important; /* 悬停时的深红色背景 */
}
</style>
