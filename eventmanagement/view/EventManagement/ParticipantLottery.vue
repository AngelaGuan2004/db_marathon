<template>
  <div id="participant_lottery">
    <el-header>
      <el-menu class="el-menu-demo" mode="horizontal" background-color="#c81623" text-color="#666" active-text-color="#ffd04b" :default-active="activeMenu">
        <el-menu-item index="1">首页</el-menu-item>
        <el-menu-item index="2">赛历</el-menu-item>
      </el-menu>
    </el-header>
    <div class="content-container">
      <div id="div-left">
        <div class="break"></div>
        <div>
            <button :class="checkClass(0)" @click="navigateTo('ParticipantLottery')">选手抽签</button>
          </div>
          <div>
            <button :class="checkClass(1)" @click="togglePacerSelection">配速员选拔</button>
          </div>
          <div v-if="showPacerOptions">
            <button class="sub-button" @click="navigateTo('PacerSelection')">待选跑者</button>
            <button class="sub-button" @click="navigateTo('PacerSubmission')">已选跑者</button>
          </div>
          <div>
            <button :class="checkClass(2)" @click="toggleEmergencyRunnerSelection">急救跑者选拔</button>
          </div>
          <div v-if="showEmergencyRunnerOptions">
            <button class="sub-button" @click="navigateTo('EmergencyRunnerSelection')">待选跑者</button>
            <button class="sub-button" @click="navigateTo('EmergencyRunnerSubmission')">已选跑者</button>
          </div>
      </div>
      <el-main class="main-content">
        <div class="right-menu">
          <el-button class="custom-button" icon="el-icon-arrow-left" @click="goBack">返回</el-button>
        </div>
        <div class="input-container">
          <el-input v-model="totalParticipants" placeholder="请输入总人数" class="input-total-participants"></el-input>
          <el-button type="primary" @click="drawLots">抽签</el-button>
        </div>
        <div v-if="paginatedParticipants.length > 0">
          <el-table :data="paginatedParticipants" class="participant-lottery-table" style="width: 100%">
            <el-table-column prop="id" label="ID" width="100"></el-table-column>
            <el-table-column prop="name" label="姓名" width="100"></el-table-column>
            <el-table-column prop="sex" label="性别" width="100"></el-table-column>
            <el-table-column prop="age" label="年龄" width="100"></el-table-column>
            <el-table-column prop="role" label="参赛身份" width="100"></el-table-column>
            <el-table-column prop="state" label="报名状态" width="100"></el-table-column>
            <el-table-column prop="number" label="参赛号码" width="100"></el-table-column>
          </el-table>
          <el-pagination
            class="pagination"
            background
            layout="prev, pager, next"
            :total="totalParticipantsCount"
            :page-size="pageSize"
            @current-change="handlePageChange"
          ></el-pagination>
        </div>
        <div v-else class="custom-empty">
          暂无数据
        </div>
      </el-main>
    </div>
  </div>
</template>


