<template>
  <div id='PhotoWall'>
    <div style="margin-left: 6%;margin-top: 20px;">
      <el-breadcrumb separator-class="el-icon-arrow-right">
        <el-breadcrumb-item :to="{ path: '/Home' }">首页</el-breadcrumb-item>
        <el-breadcrumb-item>照片墙</el-breadcrumb-item>
      </el-breadcrumb>
    </div>
    <div class="PhotoWallContent">
      <div>
        <div class="PhotoWallSearch">
          <h1 style="color: #c81623;margin-left: 25px;">比赛照片，精彩直击</h1>
          <div style="line-height: 90px;width: 30%;">
            <el-select v-model="select" slot="prepend" style="width:80px;" @change="sortPhotos">
              <el-option label="最新" value="1"></el-option>
              <el-option label="最热" value="2"></el-option>
            </el-select>
            <el-input placeholder="请输入关键词检索照片" v-model="input3" class="input-with-select" style="width:60%;"></el-input>
            <el-button slot="append" icon="el-icon-search"></el-button>
          </div>
        </div>
        <div class="PhotoWallGallery">
          <div class="PhotoFrame" v-for="(photo, index) in paginatedPhotos" :key="index">
            <span style="width: 100%;height:72%;position: relative;display: inline-block;">
              <button @click="toggleLike(index)" class="like-button" :class="{ liked: photo.liked }">
                {{ photo.liked ? '❤️' + photo.likes : '🤍' + photo.likes }}
              </button>
              <img :src="photo.src" alt="Photo" class="photo" @click="openPreview(photo)" />

            </span>

            <div class="info-box">
              <div style="text-align: left; font-size: 14px; padding-left: 5px; padding-top: 2px;line-height: 10px;">
                <p><b>摄影师：</b><span>{{ photo.photographer }}</span></p>
                <p><b>日期：</b><span> {{ photo.date }}</span></p>
                <p><b>地点：</b><span> {{ photo.location }}</span></p>
                <p><b>赛事：</b><span>{{ photo.event }}</span> </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <el-pagination v-if="photos.length > 0" background layout="prev, pager, next" :total="photos.length"
      class="pagination" :page-size="pageSize" :current-page="currentPage" @current-change="handlePageChange">
    </el-pagination>
    <!-- 图片预览框 -->
    <el-dialog :visible.sync="dialogVisible" width="60%" center>
      <img :src="currentPhoto.src" alt="Preview" style="width: 100%;" />
    </el-dialog>
  </div>
</template>

<script>

