<template>
  <div id="PackageDetail">
    <el-dialog title="物资详情" :visible.sync="dialogVisible" width="40%" class="PackageDetailDialog">
      <div style="display: flex;width: 48%;">
        <el-table :data="packages">
          <el-table-column prop="type" label="参赛物资"></el-table-column>
        </el-table>
      </div>
      <div style="display: flex;width: 48%;">
        <el-table :data="packages">
          <el-table-column prop="type" label="完赛物资"></el-table-column>
        </el-table>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getPackageDetails } from '@/api/PackageDetails';

export default {
  name: 'PackageDetail',
  data() {
    return {
      packages: [{
        type: '公路',
      }, {
        type: '公路',
      }],
      dialogVisible: true
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
    },
    handleClose() {
      this.$emit('close');
    }
  }
};
</script>

<style>
#PackageDetail .el-dialog__body {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
</style>