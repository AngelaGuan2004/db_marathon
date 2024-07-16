<template>
  <div>

    <nav class="navbar">
      <button @click="navigateTo('/')">首页</button>
      <button @click="navigateTo('/profile')">个人空间</button>  
      <select id="Photograph" v-model="subWeb" @change="switchbToSubweb" class="styled-select">
        <option value="PhotoWall" >照片墙</option>
        <option value="PhotographerCenter">摄影师中心</option>
      </select>
    </nav>
    
    <div class="main-content">
      <div class="sidebar">
        <button @click="navigateTo('/subpage1')" class="sidebar-button">照片墙</button>
        <button @click="navigateTo('/subpage2')" class="sidebar-button" style="font-weight:bold;">摄影师中心</button>
      </div>

      <div class="content">
        <div class="myPhotographyWorks" @click="navigateTo('/')">
          <h2 style="text-align:left; font-size: 24px; color: #565656;">我的摄影作品</h2>
          <div class="photo-bar">
            <div class="photo-frame" v-for="(photo, index) in photos" :key="index">
              <img :src="photo.src" alt="Photo" class="photo" />
            </div>
          </div>
          <button @click="navigateTo('/MyPhotographyWorks')" style="font-size: 16px; color: #007bff; background: none; border: none; margin-top:10px;">&gt;点此查看详情...</button>
        </div>
    
        <div class="uploadNewPhotography">
          <h2 style="text-align:left; font-size: 24px; color: #565656;">上传新摄影</h2>
          <div class="upload-button-container">
            <input type="file" ref="fileInput" @change="handleFileUpload" style="display: none;">
            <button @click="triggerFileInput" class="upload-button">+</button>
          </div>
        </div>

      </div>

    </div>

  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  data() {
    return {
      subWeb: 'PhotoWall',
      photos: [
        { src: require('@/assets/1.jpg') },
        { src: require('@/assets/2.jpg') },
        { src: require('@/assets/3.jpg') },
        { src: require('@/assets/4.jpg') },
        { src: require('@/assets/5.jpg') },
      ]
    }
  },
  methods: {
    navigateTo(path) {
      this.$router.push(path)
    },
    switchbToSubweb() {
      this.$router.push({ path: `/${this.subWeb.toLowerCase()}` })
    },
    triggerFileInput() {
      this.$refs.fileInput.click();
    },
    handleFileUpload(event) {
      const file = event.target.files[0];
      // 处理文件上传逻辑
      console.log('Selected file:', file);
    }
  }
}
</script>


<style scoped>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

html, body {
  height: 100%;
}

.navbar {
  background-color: #c81623; 
  padding: 20px;
  display: flex;
  justify-content: space-around;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  z-index: 1000; /* 确保 navbar 在最上层 */
}

.main-content {
  display: flex;
  margin-top: 80px;
}

.sidebar {
  width: 200px;
  height:100vh;
  position:fixed;
  background-color: #ffebeb;
  padding: 20px;
  box-shadow: 2px 0 5px rgba(0,0,0,0.1);
}

.sidebar-button {
  width: 100%;  background-color: #ffebeb; 
  color: #565656; 
  border: none;
  padding: 10px;
  cursor: pointer;
  font-size: 18px;
  margin-bottom: 10px;
  margin-top: 10px;
  border-radius: 5px; 
}

.sidebar-button:hover {
  background-color: #ffd7d7;
  font-weight: bold;
}

.content {
  flex: 1;
  padding: 20px;
}

.photo-bar{
  display: flex;
  gap: 10px; 
  margin-top: 10px;
  overflow-x: auto;
  white-space: nowrap;
}

.photo-frame {
  display:inline-block;
  width: 130px;
  height: 180px;
  position: relative;
  overflow: hidden;
  border: 2px;
  border-radius: 8px; 
  background-color: rgb(210, 210, 210);
  margin-left: 20px;
}

.photo {
  width: 90%;
  height: 90%;
  margin-top:7px;
  object-fit: cover;
  border-radius: 10px; 
}

button {
  background-color: #c81623;
  color:white;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  font-size: 16px;
}

.upload-button-container {
  display: flex;
  justify-content: flex-start;
  margin-left: 20px;
  margin-top: 15px;
}

.upload-button {
  background-color: #c5c5c5;
  color:white;
  border: none;
  border-radius: 8px;
  padding: 30px 45px;
  cursor: pointer;
  font-size: 50px;
  font-weight: bold;
  margin-top: 20px;
}

button:hover {
 font-weight:bold;
}

.myPhotographyWorks{
  background-color: #ffffff;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  width: 990px;
  height:300px;
  padding: 20px;
  margin-bottom: 20px;
  margin-left:210px;
}

.uploadNewPhotography {
  background-color: #ffffff;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  width: 990px;
  height:300px;
  padding: 20px;
  margin-bottom: 20px;
  margin-left:210px;
}

</style>

