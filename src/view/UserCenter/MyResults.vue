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
          <button :class="checkClass(0)" @click="goToBaoming">我的报名</button>
        </div>
        <div>
          <button :class="checkClass(1)" >我的成绩</button>
        </div>
        <div>
          <button :class="checkClass(2)" @click="goToXinxi">个人信息</button>
        </div>
        <div>
          <button :class="checkClass(3)" @click="goToZhiyuan">我的志愿</button>
        </div>
      </div>

    <div id="div-right">

    <!--插入cyy页面开始-->
    
    <div>
    <!-- 筛选和最佳成绩栏目 -->
    <div class="pick_best">
      <span>比赛类型：</span>
      <el-select v-model="filterType" placeholder="选择比赛类型">
        <el-option label="全部" value="all"></el-option>
        <el-option label="全程马拉松" value="full"></el-option>
        <el-option label="半程马拉松" value="half"></el-option>
      </el-select>

      <span>最好成绩：</span>
      <el-select v-model="bestResultType" placeholder="选择比赛类型" @change="showBest">
        <el-option label="全程马拉松" value="full"></el-option>
        <el-option label="半程马拉松" value="half"></el-option>
      </el-select>

      <div class="best_grades" v-if="bestResultSelected">
        <div :class="['result_item', bestResult.type, 'best_result']">
          {{ bestResult.name }} {{ bestResult.type === 'half' ? '半马  ' : '全马  ' }} 
          <span class="highlight">{{ bestResult.time }}</span> 
          <span>&nbsp;</span> <!-- 插入一个空格 -->
          <span class="highlight">{{ bestResult.rank }}名</span>
        </div>
      </div>

      <!-- 搜索框 -->
      <div class="search_container">
        <el-input
          v-model="searchQuery"
          placeholder="搜索比赛..."
          class="search_input"
          @keyup.enter.native="search"
        />
        <el-button type="primary" icon="el-icon-search" @click="search">搜索</el-button>
      </div>
    </div>

    <!-- 比赛成绩表格 -->
    <div class="grades">
      <table class="result_table">
        <thead>
          <tr>
            <th>比赛名称</th>
            <th>比赛类型</th>
            <th>净成绩</th>
            <th>排名</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="result in paginatedResults" :key="result.name">
            <td>{{ result.name }}</td>
            <td>{{ result.type === 'half' ? '半马' : '全马' }}</td>
            <td :style="{ color: 'red' }">{{ result.time }}</td>
            <td :style="{ color: 'red' }">{{ result.rank }}名</td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- 分页组件 -->
    <el-pagination
      v-if="filteredResults.length > 0"
      background
      layout="prev, pager, next"
      :total="filteredResults.length"
      :page-size="pageSize"
      :current-page.sync="currentPage"
      @current-change="handlePageChange"
      class="pagination_bar"
    >
    </el-pagination>
  </div>
    <!--插入cyy页面结束-->
  </div>
    </div>

  </div>
