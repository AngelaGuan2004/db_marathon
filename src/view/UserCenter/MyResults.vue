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
  <div class="pickButton" @click="toggleFilterOptions">筛选
    <ul class="pickOptions" :class="{ show: showPickOptions }">
      <li data-type="all" @click="pickResults('all')">全部</li>
      <li data-type="full" @click="pickResults('full')">全程马拉松</li>
      <li data-type="half" @click="pickResults('half')">半程马拉松</li>
    </ul>
  </div>
  <div class="bestButton" @click="toggleBestResults">最好成绩
    <ul class="bestResults" :class="{ show: showBestResults }">
      <li data-type="full" @click="showBest('full')">全程马拉松</li>
      <li data-type="half" @click="showBest('half')">半程马拉松</li>
    </ul>
  </div>
  <div class="grades" v-if="bestResultSelected">
    <div :class="['result_item', bestResult.type, 'best_result']">
      {{ bestResult.name }} {{ bestResult.type === 'half' ? '半马  ' : '全马  ' }} 
      <span class="highlight">{{ bestResult.time }}</span> 
      <span>&nbsp;</span> <!-- 插入一个空格 -->
      <span class="highlight">{{ bestResult.rank }}名</span>
    </div>
  </div>
  <!-- 搜索框 -->
  <div class="search_container">
    <form @submit.prevent="search" class="search_form">
      <input type="text" v-model="searchQuery" placeholder="搜索比赛..." class="search_input">
      <button type="submit" class="search_button">搜索</button>
    </form>
  </div>
</div>

       <!-- 比赛成绩 -->
      <div class="grades">
        <div v-for="result in filteredResults" :key="result.name" :class="['result_item', result.type, result.isBest ? 'best_result' : '']">
           {{ result.name }}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{{ result.type === 'half' ? '半马  ' : '全马  ' }} 
           <span>&nbsp;&nbsp;&nbsp;</span> 
           <span class="highlight">{{ result.time }}</span> 
           <span>&nbsp;&nbsp;&nbsp;</span> 
          <span class="highlight">{{ result.rank }}名</span>
        </div>
      </div>
  
  
      <!-- 底部统计栏 -->
      <div class="stats_bar">
        共{{ filteredResults.length }}场比赛
      </div>
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
        showPickOptions: false,
        showBestResults: false,
        bestResultSelected: false,
        bestResultType: '',
        bestResult: {},
        bestResultText: '',
        filterType: 'all',
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
      toggleFilterOptions() {
        this.showPickOptions = !this.showPickOptions;
      },
      pickResults(filter) {
        this.filterType = filter;
        this.showPickOptions = false;
      },
      toggleBestResults() {
        this.showBestResults = !this.showBestResults;
      },
      showBest(type) {
      this.bestResultSelected = true;
      this.bestResultType = type;
      const bestResult = this.results
        .filter(result => result.type === type)
        .reduce((best, result) => best.time < result.time ? best : result, {});
      this.bestResult = bestResult;
      this.showBestResults = false;
    }
  },
  computed: {
      filteredResults() {
        if (this.filterType === 'all') {
          return this.results;
        }
        return this.results.filter(result => result.type === this.filterType);
      }
    },
    mounted() {
      document.addEventListener('click', (event) => {
        if (!event.target.closest('.pickButton') && !event.target.closest('.bestButton')) {
          this.showPickOptions = false;
          this.showBestResults = false;
        }
      });
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
    /*border: 1px solid red;*/
    background-color:white;
    margin-left: 30px;
    margin-top: 30px;
  }

  #div-right{
    width: 1000px;
    height: 500px;
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
  .grades_top {
  background-color: #c81623; /* 酒红色 */
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 10px; /* 调整内边距根据需要 */
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  width: 100%;
  z-index: 1000; /* 确保 navbar 在最上层 */
}

.begin {
  display: flex; /* 将内部元素横向排列 */
}

.logo {
  margin-right: 20px; /* 将logo放置在最左边 */
}

.mainpage {
  cursor: pointer; /* 鼠标显示 */   
  text-decoration: none;
  margin-top: 15px;
  font-size: 24px; 
}

.nav_link {
  color: white; 
}

