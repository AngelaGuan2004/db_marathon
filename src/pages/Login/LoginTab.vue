<template>
  <div id="LoginTab">
    <div class="LoginLeft">
      <img src="../../assets/images/LoginBackground.png" alt="">
    </div>
    <div class="LoginRight">
      <div class="LoginRightTitle">
        <section style="flex:0 0 auto">
          <div class="LoginRightTab">
            <el-menu default-active="1" class="el-menu-demo" mode="horizontal" @select="handleSelect"
              active-text-color="rgb(219, 57, 56)">
              <el-menu-item index="1">选手登录</el-menu-item>
              <el-menu-item index="2">摄影师登录</el-menu-item>
              <el-menu-item index="3">管理员登录</el-menu-item>
            </el-menu>
          </div>
        </section>
        <div style="height:100px;margin-top:2%;margin-left: 8%;cursor: pointer;">
          <img src="../../assets/images/JiMa.png" alt="" style="height: 100%;" @click.prevent="GoToIndex">
        </div>
      </div>
      <div class="LoginRightIndexBackground">
        <div class="LoginRightIndex">
          <div class="LoginRightIndexRouter">
            <div class="LoginRightIndexForm">
              <router-view></router-view>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'LoginTab',
  data() {
    return {
      LoginRouter: ['LoginAthlete', 'LoginPhotographer', 'LoginAdmin'],
      name: '',
      phone: '',
      idNumber: '',
      password: '',
      adminKey: '',
      role: ''
    }
  },
  methods: {
    handleSelect(key) {
      this.$router.push({ name: this.LoginRouter[key - 1] })
    },
    GoToRegister() {
      this.$router.push({ name: 'Register' })
    },
    GoToIndex() {
      location.href = "index.html"
    }
  },
  mounted() {
    this.$router.push({ name: 'LoginAthlete' })
  }
}
</script>

<style>
@import "../../assets/css/Base.css";

#LoginTab {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-start;
}

.LoginLeft {
  position: relative;
}

.LoginLeft img {
  display: block;
  height: 100vh;
  min-height: 513px;
}

.LoginRight {
  height: 100vh;
  flex: 1 0 auto;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: stretch;
  background-color: white;
}

.LoginRightTab {
  padding: 40px;

}

.LoginRightTitle {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-start;
  box-sizing: border-box;
  border-bottom: 1px solid rgb(204, 204, 204);
}

.el-menu.el-menu--horizontal {
  border-bottom: 0;
  border-bottom-color: transparent;
}

.el-menu-item {
  font-size: 26px;
  font-weight: bold;
}

.el-menu--horizontal>.el-menu-item {
  margin: 0 25px;
}


.el-menu--horizontal>.el-menu-item.is-active {
  border-bottom-color: rgb(219, 57, 56);
  border-bottom-width: 5px;
}

.LoginRightIndex {
  flex: 1 0 auto;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}

.LoginRightIndexRouter {
  position: relative;
  width: 600px;
  height: 720px;
  background-size: 100% 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.LoginRightIndexForm {
  width: 70%;
  z-index: 1;
  padding: 48px;
}

.LoginRightIndexFormTitle {
  text-align: center;
  font-size: 30px;
  font-weight: bold;
  min-width: 410px;
}

.LoginRightIndexFormInput {
  margin-top: 25px;

}

.LoginRightIndexFormButton {
  width: 100%;
  color: #fff;
  background-color: #d93025;
  border: 0;
  display: inline-block;
  line-height: 1;
  white-space: nowrap;
  cursor: pointer;
  text-align: center;
  box-sizing: border-box;
  outline: 0;
  margin: 0;
  margin-top: 25px;
  transition: .1s;
  font-weight: 500;
  padding: 12px 20px;
  font-size: 18px;
  border-radius: 4px;
}

.LoginRightIndexBackground {
  position: relative;
  width: 100%;
  height: 100%;
}

.LoginRightIndexBackground::before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-image: url(../../assets/images/ChineseAthletics.png);
  background-repeat: no-repeat;
  background-size: auto 100%;
  background-position: center;
  opacity: 0.04;
  pointer-events: none;
  z-index: 0;
}

.LoginRightIndexRegister {
  font-size: 14px;
  margin-top: 20px;
  text-align: right;
  color: rgb(174, 174, 174);
  font-weight: bold;
  cursor: pointer;
}
</style>