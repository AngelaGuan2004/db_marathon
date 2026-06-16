<template>
  <div class="details-container">
    <h2>补给点详情</h2>
    <el-table :data="supplypoints">
      <el-table-column prop="location" label="补给点地点"></el-table-column>
      <el-table-column prop="type" label="补给点类型"></el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getSupplypointDetails } from '@/api/SupplypointDetails';

export default {
  data() {
    return {
      supplypoints: []
    };
  },
  created() {
    this.loadSupplypointDetails();
  },
  methods: {
    loadSupplypointDetails() {
      const eventId = this.$route.params.id;
      getSupplypointDetails(eventId)
        .then(response => {
          this.supplypoints = response.data;
        })
        .catch(error => {
          console.error('Error loading supplypoint details:', error);
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
