<template>
  <div id="SupplyManagement">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content" v-if="paginatedSupplyPoint.length > 0">
        <div style="margin: 25px 15px;font-weight: bold;font-size: 26px;">{{
          this.$route.params.name }}</div>
        <div>
          <el-table :data="paginatedSupplyPoint" class="Table" max-height="400">
            <el-table-column prop="id" label="ID" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="place" label="地点" width="200" align="center" header-align="center"></el-table-column>
            <el-table-column prop="type" label="类型" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="selected" label="已排班志愿者" width="175" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button @click="showSelectedSupply(scope.row)">已排班名单</el-button>
              </template>
            </el-table-column>
            <el-table-column prop="selection" label="待排班志愿者" width="175" align="center" header-align="center">
              <template slot-scope="scope">
                <el-button @click="showSelectionSupply(scope.row)">待排班名单</el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="Pagination" background layout="prev, pager, next" :total="totalSupplyCount"
            :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
        </div>
      </el-main>
      <div v-else class="Empty">暂无数据</div>
      <el-dialog :visible.sync="SelectedVolunVisible" :title="`${selectedSupplyPoint?.id || ''}号补给点志愿者名单`" width="45%"
        class="ManagementDialog">
        <div v-if="selectedSupplyVolun.length > 0">
          <el-table :data="selectedSupplyVolun" class="Table" max-height="300">
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
        <div v-if="selectionSupplyVolun.length > 0">
          <el-table :data="selectionSupplyVolun" class="Table" max-height="300">
            <el-table-column prop="id" label="ID" width="150" align="center" header-align="center"></el-table-column>
            <el-table-column prop="name" label="姓名" width="100" align="center" header-align="center"></el-table-column>
            <el-table-column prop="telenumber" label="电话" width="250" align="center"
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
import { getAllSupplyPoints, getVolunteersByEventId, acquireVolunteerInformation, addVolunteerToSupplyPoint, getSupplyVolunteers } from '@/api/volunteer';
export default {
  data() {
    return {
      currentPage: 1,
      pageSize: 5,
      supplyPoint: [],
      selectedSupplyPoint: null,
      SelectedVolunVisible: false,
      SelectionVolunVisible: false,
      selectedSupplyVolun: [],
      selectionSupplyVolun: [],
      totalSupplyCount: 0
    };
  },
  computed: {
    paginatedSupplyPoint() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.supplyPoint.slice(start, end);
    },
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    async loadSupplyPoints() {
      try {
        const eventId = this.$route.params.event_id; // 替换为你的 Event ID
        const response = await getAllSupplyPoints(eventId);
        console.log(response);
        this.supplyPoint = response.map(point => ({
          ...point,
          type: point.kind,  // 将kind字段映射为type
        }));
        this.totalSupplyCount = response.length;
      } catch (error) {
        console.error('Failed to load supply points:', error);
        this.$message.error('加载补给点信息失败，请稍后重试。');
      }
    },
    async showSelectionSupply(row) {
      try {
        const eventId = this.$route.params.event_id; // 替换为你的 Event ID
        this.selectedSupplyPoint = row;
        this.SelectionVolunVisible = true;

        // 获取所有志愿者信息
        const volunteers = await getVolunteersByEventId(eventId);
        const selectionVolunteers = [];

        // 过滤出已分配到补给点的志愿者
        for (const volunteer of volunteers) {
          const info = await acquireVolunteerInformation(volunteer.id, eventId);
          if (info.job_category === '补给点' && info.is_scheduled === false) {
            selectionVolunteers.push(volunteer);
          }
        }
        this.selectionSupplyVolun = selectionVolunteers.map(volunteer => ({
          ...volunteer,
          telenumber: volunteer.telephone_Number,
        }));
      } catch (error) {
        console.error('Failed to load selected volunteers:', error);
        this.$message.error('加载待排班志愿者失败，请稍后重试。');
      }
    },
    async showSelectedSupply(row) {
      try {
        const eventId = this.$route.params.event_id;
        const supplyPointId = row.id;
        const response = await getSupplyVolunteers(eventId, supplyPointId);

        this.selectedSupplyVolun = response.map(volunteer => ({
          id: volunteer.id,
          name: volunteer.name,
          telenumber: volunteer.telephone_Number // 从响应中获取电话
        }));

        this.selectedSupplyPoint = row;
        this.SelectedVolunVisible = true;
      } catch (error) {
        console.error('Failed to load selected volunteers:', error);
        this.$message.error('加载已排班志愿者失败，请稍后重试。');
      }
    },
    async submitSupplyVolun(volunteer) {
      try {
        const data = {
          volunteer_id: volunteer.id,
          supplypoint_id: this.selectedSupplyPoint.id // 假设补给点有 id 字段
        };
        console.log(data);
        await addVolunteerToSupplyPoint(data);
        this.$message.success('志愿者已成功添加到补给点');
        this.selectionSupplyVolun = this.selectionSupplyVolun.filter(v => v.id !== volunteer.id);
      } catch (error) {
        console.error('Failed to add volunteer to supply point:', error);
        this.$message.error('添加志愿者到补给点失败，请稍后重试。');
      }
    }
  },
  created() {
    this.loadSupplyPoints(); // 加载补给点信息
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