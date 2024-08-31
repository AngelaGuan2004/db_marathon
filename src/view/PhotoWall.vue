<template>
  <div>
    <nav class="navbar">
      <img src="@/assets/logo.png" alt="Logo" class="logo" style="height: 55px;">
      <button @click="navigateTo('/')">首页</button>
      <button @click="navigateTo('/profile')">个人空间</button>
      <el-dropdown>
        <button type="primary" style="margin-top: 6px;">赛事摄影<i class="el-icon-arrow-down el-icon--right"></i></button>
        <el-dropdown-menu slot="dropdown" class="custom-dropdown-menu">
          <el-dropdown-item @click="navigateTo('/photoWall')">照片墙</el-dropdown-item>
          <el-dropdown-item @click="navigateTo('/photographerCenter')">摄影师中心</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <button @click="navigateTo('/injuryEntry')">伤员</button>
    </nav>
  
    <div class="main-content">
      <div class="sidebar">
        <button @click="navigateTo('/photoWall')" class="sidebar-button" style="font-weight:bold;">照片墙</button>
        <button @click="navigateTo('/photographerCenter')" class="sidebar-button">摄影师中心</button>
      </div>
  
      <div>
        <div class="content">
          <h1 style="text-align: left; color: #c81623;margin-top: 20px">比赛照片，精彩直击
            <el-input placeholder="请输入关键词检索照片" v-model="input3" class="input-with-select" style="margin-left:300px;width:400px;">
              <el-select v-model="select" slot="prepend" style="width:80px;"  @change="sortPhotos">
                  <el-option label="最新" value="1"></el-option>
                  <el-option label="最热" value="2"></el-option>
              </el-select>
              <el-button slot="append" icon="el-icon-search"></el-button>
            </el-input>
          </h1>
        </div>
  
        <div class="photo-gallery">
          <div class="photo-frame" v-for="(photo, index) in paginatedPhotos" :key="index">
            <img :src="photo.src" alt="Photo" class="photo"  @click="openPreview(photo)"/>
            <div class="info-box">
              <div style="text-align: left; font-size: 14px; padding-left: 5px; padding-top: 5px;">
                <p>摄影师: {{ photo.photographer_name }}</p>
                <p>日期: {{ photo.time }}</p>
                <p>地点: {{ photo.location }}</p>
                <p>赛事: {{ photo.event_name }}</p>
              </div>
              <button @click="toggleLike(index)" class="like-button" :class="{ liked: photo.liked }">
                {{ photo.liked ? '❤️'+photo.likes : '🤍'+photo.likes }}
              </button>
            </div>
          </div>
        </div>

      </div>  
    </div>

    <!-- 图片预览框 -->
    <el-dialog :visible.sync="dialogVisible" width="60%" center>
      <img :src="currentPhoto.src" alt="Preview" style="width: 100%;" />
    </el-dialog>

    <el-footer style="margin-left:200px; 
                      margin-top: 50px; 
                      width:1050px;
                      background-color: #c81623;
                      display: flex; 
                      justify-content: center;">
      <el-pagination background 
                     layout="prev, pager, next" 
                     :total="10" 
                     style="margin-top: 10px;"
                     :page-size="pageSize"
                     :current-page="currentPage"
                     @current-change="handlePageChange">
      </el-pagination>
    </el-footer>

  </div>
