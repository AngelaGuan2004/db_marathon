<template>
  <div id="MedicalManagement">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content">
        <div v-if="paginatedMedicalPoint.length > 0">
          <el-table :data="paginatedMedicalPoint" class="Table">
            <el-table-column prop="id" label="ID" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="place" label="地点" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="selected" label="已排班志愿者" width="200" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button @click="showSelectedMedical(scope.row)">已排班名单</el-button>
              </template>
            </el-table-column>
            <el-table-column prop="selection" label="待排班志愿者" width="200" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button @click="showSelectionMedical(scope.row)">待排班名单</el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="pagination" background layout="prev, pager, next" :total="totalMedicalCount"
            :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
        </div>
        <div v-else class="Empty">
          暂无数据
        </div>
      </el-main>
      <el-dialog :visible.sync="SelectedVolunVisible" :title="`${selectedMedicalPoint?.id || ''}号医疗点志愿者名单`" width="50%">
        <div v-if="selectedMedicalVolun.length > 0">
          <el-table :data="selectedMedicalVolun" class="Table">
            <el-table-column prop="id" label="ID" width="100" align="center" header-align="center"></el-table-column>
            <el-table-column prop="name" label="姓名" width="100" align="center" header-align="center"></el-table-column>
            <el-table-column prop="telenumber" label="电话" width="200" align="center"
              header-align="center"></el-table-column>
          </el-table>
        </div>
        <div v-else class="Empty">
          暂无数据
        </div>
        <span slot="footer" class="dialog-footer">
          <el-button @click="SelectedVolunVisible = false">关闭</el-button>
        </span>
      </el-dialog>
      <el-dialog :visible.sync="SelectionVolunVisible" title="待排班志愿者名单" width="50%">
        <div v-if="selectionMedicalVolun.length > 0">
          <el-table :data="selectionMedicalVolun" class="Table">
            <el-table-column prop="id" label="ID" width="100" align="center" header-align="center"></el-table-column>
            <el-table-column prop="name" label="姓名" width="100" align="center" header-align="center"></el-table-column>
            <el-table-column prop="telenumber" label="电话" width="200" align="center"
              header-align="center"></el-table-column>
            <el-table-column label="选择" width="100" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button type="primary" @click="submitMedicalVolun(scope.row)">提交</el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
        <div v-else class="Empty">
          暂无数据
        </div>
        <span slot="footer" class="dialog-footer">
          <el-button @click="SelectionVolunVisible = false">关闭</el-button>
        </span>
      </el-dialog>
    </div>
  </div>
</template>

<script>
import { getAllMedicalPoints, getVolunteersByEventId, acquireVolunteerInformation, addVolunteerToMedicalPoint, getMedicalVolunteers } from '@/api/volunteer';
export default {
  data() {
    return {
      currentPage: 1,
      pageSize: 8,
      medicalPoint: [],  // Initialize as an empty array
      selectedMedicalPoint: null,
      SelectedVolunVisible: false,
      SelectionVolunVisible: false,
      selectedMedicalVolun: [],
      selectionMedicalVolun: [],
      totalMedicalCount: 0
    };
  },
  computed: {
    paginatedMedicalPoint() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.medicalPoint.slice(start, end);
    }
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    async loadMedicalPoints() {
      try {
        const eventId = "10001"; // Replace with your Event ID
        const response = await getAllMedicalPoints(eventId);
        console.log(response);
        this.medicalPoint = Array.isArray(response.data) ? response.data : [];
        this.totalMedicalCount = this.medicalPoint.length;
      } catch (error) {
        console.error('Failed to load medical points:', error);
        this.$message.error('加载医疗点信息失败，请稍后重试。');
      }
    },
    async showSelectionMedical(row) {
      try {
        const eventId = "10001"; // Replace with your Event ID
        this.selectedMedicalPoint = row;
        this.SelectionVolunVisible = true;

        // Get all volunteers
        const volunteers = await getVolunteersByEventId(eventId);
        const selectionVolunteers = [];

        // Filter out volunteers already assigned to medical points
        for (const volunteer of volunteers) {
          const info = await acquireVolunteerInformation(volunteer.id, eventId);
          if (info.job_category === '医疗点' && !info.is_scheduled) {
            selectionVolunteers.push(volunteer);
          }
        }
        this.selectionMedicalVolun = selectionVolunteers.map(volunteer => ({
          ...volunteer,
          telenumber: volunteer.telephone_Number,
        }));
      } catch (error) {
        console.error('Failed to load selected volunteers:', error);
        this.$message.error('加载待排班志愿者失败，请稍后重试。');
      }
    },
    async showSelectedMedical(row) {
      try {
        const eventId = "10001";
        const medicalPointId = row.id;
        const response = await getMedicalVolunteers(eventId, medicalPointId);

        this.selectedMedicalVolun = response.map(volunteer => ({
          id: volunteer.id,
          name: volunteer.name,
          telenumber: volunteer.telephone_Number
        }));

        this.selectedMedicalPoint = row;
        this.SelectedVolunVisible = true;
      } catch (error) {
        console.error('Failed to load selected volunteers:', error);
        this.$message.error('加载已排班志愿者失败，请稍后重试。');
      }
    },
    async submitMedicalVolun(volunteer) {
      try {
        const data = {
          volunteer_id: volunteer.id,
          medicalpoint_id: this.selectedMedicalPoint.id
        };
        console.log(data);
        await addVolunteerToMedicalPoint(data);
        this.$message.success('志愿者已成功添加到医疗点');

        // Remove the volunteer from the available list
        this.selectionMedicalVolun = this.selectionMedicalVolun.filter(v => v.id !== volunteer.id);
      } catch (error) {
        console.error('Failed to add volunteer to medical point:', error);
        this.$message.error('添加志愿者到医疗点失败，请稍后重试。');
      }
    }
  },
  created() {
    this.loadMedicalPoints();
  }
};
</script>

<style scoped>
#MedicalManagement {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: flex-end;
  background-color: white;
  margin-right: 3%;
  width: 67%;
  font-size: 15px;
  padding-top: 20px;
}
</style>
