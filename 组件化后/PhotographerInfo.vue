<template>
    <div id="UserInfo">
      <div class="UserInfo">
        <div class="UserInfoUpper">
          <div class="UserInfoTitle">基本信息</div>
        </div>
        <hr class="line" />
        <div id="UserInfoText">
          <div class="a-button">
            <p class="down-notice">姓名</p>
            <p class="down-notice2">{{ name }}</p>
          </div>
          <div class="a-button">
            <p class="down-notice">ID</p>
            <p class="down-notice2">{{ ID }}</p>
          </div>
        </div>
      </div>
      <div class="UserInfoImg">
        <img src="@/assets/images/KeepRunning.jpg" alt="">
      </div>
    </div>
  </template>
  
  <script>
  import {getPhotographerInfor} from '@/api/UserCenter.js' 
  export default {
    name: 'PhotographerInfo',
    data() {
      return {
        name:'',
        ID:3,   /* 这个参数怎么来？存疑 */
      }
    },
    mounted() {
      getPhotographerInfor(this.ID)
        .then((res) => {
          this.name = res.Name;
        })
        .catch(error => {
          console.error('查询失败:', error);
          alert('查询失败');
        });
    }
  }
  </script>
  
  <style scoped>
  #UserInfo {
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: flex-end;
  }
  
  .UserInfo {
    width: 500px;
    height: 500px;
    background-color: white;
    left: -75px;
    margin-top: 50px;
    position: relative;
  }
  
  .UserInfoImg {
    width: 450px;
    height: 100%;
    left: -200px;
    margin-top: 30px;
  }
  
  .UserInfoImg img {
    width: 100%;
  }
  
  .UserInfoUpper {
    width: 100%;
    height: 60px;
  }
  
  .UserInfoTitle {
    display: inline-block;
    line-height: 65px;
    font-size: 24px;
    font-weight: bold;
    text-align: left;
    margin-left: 50px;
  }
  
  .relative-button {
    display: inline-block;
    margin-left: 55%;
    margin-top: 10px;
    font-size: 18px;
    color: black;
    background-color: transparent;
    border: 0;
    cursor: pointer;
    width: 50px;
    height: 35px;
    border-radius: 10px;
  }
  
  .UserInfoText {
    width: 100%;
    height: 300px;
  }
  
  .line {
    border: none;
    height: 2px;
    background-color: #f4f4f6;
  }
  
  
  .down-notice {
    width: 100px;
    color: black;
    font-size: 18px;
    text-align: left;
    margin-left: 50px;
  }
  
  .down-notice2 {
    color: black;
    font-size: 18px;
    text-align: left;
    margin-left: 10px;
  }
  
  
  .a-button {
    display: flex;
    align-items: center;
  }
  
  .label-p {
    font-size: 18px;
    text-align: left;
    color: black;
  }
  
  .label-c {
    font-size: 18px;
    text-align: left;
    margin-left: 33px;
    color: black;
  }
  
  .select-reg {
    display: flex;
  }
  
  .border {
    border: 1px solid #2c3e50;
  }
  
  .error-notice {
    color: rgb(168, 27, 31);
    margin-right: 2px;
  }
  
  /* 下部分的左间部分 */
  .inactive {
    margin-left: 20px;
    font-size: 20px;
    color: black;
    background-color: white;
    margin-top: 30px;
  }
  
  .active {
    margin-left: 20px;
    margin-top: 30px;
    font-size: 20px;
    color: rgb(168, 27, 31);
    background-color: white;
  }
  
  .break {
    margin-top: 60px;
  }
  </style>