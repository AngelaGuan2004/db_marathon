<template>
  <div id="LoginAthlete">
    <div class="LoginRightIndexFormTitle">
      欢迎选手登录
    </div>
    <div class="LoginRightIndexFormInput">
      <form action="">
        <div style="margin-top: 100px;">
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
export default {
  name: 'LoginAthlete',
  data() {
    return {
      idNumber: '',
      password: '',
    }
  },
  methods: {
    GoToRegister() {
      this.$router.push({ name: 'Register' })
    }
  }
}
</script>

<style></style>