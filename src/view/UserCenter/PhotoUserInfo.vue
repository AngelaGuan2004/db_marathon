<template>
  <div>
    <div id="upper">
      <div id="upper-left" @click="goToShouye">
        <img src="@/assets/返回.png" alt="" class="img-left">
        <p class="p-upper">首页</p>
      </div>
      <img src="@/assets/上马.png" alt="" class="img-right">
    </div>
    <div id="down">
      <div id="div-left">
        <div class="break"></div> 
        <div>
          <button :class="checkClass(0)" @click="goToBaoming">我的报名</button>
        </div>
        <div>
          <button :class="checkClass(1)" @click="goToChengji">我的成绩</button>
        </div>
        <div>
          <button :class="checkClass(2)" >个人信息</button>
        </div>
        <div>
          <button :class="checkClass(3)" @click="goToZhiyuan">我的志愿</button>
        </div>
      </div>
      <div id="div-middle">
        <div id="down-upper">
          <p class="jbxx">基本信息</p>
        </div>
        <hr class="a-line" />
        <div id="down-down">
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
      <div id="div-right1">
        <img src="@/assets//veer-141859030.jpg" alt="" class="down-right-img">
      </div>
    </div>
  </div>
</template>
    
<script>
  /*import {getPhotoInfor} from '@/api/UserCenter.js' */
  export default {
    name:'PhotoUserInfo',
    data() {
      return {
        //buttonText: "编辑",
        bianji:false,
        //tele_right:true,
        //age_right:true,
        ischecked: [false, false, true, false],
        name:'李靓',
        //telephone:'12637152637',
        //ID_number:'273618633622772662',
        ID:'2272186183',   /* 这个参数怎么来？存疑 */
        //gender:'女',
        //age:'',
        //region:'',
      }
    },
    mounted(){
      /* 
      getPhotoInfor(this.ID)
        .then((res) => {
          this.name = res.name;
        })
        .catch(error => {
          console.error('查询失败:', error);
          alert('查询失败');
        });
      */
    },
    methods: {
      goToShouye(){
        
      },
      goToBaoming() {
        this.$router.push({ name: 'MyRegistrations' });
      },
      goToChengji() {
        this.$router.push({ name: 'MyResults' });
      },
      goToZhiyuan() {
        this.$router.push({ name: 'MyVolunteering' });
      },
      checkClass(index) {
        return this.ischecked[index] ? 'active' : 'inactive'
      },
    
    },

  }
</script>


<style scoped>
  /* 把我们所有标签的内外边距清零 */
  * {
      margin: 0;
      padding: 0;
      /* css3盒子模型 */
      box-sizing: border-box;
  }
  
  /* em 和 i 斜体的文字不倾斜 */
  em,
  i {
      font-style: normal
  }
  
  /* 去掉li 的小圆点 */
  li {
      list-style: none
  }
  
  img {
      /* border 0 照顾低版本浏览器 如果 图片外面包含了链接会有边框的问题 */
      border: 0;
      /* 取消图片底侧有空白缝隙的问题 */
      vertical-align: middle
  }
  
  button {
      /* 当我们鼠标经过button 按钮的时候，鼠标变成小手 */
      cursor: pointer
  }
  
  a {
      color: #666;
      text-decoration: none
  }
  
  a:hover {
      color: #c81623
  }
  
  button,
  input {
      /* "\5B8B\4F53" 就是宋体的意思 这样浏览器兼容性比较好 */
      font-family: Microsoft YaHei, Heiti SC, tahoma, arial, Hiragino Sans GB, "\5B8B\4F53", sans-serif;
      /* 默认有灰色边框我们需要手动去掉 */
      border: 0;
      outline: none;
  }
  
  body {
      /* CSS3 抗锯齿形 让文字显示的更加清晰 */
      -webkit-font-smoothing: antialiased;
      background-color: #fff;
      font: 12px/1.5 Microsoft YaHei, Heiti SC, tahoma, arial, Hiragino Sans GB, "\5B8B\4F53", sans-serif;
      color: #666
  }
  
  .hide,
  .none {
    display: none
  }
  
  /* 清除浮动 */
  .clearfix:after {
    visibility: hidden;
    clear: both;
    display: block;
    content: ".";
    height: 0
  }
  
  /*上部分*/ 
  
  #upper{
    margin-top: 0px;
    display: flex;
    justify-content: space-between;
    background-color: rgb(168, 27, 31);
    height: 120px;
  }
  #upper-left{
    display: flex;
    justify-content: space-between;
  }

  .img-left{
    margin-top: 30px;
    margin-left: 50px;
    height: 50px;
    width: 50px;
  }
  .img-right{
    margin-right: 20px;
  }
  .p-upper{
    margin-top: 32px;
    margin-left:20px;
    color:white;
    font-size: 30px;
  }
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;

  }
  
  /* 下部分 */
  #down{
    display: flex;
    justify-content: space-between;
    background-color:#f4f4f6;
  }
  #div-left{
    width: 150px;
    height: 500px;
    /*border: 1px solid red;*/
    background-color:white;
    margin-left: 30px;
    margin-top: 30px;
  }
  #div-middle{
    width: 500px;
    height: 500px;
    /*border: 1px solid green;*/
    background-color:white;
    margin-left: 20px;
    margin-top: 30px;
    margin-bottom: 28px;
    position: relative;
  }
  #div-right1{
    width: 500px;
    height: 500px;
    margin-right: 30px;
    margin-top: 30px;
  }

  .down-right-img{
    width: 500px;
    height: 420px;
  }
  
  /* 下部分的中间部分 */
  #down-upper{
    width: 400px;
    height: 50px;
  }
  #down-down{
    width: 400px;
    height: 300px;
  }
  .a-line {
    border: none;
    height: 2px; 
    background-color: #f4f4f6;
  }
  .jbxx{
    margin-top: 18px;
    color:black;
    font-size: 20px;
    text-align:left;
    margin-left:50px;
  }
  .down-notice{
    width: 80px;
    color:black;
    font-size: 18px;
    text-align:left;
    margin-left:50px;
  }
  .down-notice2{
    color:black;
    font-size: 18px;
    text-align:left;
    margin-left:0px;
  }

  
  .a-button{
    margin-top:50px;
    display: flex; 
    align-items: center;
    width: 500px;

  }
  .label-p{
    font-size: 18px;
    text-align:left;
    color:black;
  }
  .label-c{
    font-size: 18px;
    text-align:left;
    margin-left:33px;
    color:black;
  }
  .select-reg{
    display:flex;
  }
  .border{
    border:1px solid #2c3e50;
  }
  .error-notice{
    color:rgb(168, 27, 31);
    margin-right:2px;
  }
  /* 下部分的左间部分 */
  .inactive{
    margin-left:20px;
    font-size:20px;
    color:black;
    background-color: white;
    margin-top: 30px;
  }
  .active{
    margin-left:20px;
    margin-top: 30px;
    font-size:20px;
    color:rgb(168, 27, 31);
    background-color: white;
  }
  .break{
    margin-top:60px;
  }
  
  .relative-button {
    position: absolute;
    top: 18px;
    right: 100px;
    font-size:20px;
    color:rgb(168, 27, 31);
    background-color: white;
  }

</style>
    