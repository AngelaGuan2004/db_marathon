<template>
  <div id="UserVolunteerSignup">
    <div class="RegistrationContainerWrapper">
      <!-- 添加 logo 图片 -->
      <img class="UserVolunteerSignupLogo" src="../assets/images/JiMa.png" alt="Logo">
      <div class="RegistrationContainer">
        <h2>志愿者报名</h2>
        <el-form :model="volunteer">
          <el-form-item label="姓名：">
            <el-input v-model="volunteer.name" placeholder="请输入姓名"></el-input>
          </el-form-item>
          <el-form-item label="电话：">
            <el-input v-model="volunteer.phone" placeholder="请输入手机号"></el-input>
          </el-form-item>
        </el-form>
        <div class="button-group">
          <el-button type="primary" @click="modifyInfo">修改信息</el-button>
          <el-button type="success" @click="submitRegistration">提交报名</el-button>
        </div>
      </div>
    </div>
  </div>

</template>

<script>
import { registerVolunteer } from '@/api/volunteer';
/* 从 API 工具文件中引入了 registerVolunteer 函数 */

export default {
  name: 'UserVolunteerSignup',
  data() {
    return {
      volunteer: {
        name: '123',
        phone: '456'
      }
    };
  },
  mounted() {
    // this.loadVolunteerInfo();
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

<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "../assets/css/Base.css";
@import "../assets/css/UserVolunteerSignup.css";
</style>
