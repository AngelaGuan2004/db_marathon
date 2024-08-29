<template>
  <div id="SupplypointDetail">
    <el-dialog title="补给点详情" :visible.sync="dialogVisible" width="40%" :before-close="handleClose">
      <el-table :data="supplypoints">
        <el-table-column prop="location" label="补给点地点"></el-table-column>
        <el-table-column prop="type" label="补给点类型"></el-table-column>
      </el-table>
    </el-dialog>
  </div>
</template>

<script>
import { getSupplypointDetails } from '@/api/SupplypointDetails';

export default {
  name: 'SupplypointDetail',
  data() {
    return {
      supplypoints: [{
        location: '2024拉萨半程马拉松',
        type: '公路',
      }, {
        location: '2024拉萨半程马拉松',
        type: '公路',
      }],
      dialogVisible: true
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

<style></style>