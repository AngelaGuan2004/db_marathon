<template>
  <div id="LoginAdmin">
    <div class="LoginRightIndexFormTitle">
      欢迎管理员登录
    </div>
    <div class="LoginRightIndexFormInput">
      <form action="">
        <div style="margin-top: 100px;margin-bottom: 50px;">
          <el-input v-model="adminKey" placeholder="请输入管理秘钥"></el-input>
        </div>
        <div>
          <button class="LoginRightIndexFormButton" @click="LoginAdmin">
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
    }
  },
  methods: {
    GoToRegister() {
      this.$router.push({ name: 'Register' })
    },
    LoginAdmin() {
      localStorage.setItem('UserRole', 'Admin')
      location.href = 'index.html'
    }
  }
}
</script>

<style></style>