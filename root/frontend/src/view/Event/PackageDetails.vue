<template>
  <div class="details-container">
    <h2>物资详情</h2>
    <el-table :data="packages">
      <el-table-column prop="type" label="物资类型"></el-table-column>
    </el-table>
    <el-table :data="packages">
      <el-table-column prop="type" label="参赛物资"></el-table-column>
    </el-table>
    <el-table :data="packages">
      <el-table-column prop="type" label="完赛物资"></el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getPackageDetails } from '@/api/PackageDetails';

export default {
  data() {
    return {
      packages: []
    };
  },
  created() {
    this.loadPackageDetails();
  },
  methods: {
    loadPackageDetails() {
      const eventId = this.$route.params.id;
      getPackageDetails(eventId)
        .then(response => {
          this.packages = response.data;
        })
        .catch(error => {
          console.error('Error loading package details:', error);
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
