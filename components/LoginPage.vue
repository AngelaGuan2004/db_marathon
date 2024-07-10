<!-- src/components/LoginPage.vue -->
<template>
  <div class="login-page">
    <div class="login-container">
      <img src="@/assets/jima_logo.png" alt="logo" class="logo">
      <h2>欢迎登录</h2>
      <input v-model="username" type="text" placeholder="请输入账号">
      <input v-model="password" type="password" placeholder="请输入密码">
      <button @click="login">登录</button>
      <div class="links">
        <a href="#">忘记密码</a>
        <a href="#">立即注册</a>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'LoginPage',
  data() {
    return {
      username: '',
      password: ''
    }
  },
  methods: {
    login() {
      // 处理登录逻辑，例如 API 请求
      console.log('登录:', this.username, this.password);
    }
  }
}
</script>

<style scoped>
.login-page {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-image: url('@/assets/background2.png'); 
  background-size: cover;
  background-position: center;
}

.login-container {
  background-color: rgba(255, 255, 255, 0.8);
  padding: 20px;
  border-radius: 8px;
  text-align: center;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.logo {
  width: 150px;
  margin-bottom: 20px;
}

input {
  display: block;
  width: calc(100% - 20px);
  padding: 10px;
  margin: 10px 0;
  border: 1px solid #ccc;
  border-radius: 4px;
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