.title {
  flex-grow: 1;
  margin-left: 400px;
  color: white;
  font-size: 40px; 
}

  .search_container {
    margin-left: auto; /* 将搜索框放置在最右边 */
  }
  
  .search_form {
    display: flex;
    align-items: center;
  }
  
  .search_input {
    padding: 8px;
    border: none;
    height:25px;
    border-radius: 5px;
    font-size: 14px; /* 调整字体大小根据需要 */
    border: 1px solid grey; /* 设置边框颜色为黑色，宽度为1px */
  }
  
  .search_button {
    padding: 8px 15px;
    background-color:light grey;
    border: none;
    height:40px;
    border-radius: 5px;
    cursor: pointer;
    font-size: 14px; /* 调整字体大小根据需要 */
  }
  
  
  .pick_best {
    display: flex;
    justify-content: flex-start;
    align-items: center;
    margin: 20px 0;
    position: sticky;
    top: 80px; /* 位置在导航栏下面 */
    background: white;
    padding: 10px 0;
    z-index: 999;
  }
  
  .pickButton, .bestButton {
    cursor: pointer;
    position: relative;
    background-color: #c81623; /* 酒红色 */
    color: white;
    padding: 15px;
    border-radius: 10px;
    margin-right: 20px; /* 设置按钮之间的间距 */
  }
  
  .pickOptions, .bestResults {
    display: none;
    position: absolute;
    background-color: rgb(247, 247, 247);
    border: 1px solid rgb(254, 254, 254);
    color:black;
    border-radius: 1px;
    margin-top: 5px;
    list-style: none;
    padding: 10px;
    width: 150px;
    z-index: 1000; /* 确保菜单在上层显示 */
  }
  
  .pickOptions.show, .bestResults.show {
    display: block;
  }
  
  .pickOptions li, .bestResults li {
    padding: 5px 0;
    background-color:rgb(250, 250, 250);
    cursor: pointer;
  }
  
  .pickOptions li:hover, .bestResults li:hover {
    background-color: #f0f0f0;
    color:black;
  }
  
  .full_best_result, .half_best_result {
    display: inline-block;
    margin-left: 20px;
    background-color: #f0ecec; 
    color: rgb(12, 5, 5);
    padding: 5px 10px;
    border-radius: 5px;
    font-size: 20px;
  }
  
  
.grades_top {
  background-color: #c81623; /* 酒红色 */
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 10px; /* 调整内边距根据需要 */
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  width: 100%;
  z-index: 1000; /* 确保 navbar 在最上层 */
}

.begin {
  display: flex; /* 将内部元素横向排列 */
}

.logo {
  margin-right: 20px; /* 将logo放置在最左边 */
}

.mainpage {
  cursor: pointer; /* 鼠标显示 */   
  text-decoration: none;
  margin-top: 15px;
  font-size: 24px; 
}

.nav_link {
  color: white; 
}

.title {
  flex-grow: 1;
  margin-left: 400px;
  color: white;
  font-size: 40px; 
}

.search_container {
  margin-left: auto; /* 将搜索框放置在最右边 */
}

.search_form {
  display: flex;
  align-items: center;
}

.search_input {
  padding: 8px;
  border: none;
  height: 25px;
  border-radius: 5px;
  font-size: 14px; /* 调整字体大小根据需要 */
  border: 1px solid grey; /* 设置边框颜色为黑色，宽度为1px */
}

.search_button {
  padding: 8px 15px;
  background-color: lightgrey;
  border: none;
  height: 40px;
  border-radius: 5px;
  cursor: pointer;
  font-size: 14px; /* 调整字体大小根据需要 */
}

.pick_best {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  margin: 20px 0;
  position: sticky;
  top: 80px; /* 位置在导航栏下面 */
  background: white;
  padding: 10px 0;
  z-index: 999;
}

.pickButton, .bestButton {
  cursor: pointer;
  position: relative;
  background-color: #c81623; /* 酒红色 */
  color: white;
  padding: 15px;
  border-radius: 10px;
  margin-right: 20px; /* 设置按钮之间的间距 */
}

.pickOptions, .bestResults {
  display: none;
  position: absolute;
  background-color: rgb(247, 247, 247);
  border: 1px solid rgb(254, 254, 254);
  color: black;
  border-radius: 1px;
  margin-top: 5px;
  list-style: none;
  padding: 10px;
  width: 150px;
  z-index: 1000; /* 确保菜单在上层显示 */
}

.pickOptions.show, .bestResults.show {
  display: block;
}

.pickOptions li, .bestResults li {
  padding: 5px 0;
  background-color: rgb(250, 250, 250);
  cursor: pointer;
}

.pickOptions li:hover, .bestResults li:hover {
  background-color: #f0f0f0;
  color: black;
}

.full_best_result, .half_best_result {
  display: inline-block;
  margin-left: 20px;
  background-color: #f0ecec; 
  color: rgb(12, 5, 5);
  padding: 5px 10px;
  border-radius: 5px;
  font-size: 20px;
}

.grades {
  max-height: 570px;
  overflow-y: auto;
  text-align: center;
  scrollbar-width: 10;
  scrollbar-color: #888 transparent;
}

.grades::-webkit-scrollbar {
  width: 6px;
}

.grades::-webkit-scrollbar-track {
  background: transparent;
}

.grades::-webkit-scrollbar-thumb {
  background-color: #888;
  border-radius: 10px;
  border: 2px solid transparent;
}

.grades::-webkit-scrollbar-thumb:hover {
  background-color: #555;
}

.result_item {
  padding: 10px 20px;
  border: 1px solid #ddd;
  border-radius: 4px;
  height: 33px;
  font-size: 16px;
  font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
}

.result_item:nth-child(even) {
  background-color: #f9f9f9;
}

.result_item:nth-child(odd) {
  background-color: #ffffff;
}

.result_item p {
  margin: 5px 0;
}

.highlight {
  font-weight: bold;
  color: #c81623; /* 突出显示的颜色 */
}
  /*底部栏目 */
  .stats_bar {
    background-color: #c81623; /* 酒红色 */
    color: white;
    text-align: center;
    padding: 10px;
    position: fixed;
    bottom: 0;
    left: 0;
    right: 0;
    z-index: 1000;
    font-size: 24px; 
    height:30px;
  }
</style>