<script>
export default {
  data() {
    return {
      activeMenu: '1',
      activeNav: 0,
      totalParticipants: '',
      showEmergencyRunnerOptions: false,
      showPacerOptions: false,
      participants: [{"id":1,"name":"张三","sex":"男","age":28,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":2,"name":"李四","sex":"女","age":32,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":3,"name":"王五","sex":"男","age":45,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":4,"name":"赵六","sex":"女","age":22,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":5,"name":"孙七","sex":"男","age":31,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":6,"name":"周八","sex":"女","age":29,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":7,"name":"吴九","sex":"男","age":37,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":8,"name":"郑十","sex":"女","age":26,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":9,"name":"王十一","sex":"男","age":34,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":10,"name":"陈十二","sex":"女","age":30,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":11,"name":"蒋十三","sex":"男","age":40,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":12,"name":"沈十四","sex":"女","age":27,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":13,"name":"韩十五","sex":"男","age":33,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":14,"name":"杨十六","sex":"女","age":38,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":15,"name":"朱十七","sex":"男","age":25,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":16,"name":"秦十八","sex":"女","age":42,"role":"普通跑者","state":"待抽签","number":"-"},
  {"id":17,"name":"尤十九","sex":"男","age":29,"role":"慈善跑者","state":"-","number":"-"},
  {"id":18,"name":"许二十","sex":"女","age":36,"role":"慈善跑者","state":"-","number":"-"}],
      currentPage: 1,
      pageSize: 8,
    };
  },
  computed: {
    totalParticipantsCount() {
      return this.participants.length;
    },
    paginatedParticipants() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.participants.slice(start, end);
    },
    allParticipantsDrawn() {
      return this.participants.every(p => p.state !== '待抽签');
    }
  },
  methods: {
    checkClass(index) {
      return index === this.activeNav ? 'active' : 'inactive';
    },
    navigateTo(page) {
      if (page === 'ParticipantLottery') {
        this.activeNav = 0; // 保持当前页面，标志为红色
      } else if (this.allParticipantsDrawn) {
        if (page === 'PacerSelection' || page === 'PacerSubmission') {
          this.activeNav = 1;
          this.showPacerOptions =true;
          this.$router.push({ name: page });
        } else  if (page === 'EmergencyRunnerSelection' || page === 'EmergencyRunnerSubmission') {
          this.activeNav = 2;
          this.showEmergencyRunnerOptions =true;
          this.$router.push({ name: page });
      } 
      }
      else{
        this.$message.error('请先进行抽签');
      }
    },
    togglePacerSelection() {
      this.showPacerOptions = !this.showPacerOptions;
    },
    toggleEmergencyRunnerSelection() {
      this.showEmergencyRunnerOptions = !this.showEmergencyRunnerOptions;
    },
    handlePageChange(page) {
      this.currentPage = page;
    },
    goBack() {
      this.$router.push({ name: 'EventList' });
    },
    drawLots() {
      const total = parseInt(this.totalParticipants);
      if (isNaN(total) || total <= 0) {
        this.$message.error('请输入有效的总人数');
        return;
      }

      const lotteryParticipants = this.participants.filter(p => p.role !== '慈善跑者' && p.state === '待抽签');
      const charityParticipants = this.participants.filter(p => p.role === '慈善跑者');

      if (total > lotteryParticipants.length) {
        this.$message.error('输入的总人数超过了待抽签的选手数量');
        return;
      }

      // Randomly shuffle the array
      for (let i = lotteryParticipants.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [lotteryParticipants[i], lotteryParticipants[j]] = [lotteryParticipants[j], lotteryParticipants[i]];
      }

      const selectedParticipants = lotteryParticipants.slice(0, total);
      const unselectedParticipants = lotteryParticipants.slice(total);
      const totalParticipants = selectedParticipants.length + charityParticipants.length;

      // Generate unique race numbers and distribute them evenly among letters
      const letters = ['A', 'B', 'C', 'D', 'E'];
      const letterCounts = { 'A': 0, 'B': 0, 'C': 0, 'D': 0, 'E': 0 };
      const totalLetters = letters.length;
      const maxPerLetter = Math.floor(totalParticipants / totalLetters);
      const usedNumbers = new Set();

      const generateUniqueNumber = (letter) => {
        let uniqueNumber;
        do {
          const randomNumber = Math.floor(1000 + Math.random() * 9000);
          uniqueNumber = `${letter}${randomNumber}`;
        } while (usedNumbers.has(uniqueNumber));
        usedNumbers.add(uniqueNumber);
        return uniqueNumber;
      };

      const assignNumberToParticipant = (participant) => {
        let assigned = false;
        // Try to assign letters evenly
        for (const letter of letters) {
          if (letterCounts[letter] < maxPerLetter) {
            participant.number = generateUniqueNumber(letter);
            letterCounts[letter]++;
            assigned = true;
            break;
          }
        }
        // If no letter is available, assign the least used letter
        if (!assigned) {
          const minUsedLetter = letters.reduce((prev, curr) => letterCounts[prev] < letterCounts[curr] ? prev : curr);
          participant.number = generateUniqueNumber(minUsedLetter);
          letterCounts[minUsedLetter]++;
        }
      };

      selectedParticipants.forEach((p) => {
        p.state = '已中签';
        assignNumberToParticipant(p);
      });

      unselectedParticipants.forEach((p) => {
        p.state = '未中签';
        p.number = '-';
      });

      charityParticipants.forEach(p => {
        assignNumberToParticipant(p);
      });

      this.$message.success('抽签完成');
    }
  }
};
</script>

<style>
#participant_lottery {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  display: flex;
  flex-direction: column;
}

.content-container {
  display: flex;
  flex: 1;
  background-color: #f4f4f6;
  overflow: hidden;
}

#div-left {
  width: 200px;
  background-color: white;
  box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
  overflow: hidden; 
  height:580px; 
}

#div-left button {
  width: 100%;
  margin-top: 10px;
  padding: 10px;
  font-size: 16px;
  background-color: white;
  border: none;
  cursor: pointer;
}

#div-left button:hover {
  background-color: #f5f5f5;
}

#div-left button.active {
  color: #c81623;
  background-color: #fff0f0;
}

.main-content {
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}


.input-container {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 10px;
  margin: 20px auto;
  width: 50%;
}

.input-total-participants {
  flex-grow: 1;
}

.right-menu {
  display: flex;
  justify-content: flex-start; /* 将内容对齐到左边 */
  align-items: center; /* 垂直居中对齐 */
  padding: 10px;
}

.custom-button {
  margin-right: auto; /* 确保按钮在容器的左侧 */
}


.participant-lottery-table {
  width: 100%;
  table-layout: fixed;
  border: none;
}

.participant-lottery-table th .cell,
.participant-lottery-table td .cell {
  display: inline-block;
  width: 100%;
  text-align: center;
  height: 20px; /* Adjust the height as needed */
  border: none;
  vertical-align: middle; /* 垂直居中 */
  line-height: 20px; /* 确保行高与单元格高度一致 */
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

.custom-empty {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 200px;
  color: #999;
  width: 100%;
  text-align: center;
}
</style>

