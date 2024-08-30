<template>
  <div id="Register">
    <div class="LoginRightIndexFormTitle">
      欢迎注册
    </div>
    <div class="LoginRightIndexFormInput">
      <form @submit.prevent="register">
        <div style="margin-top: 50px;">
          <el-input v-model="name" placeholder="请输入姓名"></el-input>
        </div>
        <div style="margin-top: 25px; margin-bottom: 25px;">
          <el-input v-model="idNumber" placeholder="请输入身份证号" @input="validateIdNumber"></el-input>
          <div v-if="idNumberError" class="error-message">{{ idNumberError }}</div>
        </div>
        <div style="margin-top: 25px; margin-bottom: 25px;">
          <el-input v-model="password" placeholder="请输入密码" show-password></el-input>
        </div>
        <div style="margin-top: 25px; margin-bottom: 30px;">
          <el-input v-model="confirmPassword" placeholder="请确认输入密码" show-password @input="validatePassword"></el-input>
          <div v-if="passwordMismatch" class="error-message">密码与确认密码不一致</div>
        </div>
        <div class="RegistGender">
          <div style="display:flex;margin-left: 15px;font-size: 16px;">
            请选择性别：
          </div>
          <div style="display:flex;margin-left: 20px;">
            <el-radio v-model="gender" label="1" style="float: left;margin-left: 25px;">男</el-radio>
            <el-radio v-model="gender" label="2" style="float: right;margin-right: 25px;">女</el-radio>
          </div>
        </div>
        <div class="RegisterReturn">
          <div class="RegisterReturnLogin" @click="ReturnLogin">
            返回登录
          </div>
          <div style="display: inline-block;width: 75%;margin-top: 7%;
          text-align: right;">
            <button class="LoginRightIndexFormButton" @click.prevent="register" style="width: 80%;">
              <span>注册</span>
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { registerPlayer } from '@/api/player';

export default {
  name: 'Register',
  data() {
    return {
      name: '',
      idNumber: '',
      password: '',
      confirmPassword: '',
      gender: '',
      age: null,
      passwordMismatch: false,
      idNumberError: '' // 表示身份证号是否有错误
    }
  },
  methods: {
    validateIdNumber() {
      const idNumberPattern = /^[1-9]\d{5}(19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}(\d|X)$/;
      if (!idNumberPattern.test(this.idNumber)) {
        this.idNumberError = '身份证格式不正确';
        setTimeout(() => {
          this.idNumberError = ''
        }, 1000)
        return;
      }
      this.idNumberError = '';
      this.calculateAge();
    },
    validatePassword() {
      if (this.password !== this.confirmPassword) {
        this.passwordMismatch = true;
      } else {
        this.passwordMismatch = false;
      }
    },
    calculateAge() {
      const currentYear = 2024;
      const birthYear = parseInt(this.idNumber.slice(6, 10));
      if (!isNaN(birthYear)) {
        this.age = currentYear - birthYear;
      }
    },
    register() {
      if (this.passwordMismatch || this.idNumberError) {
        this.$message.warning('输入数据有误，请修改');
        return; // 如果有错误，不进行注册
      }

      const data = {
        Id: 645132,
        Name: this.name,
        Gender: this.gender,
        Age: this.age,
        Id_Number: this.idNumber,
        Password: this.password,
        Region: '',
        Telephone_Number: '',
      };

      registerPlayer(data).then((response) => {
        this.$message.success('注册成功');
        setTimeout(() => {
          location.href = 'index.html'
        }, 1000)
      }).catch(error => {
        console.error(error)
        this.$message.error('注册失败，请重试');
      })
    },

    ReturnLogin() {
      this.$router.back();
    }
  }
}
</script>

<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";
@import "@/assets/css/Register.css";
</style>
