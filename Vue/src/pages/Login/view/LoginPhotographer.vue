<template>
  <div id="LoginPhotographer">
    <div class="LoginRightIndexFormTitle">
      欢迎摄影师登录
    </div>
    <div class="LoginRightIndexFormInput">
      <form @submit.prevent="LoginPhotographer">
        <div style="margin-top: 50px;text-align: center;">
          <el-input v-model="name" placeholder="请输入姓名"></el-input>
        </div>
        <div style="margin-top: 25px; margin-bottom: 50px;text-align: center;">
          <el-input v-model="password" placeholder="请输入密码" show-password></el-input>
        </div>
        <div>
          <span style="margin-left: 12%;width: 73%;text-align: center;display: inline-block;">
            <button class="LoginRightIndexFormButton">
              <span>登录</span>
            </button>
          </span>
          <span class="LoginRightIndexRegister" @click="GoToRegister">立即注册 </span>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { loginPhotographer } from '@/api/player';

export default {
  name: 'LoginPhotographer',
  data() {
    return {
      name: '',
      password: '',
      errorMessage: '' // 用于显示错误信息
    }
  },
  methods: {
    GoToRegister() {
      this.$router.push({ name: 'Register' })
    },
    async LoginPhotographer() {
      // 组织要发送的数据
      const data = {
        Id: 1, // 固定的 ID 值
        Name: this.name,
        Password: this.password
      };

      // 调用API进行登录
      try {
        const response = await loginPhotographer(data);
        if (response.data) {
          this.$message.success('登录成功！')
          localStorage.setItem('UserRole', 'Photographer');
          setTimeout(() => {
            location.href = 'index.html'; // 登录成功后跳转到首页
          }, 1000)
        }
      } catch (error) {
        this.errorMessage = '登录失败，请重试'; // 处理登录失败情况
        this.$message.error(this.errorMessage);
      }
    }
  }
}
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import "@/assets/css/LoginTab.css";
</style>