</template>
  
  <script>
  import { getAllPhotos } from '@/api/Photo';

  export default {
    name: 'PhotoWall',
    props: {
      msg: String
    },
    data() {
      return {
        // photos: [
        //   { src: require('@/assets/1.jpg'), liked: false, event: '北京马拉松', photographer: '摄影师1', date: '2023-07-10', location: '地点1', likes: 1000},
        //   { src: require('@/assets/2.jpg'), liked: false, event: '北京马拉松', photographer: '摄影师2', date: '2023-07-11', location: '地点2', likes: 900},
        //   { src: require('@/assets/3.jpg'), liked: false, event: '北京马拉松', photographer: '摄影师3', date: '2023-07-12', location: '地点3', likes: 800},
        //   { src: require('@/assets/4.jpg'), liked: false, event: '上海国际马拉松', photographer: '摄影师4', date: '2023-07-13', location: '地点4', likes: 700},
        //   { src: require('@/assets/5.jpg'), liked: false, event: '上海国际马拉松', photographer: '摄影师5', date: '2023-07-14', location: '地点5', likes: 600},
        //   { src: require('@/assets/6.jpg'), liked: false, event: '广州马拉松', photographer: '摄影师6', date: '2023-07-15', location: '地点6', likes: 500},
        //   { src: require('@/assets/7.jpg'), liked: false, event: '广州马拉松', photographer: '摄影师7', date: '2023-07-15', location: '地点7', likes: 499},
        //   { src: require('@/assets/8.jpg'), liked: false, event: '厦门马拉松', photographer: '摄影师8', date: '2023-07-20', location: '地点8', likes: 620},
        //   { src: require('@/assets/9.jpg'), liked: false, event: '厦门马拉松',   photographer: '摄影师9', date: '2023-07-17', location: '地点9', likes: 50},
        // ],
        photos:[],
        input3: '',  // 这是用于暂存输入内容的变量
        select: '2', // 默认排序为最热
        dialogVisible: false,
        currentPhoto: {},
        currentPage: 1, // 当前页码
        pageSize: 8,    // 每页显示的照片数量
      }
    },
    computed: {
      totalPhotos() {
        return this.photos.length;
      },
      paginatedPhotos() {
        const start = (this.currentPage - 1) * this.pageSize;
        const end = this.currentPage * this.pageSize;
        return this.photos.slice(start, end);
      }
    },
    async mounted(){
      await this.getPhotos();
    },
    methods: {
      async getPhotos(){
        try {
          const response = await getAllPhotos();
          this.photos = response;
          console.log("收到的数据:", this.photos);
        } catch (error) {
          console.error('获所有照片时发生错误:', error);
        }
      },
      navigateTo(_path) {
        this.$router.push({path:_path},()=>{})
      },
      toggleLike(index) {
        this.photos[index].liked = !this.photos[index].liked;
        this.photos[index].likes += this.photos[index].liked ? 1 : -1;
        // 这里可以添加与数据库的交互来更新点赞数
      },
      getPhotoTooltip(photo) {
        return `摄影师: ${photo.photographer}<br>日期: ${photo.time}<br>地点: ${photo.location}`;
      },
      sortPhotos() {
        if (this.select === '1') {
          // 按日期排序，最新的在前
          this.photos.sort((a, b) => new Date(b.time) - new Date(a.time));
        } else if (this.select === '2') {
          // 按点赞数排序，最多的在前
          this.photos.sort((a, b) => b.likes - a.likes);
        }
      },
      openPreview(photo) {
        this.currentPhoto = photo;
        this.dialogVisible = true;
      },
      handlePageChange(page) {
      this.currentPage = page;
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
  
  .el-dropdown-menu {
    background-color: #c81623;
    margin-top: 40px;
    border-radius: 5px;
    border-color: #c81623;
  }

  .el-dropdown-menu__item {
    color: white;
  }

  .el-dropdown-menu__item:hover {
    background-color: white;
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
    box-shadow: 2px 0 5px rgb(209, 209, 209);
  }
  
  .sidebar-button {
    width: 100%;  background-color: #ffebeb; 
    color: #565656; 
    border: none;
    padding: 10px;
    cursor: pointer;
    font-size: 18px;
    margin-bottom: 10px;
    margin-top: 20px;
    border-radius: 5px; 
  }
  
  .sidebar-button:hover {
    background-color: #ffd7d7;
    font-weight: bold;
  }
  
  .content {
    flex: 1;
    padding: 20px;
    margin-left: 200px;
  }
  
  .el-select .el-input {
    background-color: #fcd7d7;
  }

  .input-with-select {
    color: #cf1e1e;
  }
  
  button {
    background-color: #c81623;
    color:white;
    border: none;
    padding: 10px 20px;
    cursor: pointer;
    font-size: 16px;
  }
  
  button:hover {
   font-weight:bold;
  }
  
  .current_button{
    background-color: #ff6666;
    color: white;
  }
  
  .photo-gallery {
    flex: 1 1 auto;
    display: flex;
    flex-wrap: wrap;
    overflow-y: auto;
    column-gap: 16px; 
    row-gap: 30px;
    background-color: white;
    padding: 10px;
    margin-left: 200px;
  }
  
  .photo-frame {
    width: 220px;
    height: 400px;
    position: relative;
    overflow: hidden;
    border: 2px;
    background-color: white;
    margin-left: 20px;
  }
  
  .photo {
    width: 100%;
    height: 73%;
    object-fit: cover;
    border-radius: 8px; 
    transition: transform 0.3s;
  }

  .photo:hover {
    transform: scale(1.05);
  }
  
  .info-box{
    margin-top: 6px; 
    width: 100%;
    height: 21%;
    background-color: #f2f2f2;
    border-radius: 8px; 
  }
  
  .like-button {
    position: absolute;
    bottom: 30px; 
    right: 5px; 
    background-color: #dcdcdc;
    border: none;
    cursor: pointer;
    font-size: 13wpx;
    padding: 2px 10px;
    border-radius: 8px; 
  }
  
  .like-button.liked {
    background-color: #ff6666;
  }
  
  .styled-select {
    background-color: #c81623; 
    color: #f0f0f0; 
    padding: 5px; 
    border-radius: 10px;
    border: none; 
  }
  
  .styled-select option {
    background-color: #e8e8e8; 
    color: #000;
  }
  </style>
  
  
