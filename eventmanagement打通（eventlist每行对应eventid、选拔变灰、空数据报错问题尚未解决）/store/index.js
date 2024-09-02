//引入Vue核心库
import Vue from 'vue'
//引入Vuex
import Vuex from 'vuex'


//应用Vuex插件
Vue.use(Vuex)

//创建并暴露store
export default new Vuex.Store({
  state: {
    
    supplyVolunteers: [],
    medicalVolunteers: [],
    carVolunteers: [],
    supplyPointVolunteers: {},
    medicalPointVolunteers: {},
    carPointVolunteers: {},
    availableVoluns: [
      { id: "1", name: "张三", telenumber: "12345678901", tosupply: false, tomedical: false, tocar: false },
      { id: "2", name: "李四", telenumber: "12345678902", tosupply: false, tomedical: false, tocar: false },
      { id: "3", name: "王五", telenumber: "12345678903", tosupply: false, tomedical: false, tocar: false },
      { id: "4", name: "赵六", telenumber: "12345678904", tosupply: false, tomedical: false, tocar: false },
      { id: "5", name: "钱七", telenumber: "12345678905", tosupply: false, tomedical: false, tocar: false },
      { id: "6", name: "孙八", telenumber: "12345678906", tosupply: false, tomedical: false, tocar: false },
      { id: "7", name: "李九", telenumber: "12345678907", tosupply: false, tomedical: false, tocar: false },
      { id: "8", name: "周十", telenumber: "12345678908", tosupply: false, tomedical: false, tocar: false },
      { id: "9", name: "吴一", telenumber: "12345678909", tosupply: false, tomedical: false, tocar: false },
      { id: "10", name: "郑二", telenumber: "12345678910", tosupply: false, tomedical: false, tocar: false },
      { id: "11", name: "王三", telenumber: "12345678911", tosupply: false, tomedical: false, tocar: false },
      { id: "12", name: "刘四", telenumber: "12345678912", tosupply: false, tomedical: false, tocar: false },
      { id: "13", name: "陈五", telenumber: "12345678913", tosupply: false, tomedical: false, tocar: false },
      { id: "14", name: "杨六", telenumber: "12345678914", tosupply: false, tomedical: false, tocar: false },
      { id: "15", name: "黄七", telenumber: "12345678915", tosupply: false, tomedical: false, tocar: false },
      { id: "16", name: "张八", telenumber: "12345678916", tosupply: false, tomedical: false, tocar: false },
      { id: "17", name: "赵九", telenumber: "12345678917", tosupply: false, tomedical: false, tocar: false },
      { id: "18", name: "钱十", telenumber: "12345678918", tosupply: false, tomedical: false, tocar: false },
      { id: "19", name: "周一", telenumber: "12345678919", tosupply: false, tomedical: false, tocar: false },
      { id: "20", name: "吴二", telenumber: "12345678920", tosupply: false, tomedical: false, tocar: false },
      { id: "21", name: "郑三", telenumber: "12345678921", tosupply: false, tomedical: false, tocar: false },
      { id: "22", name: "王四", telenumber: "12345678922", tosupply: false, tomedical: false, tocar: false }
    ]
  },
  mutations: {
    

    SET_SUPPLY_VOLUNTEERS(state, volunteers) {
      state.supplyVolunteers = volunteers;
    },
    SET_MEDICAL_VOLUNTEERS(state, volunteers) {
      state.medicalVolunteers = volunteers;
    },
    SET_CAR_VOLUNTEERS(state, volunteers) {
      state.carVolunteers = volunteers;
    },
    SET_SUPPLY_POINT_VOLUNTEERS(state, { pointId, volunteers }) {
      Vue.set(state.supplyPointVolunteers, pointId, volunteers);
    },
    SET_MEDICAL_POINT_VOLUNTEERS(state, { pointId, volunteers }) {
      Vue.set(state.medicalPointVolunteers, pointId, volunteers);
    },
    SET_CAR_POINT_VOLUNTEERS(state, { pointId, volunteers }) {
      Vue.set(state.carPointVolunteers, pointId, volunteers);
    },
    REMOVE_VOLUNS_FROM_AVAILABLE(state, volunteers) {
      state.availableVoluns = state.availableVoluns.filter(
        v => !volunteers.some(vol => vol.id === v.id)
      );
    }
  },
  actions: {
    saveSelectedVolun({ commit }, { pointId, supply, medical, car }) {
      commit('SET_SUPPLY_POINT_VOLUNTEERS', { pointId, volunteers: supply });
      commit('SET_MEDICAL_POINT_VOLUNTEERS', { pointId, volunteers: medical });
      commit('SET_CAR_POINT_VOLUNTEERS', { pointId, volunteers: car });
      commit('SET_SUPPLY_VOLUNTEERS', supply);
      commit('SET_MEDICAL_VOLUNTEERS', medical);
      commit('SET_CAR_VOLUNTEERS', car);
      commit('REMOVE_VOLUNS_FROM_AVAILABLE', [...supply, ...medical, ...car]);
    }
  },
  getters: {
    getSupplyVolunteers: state => state.supplyVolunteers,
    getMedicalVolunteers: state => state.medicalVolunteers,
    getCarVolunteers: state => state.carVolunteers,
    getAvailableVoluns: state => state.availableVoluns,
    getSupplyPointVolunteers: (state) => (pointId) => {
      return state.supplyPointVolunteers[pointId] || [];
    },
    getMedicalPointVolunteers: (state) => (pointId) => {
      return state.medicalPointVolunteers[pointId] || [];
    },
    getCarPointVolunteers: (state) => (pointId) => {
      return state.carPointVolunteers[pointId] || [];
    }
  }
})