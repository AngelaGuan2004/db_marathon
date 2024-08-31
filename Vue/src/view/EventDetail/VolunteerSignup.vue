<template>
  <div id="VolunteerSignup">
    <el-dialog title="志愿者登录" :visible.sync="dialogVisible" width="55%">
      <div class="RegistrationContainerWrapper">
        <!-- 添加 logo 图片 -->
        <img class="VolunteerSignupLogo" src="@/assets/images/JiMa.png" alt="Logo">
        <div class="RegistrationContainer">
          <h1 style="margin-bottom: 30px;">志愿者登录</h1>
          <div class="RegistrationInformation">
            <div><b> 姓名：</b><span> {{ volunteer.name }}</span></div>
            <div><b style="margin-right: 5px;"> 手机号：</b><span>{{ volunteer.phone }}</span> </div>
          </div>
          <div class="button-group">
            <el-button type="primary" @click="modifyInfo">修改信息</el-button>
            <el-button type="success" @click="submitRegistration">提交报名</el-button>
          </div>
        </div>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { registerVolunteer } from '@/api/volunteer';
/* 从 API 工具文件中引入了 registerVolunteer 函数 */

export default {
  name: 'VolunteerSignup',
  data() {
    return {
      volunteer: {
        name: '王中凯',
        phone: '13045395263'
      },
      dialogVisible: true
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
      this.$router.push({ name: 'UserTab' });
    }
  }
};
</script>

<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";
@import "@/assets/css/VolunteerSignup.css";
</style>
