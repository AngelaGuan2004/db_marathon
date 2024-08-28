<template>
  <div id="LoginAdmin">
    <div class="LoginRightIndexFormTitle">
      欢迎管理员登录
    </div>
    <div class="LoginRightIndexFormInput">
      <form @submit.prevent="LoginAdmin">
        <div style="margin-top: 100px;margin-bottom: 50px;">
          <el-input v-model="adminKey" placeholder="请输入管理秘钥"></el-input>
          <p v-if="errorMessage" style="color: red; font-size: 14px; margin-top: 10px;">
            {{ errorMessage }}
          </p>
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
export default {
  name: 'LoginAdmin',
  data() {
    return {
      adminKey: '',
      errorMessage: '', // 存储错误信息
    }
  },
  methods: {
    GoToRegister() {
      this.$router.push({ name: 'Register' })
    },
    LoginAdmin() {
      if (this.adminKey === 'jima2024') {
        localStorage.setItem('UserRole', 'Admin')
        location.href = 'index.html'
      } else {
        this.errorMessage = '密钥错误，请重新输入'
      }
    }
  }
}
</script>

<style>

</style>
