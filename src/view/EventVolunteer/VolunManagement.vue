<template>
  <div id="VolunManagement">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content">
        <div class="Button">
          <div style="margin-left: 55%;margin-top: 10px;">
            <el-button type="primary" @click="save" style="margin: 0 10px;">保存</el-button>
            <el-button type="primary" @click="cancel">取消</el-button>
          </div>
        </div>
        <div v-if="paginatedVolunteers.length > 0">
          <el-table :data="paginatedVolunteers" class="Table" style="width: 100%">
            <el-table-column prop="id" label="ID" width="100"></el-table-column>
            <el-table-column prop="name" label="姓名" width="100"></el-table-column>
            <el-table-column prop="telenumber" label="电话" width="200"></el-table-column>
            <el-table-column label="补给点" width="100">
              <template slot-scope="scope">
                <el-checkbox v-model="scope.row.tosupply"
                  @change="handleCheckboxChange(scope.row, 'tosupply')"></el-checkbox>
              </template>
            </el-table-column>
            <el-table-column label="医疗点" width="100">
              <template slot-scope="scope">
                <el-checkbox v-model="scope.row.tomedical"
                  @change="handleCheckboxChange(scope.row, 'tomedical')"></el-checkbox>
              </template>
            </el-table-column>
            <el-table-column label="摆渡车" width="100">
              <template slot-scope="scope">
                <el-checkbox v-model="scope.row.tocar" @change="handleCheckboxChange(scope.row, 'tocar')"></el-checkbox>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="Pagination" background layout="prev, pager, next" :total="totalVolunteersCount"
            :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
        </div>
        <div v-else class="Empty">
          暂无数据
        </div>
      </el-main>
    </div>
  </div>
</template>

<script>
export default {
  name: 'VolunManagement',
  data() {
    return {
      currentPage: 1,
      pageSize: 8,
      volunteers: [] // 存储从 Vuex 中获取的志愿者数据
    };
  },
  computed: {
    totalVolunteersCount() {
      return this.volunteers.length;
    },
    paginatedVolunteers() { // 确保 paginatedVolunteers 计算正确
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.volunteers.slice(start, end);
    }
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    handleCheckboxChange(row, selectedField) {
      if (selectedField === 'tosupply') {
        row.tomedical = false;
        row.tocar = false;
      } else if (selectedField === 'tomedical') {
        row.tosupply = false;
        row.tocar = false;
      } else if (selectedField === 'tocar') {
        row.tosupply = false;
        row.tomedical = false;
      }
    },
    save() {
      const selectedSupplyVolun = this.volunteers.filter(p => p.tosupply);
      const selectedMedicalVolun = this.volunteers.filter(p => p.tomedical);
      const selectedCarVolun = this.volunteers.filter(p => p.tocar);
      this.$store.dispatch('saveSelectedVolun', {
        supply: selectedSupplyVolun,
        medical: selectedMedicalVolun,
        car: selectedCarVolun
      }).then(() => {
        // 保存成功后，从页面上移除已选择的志愿者
        this.volunteers = this.$store.getters.getAvailableVoluns;
        this.$message.success('保存成功');
      }).catch(() => {
        this.$message.error('保存失败');
      });
    },
    cancel() {
      this.volunteers.forEach(p => p.tosupply = false);
      this.volunteers.forEach(p => p.tomedical = false);
      this.volunteers.forEach(p => p.tocar = false);
      this.$message.info('已取消所有勾选');
    }
  },
  created() {
    this.volunteers = this.$store.getters.getAvailableVoluns;
  }
};
</script>

<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/PacerAndEmergencyTable.css";

#VolunManagement {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: flex-end;
  background-color: white;
  margin-top: 80px;
  margin-right: 3%;
  width: 67%;
  font-size: 15px;
}
</style>