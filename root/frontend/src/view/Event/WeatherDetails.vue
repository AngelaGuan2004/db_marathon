<template>
  <div class="modal-overlay" @click="closeModal">
    <div class="modal-content" @click.stop>
      <h2>天气详情</h2>
      <ul>
        <li v-for="(weather, index) in weatherDetails" :key="index">
          <p><b>发生时间：</b>{{ weather.time }}</p>
          <p><b>气温：</b>{{ weather.temperature }}</p>
          <p><b>天气情况：</b>{{ weather.condition }}</p>
          <p><b>比赛是否能正常进行：</b>{{ weather.canProceed }}</p>
        </li>
      </ul>
      <button @click="closeModal">关闭</button>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    weatherDetails: Array,
  },
  methods: {
    closeModal() {
      this.$emit('close');
    }
  }
}
</script>

<style>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal-content {
  background: white;
  padding: 20px;
  border-radius: 8px;
  max-width: 500px;
  width: 100%;
}
</style>
