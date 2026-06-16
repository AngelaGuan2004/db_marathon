<template>
  <div class="details-container">
    <h2>医疗点详情</h2>
    <el-table :data="medicalPoints">
      <el-table-column prop="location" label="医疗点位置"></el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getMedicalDetails } from '@/api/MedicalDetails';

export default {
  data() {
    return {
      medicalPoints: []
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
