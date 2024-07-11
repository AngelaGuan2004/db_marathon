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
            <input v-model="phone" type="text" placeholder="请输入身份证号">
          </div>
        </div>
        <div class="form-group">
          <div class="form-row">
            <label>密码</label>
            <div class="password-input">
              <input v-model="password" :type="showPassword ? 'text' : 'password'" placeholder="请输入密码">
              <span class="password-toggle" @click="togglePasswordVisibility('password')">
                <svg v-if="showPassword" xmlns="http://www.w3.org/2000/svg" class="eye-icon" viewBox="0 0 24 24" width="24" height="24"><path d="M12 4c-4.43 0-8 5.37-8 8s3.57 8 8 8 8-5.37 8-8-3.57-8-8-8zm0 14c-2.21 0-4-2.69-4-4s1.79-4 4-4 4 2.69 4 4-1.79 4-4 4zm0-7c-1.1 0-2 .9-2 2s.9 2 2 2 2-.9 2-2-.9-2-2-2z"/><path d="M0 0h24v24H0z" fill="none"/></svg>
                <svg v-else xmlns="http://www.w3.org/2000/svg" class="eye-icon" viewBox="0 0 24 24" width="24" height="24"><path d="M12 6a10 10 0 0 0-9.85 8A10 10 0 0 0 12 18c2.53 0 4.82-.94 6.6-2.49l-1.4-1.4A7.936 7.936 0 0 1 12 16c-2.21 0-4-1.79-4-4 0-.79.28-1.51.74-2.08l-1.4-1.4A7.963 7.963 0 0 0 4 12c0 4.42 3.58 8 8 8s8-3.58 8-8-3.58-8-8-8z"/><path d="M0 0h24v24H0z" fill="none"/></svg>
              </span>
            </div>
          </div>
          <div class="form-row">
            <label>确认密码</label>
            <div class="password-input">
              <input v-model="confirmPassword" :type="showConfirmPassword ? 'text' : 'password'" placeholder="请确认密码">
              <span class="password-toggle" @click="togglePasswordVisibility('confirmPassword')">
                <svg v-if="showConfirmPassword" xmlns="http://www.w3.org/2000/svg" class="eye-icon" viewBox="0 0 24 24" width="24" height="24"><path d="M12 4c-4.43 0-8 5.37-8 8s3.57 8 8 8 8-5.37 8-8-3.57-8-8-8zm0 14c-2.21 0-4-2.69-4-4s1.79-4 4-4 4 2.69 4 4-1.79 4-4 4zm0-7c-1.1 0-2 .9-2 2s.9 2 2 2 2-.9 2-2-.9-2-2-2z"/><path d="M0 0h24v24H0z" fill="none"/></svg>
                <svg v-else xmlns="http://www.w3.org/2000/svg" class="eye-icon" viewBox="0 0 24 24" width="24" height="24"><path d="M12 6a10 10 0 0 0-9.85 8A10 10 0 0 0 12 18c2.53 0 4.82-.94 6.6-2.49l-1.4-1.4A7.936 7.936 0 0 1 12 16c-2.21 0-4-1.79-4-4 0-.79.28-1.51.74-2.08l-1.4-1.4A7.963 7.963 0 0 0 4 12c0 4.42 3.58 8 8 8s8-3.58 8-8-3.58-8-8-8z"/><path d="M0 0h24v24H0z" fill="none"/></svg>
              </span>
            </div>
            <p v-if="passwordMismatch" class="error-message">确认密码与密码不符！</p>
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
        <button type="submit">注册</button>
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
      passwordMismatch: false // 表示密码是否不匹配
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
  position: relative;
}

.password-toggle {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
  cursor: pointer;
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
