<template>
  <div class="registration-container">
    <h2>赛事报名</h2>
    <el-form :model="participant" label-width="120px">
      <el-form-item label="选手姓名">
        <el-input v-model="participant.name"></el-input>
      </el-form-item>
      <el-form-item label="选手性别">
        <el-select v-model="participant.gender" placeholder="请选择">
          <el-option label="男" value="男"></el-option>
          <el-option label="女" value="女"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="选手年龄">
        <el-input v-model="participant.age" type="number"></el-input>
      </el-form-item>
      <el-form-item label="选手身份证号">
        <el-input v-model="participant.idCard"></el-input>
      </el-form-item>
      <el-form-item label="地区">
        <el-input v-model="participant.region"></el-input>
      </el-form-item>
      <el-form-item label="联系方式">
        <el-input v-model="participant.contact"></el-input>
      </el-form-item>
    </el-form>
    <div class="button-group">
      <button type="primary" @click="modifyInfo">修改信息</button>
      <button type="success" @click="submitRegistration">提交报名</button>
    </div>
  </div>
</template>

<script>
import { getParticipantInfo, registerParticipant } from '@/api/EventRegistration';

export default {
  data() {
    return {
      participant: {
        name: '刘文美',
        gender: '女',
        age: '20',
        idCard: '',
        region: '',
        contact: ''
      }
    };
  },
  created() {
    this.loadParticipantInfo();
  },
  methods: {
    loadParticipantInfo() {
      const eventId = this.$route.params.id;
      getParticipantInfo(eventId)
        .then(response => {
          this.participant = response.data;
        })
        .catch(error => {
          console.error('Error loading participant info:', error);
          this.$message.error('加载选手信息失败');
        });
    },
    modifyInfo() {
      this.$message.info('信息已修改');
    },
    submitRegistration() {
      const eventId = this.$route.params.id;
      registerParticipant(eventId, this.participant)
        .then(() => {
          this.$message.success('报名成功');
        })
        .catch(error => {
          console.error('Error submitting registration:', error);
          this.$message.error('报名失败');
        });
    }
}

};
</script>

<style>
.registration-container {
  max-width: 600px;
  margin: 0 auto;
  padding: 30px;
  background-color: white;
  border: 1px solid grey;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}
.button-group {
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
}
button{
  background-color: rgb(201, 64, 64);
}

</style>
