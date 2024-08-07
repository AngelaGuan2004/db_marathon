<template>
  <div>
    <nav class="navbar">
      <img src="@/assets/logo.png" alt="Logo" class="logo" style="height: 55px;">
      <button @click="navigateTo('/')">首页</button>
      <button @click="navigateTo('/profile')">个人空间</button>
      <button @click="navigateTo('/profile')">赛事摄影</button>
      <!--select id="Photograph" v-model="subWeb" class="styled-select">
        <option value="PhotoWall" >照片墙</option>
        <option value="PhotographerCenter">摄影师中心</option>
      </select-->
    </nav>       
    
    <div class="main-content">
      <div class="sidebar">
        <button @click="navigateTo('/photoWall')" class="sidebar-button">照片墙</button>
        <button @click="navigateTo('/photographerCenter')" class="sidebar-button" style="font-weight:bold;">摄影师中心</button>
      </div>
      
      <div class="content">
        <div class="titlebar">
          <button class="back-button" @click="navigateTo('/photographerCenter')">‹</button>
          <h1 style="text-align: left; color: #969696;margin-top: 10px;margin-left:10px">我的摄影作品</h1>
        </div>
      </div>

    </div>


  </div>
</template>


<script>

export default {
    name:'MyPhotographyWorks',
    methods:{
      navigateTo(_path) {
        this.$router.push({path:_path},()=>{})
      },
    }
}
</script>


<style scoped>
 * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  
  html, body {
    height: 100%;
  }
  
  .navbar {
    background-color: #c81623; 
    padding: 20px;
    display: flex;
    justify-content: space-around;
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    z-index: 1000; /* 确保 navbar 在最上层 */
  }

  button {
    background-color: #c81623;
    color:white;
    border: none;
    padding: 10px 20px;
    cursor: pointer;
    font-size: 16px;
  }

  .main-content {
    display: flex;
    margin-top: 80px;
  }
  .sidebar {
    width: 200px;
    height:100vh;
    position:fixed;
    background-color: #ffebeb;
    padding: 20px;
    box-shadow: 2px 0 5px rgba(0,0,0,0.1);
  }
  
  .sidebar-button {
    width: 100%;  background-color: #ffebeb; 
    color: #565656; 
    border: none;
    padding: 10px;
    cursor: pointer;
    font-size: 18px;
    margin-bottom: 10px;
    margin-top: 20px;
    border-radius: 5px; 
  }
  
  .sidebar-button:hover {
    background-color: #ffd7d7;
    font-weight: bold;
  }

  .content {
    flex: 1;
    margin-left:200px;
  }

  .titlebar{
    display: flex;
    height: 65px;
    width:100%;
    margin-top: 20px;
    /*border-bottom: 2px solid #e1e1e1;*/
  }

  .back-button{
    background-color:white;
    color:#969696;
    font-size:50px;
    height: 60px;
    padding-top: 0px;
    margin-left:5px;
  }

  .back-button:hover{
    background-color: #ffd7d7;
    font-weight: bold;
  }


</style>
