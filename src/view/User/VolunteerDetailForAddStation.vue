<template>
  <div id="VolunteerDetailForAddStation">
    <el-dialog title="赛事志愿详情" :visible.sync="dialogTableVisible" :before-close="handleClose" width="35%">
      <div class="AidStation" v-if="aidStation">
        <div class="AidStationHeader" style="color: rgb(168, 27, 31);">{{ aidStation.eventTitle }}</div>
        <div class="AidStationContent">
          <div style="width: 100%;">
            <div class="AidStationItem">
              <span style="width: 40%;font-weight: bold;">志愿者状态：</span><span>{{ aidStation.status }}</span>
            </div>
            <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">工作种类：</span><span>{{ aidStation.job_category }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">是否排班：</span><span>{{ aidStation.is_scheduled }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">搭档：</span><span>{{ aidStation.partners }}</span>
              </div>
          </div>
        </div>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { acquireVolunteerInformation } from '@/api/detail';

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
  watch: {
    'this.$route.params.event_id': 'loadAidStation'
  },
  methods: {
    loadVolunteerInformation() {
      const volunteerId = this.$route.params.volunteer_id;
      const eventId = this.$route.params.event_id;

      acquireVolunteerInformation(eventId,10001)
        .then(response => {
          console.log(response)
          this.aidStation = response; // 将后端返回的数据赋值给 aidStation
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

