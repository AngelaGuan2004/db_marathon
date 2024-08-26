<template>
  <div id="LoginPhotographer">
    <div class="LoginRightIndexFormTitle">
      欢迎摄影师登录
    </div>
    <div class="LoginRightIndexFormInput">
      <form action="">
        <div style="margin-top: 100px;">
          <el-input v-model="name" placeholder="请输入姓名"></el-input>
        </div>
        <div style="margin-top: 25px; margin-bottom: 50px;">
          <el-input v-model="password" placeholder="请输入密码" show-password></el-input>
        </div>
        <div>
          <button class="LoginRightIndexFormButton" @click="LoginPhotographer">
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
  name: 'LoginPhotographer',
  data() {
    return {
      name: '',
      password: '',
    }
  },
  methods: {
    GoToRegister() {
      this.$router.push({ name: 'Register' })
    },
    LoginPhotographer() {
      localStorage.setItem('UserRole', 'Photographer')
      location.href = 'index.html'
    }
  }
}
</script>

<style></style>