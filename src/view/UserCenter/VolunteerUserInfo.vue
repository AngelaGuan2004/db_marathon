<template>
  <div>
    <div id="upper">
      <div id="upper-left" @click="goToShouye">
          <img src="@/assets/LwmReturn.png" alt="" class="img-left">
          <p class="p-upper">首页</p>
      </div>
      <img src="@/assets/LwmShm.png" alt="" class="img-right">
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

        <button class="relative-button" @click="changeButton">{{ buttonText }}</button>
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
            <p class="down-notice">手机号</p>
            <p class="down-notice2" v-if="!bianji">{{ telephone }}</p>
            <input  v-model="telephone" placeholder="请输入您的电话"  v-if="bianji" class="down-notice2 border"/>
            <p class='error-notice' v-if="bianji && !tele_right">输入有误</p>
          </div>
          <div class="a-button">
            <p class="down-notice">ID</p>
            <p class="down-notice2">{{ ID }}</p>
          </div>
        </div>
      </div>
      <div id="div-right1">
          <img src="@/assets/KeepRunning.jpg" alt="" class="down-right-img">
      </div>
    </div>
  </div>
</template>
    
<script>
  /*import {completeVolunteerInfor} from '@/api/UserCenter.js'*/
  /*import {getVolunteerInfor} from '@/api/UserCenter.js' */
  export default {
    name:'VolunteerUserInfo',
    data() {
      return {
        buttonText: "编辑",
        bianji:false,
        tele_right:true,
        //age_right:true,
        ischecked: [false, false, true, false],
        name:'李靓',
        old_telephone:'12637152637',
        telephone:'12637152637',
        //ID_number:'273618633622772662',
        ID:'2272186183',   /* 这个参数怎么来？存疑 */
        //gender:'女',
        //age:'',
        //region:'',

      }
    },
    mounted(){
      /* 
      getVolunteerInfor(this.ID)
        .then((res) => {
          this.name = res.name;
          this.old_telephone = res.Telephone_number;
          this.telephone = res.Telephone_number;
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
      validateAge(age) {
          // 检查每一位是否为数字
          if (/^\d+$/.test(age)) {
            const ageNum = parseInt(age, 10);
            if (ageNum >= 1 && ageNum <= 100) {
              this.age_right = true;
            } else {
              this.age_right = false;
            }
          } else {
            this.age_right = false;
          }
        },
      changeButton() {
        //一层：如果现在是展示状态
        if(this.bianji==false){
          this.buttonText='保存'
          this.bianji=true
        }
        //一层：如果现在是编辑状态
        else if(this.bianji==true){
          //二层：如果编辑的内容合规
          if(this.tele_right==true){
            //三层：没有改变数据
            if(this.old_telephone==this.telephone){
              this.buttonText='编辑'
              this.bianji=false
            }
            //三层：改变了数据
            else{
              this.$confirm("是否确定删除选中的数据?", "提示", {
                confirmButtonText: "确定（A按钮）",
                cancelButtonText: "取消（B按钮）",
                type: "warning",
                distinguishCancelAndClose: true,    // 重要，设置为true才会把右上角X和取消区分开来
                closeOnClickModal: false
              }).then(() => {
                // 确认通过执行逻辑        A按钮逻辑
                this.old_telephone=this.telephone;
                /* 调用js */
                /*this.submit();*/
                this.buttonText='编辑'
                this.bianji=false
                
              }).catch((e) => {
                if (e == 'cancel') {
                    // 确认不通过执行逻辑   B按钮逻辑
                    
                } else if(e == 'close') {
                    // 右上角X的执行逻辑
                
                }
              })
            }
          }
          //二层：编辑的内容不合规
          else{
            this.$confirm("是否确定删除选中的数据?", "提示", {
              confirmButtonText: "退出（不保存编辑）（A按钮）",
              cancelButtonText: "好的（B按钮）",
              type: "warning",
              distinguishCancelAndClose: true,    // 重要，设置为true才会把右上角X和取消区分开来
              closeOnClickModal: false
            }).then(() => {
              // 确认通过执行逻辑        A按钮逻辑
              this.telephone=this.old_telephone;
              this.buttonText='编辑'
              this.bianji=false
            }).catch((e) => {
              if (e == 'cancel') {
                  // 确认不通过执行逻辑   B按钮逻辑
                  
              } else if(e == 'close') {
                  // 右上角X的执行逻辑 
              
              }
            })
   
          }
        }
      },
      /*
      submit() {
        try{
          completeVolunteerInfor({ID: this.ID,name:this.name,Telephone_number:this.telephone}).then((res) => {
            if (res.data === false) {
              alert("总会成功的！");
            } else {
              alert("提交成功");
            }
          })
        }catch(error){
          console.error(error);
          alert('总会成功的！');
        }
      },
      */
      
    },
    computed: {
  
    },
    watch: {
      telephone(newVal) {
        if(this.telephone != ''){
          var reg = /^1\d{10}$/;
          this.tele_right = reg.test(newVal);
        }
        else{
          this.tele_right = true;
        }
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
  