<template>

    <div class="background-image">
        <div class="content">
            很抱歉，您还不是志愿者，
            <a @click="register" class="button">点此注册</a>
            成为志愿者。
        </div>
    </div>

</template>

<script>
    export default{
        name:'NotVolunteer',
        data() {
            return {
            }
        },
        methods:{
            //进行页面跳转的函数
            register() {
                window.alert("跳转");
            }
        },
    }
</script>

<style scoped>
.background-image {
  background-image: url('@/assets/AdwocateVolunteering.jpg');
  background-size: cover;
  background-repeat: no-repeat;
  min-height: 100vh;
  width: 100%;
}
.button {
  color: red; 
}
</style>
