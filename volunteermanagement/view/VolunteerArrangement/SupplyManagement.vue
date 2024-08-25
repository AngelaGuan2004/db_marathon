<template>
    <div id="supply-management">
      <el-header>
        <el-menu
          class="el-menu-demo"
          mode="horizontal"
          background-color="#c81623"
          text-color="#666"
          active-text-color="#ffd04b"
          :default-active="activeMenu"
        >
          <el-menu-item index="1">首页</el-menu-item>
          <el-menu-item index="2">赛历</el-menu-item>
        </el-menu>
      </el-header>
      <div class="content-container">
        <div id="div-left">
          <div class="break"></div>
          <div>
            <button :class="checkClass(0)" @click="navigateTo('VolunManagement')">志愿者分工</button>
          </div>
          <div>
            <button :class="checkClass(1)" @click="navigateTo('SupplyManagement')">补给点排班</button>
          </div>
          <div>
            <button :class="checkClass(2)" @click="navigateTo('MedicalManagement')">医疗点排班</button>
          </div>
          <div>
            <button :class="checkClass(3)" @click="navigateTo('CarManagement')">摆渡车排班</button>
          </div>
        </div>
        <el-main class="main-content">
          <div class="right-menu">
            <el-button class="custom-button" icon="el-icon-arrow-left" @click="goBack">返回</el-button>
          </div>
          <div v-if="paginatedSupplyPoint.length > 0">
            <el-table :data="paginatedSupplyPoint" class="supplypoint-management-table">
              <el-table-column prop="id" label="ID" width="150" align="center" header-align="center"></el-table-column>
              <el-table-column prop="place" label="地点" width="150" align="center" header-align="center"></el-table-column>
              <el-table-column prop="type" label="类型" width="150" align="center" header-align="center"></el-table-column>
              <el-table-column prop="selected" label="已排班志愿者" width="200" align="center" header-align="center">
                <template slot-scope="scope">
                  <el-button @click="showSelectedSupply(scope.row)">已排班名单</el-button>
                </template>
              </el-table-column>
              <el-table-column prop="selection" label="待排班志愿者" width="200" align="center" header-align="center">
                <template slot-scope="scope">
                  <el-button @click="showSelectionSupply(scope.row)">待排班名单</el-button>
                </template>
              </el-table-column>
            </el-table>
            <el-pagination
              class="pagination"
              background
              layout="prev, pager, next"
              :total="totalSupplyCount"
              :page-size="pageSize"
              @current-change="handlePageChange"
            ></el-pagination>
          </div>
          <div v-else class="custom-empty">
            暂无数据
          </div>
        </el-main>
        <el-dialog :visible.sync="SelectedVolunVisible" :title="`${selectedSupplyPoint?.id || ''}号补给点志愿者名单`" width="70%">
          <div v-if="selectedSupplyVolun.length > 0">
            <el-table :data="selectedSupplyVolun" class="volun-list-table">
              <el-table-column prop="id" label="ID" width="100" align="center" header-align="center"></el-table-column>
              <el-table-column prop="name" label="姓名" width="100" align="center" header-align="center"></el-table-column>
              <el-table-column prop="telenumber" label="电话" width="200" align="center" header-align="center"></el-table-column>
            </el-table>
          </div>
          <div v-else class="custom-empty">
            暂无数据
          </div>
          <span slot="footer" class="dialog-footer">
            <el-button @click="SelectedVolunVisible = false">关闭</el-button>
          </span>
        </el-dialog>
        <el-dialog :visible.sync="SelectionVolunVisible" title="待排班志愿者名单" width="70%">
          <div v-if="selectionSupplyVolun.length > 0">
            <el-table :data="selectionSupplyVolun" class="volun-list-table">
              <el-table-column prop="id" label="ID" width="100" align="center" header-align="center"></el-table-column>
              <el-table-column prop="name" label="姓名" width="100" align="center" header-align="center"></el-table-column>
              <el-table-column prop="telenumber" label="电话" width="200" align="center" header-align="center"></el-table-column>
              <el-table-column label="选择" width="100" align="center" header-align="center">
                <template slot-scope="scope">
                  <el-button type="primary" @click="submitSupplyVolun(scope.row)">提交</el-button>
                </template>
              </el-table-column>
            </el-table>
          </div>
          <div v-else class="custom-empty">
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
  export default {
    data() {
      return {
        activeMenu: '1',
        activeNav: 1,
        currentPage: 1,
        pageSize: 8,
        supplyPoint: [],
        selectedSupplyPoint: null,
        SelectedVolunVisible: false,
        SelectionVolunVisible: false,
        totalSupplyCount: 0
      };
    },
    computed: {
      paginatedSupplyPoint() {
        const start = (this.currentPage - 1) * this.pageSize;
        const end = this.currentPage * this.pageSize;
        return this.supplyPoint.slice(start, end);
      },
      selectedSupplyVolun() {
        return this.$store.getters.getSupplyPointVolunteers(this.selectedSupplyPoint?.id || "");
      },
      selectionSupplyVolun() {
        return this.$store.getters.getSupplyVolunteers;
      }
    },
    methods: {
      checkClass(index) {
        return index === this.activeNav ? 'active' : 'inactive';
      },
      navigateTo(page) {
        if (page === 'VolunManagement') {
          this.activeNav = 0;
          this.$router.push({ name: page });
        } else if (page === 'SupplyManagement') {
          this.activeNav = 1;
        } else if (page === 'MedicalManagement') {
          this.activeNav = 2;
          this.$router.push({ name: page });
        } else if (page === 'CarManagement') {
          this.activeNav = 3;
          this.$router.push({ name: page });
        }
      },
      handlePageChange(page) {
        this.currentPage = page;
      },
      goBack() {
        //this.$router.push({ name: 'EventList' });
      },
      showSelectedSupply(row) {
        this.selectedSupplyPoint = row;
        this.SelectedVolunVisible = true;
      },
      showSelectionSupply(row) {
        this.selectedSupplyPoint = row;
        this.SelectionVolunVisible = true;
      },
      submitSupplyVolun(volunteer) {
        // 将志愿者从待排班志愿者中移除，并添加到对应补给点的已排班志愿者列表中
        const newSupplyVolunteers = this.selectionSupplyVolun.filter(v => v.id !== volunteer.id);
        const updatedVolunteers = [...this.selectedSupplyVolun, volunteer];
  
        // 更新 Vuex Store 中的志愿者数据
        this.$store.dispatch('saveSelectedVolun', {
          pointId: this.selectedSupplyPoint.id,
          supply: updatedVolunteers,
          medical: [], // 如果有其他类型的志愿者处理，可以在这里添加
          car: []
        });
        this.$store.commit('SET_SUPPLY_VOLUNTEERS', newSupplyVolunteers);
      }
    },
    created() {
      // 补给点数据从服务器获取（这里简化为从store中取）
      this.supplyPoint = [
        { id: "1", place: "赛道起始处", type: "水站" },
        { id: "2", place: "赛道5公里处前100米", type: "功能饮料点" },
        { id: "3", place: "赛道终点处", type: "能量补给点" }
      ];
      this.totalSupplyCount = this.supplyPoint.length; // 设置总数量
    }
  };
  </script>
  
  <style>
  #supply-management {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    display: flex;
    flex-direction: column;
  }
  
  .content-container {
    display: flex;
    flex: 1;
    background-color: #f4f4f6;
    overflow: hidden;
  }
  
  #div-left {
    width: 200px;
    background-color: white;
    box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
    overflow: hidden;
    height: 630px;
  }
  
  #div-left button {
    width: 100%;
    margin-top: 15px;
    padding: 10px;
    font-size: 16px;
    background-color: white;
    border: none;
    cursor: pointer;
  }
  
  #div-left button:hover {
    background-color: #f5f5f5;
  }
  
  #div-left button.active {
    color: #c81623;
    background-color: #fff0f0;
  }
  
  .main-content {
    flex-grow: 1;
    display: flex;
    flex-direction: column;
    padding: 20px;
  }
  
  .right-menu {
    display: flex;
    justify-content: flex-start;
    align-items: center;
    margin-bottom: 10px;
  }
  
  .custom-button {
    margin-right: auto;
  }
  
  .supplypoint-management-table {
    table-layout: auto;
    width: 100%;
    margin: 0 auto;
  }
  
  .volun-list-table {
    width: 100%;
    table-layout: fixed;
  }
  
  .el-dialog {
    padding: 10px;
  }
  
  .el-main {
    padding: 0;
  }
  
  .pagination {
    display: flex;
    justify-content: center;
    margin: 20px 0;
  }
  
  .custom-empty {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 200px;
    color: #999;
    width: 100%;
    text-align: center;
  }
  </style>
  