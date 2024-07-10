<template>
  <div class="register-page">
    <div class="register-container">
      <img src="@/assets/jima_logo.png" alt="logo" class="logo">
      <h2>欢迎注册</h2>
      <form @submit.prevent="register">
        <div class="form-group">
          <div class="form-row">
            <label>身份</label>
            <select v-model="role">
              <option value="admin">管理员</option>
              <option value="athlete">选手</option>
              <option value="photographer">摄影师</option>
            </select>
          </div>
        </div>
        <div class="form-group">
          <div class="form-row">
            <label>姓名</label>
            <input v-model="name" type="text" placeholder="请输入姓名">
          </div>
          <div class="form-row">
            <label>手机号</label>
            <input v-model="phone" type="text" placeholder="请输入手机号">
          </div>
        </div>
        <div class="form-group">
          <div class="form-row">
            <label>身份证号</label>
            <input v-model="idNumber" type="text" placeholder="请输入身份证号">
          </div>
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
        <a href="#">已有账号？请登录</a>
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
      idNumber: '',
      gender: '',
      role: ''
    }
  },
  methods: {
    register() {
      // 处理注册逻辑，例如 API 请求
      console.log('注册信息:', this.name, this.phone, this.idNumber, this.region, this.gender, this.role);
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
  width: 150px;
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

input, select {
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
