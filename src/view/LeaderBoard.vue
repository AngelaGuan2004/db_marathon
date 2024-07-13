<template>
    <div>
      <!-- 顶部筛选栏 -->
      <nav class="topBar">
        <!-- 显示logo -->
        <div class="logo">
          <img src="@/assets/logo.png" alt="Logo" class="index_logo" style="height: 55px;">
        </div>
        <!-- 返回键 -->
        <div class="back">
          <router-link to="/" class="nav_link">返回</router-link>
        </div>
        <!-- 搜索框搜索比赛名称 -->
        <div class="search_container">
          <form @submit.prevent="search" class="search_form">
            <input type="text" v-model="searchQuery" placeholder="搜索比赛..." class="search_input">
            <button type="submit" class="search_button">搜索</button>
          </form>
        </div>
      </nav>
  
      <!-- 筛选马拉松类型和性别 -->
      <div class="filter-container">
        <div class="typeButton" ref="typeButton" @click="toggleMarathonType">马拉松类型
          <ul class="dropdown" :class="{ show: showMarathonType }">
            <li @click="chooseMarathonType('全部')">全部</li>
            <li @click="chooseMarathonType('全程马拉松')">全程马拉松</li>
            <li @click="chooseMarathonType('半程马拉松')">半程马拉松</li>
          </ul>
        </div>
        <div class="genderButton" ref="genderButton" @click="toggleGender">性别
          <ul class="dropdown" :class="{ show: showGender }">
            <li @click="chooseGender('全部')">全部</li>
            <li @click="chooseGender('男')">男</li>
            <li @click="chooseGender('女')">女</li>
          </ul>
        </div>
        <!-- 排序选择按钮 -->
        <div class="sort">
          <button @click="toggleSort" class="sort-button">排序方式: {{ sortType }}</button>
          <ul v-if="sortGrade" class="sort-options">
            <li @click="chooseSortType('顺序排列')">顺序排列</li>
            <li @click="chooseSortType('逆序排列')">逆序排列</li>
          </ul>
        </div>
      </div>
  
      <!-- 成绩排行榜 -->
      <div class="rank">
        <!-- 排行榜表头 -->
        <div class="rank_header">
          <div class="header_item">姓名</div>
          <div class="header_item">性别</div>
          <div class="header_item">比赛名称</div>
          <div class="header_item">比赛成绩</div>
          <div class="header_item">比赛排名</div>
        </div>
        <!-- 比赛成绩列表 -->
        <div v-for="result in filteredResults" :key="result.name" class="result_item">
          <div>{{ result.name }}</div>
          <div>{{ result.gender }}</div>
          <div>{{ result.race }}</div>
          <div class="highlight">{{ result.netTime }}</div>
          <div class="highlight">{{ result.rank }}名</div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'rank',
    data() {
      return {
        showMarathonType: false,
        showGender: false,
        selectedMarathonType: '全部',
        selectedGender: '全部',
        marathonType: '',
        sortType: '顺序排列',
        gender: '',
        searchQuery: '',
        results: [], // 存储比赛成绩的数组
        filteredResults: [], // 筛选后的比赛成绩数组
        rankStats: {
          totalParticipants: 0,
          averageTime: '',
          fastestTime: '',
          slowestTime: ''
        }
      };
    },
    created() {
      // 模拟从 API 获取比赛成绩数据
      this.results =[
  { name: '张三', gender: '男', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:30:45', rank: 1 },
  { name: '李四', gender: '男', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:15:30', rank: 2 },
  { name: '王五', gender: '女', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:35:00', rank: 3 },
  { name: '赵六', gender: '女', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:18:00', rank: 4 },
  { name: '钱七', gender: '男', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:28:15', rank: 5 },
  { name: '孙八', gender: '男', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:16:45', rank: 6 },
  { name: '周九', gender: '女', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:32:30', rank: 7 },
  { name: '吴十', gender: '女', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:20:10', rank: 8 },
  { name: '郑十一', gender: '男', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:27:55', rank: 9 },
  { name: '王十二', gender: '男', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:14:20', rank: 10 },
  { name: '陈十三', gender: '女', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:33:20', rank: 11 },
  { name: '杨十四', gender: '女', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:19:30', rank: 12 },
  { name: '朱十五', gender: '男', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:29:40', rank: 13 },
  { name: '秦十六', gender: '男', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:17:00', rank: 14 },
  { name: '何十七', gender: '女', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:34:15', rank: 15 },
  { name: '吕十八', gender: '女', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:21:05', rank: 16 },
  { name: '张十九', gender: '男', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:26:50', rank: 17 },
  { name: '孔二十', gender: '男', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:13:50', rank: 18 },
  { name: '曹二十一', gender: '女', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:31:00', rank: 19 },
  { name: '严二十二', gender: '女', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:18:45', rank: 20 },
  { name: '华二十三', gender: '男', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:25:20', rank: 21 },
  { name: '金二十四', gender: '男', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:15:00', rank: 22 },
  { name: '魏二十五', gender: '女', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:30:10', rank: 23 },
  { name: '陶二十六', gender: '女', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:19:00', rank: 24 },
  { name: '姜二十七', gender: '男', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:24:30', rank: 25 },
  { name: '戚二十八', gender: '男', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:14:40', rank: 26 },
  { name: '谢二十九', gender: '女', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:32:45', rank: 27 },
  { name: '邹三十', gender: '女', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:20:30', rank: 28 },
  { name: '喻三十一', gender: '男', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:23:10', rank: 29 },
  { name: '柏三十二', gender: '男', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:16:15', rank: 30 },
  { name: '水三十三', gender: '女', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:29:20', rank: 31 },
  { name: '窦三十四', gender: '女', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:21:50', rank: 32 },
  { name: '章三十五', gender: '男', race: '上海市全程马拉松', type: '全程马拉松', netTime: '02:22:40', rank: 33 },
  { name: '云三十六', gender: '男', race: '北京市半程马拉松', type: '半程马拉松', netTime: '01:13:20', rank: 34 },
   ];
  
      // 初始加载默认的比赛成绩
      this.applyChoose();
    },
    methods: {
      toggleMarathonType() {
        this.showMarathonType = !this.showMarathonType;
        this.showGender = false;
  
        if (this.showMarathonType) {
          document.addEventListener('click', this.closeDropdown);
        } else {
          document.removeEventListener('click', this.closeDropdown);
        }
      },
      toggleGender() {
        this.showGender = !this.showGender;
        this.showMarathonType = false;
  
        if (this.showGender) {
          document.addEventListener('click', this.closeDropdown);
        } else {
          document.removeEventListener('click', this.closeDropdown);
        }
      },
      toggleSort() {
        this.sortGrade = !this.sortGrade;
        this.showMarathonType = false;
        this.showGender = false;
  
        if (this.sortGrade) {
          document.addEventListener('click', this.closeDropdown);
        } else {
          document.removeEventListener('click', this.closeDropdown);
        }
      },
      closeDropdown(event) {
        const typeButton = this.$refs.typeButton;
        const genderButton = this.$refs.genderButton;
  
        if (!typeButton.contains(event.target) && !genderButton.contains(event.target)) {
          this.showMarathonType = false;
          this.showGender = false;
          document.removeEventListener('click', this.closeDropdown);
        }
      },
      chooseMarathonType(type) {
        this.selectedMarathonType = type;
        this.marathonType = type;
        this.showMarathonType = false;
        this.applyChoose();
      },
      chooseGender(gender) {
        this.selectedGender = gender;
        this.gender = gender;
        this.showGender = false;
        this.applyChoose();
      },
      chooseSortType(sortType) {
        this.selectedSortType = sortType;
        this.sortGrade = false;
        this.applySort();
      },
      applyChoose() {
        // 根据筛选条件过滤和排序
        let filteredResults = this.results;
  
        // 根据马拉松类型筛选
        if (this.marathonType && this.marathonType !== '全部') {
          filteredResults = filteredResults.filter(result => result.type === this.marathonType);
        }
  
        // 根据性别筛选
        if (this.gender && this.gender !== '全部') {
          filteredResults = filteredResults.filter(result => result.gender === this.gender);
        }
  
        // 根据其他条件继续筛选...
  
        // 按照净成绩排序，默认升序
        filteredResults.sort((a, b) => {
          // 根据具体的比较逻辑排序
          return a.netTime - b.netTime; // 假设 netTime 是以秒为单位的数字
        });
  
        // 更新成绩排行榜数据
        this.filteredResults = filteredResults;
  
        // 更新排名
        this.updateRanking();
      },

      applySort() {
  let sortedResults = this.results.slice(); // 复制一份原始数据以免修改原始数据

  // 根据净成绩排序
  if (this.sortType === '顺序排列') {
    sortedResults.sort((a, b) => {
      return this.convertTimeToSeconds(a.netTime) - this.convertTimeToSeconds(b.netTime);
    });
  } else if (this.sortType === '逆序排列') {
    sortedResults.sort((a, b) => {
      return this.convertTimeToSeconds(b.netTime) - this.convertTimeToSeconds(a.netTime);
    });
  }
  this.filteredResults = sortedResults;
  this.updateRanking();
},

updateRanking() {
  this.filteredResults.forEach((result, index) => {
    result.rank = index + 1;
  });
},

      search() {
        // 执行搜索逻辑
        console.log(`搜索查询：${this.searchQuery}`);
      },
      convertTimeToSeconds(time) {
  const [hours, minutes, seconds] = time.split(':').map(Number);
  return hours * 3600 + minutes * 60 + seconds;
},


    },
  };
  </script>
  
  <style scoped>
  .topBar {
    background-color: #c81623;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 10px;
    position: fixed;
    top: 0;
    width: 100%;
    z-index: 1000;
  }
  
  .back {
    margin-right: auto;
    color: white;
    cursor: pointer;
    text-decoration: none;
    margin-top: 15px;
    font-size: 24px;
  }
  
  .nav_link {
    color: white;
  }
  
  .logo {
    margin-right: 20px;
  }
  
  .search_container {
    flex-grow: 1;
    display: flex;
    justify-content: center;
    margin-right: 120px;
  }
  
  .search_form {
    display: flex;
    align-items: center;
  }
  
  .search_input {
    padding: 8px;
    border: none;
    height: 30px;
    border-radius: 5px;
    font-size: 15px;
    border: 1px solid grey;
  }
  
  .search_button {
    padding: 8px 15px;
    background-color: rgb(250, 242, 242);
    border: none;
    height: 48px;
    border-radius: 5px;
    cursor: pointer;
    font-size: 15px;
  }
  
  .filter-container {
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 10px 0;
  position: fixed;
  top: 60px; /* Adjust top position according to your design */
  width: 100%;
  background: white;
  padding: 15px 0;
  z-index: 999;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  gap: 50px;
}

.typeButton,
.genderButton,
.sort-button {
  cursor: pointer;
  position: relative;
  background-color: #c81623;
  color: white;
  padding: 10px 15px;
  border-radius: 5px;
  margin-right: 20px;
  border: none;
  transition: background-color 0.3s ease;
}

.typeButton:hover,
.genderButton:hover,
.sort-button:hover {
  background-color: #a10e1b;
}

.dropdown {
  display: none;
  position: absolute;
  top: 100%;
  left: 0;
  background-color: white;
  color: black;
  width: 110px;
  z-index: 1000;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  list-style: none;
  padding: 0;
  margin: 0;
}

.dropdown.show {
  display: block;
}

.dropdown li {
  padding: 10px 15px;
  border-bottom: 1px solid #ccc;
  background: white;
  text-align: left;
  cursor: pointer;
}

.dropdown li:hover {
  background-color: #f1f1f1;
}

  .rank_header {
  display: flex;
  justify-content: center; 
  align-items: center; 
  gap: 210px; 
  height: 50px; 
  font-size: 30px;
  position: fixed; 
  top: 130px;
  width: 100%; 
  background-color: white;
  padding: 10px 0; 
  z-index: 998; 
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}


  .rank {
    max-height: calc(100vh - 200px); /* Adjust height based on your design */
    overflow-y: auto;
    text-align: center;
    margin-top: 200px; /* Ensure race results start below filter-container and rank_header */
    scrollbar-width: 10;
    scrollbar-color: #888 transparent;
  }
  
  .rank::-webkit-scrollbar {
    width: 6px;
  }
  
  .rank::-webkit-scrollbar-track {
    background: transparent;
  }
  
  .rank::-webkit-scrollbar-thumb {
    background-color: #888;
    border-radius: 10px;
    border: 2px solid transparent;
  }
  
  .rank::-webkit-scrollbar-thumb:hover {
    background-color: #555;
  }
  
  .result_item {
    padding: 10px 20px;
    border: 1px solid #ddd;
    border-radius: 4px;
    height: 33px;
    font-size: 16px;
    font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
  
  .result_item:nth-child(even) {
    background-color: #f9f9f9;
  }
  
  .result_item:nth-child(odd) {
    background-color: #ffffff;
  }
  
  .result_item .highlight {
    font-weight: bold;
    color: #c81623;
    margin-right: 10px;
  }
  </style>
  