<template>
  <div id="MedicalDetail">
    <el-dialog title="医疗点详情" :visible.sync="dialogVisible" width="40%" :before-close="handleClose">
      <el-table :data="medicalPoints">
        <el-table-column prop="place" label="医疗点位置"></el-table-column>
      </el-table>
    </el-dialog>
  </div>
</template>

<script>
import { getMedicalDetails } from '@/api/MedicalDetails';

export default {
  name: 'MedicalDetail',
  data() {
    return {
      medicalPoints: [], // 初始化为空数组
      dialogVisible: true
    };
  },
  props: ['eventId'],
  created() {
    console.log(this.eventId)
    this.loadMedicalDetails(); // 在组件创建时加载医疗点数据
  },
  methods: {
    async loadMedicalDetails() {
      const eventId = this.eventId; // 从路由参数或默认值中获取eventId
      try {
        const response = await getMedicalDetails(eventId);
        if (response && response.data) {
          this.medicalPoints = response.data; // 将API返回的数据赋值给medicalPoints数组
        } else {
          this.$message.error('未收到有效响应数据');
          throw new Error('未收到有效响应数据');
        }
      } catch (error) {
        this.$message.error('加载医疗点详情失败');
        console.error('加载医疗点详情失败:', error);
      }
    },
    handleClose() {
      this.$emit('close');
    }
  }
};
</script>

<style scoped></style>
