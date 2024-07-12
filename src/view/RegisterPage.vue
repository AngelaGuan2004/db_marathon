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
export default {
  name: 'RegisterPage',
  data() {
    return {
      name: '',
      phone: '',
      password: '',
      confirmPassword: '',
      gender: '',
      role: '',
      showPassword: false, // 控制密码显示/隐藏
      showConfirmPassword: false, // 控制确认密码显示/隐藏
      passwordMismatch: false, // 表示密码是否不匹配
      isValidIdNumber: true // 表示身份证号格式是否正确
    }
  },
  methods: {
    register() {
      // 检查密码是否一致
      if (this.password !== this.confirmPassword) {
        this.passwordMismatch = true;
        return;
      } else {
        this.passwordMismatch = false;
      }

      // 处理注册逻辑，例如 API 请求
      console.log('注册信息:', this.name, this.phone, this.password, this.gender, this.role);
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
.register-page {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-image: url('@/assets/background.png'); 
  background-size: cover;
  background-position: center;
}

.register-container {
  background-color: rgba(255, 255, 255, 0.8);
  padding: 20px;
  border-radius: 8px;
  text-align: center;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.logo {
  width: 180px;
  margin-bottom: 20px;
}

form {
  display: flex;
  flex-direction: column;
}

.form-group {
  display: flex;
  justify-content: space-between;
}

.form-row {
  flex: 1;
  display: flex;
  flex-direction: column;
  margin-right: 10px;
}

.form-row:last-child {
  margin-right: 0;
}

label {
  text-align: left;
  margin: 10px 0 5px;
}

.input-container {
  position: relative;
}

input, select {
  display: block;
  width: calc(100% - 20px);
  padding: 10px;
  margin: 10px 0;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.password-input {
  display: flex;
  align-items: center;
}

.password-toggle {
  cursor: pointer;
  margin-left: -30px;
}

.eye-icon {
  fill: #757575;
  width: 24px;
  height: 24px;
  cursor: pointer;
}

.error-message {
  color: red;
  font-size: 12px;
  margin-top: -10px;
}

button {
  display: block;
  width: 100%;
  padding: 10px;
  background-color: #ff4b4b;
  border: none;
  color: white;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 10px;
}

button:hover {
  background-color: #ff1a1a;
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.links {
  margin-top: 20px;
}

.links a {
  color: #007bff;
  text-decoration: none;
  margin: 0 10px;
}

.links a:hover {
  text-decoration: underline;
}
</style>
