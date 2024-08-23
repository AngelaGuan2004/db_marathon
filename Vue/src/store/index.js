//引入Vue核心库
import Vue from 'vue'
//引入Vuex
import Vuex from 'vuex'
//应用Vuex插件
Vue.use(Vuex)

const VuexModules = {
  //开启命名空间
  namespace: true,
  //准备actions对象——响应组件中用户的动作
  actions: {},
  //准备mutations对象——修改state中的数据
  mutations: {},
  //准备state对象——保存具体的数据
  state: {}
}


//创建并暴露store
export default new Vuex.Store({
  modules: {
    VuexModules
  }
})