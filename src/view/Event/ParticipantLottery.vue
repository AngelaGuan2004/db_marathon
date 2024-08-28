<template>
  <div id="ParticipantLottery">
    <el-main class="ParticipantLotteryContent">
      <!-- <div class="right-menu">
              <el-button class="custom-button" icon="el-icon-arrow-left" @click="goBack">返回</el-button>
            </div> -->
      <div class="ParticipantLotteryInput">
        <el-input v-model="totalParticipants" placeholder="请输入总人数" class="InputTotalParticipants"></el-input>
        <el-button type="primary" @click="drawLots">抽签</el-button>
      </div>
      <div v-if="paginatedParticipants.length > 0">
        <el-table :data="paginatedParticipants" class="ParticipantLotteryTable">
          <el-table-column prop="id" label="ID" width="100"></el-table-column>
          <el-table-column prop="name" label="姓名" width="120"></el-table-column>
          <el-table-column prop="sex" label="性别" width="100"></el-table-column>
          <el-table-column prop="age" label="年龄" width="100"></el-table-column>
          <el-table-column prop="role" label="参赛身份" width="150"></el-table-column>
          <el-table-column prop="state" label="报名状态" width="120"></el-table-column>
          <el-table-column prop="number" label="参赛号码" width="150"></el-table-column>
        </el-table>
        <el-pagination class="Pagination" background layout="prev, pager, next" :total="totalParticipantsCount"
          :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
      </div>
      <div v-else class="ParticipantLotteryTableEmpty">
        暂无数据
      </div>
    </el-main>
  </div>
</template>

<script>
export default {
  name: 'ParticipantLottery',
  data() {
    return {
      totalParticipants: '',
      participants: [{ "id": 1, "name": "张三", "sex": "男", "age": 28, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 2, "name": "李四", "sex": "女", "age": 32, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 3, "name": "王五", "sex": "男", "age": 45, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 4, "name": "赵六", "sex": "女", "age": 22, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 5, "name": "孙七", "sex": "男", "age": 31, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 6, "name": "周八", "sex": "女", "age": 29, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 7, "name": "吴九", "sex": "男", "age": 37, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 8, "name": "郑十", "sex": "女", "age": 26, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 9, "name": "王十一", "sex": "男", "age": 34, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 10, "name": "陈十二", "sex": "女", "age": 30, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 11, "name": "蒋十三", "sex": "男", "age": 40, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 12, "name": "沈十四", "sex": "女", "age": 27, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 13, "name": "韩十五", "sex": "男", "age": 33, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 14, "name": "杨十六", "sex": "女", "age": 38, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 15, "name": "朱十七", "sex": "男", "age": 25, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 16, "name": "秦十八", "sex": "女", "age": 42, "role": "普通跑者", "state": "待抽签", "number": "-" },
      { "id": 17, "name": "尤十九", "sex": "男", "age": 29, "role": "慈善跑者", "state": "-", "number": "-" },
      { "id": 18, "name": "许二十", "sex": "女", "age": 36, "role": "慈善跑者", "state": "-", "number": "-" }],
      currentPage: 1,
      pageSize: 8,
    }
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
    },
    handlePageChange(page) {
      this.currentPage = page;
    },
  }
}
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/ParticipantLottery.css";
</style>