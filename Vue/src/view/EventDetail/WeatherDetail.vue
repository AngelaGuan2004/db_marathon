<template>
  <div id="WeatherDetail">
    <el-dialog title="天气详情" :visible.sync="dialogVisible" width="40%" :before-close="handleClose">
      <el-timeline>
        <el-timeline-item v-for="(weather, index) in weatherDetails" :key="index" :timestamp="weather.time"
          placement="top" size="large">
          <el-card>
            <p><b>气温：</b>{{ weather.temperature }}</p>
            <p><b>天气情况：</b>{{ weather.condition }}</p>
            <p><b>比赛是否能正常进行：</b>{{ weather.canProceed }}</p>
          </el-card>
        </el-timeline-item>
      </el-timeline>
    </el-dialog>
  </div>
</template>

<script>
export default {
  name: 'WeatherDetail',
  data() {
    return {
      dialogVisible: true
    }
  },
  props: {
    weatherDetails: Array,
  },
  methods: {
    handleClose() {
      this.$emit('close');
    }
  }
}
</script>

<style>
#WeatherDetail .el-timeline-item__timestamp {
  font-size: 16px;
  font-weight: bold;
  margin-bottom: 10px;
  color: black;
}

#WeatherDetail .el-card__body {
  padding: 0 20px;

}

#WeatherDetail .el-dialog__body {
  padding-left: 50px;
}

#WeatherDetail .el-card {
  width: 80%;
}
</style>
