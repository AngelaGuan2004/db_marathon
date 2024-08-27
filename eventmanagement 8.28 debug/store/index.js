//引入Vue核心库
import Vue from 'vue'
//引入Vuex
import Vuex from 'vuex'
//应用Vuex插件
Vue.use(Vuex)

const availablePacersModule = {
  namespaced: true,
  state: {
    availablePacers: [],  // 待选配速员
    selectedPacers: []    // 已选配速员
  },
  mutations: {
    setAvailablePacers(state, participants) {
      state.availablePacers = participants;
    },
    setSelectedPacers(state, participants) {
      state.selectedPacers = participants;
    }
  },
  actions: {
    saveSelectedPacers({ commit, state }, selectedParticipants) {
      // 将选中的跑者添加到已选配速员列表中
      const updatedSelected = [...state.selectedPacers, ...selectedParticipants];
      commit('setSelectedPacers', updatedSelected);

      // 从待选跑者列表中移除选中的配速员
      const updatedAvailable = state.availablePacers.filter(p => !selectedParticipants.includes(p));
      commit('setAvailablePacers', updatedAvailable);
    }
  },
  getters: {
    getAvailablePacers(state) {
      return state.availablePacers;
    },
    getSelectedPacers(state) {
      return state.selectedPacers;
    }
  }
};

const availableEmergencyRunnersModule = {
  namespaced: true,
  state: {
    availableEmergencyRunners: [],  // 待选急救跑者
    selectedEmergencyRunners: []    // 已选急救跑者
  },
  mutations: {
    setAvailableEmergencyRunners(state, participants) {
      state.availableEmergencyRunners = participants;
    },
    setSelectedEmergencyRunners(state, participants) {
      state.selectedEmergencyRunners = participants;
    }
  },
  actions: {
    saveSelectedEmergencyRunners({ commit, state }, selectedParticipants) {
      // 将选中的跑者添加到已选急救跑者列表中
      const updatedSelected = [...state.selectedEmergencyRunners, ...selectedParticipants];
      commit('setSelectedEmergencyRunners', updatedSelected);

      // 从待选跑者列表中移除选中的急救跑者
      const updatedAvailable = state.availableEmergencyRunners.filter(p => !selectedParticipants.includes(p));
      commit('setAvailableEmergencyRunners', updatedAvailable);
    }
  },
  getters: {
    getAvailableEmergencyRunners(state) {
      return state.availableEmergencyRunners;
    },
    getSelectedEmergencyRunners(state) {
      return state.selectedEmergencyRunners;
    }
  }
};

// 创建 store 实例
export default new Vuex.Store({
  modules: {
    availablePacersModule,
    availableEmergencyRunnersModule
  },
  actions: {
    loadInitialData({ commit }) {
      // 初始化配速员的待选列表
      const initialPacers = [
        { id: "1", name: "张三", sex: "男", age: "30", number: "A1234", isEmergencyRunner: false, history: [{ index: 1, eventDate: '2024-09-22', eventName: "天津塘沽马拉松", eventType: "全马", result: "3:35:00", ranking: 7 }] },
        { id: "2", name: "李四", sex: "女", age: "22", number: "B2345", isEmergencyRunner: false },
        { id: "3", name: "王五", sex: "男", age: "45", number: "C3456", isEmergencyRunner: false },
        { id: "4", name: "赵六", sex: "女", age: "28", number: "D4567", isEmergencyRunner: false },
        { id: "5", name: "孙七", sex: "男", age: "36", number: "E5678", isEmergencyRunner: false },
        { id: "6", name: "周八", sex: "女", age: "25", number: "A6789", isEmergencyRunner: false },
        { id: "7", name: "吴九", sex: "男", age: "40", number: "B7890", isEmergencyRunner: false },
        { id: "8", name: "郑十", sex: "女", age: "33", number: "C8901", isEmergencyRunner: false },
        { id: "9", name: "冯十一", sex: "男", age: "27", number: "D9012", isEmergencyRunner: false },
        { id: "10", name: "陈十二", sex: "女", age: "31", number: "E0123", isEmergencyRunner: false }
      ];
      commit('availablePacersModule/setAvailablePacers', initialPacers);

      // 初始化急救跑者的待选列表
      const initialEmergencyRunners = [
        { id: "11", name: "褚十三", sex: "男", age: "29", number: "A1235", isEmergencyRunner: false, history: [{ index: 1, eventDate: '2024-09-22', eventName: "天津塘沽马拉松", eventType: "全马", result: "3:35:00", ranking: 7 }] },
        { id: "12", name: "卫十四", sex: "女", age: "24", number: "B2346", isEmergencyRunner: false },
        { id: "13", name: "蒋十五", sex: "男", age: "35", number: "C3457", isEmergencyRunner: false },
        { id: "14", name: "沈十六", sex: "女", age: "26", number: "D4568", isEmergencyRunner: false },
        { id: "15", name: "韩十七", sex: "男", age: "38", number: "E5679", isEmergencyRunner: false },
        { id: "16", name: "杨十八", sex: "女", age: "32", number: "A6780", isEmergencyRunner: false },
        { id: "17", name: "朱十九", sex: "男", age: "37", number: "B7891", isEmergencyRunner: false },
        { id: "18", name: "秦二十", sex: "女", age: "28", number: "C8902", isEmergencyRunner: false },
        { id: "19", name: "尤二十一", sex: "男", age: "39", number: "D9013", isEmergencyRunner: false },
        { id: "20", name: "许二十二", sex: "女", age: "30", number: "E0124", isEmergencyRunner: false }
      ];
      commit('availableEmergencyRunnersModule/setAvailableEmergencyRunners', initialEmergencyRunners);
    }
  }
});
