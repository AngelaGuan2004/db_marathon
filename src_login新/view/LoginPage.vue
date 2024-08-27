<!-- src/components/LoginPage.vue -->
<template>
  <div class="register-page">
    <div class="register-container">
      <img src="@/assets/jima_logo.png" alt="logo" class="logo">
      <h2>欢迎登录</h2>
      <form @submit.prevent="handleLogin">
        <div class="form-group">
          <div class="form-row">
            <label>身份</label>
            <select v-model="role" class="form-control">
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
              <input v-model="idNumber" type="text" placeholder="请输入身份证号" class="form-control">
              <small v-if="idNumber && !isValidIdNumber" class="error-message">身份证格式不正确！</small>
            </div>
            <div class="form-row">
              <label>密码</label>
              <div class="password-wrapper">
                <input v-model="athletePassword" :type="showAthletePassword ? 'text' : 'password'" placeholder="请输入密码" class="form-control password-input">
                <span class="password-toggle" @click="togglePasswordVisibility('athletePassword')">
                  <img :src="showAthletePassword ? require('@/assets/eye-open.png') : require('@/assets/eye-closed.png')" class="eye-icon" alt="显示密码">
                </span>
              </div>
            </div>
          </div>
        </div>

        <div v-if="role === 'photographer'">
          <div class="form-group">
            <div class="form-row">
              <label>姓名</label>
              <input v-model="name" type="text" placeholder="请输入姓名" class="form-control">
            </div>
            <div class="form-row">
              <label>密码</label>
              <div class="password-wrapper">
                <input v-model="photoPassword" :type="showPhotoPassword ? 'text' : 'password'" placeholder="请输入密码" class="form-control password-input">
                <span class="password-toggle" @click="togglePasswordVisibility('photoPassword')">
                  <img :src="showPhotoPassword ? require('@/assets/eye-open.png') : require('@/assets/eye-closed.png')" class="eye-icon" alt="显示密码">
                </span>
              </div>
            </div>
          </div>
        </div>

        <div v-if="role === 'admin'">
          <div class="form-group">
            <div class="form-row">
              <label>管理密钥</label>
              <div class="password-wrapper">
                <input v-model="adminKey" :type="showAdminKey ? 'text' : 'password'" placeholder="请输入管理密钥" class="form-control password-input">
                <span class="password-toggle" @click="togglePasswordVisibility('adminKey')">
                  <img :src="showAdminKey ? require('@/assets/eye-open.png') : require('@/assets/eye-closed.png')" class="eye-icon" alt="显示管理密钥">
                </span>
              </div>
            </div>
          </div>
        </div>

        <button type="submit" :disabled="!isFormValid">登录</button>
      </form>
      <div class="links">
        <a href="#" @click.prevent="goToRegister">没有账号，立刻注册</a>
      </div>
    </div>
  </div>
</template>

<script>
import { loginPlayer, loginPhotographer, loginVolunteer } from '@/utils/player';

export default {
  name: 'RegisterPage',
  data() {
    return {
      name: '',
      idNumber: '',
      athletePassword: '',
      photoPassword: '',
      adminKey: '',
      role: '',
      showAthletePassword: false,
      showPhotoPassword: false,
      showAdminKey: false
    }
  },
  computed: {
    isValidIdNumber() {
      const regex = /^[1-9]\d{5}(19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}(\d|X)$/i;
      return regex.test(this.idNumber);
    },
    isFormValid() {
      if (this.role === 'athlete') {
        return this.idNumber && this.isValidIdNumber && this.athletePassword;
      } else if (this.role === 'photographer') {
        return this.name && this.photoPassword;
      } else if (this.role === 'admin') {
        return this.adminKey;
      } else {
        return false;
      }
    }
  },
  methods: {
    async handleLogin() {
      try {
        let response;

        if (this.role === 'athlete') {
          response = await loginPlayer({ name: this.name, password: this.athletePassword, idNumber: this.idNumber });
        } else if (this.role === 'photographer') {
          response = await loginPhotographer({ name: this.name, password: this.photoPassword });
        } else if (this.role === 'admin') {
          // Admin login logic can be added here
          alert('管理员登录逻辑尚未实现');
          return;
        }

        if (response) {
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
        }
      } catch (error) {
        console.error('登录失败:', error);
        alert('登录失败，请检查输入信息');
      }
    },
    goToRegister() {
      if (this.$route.name !== 'RegisterPage') {
        this.$router.push({ name: 'RegisterPage' });
      }
    },
    togglePasswordVisibility(field) {
      if (field === 'athletePassword') {
        this.showAthletePassword = !this.showAthletePassword;
      } else if (field === 'photoPassword') {
        this.showPhotoPassword = !this.showPhotoPassword;
      } else if (field === 'adminKey') {
        this.showAdminKey = !this.showAdminKey;
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

.password-wrapper {
  position: relative;
}

.form-control {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
}

.password-input {
  width: 100%;
  padding-right: 40px; /* 为图标预留空间 */
}

.password-toggle {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
  cursor: pointer;
}

.eye-icon {
  width: 24px;
  height: 24px;
}

label {
  text-align: left;
  margin-bottom: 5px;
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

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

button:hover:not(:disabled) {
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

.error-message {
  color: red;
  font-size: 12px;
  text-align: left;
  margin-top: 0px;
}
</style>
