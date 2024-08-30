<template>
  <div id="CarManagement">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content">
        <div v-if="paginatedCarPoint.length > 0">
          <el-table :data="paginatedCarPoint" class="Table">
            <el-table-column prop="id" label="ID" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="departureTime" label="出发时间" width="150" align="center"
              header-align="center"></el-table-column>
            <el-table-column prop="arrivalTime" label="到达时间" width="150" align="center"
              header-align="center"></el-table-column>
            <el-table-column prop="selected" label="已排班志愿者" width="200" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button @click="showSelectedCar(scope.row)">已排班名单</el-button>
              </template>
            </el-table-column>
            <el-table-column prop="selection" label="待排班志愿者" width="200" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button @click="showSelectionCar(scope.row)">待排班名单</el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="Pagination" background layout="prev, pager, next" :total="totalCarCount"
            :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
        </div>
        <div v-else class="Empty">
          暂无数据
        </div>
      </el-main>
      <el-dialog :visible.sync="SelectedVolunVisible" :title="`${selectedCarPoint?.id || ''}号摆渡车志愿者名单`" width="50%">
        <div v-if="selectedCarVolun.length > 0">
          <el-table :data="selectedCarVolun" class="Table">
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
        <div v-if="selectionCarVolun.length > 0">
          <el-table :data="selectionCarVolun" class="Table">
            <el-table-column prop="id" label="ID" width="100" align="center" header-align="center"></el-table-column>
            <el-table-column prop="name" label="姓名" width="100" align="center" header-align="center"></el-table-column>
            <el-table-column prop="telenumber" label="电话" width="200" align="center"
              header-align="center"></el-table-column>
            <el-table-column label="选择" width="100" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button type="primary" @click="submitCarVolun(scope.row)">提交</el-button>
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
  name: 'CarManagement',
  data() {
    return {
      currentPage: 1,
      pageSize: 8,
      carPoint: [],
      selectedCarPoint: null,
      SelectedVolunVisible: false,
      SelectionVolunVisible: false,
      totalCarCount: 0
    };
  },
  computed: {
    paginatedCarPoint() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.carPoint.slice(start, end);
    },
    selectedCarVolun() {
      return this.$store.getters.getCarPointVolunteers(this.selectedCarPoint?.id || "");
    },
    selectionCarVolun() {
      return this.$store.getters.getCarVolunteers;
    }
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    showSelectedCar(row) {
      this.selectedCarPoint = row;
      this.SelectedVolunVisible = true;
    },
    showSelectionCar(row) {
      this.selectedCarPoint = row;
      this.SelectionVolunVisible = true;
    },
    submitCarVolun(volunteer) {
      // 将志愿者从待排班志愿者中移除，并添加到对应补给点的已排班志愿者列表中
      const newCarVolunteers = this.selectionCarVolun.filter(v => v.id !== volunteer.id);
      const updatedVolunteers = [...this.selectedCarVolun, volunteer];

      // 更新 Vuex Store 中的志愿者数据
      this.$store.dispatch('saveSelectedVolun', {
        pointId: this.selectedCarPoint.id,
        supply: [],
        medical: [],
        car: updatedVolunteers
      });
      this.$store.commit('SET_CAR_VOLUNTEERS', newCarVolunteers);
    }
  },
  created() {
    // 补给点数据从服务器获取（这里简化为从store中取）
    this.carPoint = [
      { id: "1", departureTime: "6:00", arrivalTime: "6:25" },
      { id: "2", departureTime: "6:10", arrivalTime: "6:35" },
      { id: "3", departureTime: "6:20", arrivalTime: "6:45" }
    ];
    this.totalCarCount = this.carPoint.length; // 设置总数量
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/PacerAndEmergencyTable.css";

#CarManagement {
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