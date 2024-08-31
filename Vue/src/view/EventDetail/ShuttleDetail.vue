<template>
  <div id="ShuttleDetail">
    <el-dialog title="接驳车详情" :visible.sync="dialogVisible" width="40%">
      <el-table :data="shuttles">
        <el-table-column prop="departureTime" label="出发时间"></el-table-column>
        <el-table-column prop="arrivalTime" label="到达时间"></el-table-column>
      </el-table>
    </el-dialog>
  </div>
</template>

<script>
import { getShuttleDetails } from '@/api/ShuttleDetails';

export default {
  name: 'ShuttleDetail',
  data() {
    return {
      shuttles: [{
        departureTime: '123546',
        arrivalTime: '123456'
      }, {
        departureTime: '123546',
        arrivalTime: '123456'
      }],
      dialogVisible: true
    };
  },
  created() {
    this.loadShuttleDetails();
  },
  methods: {
    loadShuttleDetails() {
      const eventId = this.$route.params.id;
      getShuttleDetails(eventId)
        .then(response => {
          this.shuttles = response.data;
        })
        .catch(error => {
          console.error('Error loading shuttle details:', error);
        });
    },
    handleClose() {
      this.$emit('close');
    }
  }
};
</script>

<style></style>