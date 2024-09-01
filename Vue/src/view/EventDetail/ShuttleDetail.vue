<template>
  <div id="ShuttleDetail">
    <el-dialog title="接驳车详情" :visible.sync="dialogVisible" width="40%" :before-close="handleClose">
      <el-table :data="shuttles">
        <el-table-column prop="departure_Time" label="出发时间"></el-table-column>
        <el-table-column prop="arrival_Time" label="到达时间"></el-table-column>
      </el-table>
    </el-dialog>
  </div>
</template>

<script>
import { getShuttleDetails } from '@/api/ShuttleDetails'; // 引入你的API函数

export default {
  name: 'ShuttleDetail',
  data() {
    return {
      shuttles: [], // 初始化为空数组
      dialogVisible: true,
    };
  },
  props: ['eventId'],
  mounted() {
    this.loadShuttleDetails(); // 在组件挂载时加载接驳车数据
  },
  methods: {
    async loadShuttleDetails() {
      const eventId = this.eventId; // 暂时指定为 10001
      try {
        console.log('发起请求 - Event ID:', eventId);
        const res = await getShuttleDetails(eventId);
        console.log('接口响应:', res);

        if (res) {
          this.shuttles = res; // 将API返回的数据赋值给shuttles数组
        } else {
          throw new Error('未收到有效响应数据');
        }
      } catch (error) {
        console.error('加载接驳车详情失败:', error);
        alert('加载接驳车详情失败');
      }
    },
    handleClose() {
      this.$emit('close');
    },
  },
};
</script>

<style scoped></style>