export default {
  name: 'PhotoWall',
  props: {
    msg: String
  },
  data() {
    return {
      photos: [
        { src: require('@/assets/images/1.jpg'), liked: false, event: '北京马拉松', photographer: '摄影师1', date: '2023-07-10', location: '地点1', likes: 1000 },
        { src: require('@/assets/images/2.jpg'), liked: false, event: '北京马拉松', photographer: '摄影师2', date: '2023-07-11', location: '地点2', likes: 900 },
        { src: require('@/assets/images/3.jpg'), liked: false, event: '北京马拉松', photographer: '摄影师3', date: '2023-07-12', location: '地点3', likes: 800 },
        { src: require('@/assets/images/4.jpg'), liked: false, event: '上海国际马拉松', photographer: '摄影师4', date: '2023-07-13', location: '地点4', likes: 700 },
        { src: require('@/assets/images/5.jpg'), liked: false, event: '上海国际马拉松', photographer: '摄影师5', date: '2023-07-14', location: '地点5', likes: 600 },
        { src: require('@/assets/images/6.jpg'), liked: false, event: '广州马拉松', photographer: '摄影师6', date: '2023-07-15', location: '地点6', likes: 500 },
        { src: require('@/assets/images/7.jpg'), liked: false, event: '广州马拉松', photographer: '摄影师7', date: '2023-07-15', location: '地点7', likes: 499 },
        { src: require('@/assets/images/8.jpg'), liked: false, event: '厦门马拉松', photographer: '摄影师8', date: '2023-07-20', location: '地点8', likes: 620 },
        { src: require('@/assets/images/9.jpg'), liked: false, event: '厦门马拉松', photographer: '摄影师9', date: '2023-07-17', location: '地点9', likes: 50 },
        { src: require('@/assets/images/1.jpg'), liked: false, event: '北京马拉松', photographer: '摄影师1', date: '2023-07-10', location: '地点1', likes: 1000 },
        { src: require('@/assets/images/2.jpg'), liked: false, event: '北京马拉松', photographer: '摄影师2', date: '2023-07-11', location: '地点2', likes: 900 },
        { src: require('@/assets/images/3.jpg'), liked: false, event: '北京马拉松', photographer: '摄影师3', date: '2023-07-12', location: '地点3', likes: 800 },
        { src: require('@/assets/images/4.jpg'), liked: false, event: '上海国际马拉松', photographer: '摄影师4', date: '2023-07-13', location: '地点4', likes: 700 },
        { src: require('@/assets/images/5.jpg'), liked: false, event: '上海国际马拉松', photographer: '摄影师5', date: '2023-07-14', location: '地点5', likes: 600 },
        { src: require('@/assets/images/6.jpg'), liked: false, event: '广州马拉松', photographer: '摄影师6', date: '2023-07-15', location: '地点6', likes: 500 },
        { src: require('@/assets/images/7.jpg'), liked: false, event: '广州马拉松', photographer: '摄影师7', date: '2023-07-15', location: '地点7', likes: 499 },
        { src: require('@/assets/images/8.jpg'), liked: false, event: '厦门马拉松', photographer: '摄影师8', date: '2023-07-20', location: '地点8', likes: 620 },
        { src: require('@/assets/images/9.jpg'), liked: false, event: '厦门马拉松', photographer: '摄影师9', date: '2023-07-17', location: '地点9', likes: 50 },
      ],
      //photos:[],
      input3: '',  // 这是用于暂存输入内容的变量
      select: '2', // 默认排序为最热
      dialogVisible: false,
      currentPhoto: {},
      currentPage: 1, // 当前页码
      pageSize: 10,    // 每页显示的照片数量
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
    },
  },
  methods: {
    navigateTo(_path) {
      this.$router.push({ path: _path }, () => { })
    },
    toggleLike(index) {
      this.photos[index].liked = !this.photos[index].liked;
      this.photos[index].likes += this.photos[index].liked ? 1 : -1;
      // 这里可以添加与数据库的交互来更新点赞数
    },
    getPhotoTooltip(photo) {
      return `摄影师: ${photo.photographer}<br>日期: ${photo.date}<br>地点: ${photo.location}`;
    },
    sortPhotos() {
      if (this.select === '1') {
        // 按日期排序，最新的在前
        this.photos.sort((a, b) => new Date(b.date) - new Date(a.date));
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
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";

#PhotoWall {
  margin-top: 125px;
}

#PhotoWall .el-dropdown-menu {
  background-color: #c81623;
  margin-top: 40px;
  border-radius: 5px;
  border-color: #c81623;
}

#PhotoWall .el-dropdown-menu__item {
  color: white;
}

#PhotoWall .el-dropdown-menu__item:hover {
  background-color: white;
}

.PhotoWallContent {
  display: flex;
  margin-top: 15px;
  margin-left: 10%;
  margin-right: 7%;
  padding: 20px;
}

.PhotoWallSearch {
  display: flex;
  justify-content: space-between;
  margin-bottom: 15px;
}

#PhotoWall .el-select .el-input {
  background-color: #fcd7d7;
}

#PhotoWall .input-with-select {
  color: #cf1e1e;
}

#PhotoWall .PhotoFrame button {
  background-color: #c81623;
  color: white;
  border: none;
  padding: 8px 10px;
  cursor: pointer;
  font-size: 14px;
}

#PhotoWall .el-button {
  background-color: #c81623;
  color: white;
}

#PhotoWall button:hover {
  font-weight: bold;
}

.PhotoWallGallery {
  flex: 1 1 auto;
  display: flex;
  flex-wrap: wrap;
  overflow-y: auto;
  column-gap: 16px;
  row-gap: 30px;
  background-color: white;
  padding: 25px 10px;
}

.PhotoFrame {
  width: 220px;
  height: 50vh;
  position: relative;
  overflow: hidden;
  border: 2px;
  background-color: white;
  margin-left: 20px;
}

.photo {
  width: 100%;
  height: 100%;
  object-fit: cover;
  border-radius: 8px;
  cursor: pointer;
}

.info-box {
  margin-top: 6px;
  padding-left: 10px;
  width: 100%;
  height: 25%;
  background-color: #f2f2f2;
  border-radius: 8px;
}

.like-button {
  position: absolute;
  top: 5px;
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

.pagination {
  display: flex;
  justify-content: center;
  margin: 15px 0 30px;
}
</style>