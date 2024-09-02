<template>
  <div id="SupplyManagement">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content">
        <div v-if="paginatedSupplyPoint.length > 0">
          <el-table :data="paginatedSupplyPoint" class="Table">
            <el-table-column prop="id" label="ID" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="place" label="地点" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="type" label="类型" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="selected" label="已排班志愿者" width="200" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button @click="showSelectedSupply(scope.row)">已排班名单</el-button>
              </template>
            </el-table-column>
            <el-table-column prop="selection" label="待排班志愿者" width="200" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button @click="showSelectionSupply(scope.row)">待排班名单</el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="Pagination" background layout="prev, pager, next" :total="totalSupplyCount"
            :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
        </div>
        <div v-else class="Empty">
          暂无数据
        </div>
      </el-main>
      <el-dialog :visible.sync="SelectedVolunVisible" :title="`${selectedSupplyPoint?.id || ''}号补给点志愿者名单`" width="50%">
        <div v-if="selectedSupplyVolun.length > 0">
          <el-table :data="selectedSupplyVolun" class="Table">
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
        <div v-if="selectionSupplyVolun.length > 0">
          <el-table :data="selectionSupplyVolun" class="Table">
            <el-table-column prop="id" label="ID" width="100" align="center" header-align="center"></el-table-column>
            <el-table-column prop="name" label="姓名" width="100" align="center" header-align="center"></el-table-column>
            <el-table-column prop="telenumber" label="电话" width="200" align="center"
              header-align="center"></el-table-column>
            <el-table-column label="选择" width="100" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button type="primary" @click="submitSupplyVolun(scope.row)">提交</el-button>
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
  data() {
    return {
      currentPage: 1,
      pageSize: 8,
      supplyPoint: [],
      selectedSupplyPoint: null,
      SelectedVolunVisible: false,
      SelectionVolunVisible: false,
      totalSupplyCount: 0
    };
  },
  computed: {
    paginatedSupplyPoint() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.supplyPoint.slice(start, end);
    },
    selectedSupplyVolun() {
      return this.$store.getters.getSupplyPointVolunteers(this.selectedSupplyPoint?.id || "");
    },
    selectionSupplyVolun() {
      return this.$store.getters.getSupplyVolunteers;
    }
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    showSelectedSupply(row) {
      this.selectedSupplyPoint = row;
      this.SelectedVolunVisible = true;
    },
    showSelectionSupply(row) {
      this.selectedSupplyPoint = row;
      this.SelectionVolunVisible = true;
    },
    submitSupplyVolun(volunteer) {
      // 将志愿者从待排班志愿者中移除，并添加到对应补给点的已排班志愿者列表中
      const newSupplyVolunteers = this.selectionSupplyVolun.filter(v => v.id !== volunteer.id);
      const updatedVolunteers = [...this.selectedSupplyVolun, volunteer];

      // 更新 Vuex Store 中的志愿者数据
      this.$store.dispatch('saveSelectedVolun', {
        pointId: this.selectedSupplyPoint.id,
        supply: updatedVolunteers,
        medical: [], // 如果有其他类型的志愿者处理，可以在这里添加
        car: []
      });
      this.$store.commit('SET_SUPPLY_VOLUNTEERS', newSupplyVolunteers);
    }
  },
  created() {
    // 补给点数据从服务器获取（这里简化为从store中取）
    this.supplyPoint = [
      { id: "1", place: "赛道起始处", type: "水站" },
      { id: "2", place: "赛道5公里处前100米", type: "功能饮料点" },
      { id: "3", place: "赛道终点处", type: "能量补给点" }
    ];
    this.totalSupplyCount = this.supplyPoint.length; // 设置总数量
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/PacerAndEmergencyTable.css";

#SupplyManagement {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: flex-end;
  background-color: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  margin-right: 3%;
  width: 67%;
  font-size: 15px;
  padding-top: 20px;
}
</style>