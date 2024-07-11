<!-- src/components/LoginPage.vue -->
<template>
  <div class="register-page">
    <div class="register-container">
      <img src="@/assets/jima_logo.png" alt="logo" class="logo">
      <h2>欢迎登录</h2>
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

        <div v-if="role === 'athlete'">
          <div class="form-group">
            <div class="form-row">
              <label>身份证号</label>
              <input v-model="idNumber" type="text" placeholder="请输入身份证号">
            </div>
            <div class="form-row">
              <label>密码</label>
              <input v-model="password" type="password" placeholder="请输入密码">
            </div>
          </div>
        </div>

        <div v-if="role === 'photographer'">
          <div class="form-group">
            <div class="form-row">
              <label>姓名</label>
              <input v-model="name" type="text" placeholder="请输入姓名">
            </div>
            <div class="form-row">
              <label>密码</label>
              <input v-model="password" type="password" placeholder="请输入密码">
            </div>
          </div>
        </div>

        <div v-if="role === 'admin'">
          <div class="form-group">
            <div class="form-row">
              <label>管理密钥</label>
              <input v-model="adminKey" type="text" placeholder="请输入管理密钥">
            </div>
          </div>
        </div>

        <button type="submit">注册</button>
      </form>
      <div class="links">
        <a href="#" @click.prevent="goToRegister">没有账号，立刻注册</a>
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
      password: '',
      adminKey: '',
      role: ''
    }
  },
  methods: {
    register() {
      // 处理注册逻辑，例如 API 请求
      console.log('注册信息:', this.name, this.phone, this.idNumber, this.password, this.adminKey, this.role);
      // 根据身份进行跳转
      switch (this.role) {
        case 'admin':
          this.$router.push({ name: 'ManagerPage' });
          break;
        case 'athlete':
          this.$router.push({ name: 'AthletePage' });
          break;
        case 'photographer':
          this.$router.push({ name: 'PhotographerPage' });
          break;
      }
    },
    goToRegister() {
      this.$router.push({ name: 'RegisterPage' });
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
  background-image: url('@/assets/background2.png'); 
  background-size: cover;
  background-position: center;
}

.register-container {
  background-color: rgba(255, 255, 255, 0.8);
  padding: 50px;
  border-radius: 8px;
  text-align: center;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.logo {
  width: 170px;
  margin-bottom: 20px;
}

form {
  display: flex;
  flex-direction: column;
}

.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
}

.form-row {
  display: flex;
  flex-direction: column;
  margin-bottom: 10px;
}

label {
  text-align: left;
  margin-bottom: 5px;
}

input {
  display: block;
  width: 90%;
  padding: 10px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
}
select {
  display: block;
  width: 100%;
  padding: 10px;
  margin-bottom: 10px;
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
}

.links a:hover {
  text-decoration: underline;
}
</style>
