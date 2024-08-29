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
export default {
  name: 'MedicalManagement',
  data() {
    return {
      currentPage: 1,
      pageSize: 8,
      medicalPoint: [],
      selectedMedicalPoint: null,
      SelectedVolunVisible: false,
      SelectionVolunVisible: false,
      totalMedicalCount: 0
    };
  },
  computed: {
    paginatedMedicalPoint() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.medicalPoint.slice(start, end);
    },
    selectedMedicalVolun() {
      return this.$store.getters.getMedicalPointVolunteers(this.selectedMedicalPoint?.id || "");
    },
    selectionMedicalVolun() {
      return this.$store.getters.getMedicalVolunteers;
    }
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    showSelectedMedical(row) {
      this.selectedMedicalPoint = row;
      this.SelectedVolunVisible = true;
    },
    showSelectionMedical(row) {
      this.selectedMedicalPoint = row;
      this.SelectionVolunVisible = true;
    },
    submitMedicalVolun(volunteer) {
      // 将志愿者从待排班志愿者中移除，并添加到对应补给点的已排班志愿者列表中
      const newMedicalVolunteers = this.selectionMedicalVolun.filter(v => v.id !== volunteer.id);
      const updatedVolunteers = [...this.selectedMedicalVolun, volunteer];

      // 更新 Vuex Store 中的志愿者数据
      this.$store.dispatch('saveSelectedVolun', {
        pointId: this.selectedMedicalPoint.id,
        supply: [],
        medical: updatedVolunteers,
        car: []
      });
      this.$store.commit('SET_MEDICAL_VOLUNTEERS', newMedicalVolunteers);
    }
  },
  created() {
    // 补给点数据从服务器获取（这里简化为从store中取）
    this.medicalPoint = [
      { id: "1", place: "赛道起始处" },
      { id: "2", place: "赛道5公里处前100米" },
      { id: "3", place: "赛道终点处" }
    ];
    this.totalMedicalCount = this.medicalPoint.length; // 设置总数量
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/PacerAndEmergencyTable.css";

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