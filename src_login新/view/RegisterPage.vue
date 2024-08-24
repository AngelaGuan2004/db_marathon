<template>
  <div class="register-page">
    <div class="register-container">
      <img src="@/assets/jima_logo.png" alt="logo" class="logo">
      <h2>欢迎注册</h2>
      <form @submit.prevent="register">
        <div class="form-group">
          <div class="form-row">
            <label>姓名</label>
            <input v-model="name" type="text" placeholder="请输入姓名">
          </div>
          <div class="form-row">
            <label>身份证号</label>
            <div class="input-container">
              <input v-model="phone" type="text" placeholder="请输入身份证号" @input="validateIdNumber">
              <p v-if="phone && !isValidIdNumber" class="error-message">身份证格式不正确！</p>
            </div>
          </div>
        </div>
        <div class="form-group">
          <div class="form-row">
            <label>密码</label>
            <div class="password-input">
              <input v-model="password" :type="showPassword ? 'text' : 'password'" placeholder="请输入密码" @input="validatePasswordMatch">
              <span class="password-toggle" @click="togglePasswordVisibility('password')">
                <img v-if="showPassword" src="@/assets/eye-open.png" class="eye-icon" alt="显示密码">
                <img v-else src="@/assets/eye-closed.png" class="eye-icon" alt="隐藏密码">
              </span>
            </div>
          </div>
          <div class="form-row">
            <label>确认密码</label>
            <div class="password-input">
              <input v-model="confirmPassword" :type="showConfirmPassword ? 'text' : 'password'" placeholder="请确认密码" @input="validatePasswordMatch">
              <span class="password-toggle" @click="togglePasswordVisibility('confirmPassword')">
                <img v-if="showConfirmPassword" src="@/assets/eye-open.png" class="eye-icon" alt="显示密码">
                <img v-else src="@/assets/eye-closed.png" class="eye-icon" alt="隐藏密码">
              </span>
            </div>
            <p v-if="confirmPassword && passwordMismatch" class="error-message">确认密码与密码不一致！</p>
          </div>
        </div>
        <div class="form-group">
          <div class="form-row">
            <label>性别</label>
            <select v-model="gender">
              <option value="male">男</option>
              <option value="female">女</option>
            </select>
          </div>
        </div>
        <button type="submit" :disabled="!isValidIdNumber || passwordMismatch">注册</button>
      </form>
      <div class="links">
        <a href="#" @click.prevent="goToLogin">已有账号？请登录</a>
      </div>
    </div>
  </div>
</template>

<script>
import { registerPlayer } from '@/api/player';//导入 player.js 中的 registerPlayer 方法

export default {
  name: 'RegisterPage',
  data() {
    return {
      name: '',
      phone: '',
      password: '',
      confirmPassword: '',
      gender: '',
      showPassword: false,
      showConfirmPassword: false,
      passwordMismatch: false,
      isValidIdNumber: true
    }
  },
  methods: {
    async register() {
      // 检查密码是否一致
      if (this.password !== this.confirmPassword) {
        this.passwordMismatch = true;
        return;
      } else {
        this.passwordMismatch = false;
      }

      // 发送注册请求
      try {
        const response = await registerPlayer({
          name: this.name,
          idNumber: this.phone,
          password: this.password,
          gender: this.gender
        });
        
        // 根据需要处理响应结果
        console.log('注册成功:', response.data);
        this.$router.push({ name: 'LoginPage' });
      } catch (error) {
        console.error('注册失败:', error);
        // 根据需要处理错误
      }
    },
    goToLogin() {
      this.$router.push({ name: 'LoginPage' });
    },
    togglePasswordVisibility(field) {
      if (field === 'password') {
        this.showPassword = !this.showPassword;
      } else if (field === 'confirmPassword') {
        this.showConfirmPassword = !this.showConfirmPassword;
      }
    },
    validateIdNumber() {
      const regex = /^[1-9]\d{5}(19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}(\d|X)$/i;
      this.isValidIdNumber = regex.test(this.phone);
    },
    validatePasswordMatch() {
      this.passwordMismatch = this.password !== this.confirmPassword;
    }
  }
}
</script>

<style scoped>
/* 保持原有样式不变 */
</style>
