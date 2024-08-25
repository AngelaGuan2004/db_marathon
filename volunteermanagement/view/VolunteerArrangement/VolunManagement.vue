<template>
    <div id="volun-management">
      <el-header>
        <el-menu class="el-menu-demo" mode="horizontal" background-color="#c81623" text-color="#666" active-text-color="#ffd04b" :default-active="activeMenu">
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
          <div class="save_or_cancel">
            <el-button type="primary" @click="save">保存</el-button>
            <el-button type="primary" @click="cancel">取消</el-button>
          </div>
          <div v-if="paginatedVolunteers.length > 0">
            <el-table :data="paginatedVolunteers" class="volun-management-table" style="width: auto">
              <el-table-column prop="id" label="ID" width="100"></el-table-column>
              <el-table-column prop="name" label="姓名" width="100"></el-table-column>
              <el-table-column prop="telenumber" label="电话" width="200"></el-table-column>
              <el-table-column label="补给点" width="100">
                <template slot-scope="scope">
                  <el-checkbox v-model="scope.row.tosupply" @change="handleCheckboxChange(scope.row, 'tosupply')"></el-checkbox>
                </template>
              </el-table-column>
              <el-table-column label="医疗点" width="100">
                <template slot-scope="scope">
                  <el-checkbox v-model="scope.row.tomedical" @change="handleCheckboxChange(scope.row, 'tomedical')"></el-checkbox>
                </template>
              </el-table-column>
              <el-table-column label="摆渡车" width="100">
                <template slot-scope="scope">
                  <el-checkbox v-model="scope.row.tocar" @change="handleCheckboxChange(scope.row, 'tocar')"></el-checkbox>
                </template>
              </el-table-column>
            </el-table>
            <el-pagination
              class="pagination"
              background
              layout="prev, pager, next"
              :total="totalVolunteersCount"
              :page-size="pageSize"
              @current-change="handlePageChange"
            ></el-pagination>
          </div>
          <div v-else class="custom-empty">
            暂无数据
          </div>
        </el-main>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        activeMenu: '1',
        activeNav: 0,
        currentPage: 1,
        pageSize: 8,
        volunteers: [] // 存储从 Vuex 中获取的志愿者数据
      };
    },
    computed: {
      totalVolunteersCount() {
        return this.volunteers.length;
      },
      paginatedVolunteers() { // 确保 paginatedVolunteers 计算正确
        const start = (this.currentPage - 1) * this.pageSize;
        const end = this.currentPage * this.pageSize;
        return this.volunteers.slice(start, end);
      }
    },
    methods: {
      checkClass(index) {
        return index === this.activeNav ? 'active' : 'inactive';
      },
      navigateTo(page) {
        if (page === 'VolunManagement') {
          this.activeNav = 0;
        } else if(page === 'SupplyManagement') {
            this.activeNav = 1;
            this.$router.push({ name: page });
        } else  if (page === 'MedicalManagement') {
            this.activeNav = 2;
            this.$router.push({ name: page });
        }  else  if (page === 'CarManagement') {
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
      handleCheckboxChange(row, selectedField) {
        if (selectedField === 'tosupply') {
          row.tomedical = false;
          row.tocar = false;
        } else if (selectedField === 'tomedical') {
          row.tosupply = false;
          row.tocar = false;
        } else if (selectedField === 'tocar') {
          row.tosupply = false;
          row.tomedical = false;
        }
      },
      save() {
        const selectedSupplyVolun = this.volunteers.filter(p => p.tosupply);
        const selectedMedicalVolun = this.volunteers.filter(p => p.tomedical);
        const selectedCarVolun = this.volunteers.filter(p => p.tocar);
        this.$store.dispatch('saveSelectedVolun', {
          supply: selectedSupplyVolun,
          medical: selectedMedicalVolun,
          car: selectedCarVolun
        }).then(() => {
          // 保存成功后，从页面上移除已选择的志愿者
          this.volunteers = this.$store.getters.getAvailableVoluns;
          this.$message.success('保存成功');
        }).catch(() => {
          this.$message.error('保存失败');
        });
      },
      cancel() {
        this.volunteers.forEach(p => p.tosupply = false);
        this.volunteers.forEach(p => p.tomedical = false);
        this.volunteers.forEach(p => p.tocar = false);
        this.$message.info('已取消所有勾选');
      }
    },
    created() {
      this.volunteers = this.$store.getters.getAvailableVoluns;
    }
  };
  </script>
  
  <style>
  #volun-management {
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
    height:630px; 
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
    overflow: hidden;
  }
  
  .right-menu {
    display: flex;
    justify-content: flex-start;
    align-items: center;
  }
  
  .custom-button {
    margin-right: auto;
  }
  
  .save_or_cancel {
    display: flex;
    justify-content: flex-end;
    margin-bottom: 20px;
  }

  .volun-management-table {
    table-layout: auto;
    border: none;
    width: auto;
    margin: 0 auto;
  }

  .el-main {
    padding: 0; /* 确保主内容区域的左右内边距为0 */
  }
  
  .volun-management-table th .cell,
  .volun-management-table td .cell {
    display: inline-block;
    width: 100%;
    padding: 0 5px;
    text-align: center;
    height: 20px;
    border: none;
    vertical-align: middle;
    line-height: 20px;
  }
  
  .el-table__header-wrapper,
  .el-table__body-wrapper {
    display: flex;
    justify-content: center;
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
  