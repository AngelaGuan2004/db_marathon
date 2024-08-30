<template>
  <div id="VolunteerDetailForAddStation">
    <el-dialog title="赛事志愿详情" :visible.sync="dialogTableVisible" :before-close="handleClose" width="35%">
      <div class="AidStation" v-if="aidStation">
        <div class="AidStationHeader" style="  color: rgb(168, 27, 31);">{{ aidStation.eventTitle }}</div>
        <div class="AidStationContent">
          <div style="width: 100%;">
            <div class="AidStationItem">
              <span style="width: 40%;font-weight: bold;">赛事名称：</span><span>{{ aidStation.eventTitle }}</span>
            </div>
            <div v-if="this.aidStation.voltype === '补给'">
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">补给点 ID：</span><span>{{ aidStation.aidId }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">补给点地点：</span><span> {{ aidStation.aidLocation }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">补给点类型：</span><span>{{ aidStation.aidType }}</span>
              </div>
            </div>
            <div v-else-if="this.aidStation.voltype === '医疗'">
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">医疗点 ID：</span><span>{{ aidStation.medicalId }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">医疗点位置：</span><span> {{ aidStation.medicalLocation }}</span>
              </div>
            </div>
            <div v-else-if="this.aidStation.voltype === '接驳车'">
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">接驳车 ID：</span><span>{{ aidStation.shuttleId }}</span>
              </div>
              <div class="AidStationItem">
                <span style="width: 40%;font-weight: bold;">出发时间：</span><span>{{ aidStation.shuttleDepartureTime
                  }}</span>
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
import axios from 'axios';
export default {
  name: 'VolunteerDetailForAddStation',
  data() {
    return {
      aidStation: {
        eventTitle: '123213123',
        id: this.$route.params.id,
        aidId: '5645465',
        aidLocation: '123123213',
        aidType: '1231231232',
        medicalId: '1231231232',
        medicalLocation: '1231231232',
        shuttleId: '1231231232',
        shuttleDepartureTime: '1231231232',
        shuttleArrivalTime: '1231231232',
        voltype: this.$route.params.voltype,
      },
      dialogTableVisible: true
    };
  },
  created() {
    // this.loadAidStation();
  },
  watch: {
    'this.$route.params.id': 'loadAidStation'
  },
  methods: {
    loadAidStation() {
      const aidStationId = this.$route.params.id;
      axios.get(`/api/aid-stations/${aidStationId}`)
        .then(response => {
          this.aidStation = response.data;
        })
        .catch(error => {
          console.error('Error loading aid station info:', error);
          this.$message.error('加载补给点信息失败');
        });
    },
    handleClose() {
      this.$router.back()
    }
  }
}
</script>

<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";
@import "@/assets/css/VolunteerDetailForAddStation.css";
</style>
