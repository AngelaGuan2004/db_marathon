<template>
  <div class="details-container">
    <h2>接驳车详情</h2>
    <el-table :data="shuttles">
      <el-table-column prop="departureTime" label="出发时间"></el-table-column>
      <el-table-column prop="arrivalTime" label="到达时间"></el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getShuttleDetails } from '@/api/ShuttleDetails';

export default {
  data() {
    return {
      shuttles: []
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
    }
  }
};
</script>

<style>
.details-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 30px;
  background-color: white;
  border: 1px solid grey;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}
</style>
