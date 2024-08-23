<template>
  <div id="Register">
    <div class="LoginRightIndexFormTitle">
      欢迎注册
    </div>
    <div class="LoginRightIndexFormInput">
      <form action="">
        <div style="margin-top: 75px;">
          <el-input v-model="name" placeholder="请输入姓名"></el-input>
        </div>
        <div style="margin-top: 25px; margin-bottom: 25px;">
          <el-input v-model="idNumber" placeholder="请输入身份证号"></el-input>
        </div>
        <div style="margin-top: 25px; margin-bottom: 25px;">
          <el-input v-model="password" placeholder="请输入密码" show-password></el-input>
        </div>
        <div style="margin-top: 25px; margin-bottom: 25px;">
          <el-input v-model="confirmPassword" placeholder="请确认输入密码" show-password></el-input>
        </div>
        <div class="RegistGender">
          <div style="display:flex;margin-left: 15px;font-size: 14px;">
            请选择性别：
          </div>
          <div style="display:flex;">
            <el-radio v-model="gender" label="1" style="float: left;margin-left: 25px;">男</el-radio>
            <el-radio v-model="gender" label="2" style="float: right;margin-right: 25px;">女</el-radio>
          </div>
        </div>
        <div class="RegisterReturn">
          <div class="RegisterReturnLogin" @click="ReturnLogin">
            返回登录
          </div>
          <div style="display: flex;width: 75%;">
            <button class="LoginRightIndexFormButton">
              <span>注册</span>
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Register',
  data() {
    return {
      name: '',
      idNumber: '',
      password: '',
      confirmPassword: '',
      gender: '',
      role: '',
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
    GoToIndex() {
      location.href = "index.html"
    },
    ReturnLogin() {
      this.$router.back()
    }
  }
}
</script>

<style>
.el-radio {
  color: black;
  font-size: 18px
}

.el-radio__input.is-checked .el-radio__inner {
  border-color: rgb(217, 48, 37);
  background: rgb(217, 48, 37);

}

.el-radio__input.is-checked+.el-radio__label {
  color: rgb(217, 48, 37);
}

.RegistGender {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-start;
  margin-top: 25px;
  height: 20px;
}

.RegisterReturn {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-start;
}

.RegisterReturnLogin {
  font-size: 14px;
  display: flex;
  color: rgb(225, 89, 81);
  margin: 40px 30px 0 10px;
  font-weight: bold;
  cursor: pointer;
}
</style>
