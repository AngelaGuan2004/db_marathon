<template>
  <div id="MedicalDetail">
    <el-dialog title="医疗点详情" :visible.sync="dialogVisible" width="40%" :before-close="handleClose">
      <el-table :data="medicalPoints">
        <el-table-column prop="location" label="医疗点位置"></el-table-column>
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
      medicalPoints: [{
        location: '2024拉萨半程马拉松',
      }, {
        location: '2024拉萨半程马拉松',
      }],
      dialogVisible: true
    };
  },
  created() {
    this.loadMedicalDetails();
  },
  methods: {
    loadMedicalDetails() {
      const eventId = this.$route.params.id;
      getMedicalDetails(eventId)
        .then(response => {
          this.medicalPoints = response.data;
        })
        .catch(error => {
          console.error('Error loading medical details:', error);
        });
    }
  }
};
</script>

<style></style>