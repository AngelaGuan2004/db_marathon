<template>
    <div>
      <!--上部分红色-->
      <div id="upper">
          <div id="upper-left" @click="goToShouye">
              <img src="@/assets/返回.png" alt="" class="img-left">
              <p class="p-upper">首页</p>
          </div>
          <img src="@/assets/上马.png" alt="" class="img-right">
      </div>
  
      
      <!--下侧-->
      <div id="down">
      <!--左边-->
      <div id="div-left">
          <div class="break"></div> 
          <div>
            <button :class="checkClass(0)" >我的报名</button>
          </div>
          <div>
            <button :class="checkClass(1)" @click="goToChengji">我的成绩</button>
          </div>
          <div>
            <button :class="checkClass(2)" @click="goToXinxi">个人信息</button>
          </div>
          <div>
            <button :class="checkClass(3)" @click="goToZhiyuan">我的志愿</button>
          </div>
        </div>

        <!--右边-->
        <div id="div-right">

           <!--尝试开始-->
          <el-main>
            <div style="width: 100%; overflow: auto ;height: 420px;margin-top: 20px;">
        <el-table :data="events"   class="table" @row-click="handleRowClick"   row-class-name="clickable-row" stripe>
          <el-table-column prop="name" label="比赛名称" width="300"></el-table-column>
          <el-table-column prop="event_date" label="时间" width="150"></el-table-column>
          <el-table-column prop="type" label="赛事类型" width="150"></el-table-column>
          <el-table-column prop="result" label="结果" width="150">
            <template slot-scope="scope">
    <div style="color: #c81623;">{{ scope.row.result }}</div>
  </template>
          </el-table-column>
          <el-table-column prop="number" label="号码" width="150"></el-table-column>
        </el-table>
            </div>
      </el-main>
 <!--尝试结束-->


        </div>
  
  
      </div>
  
    </div>
  </template>
  <script>
  export default{
    name:'MyRegistrations',
    data() {
        return {
         
          ischecked: [true, false, false, false],

          /* 尝试开始 */
          activeMenu: '2',
        searchName: '',


        events: [
          { id:'1', name: '2024天津团泊湖半程马拉松',  event_date:'2024 7 12',type: '半马', result: '已中签', number:'22222'},
          { id:'2', name: '2024天津团泊湖半程马拉松',  event_date:'2024 7 12',type: '全马', result: '已中签', number:'22222'},
          { id:'3', name: '2024上海马拉松',  event_date:'2024 7 12',type: '半马', result: '已中签', number:'22222'},
          { id:'4', name: '2024上海马拉松',  event_date:'2024 7 12',type: '全马', result: '已中签', number:'22222'},
          { id:'1', name: '2024天津团泊湖半程马拉松',  event_date:'2024 7 12',type: '半马', result: '已中签', number:'22222'},
          { id:'2', name: '2024天津团泊湖半程马拉松',  event_date:'2024 7 12',type: '全马', result: '已中签', number:'22222'},
          { id:'3', name: '2024上海马拉松',  event_date:'2024 7 12',type: '半马', result: '已中签', number:'22222'},
          { id:'4', name: '2024上海马拉松',  event_date:'2024 7 12',type: '全马', result: '已中签', number:'22222'},
          { id:'1', name: '2024天津团泊湖半程马拉松',  event_date:'2024 7 12',type: '半马', result: '已中签', number:'22222'},
          { id:'2', name: '2024天津团泊湖半程马拉松',  event_date:'2024 7 12',type: '全马', result: '已中签', number:'22222'},
          { id:'3', name: '2024上海马拉松',  event_date:'2024 7 12',type: '半马', result: '已中签', number:'22222'},
          { id:'4', name: '2024上海马拉松',  event_date:'2024 7 12',type: '全马', result: '已中签', number:'22222'},
          { id:'1', name: '2024天津团泊湖半程马拉松',  event_date:'2024 7 12',type: '半马', result: '已中签', number:'22222'},
          { id:'2', name: '2024天津团泊湖半程马拉松',  event_date:'2024 7 12',type: '全马', result: '已中签', number:'22222'},
          { id:'3', name: '2024上海马拉松',  event_date:'2024 7 12',type: '半马', result: '已中签', number:'22222'},
          { id:'4', name: '2024上海马拉松',  event_date:'2024 7 12',type: '全马', result: '已中签', number:'22222'},
         
        ],
        currentPage: 1,
        pageSize: 5,
         /* 尝试结束 */
  
        }
    },
    methods:{
      goToShouye(){
        
      },
      goToXinxi() {
        this.$router.push({ name: 'UserInfo' });
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
  
      handleRowClick(row) {
        window.alert(row.id);
        console.log(row.id);
      },
    },
    computed:{

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
      margin-top: 30px;
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
   
    .down-right-img{
      width: 500px;
      height: 420px;
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
      top: 8px;
      right: 100px;
      font-size:20px;
      color:rgb(168, 27, 31);
      background-color: white;
    }
    /* 下部分的右边 */
    #div-right{
      margin-right:20px;
      background-color: white;
      margin-top:30px;
      width: 1000px;
      height: 500px;
      font-size:15px;
    }
    
  .el-menu-demo {
    flex: 1;
    margin-left: 20px;
  }
  
  .filter-container {
    display: flex;
    justify-content: center;
    align-items: center;
    gap: 10px;
    margin: 20px 0;
  }
  
  .input-with-select {
    width: 300px;
  }
  
  .el-table {
    width: 100%;
    table-layout: fixed;
    border: none; /* 移除表格的边框 */
  }
  
  .el-table th .cell,
  .el-table td .cell {
    display: inline-block;
    width: 100%;
    text-align: center;
    height: 43px;
    border: none; /* 移除单元格的边框 */
  }
  
  .el-table__header-wrapper,
  .el-table__body-wrapper {
    display: flex;
    justify-content: center;
  }
  
  .pagination {
    display: flex;
    justify-content: center;
    margin: 20px 0;
  }
  
  .clickable-row {
    cursor: pointer;
  }
  
  .clickable-row:hover {
    background-color: #f5f5f5;
  }
  .table .clickable-row{
  text-align: center;
  vertical-align: middle;
  line-height: 40px;  
}

  </style>

  