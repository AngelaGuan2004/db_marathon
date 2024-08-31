<template>
  <div>
    <nav class="navbar">
      <img src="@/assets/logo.png" alt="Logo" class="logo" style="height: 55px;">
      <button @click="navigateTo('/')">首页</button>
      <button @click="navigateTo('/profile')">个人空间</button>
      <button @click="navigateTo('/photoWall')">赛事摄影</button>
      <button @click="navigateTo('/injuryEntry')">伤员</button>
    </nav>       
    
    <div class="main-content">
      <div class="sidebar">
        <button @click="navigateTo('/photoWall')" class="sidebar-button">照片墙</button>
        <button @click="navigateTo('/photographerCenter')" class="sidebar-button" style="font-weight:bold;">摄影师中心</button>
      </div>
      
      <div class="content">
        <div class="titlebar">
          <button class="back-button" @click="navigateTo('/photographerCenter')">‹</button>
          <h1 style="text-align: left; color: #969696;margin-top: 10px;margin-left:10px">我的摄影作品</h1>
        </div>
        
        <div class="recent-works">
          <h2 style="text-align:left; font-size: 24px; color: #565656;">近期作品</h2>
          <el-carousel :interval="4000" type="card" height="260px" style="width: 90%;margin-left: 40px;">
            <el-carousel-item v-for="(photo, index) in recentPhotos" :key="index">
              <img :src="photo.src" alt="Photo" class="carousel-image" />
            </el-carousel-item>
          </el-carousel>
        </div>

        <div class="all-works">
          <h2 style="text-align:left; font-size: 24px; color: #565656;">全部作品
            <el-select v-model="select" slot="prepend" style="width:80px; margin-left: 750px;" @change="sortPhotos">
              <el-option label="最新" value="1"></el-option>
              <el-option label="最热" value="2"></el-option>
            </el-select>
          </h2>
          <div style="
              flex: 1 1 auto;
              display: flex;
              flex-wrap: wrap;
              column-gap: 16px; 
              row-gap: 15px;
              padding: 10px;">
            <div class="photo-frame" v-for="photo in myphotos" :key="photo.id">
              <img :src="photo.address" alt="Photo" class="photo" @click="openPreview(photo)"/>
              <div class="info-box">
                <div style="text-align: left; font-size: 14px; padding-left: 5px; padding-top: 5px;">
                  <p>日期: {{ photo.time }}</p>
                  <p>地点: {{ photo.location }}</p>
                  <p>赛事: {{ photo.event_id}}</p>
                  <p style="color:crimson">❤️: {{ photo.likes}}</p>
                </div>
               
              </div>
            </div>
          </div>
          <!-- 图片预览框 -->
          <el-dialog :visible.sync="dialogVisible" width="60%" center>
            <img :src="currentPhoto.address" alt="Preview" style="width: 100%;" />
          </el-dialog>
          <!-- 翻页 -->
          <div style="display: flex; justify-content: center; margin-top: 10px;">
            <el-pagination
              background
              layout="prev, pager, next"
              :total="10"
              >
            </el-pagination>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>


<script>
import { inquiryPhotoByPhotographer } from '@/api/Photo';


