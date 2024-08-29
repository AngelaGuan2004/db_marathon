<template>
  <div id="VolunteerDetailForAddStation">
    <el-dialog title="赛事志愿详情" :visible.sync="dialogTableVisible" :before-close="handleClose" width="35%">
      <div class="AidStation" v-if="aidStation">
        <div class="AidStationHeader" style="color: rgb(168, 27, 31);">{{ aidStation.eventTitle }}</div>
        <div class="AidStationContent">
          <div style="width: 100%;">
            <div class="AidStationItem">
              <span style="width: 40%;font-weight: bold;">赛事名称：</span><span>{{ aidStation.eventTitle }}</span>
            </div>
            <div v-if="aidStation.jobCategory === '补给点'">
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">补给点 ID：</span><span>{{ aidStation.aidId }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">补给点地点：</span><span>{{ aidStation.aidLocation }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">补给点类型：</span><span>{{ aidStation.aidType }}</span>
              </div>
            </div>
            <div v-else-if="aidStation.jobCategory === '医疗点'">
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">医疗点 ID：</span><span>{{ aidStation.medicalId }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">医疗点位置：</span><span>{{ aidStation.medicalLocation }}</span>
              </div>
            </div>
            <div v-else-if="aidStation.jobCategory === '接驳车'">
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">接驳车 ID：</span><span>{{ aidStation.shuttleId }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">出发时间：</span><span>{{ aidStation.shuttleDepartureTime }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">到达时间：</span><span>{{ aidStation.shuttleArrivalTime }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { acquireVolunteerInformation } from '@/api/volunteer';

export default {
  name: 'VolunteerDetailForAddStation',
  data() {
    return {
      aidStation: null, // 初始值设为空，等待后端数据填充
      dialogTableVisible: true
    };
  },
  created() {
    this.loadVolunteerInformation();
  },
  methods: {
    loadVolunteerInformation() {
      const volunteerId = this.$route.params.volunteer_id;
      const eventId = this.$route.params.event_id;

      acquireVolunteerInformation(volunteerId, eventId)
        .then(response => {
          this.aidStation = response.data; // 将后端返回的数据赋值给 aidStation
        })
        .catch(error => {
          console.error('Error loading volunteer info:', error);
          this.$message.error('加载志愿者信息失败');
        });
    },
    handleClose() {
      this.$router.back();
    }
  }
}
</script>

<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";
@import "@/assets/css/VolunteerDetailForAddStation.css";
</style>

