<template>
  <div id="LoginAthlete">
    <div class="LoginRightIndexFormTitle">
      欢迎选手登录
    </div>
    <div class="LoginRightIndexFormInput">
      <form @submit.prevent="LoginAthlete">
        <div style="margin-top: 50px;">
          <el-input v-model="name" placeholder="请输入姓名"></el-input>
        </div>
        <div style="margin-top: 25px;">
          <el-input v-model="idNumber" placeholder="请输入身份证号"></el-input>
        </div>
        <div style="margin-top: 25px; margin-bottom: 50px;">
          <el-input v-model="password" placeholder="请输入密码" show-password></el-input>
        </div>
        <div>
          <button class="LoginRightIndexFormButton">
            <span>登录</span>
          </button>
        </div>
      </form>
    </div>
    <div class="LoginRightIndexRegister" @click="GoToRegister">
      立即注册
    </div>
  </div>
</template>

<script>
import { loginPlayer } from '@/api/player'; 

export default {
  name: 'LoginAthlete',
  data() {
    return {
      Id: 1,
      name: '',
      idNumber: '',
      password: '',
      errorMessage: '' // 用于显示错误信息
    }
  },
  methods: {
    GoToRegister() {
      this.$router.push({ name: 'Register' })
    },
    async LoginAthlete() {
      // 组织要发送的数据
      const data = {
        Name: this.name,
        Id_Number: this.idNumber,
        Password: this.password
      };

      // 调用API进行登录
      try {
        const response = await loginPlayer(data);
        if (response.data) {
          localStorage.setItem('UserRole', 'Athlete');
          location.href = 'index.html'; // 登录成功后跳转到首页
        }
      } catch (error) {
        this.errorMessage = '登录失败，请检查用户名、身份证号和密码';
      }
    }
  }
}
</script>

<style>
</style>