</template>
<script>
export default{
  name:'MyRegistrations',
  data() {
      return {
       
        ischecked: [false, true, false, false],
        searchQuery: '',
      bestResultSelected: false,
      bestResultType: '',
      bestResult: {},
      filterType: 'all',
      currentPage: 1,
      pageSize: 8,
      results: [
        { name: '上海国际马拉松', type: 'full', time: '3:45:30', rank: 123 },
        { name: '杭州半程马拉松', type: 'half', time: '1:45:20', rank: 56 },
        { name: '北京马拉松', type: 'full', time: '3:50:10', rank: 150 },
        { name: '广州半程马拉松', type: 'half', time: '1:50:00', rank: 78 },
        { name: '深圳国际马拉松', type: 'full', time: '3:55:40', rank: 200 },
        { name: '成都半程马拉松', type: 'half', time: '1:55:30', rank: 90 },
        { name: '重庆国际马拉松', type: 'full', time: '4:05:20', rank: 250 },
        { name: '南京半程马拉松', type: 'half', time: '2:00:10', rank: 100 },
        { name: '武汉国际马拉松', type: 'full', time: '4:15:00', rank: 300 },
        { name: '西安半程马拉松', type: 'half', time: '2:05:50', rank: 120 },
        { name: '长沙国际马拉松', type: 'full', time: '4:25:30', rank: 350 },
        { name: '青岛半程马拉松', type: 'half', time: '2:10:40', rank: 140 },
        { name: '苏州国际马拉松', type: 'full', time: '4:35:20', rank: 400 },
        { name: '天津半程马拉松', type: 'half', time: '2:15:30', rank: 160 },
        { name: '哈尔滨国际马拉松', type: 'full', time: '4:45:50', rank: 450 }
      ]
      }
  },
  methods:{
    goToShouye(){
        
      },
    goToXinxi() {
      this.$router.push({ name: 'UserInfo' });
    },
    goToBaoming() {
      this.$router.push({ name: 'MyRegistrations' });
    },
    goToZhiyuan() {
      this.$router.push({ name: 'MyVolunteering' });
    },
    checkClass(index) {
        return this.ischecked[index] ? 'active' : 'inactive'
      },
      search() {
      console.log('Searching for:', this.searchQuery);
    },
    showBest(type) {
      this.bestResultSelected = true;
      const bestResult = this.results
        .filter(result => result.type === type)
        .reduce((best, result) => best.time < result.time ? best : result, {});
      this.bestResult = bestResult;
    },
    handlePageChange(page) {
      this.currentPage = page;
    }
  
    },
    computed: {
    filteredResults() {
      if (this.filterType === 'all') {
        return this.results;
      }
      return this.results.filter(result => result.type === this.filterType);
    },
    paginatedResults() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = start + this.pageSize;
      return this.filteredResults.slice(start, end);
    }
  }
      
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

      background-color:white;
      margin-left: 30px;
      margin-top: 30px;
    }
    #div-middle{
      width: 500px;
      height: 420px;
      /*border: 1px solid green;*/
      background-color:white;
      margin-left: 20px;
      margin-top: 30px;
      margin-bottom: 28px;
      position: relative;
    }
    #div-right{
      width: 1000px;
      height: 420px;
      margin-right: 30px;
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
      background-color: white;
      margin-top:30px;
      width: 1000px;
      height: 500px;
      font-size:15px;
  
    }
    .search_container {
  display: flex;
  align-items: center;
  margin-left: auto; /* 将搜索框放置在最右边 */
}

.el-input {
  width: 200px; 
}

.el-button--primary {
  background-color: #c81623; /* 酒红色 */
}

.pick_best {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin: 20px 0;

  top: 50px; /* 位置在导航栏下面 */
  background: white;
  padding: 0px 0;
  height:30px;
}

.best_result {
  display: inline-block;
  margin-left: 20px;
  background-color: #ffffff;
  color: rgb(12, 5, 5);
  padding: 5px 8px 2px 8px;
  font-size: 20px;
  line-height: 24px; 
}

.grades {
  z-index: 1000; 
  max-height: calc(100vh - 280px); 
  overflow-y: auto; /* 滚动条 */
  text-align: center;
  position: relative;
  max-height:550px;
  scrollbar-color: #888 transparent;
}


.result_table {
  width: 100%;
  border-collapse: collapse;
  text-align: left;
  font-size: 16px;
  font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
  margin-top: 20px;
}

.result_table th, .result_table td {
  padding: 10px 20px;
  border-bottom: 1px solid #ddd;
}

.result_table th {
  background-color: #f9f9f9;
  font-weight: bold;
}

.result_table td {
  vertical-align: middle;
}

.result_table td.red {
  color: red;
}

.pagination_bar {
  background-color: #ffffff;
  text-align: center;
  padding: 10px;

  bottom: 0;
  left: 0;
  right: 0;
  z-index: 1000;
  font-size: 24px; 
  height: 50px;
}




</style>