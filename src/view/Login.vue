<template>
            <el-form :model="ruleForm"  ref="ruleForm" label-width="100px" class="demo-ruleForm">
                <el-form-item label="姓名" prop="user_name" style="width:350px;">
                    <el-input v-model="ruleForm.user_name"></el-input>
                </el-form-item>
                <el-form-item label="身份证号" prop="id" style="width:350px;">
                    <el-input type="password" v-model="ruleForm.id" autocomplete="off"></el-input>
                </el-form-item>
                <el-form-item label="密码" prop="password" style="width:350px;">
                    <el-input type="password" v-model="ruleForm.password" autocomplete="off"></el-input>
                </el-form-item>
                 <el-form-item>
                    <el-button type="primary" @click="submitForm('ruleForm')">提交</el-button>
                </el-form-item>
            </el-form>

</template>

<script>
import { adduser } from '@/api/login.js'
export default {
    name: "AddUser",
    data() {
        return {
            ruleForm: {
                user_name: '',
                id: '',
                password: ''
            }  
        };
    },
    methods: {
        submitForm(formName){
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    console.log('submit!');
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
            let vm = this; 
            let param = {
                user_name: this.ruleForm.user_name,
                id: this.ruleForm.id,
                password: this.ruleForm.passowrd 

            };   
            adduser(param).then(function (res) {
                if (res.data === false) {
                    vm.$message.error("提交失败");
                    vm.resetForm(formName);
                } else {
                    vm.$message.success("提交成功");
                    //vm.$router.push({ path: "/login" }); //接下来进入到哪个路由
                }
            })   
        }
    }
}


</script>