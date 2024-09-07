<template>
  <div id="CarManagement">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content" v-if="paginatedCarPoint.length > 0">
        <div style="margin: 25px 15px;font-weight: bold;font-size: 26px;">{{
          this.$route.params.name }}</div>
        <div>
          <el-table :data="paginatedCarPoint" class="Table" max-height="400">
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
      </el-main>
      <div v-else class="Empty">暂无数据</div>
      <el-dialog :visible.sync="SelectedVolunVisible" :title="`${selectedCarPoint?.id || ''}号摆渡车志愿者名单`" width="45%"
        class="ManagementDialog">
        <div v-if="selectedCarVolun.length > 0">
          <el-table :data="selectedCarVolun" class="Table" max-height="300">
            <el-table-column prop="id" label="ID" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="name" label="姓名" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="telenumber" label="电话" width="250" align="center"
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
      <el-dialog :visible.sync="SelectionVolunVisible" title="待排班志愿者名单" width="45%" class="ManagementDialog">
        <div v-if="selectionCarVolun.length > 0">
          <el-table :data="selectionCarVolun" class="Table" max-height="300">
            <el-table-column prop="id" label="ID" width="175" align="center" header-align="center"></el-table-column>
            <el-table-column prop="name" label="姓名" width="125" align="center" header-align="center"></el-table-column>
            <el-table-column prop="telenumber" label="电话" width="250" align="center"
              header-align="center"></el-table-column>
            <el-table-column label="选择" width="125" align="center" header-align="center">
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
import { getAllCarPoints, getVolunteersByEventId, acquireVolunteerInformation, getCarVolunteers, addVolunteerToCarPoint } from '@/api/volunteer';
export default {
  name: 'CarManagement',
  data() {
    return {
      currentPage: 1,
      pageSize: 5,
      carPoint: [],
      selectedCarPoint: null,
      SelectedVolunVisible: false,
      SelectionVolunVisible: false,
      selectedCarVolun: [],
      selectionCarVolun: [],
      totalCarCount: 0
    };
  },
  computed: {
    paginatedCarPoint() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.carPoint.slice(start, end);
    },
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    async loadCarPoints() {
      try {
        const eventId = this.$route.params.event_id;
        const response = await getAllCarPoints(eventId);

        this.carPoint = response.map(point => ({
          ...point,
          departureTime: point.departure_Time,
          arrivalTime: point.arrival_Time,
        }));
        this.totalCarCount = response.length;
      } catch (error) {
        console.error('Failed to load car points:', error);
        this.$message.error('加载接驳车信息失败，请稍后重试。');
      }
    },
    async showSelectionCar(row) {
      try {
        const eventId = this.$route.params.event_id;
        this.selectedCarPoint = row;
        this.SelectionVolunVisible = true;

        const volunteers = await getVolunteersByEventId(eventId);
        const selectionVolunteers = [];

        for (const volunteer of volunteers) {
          const info = await acquireVolunteerInformation(volunteer.id, eventId);
          if (info.job_category === '接驳车' && !info.is_scheduled) {
            selectionVolunteers.push(volunteer);
          }
        }

        this.selectionCarVolun = selectionVolunteers.map(volunteer => ({
          id: volunteer.id,
          name: volunteer.name,
          telenumber: volunteer.telephone_Number,
        }));
      } catch (error) {
        console.error('Failed to load selected volunteers:', error);
        this.$message.error('加载待排班志愿者失败，请稍后重试。');
      }
    },
    async showSelectedCar(row) {
      try {
        const eventId = this.$route.params.event_id;
        const carPointId = row.id;
        const response = await getCarVolunteers(eventId, carPointId);

        this.selectedCarVolun = response.map(volunteer => ({
          id: volunteer.id,
          name: volunteer.name,
          telenumber: volunteer.telephone_Number,
        }));

        this.selectedCarPoint = row;
        this.SelectedVolunVisible = true;
      } catch (error) {
        console.error('Failed to load selected volunteers:', error);
        this.$message.error('加载已排班志愿者失败，请稍后重试。');
      }
    },
    async submitCarVolun(volunteer) {
      try {
        const data = {
          volunteer_id: volunteer.id,
          shuttlecar_id: this.selectedCarPoint.id,
        };
        await addVolunteerToCarPoint(data);
        this.$message.success('志愿者已成功添加到接驳车');
        this.selectionCarVolun = this.selectionCarVolun.filter(v => v.id !== volunteer.id);
      } catch (error) {
        console.error('Failed to add volunteer to shuttlecar:', error);
        this.$message.error('添加志愿者到接驳车失败，请稍后重试。');
      }
    }
  },
  created() {
    this.loadCarPoints();
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
  align-items: center;
  background-color: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: 105vh;
  height: 65vh;
  margin-right: 50px;
  margin-bottom: 75px;
  font-size: 15px;
}
</style>