export default {
    name:'MyPhotographyWorks',
    data(){
      return{
        photographerID: 73,
        // myphotos: [
        //   { src: require('@/assets/1.jpg'), date: '2023-07-10', location: '地点1', likes: 1000 },
        //   { src: require('@/assets/3.jpg'), date: '2023-07-12', location: '地点3', likes: 800 },
        //   { src: require('@/assets/5.jpg'), date: '2023-07-14', location: '地点5', likes: 600 },
        //   { src: require('@/assets/7.jpg'), date: '2023-07-15', location: '地点7', likes: 499 },
        //   { src: require('@/assets/9.jpg'), date: '2023-07-17', location: '地点9', likes: 50 },
        //   { src: require('@/assets/10.jpg'), date: '2023-06-10', location: '地点3', likes: 666},
        //   { src: require('@/assets/11.jpg'), date: '2023-05-22', location: '地点1', likes: 888 },
        // ],
        myphotos:[],
        select: '2', // 默认排序为最热
        dialogVisible: false,
        currentPhoto: {}
      }
    },
    computed: {
      recentPhotos() {
        // 将 myphotos 按日期降序排列，并取出最近的 6 张照片
        return this.myphotos
          .slice() // 创建 myphotos 的浅拷贝
          .sort((a, b) => new Date(b.time) - new Date(a.time))
          .slice(0, 6);
      }
    },
    async mounted() {
      await this.fetchPhotosByPhotographer(this.photographerID);
    },
     methods:{
      navigateTo(_path) {
        this.$router.push({path:_path},()=>{})
      },
      sortPhotos() {
        if (this.select === '1') {
          // 按日期排序，最新的在前
          this.myphotos.sort((a, b) => new Date(b.time) - new Date(a.time));
        } else if (this.select === '2') {
          // 按点赞数排序，最多的在前
          this.myphotos.sort((a, b) => b.likes - a.likes);
        }
      },
      openPreview(photo) {
        this.currentPhoto = photo;
        this.dialogVisible = true;
      },
      async fetchPhotosByPhotographer(ID) {
        try {
          const response = await inquiryPhotoByPhotographer(ID);
          this.myphotos = response;
          console.log("收到的照片数据:", this.myphotos);
        } catch (error) {
          console.error('获取摄影作品时发生错误:', error);
          this.$message.error('获取摄影作品失败');
        }
      }
    }
}
    //一张photo的数据：
    // {
    //     "id": 8,
    //     "event_id": "10091",
    //     "time": "2023/3/26 0:00:00",
    //     "location": "联创街",
    //     "photographer_id": 73,
    //     "address": "C:\\Users\\Administrator\\Desktop\\DBexcel\\photo\\91\\8.jpg",
    //     "likes": 921
    // }
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

  button {
    background-color: #c81623;
    color:white;
    border: none;
    padding: 10px 20px;
    cursor: pointer;
    font-size: 16px;
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
    margin-top: 20px;
    border-radius: 5px; 
  }
  
  .sidebar-button:hover {
    background-color: #ffd7d7;
    font-weight: bold;
  }

  .content {
    flex: 1;
    margin-left:200px;
  }

  .titlebar{
    display: flex;
    height: 65px;
    width:100%;
    margin-top: 20px;
  }

  .back-button{
    background-color:white;
    color:#969696;
    font-size:50px;
    height: 60px;
    padding-top: 0px;
    margin-left:10px;
  }

  .back-button:hover{
    background-color: #ffd7d7;
    font-weight: bold;
  }

  .recent-works {
    background-color: #ffffff;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    width: 1000px;
    height:340px;
    padding: 20px;
    margin-top: 10px;
    margin-left:30px;
  }

  .all-works {
    background-color: #ffffff;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    width: 1000px;
    height:900px;
    padding: 20px;
    margin-top: 30px;
    margin-left:30px;
  }

  .el-carousel__item h3 {
    color: #475669;
    font-size: 14px;
    opacity: 0.75;
    line-height: 200px;
    margin: 0;
  }
  
  .el-carousel__item:nth-child(2n) {
    background-color: #99a9bf;
  }
  
  .el-carousel__item:nth-child(2n+1) {
    background-color: #d3dce6;
  }

  .carousel-image {
    width: 100%; 
    height: 100%; 
    object-fit: cover; 
  }
  
  .photo-frame {
    width: 200px;
    height: 370px;
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
    height: 22%;
    background-color: #f2f2f2;
    border-radius: 8px; 
  }
  
  .photo-manage-button{
    width: 20px; 
    height: 25px; 
    margin-top: 6px; 
    margin-left: 8px; 
    background-color: #409effc7;
    font-size: 13px;
    text-align: center;
  }
</style>
