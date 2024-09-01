<template>
  <div id="MyPhotographyWorks">
    <div style="display: flex;">
      <div class="MyPhotographyWorksContent">
        <div class="recent-works">
          <h2 style=" color: black;">近期作品</h2>
          <el-carousel :interval="4000" type="card" height="260px" style="width: 90%;margin-left: 40px;">
            <el-carousel-item v-for="(photo, index) in recentPhotos" :key="index">
              <img :src="photo.src" alt="Photo" class="carousel-image" />
            </el-carousel-item>
          </el-carousel>
        </div>

        <div class="all-works">
          <div style="display: flex;justify-content: space-between;">
            <h2 style=" color: black;">全部作品</h2>
            <el-select v-model="select" slot="prepend" style="width:80px;margin-top: 15px;margin-right: 50px;"
              @change="sortPhotos">
              <el-option label="最新" value="1"></el-option>
              <el-option label="最热" value="2"></el-option>
            </el-select>
          </div>
          <div style="
              flex: 1 1 auto;
              display: flex;
              flex-wrap: wrap;
              column-gap: 16px; 
              row-gap: 15px;
              padding: 10px;
              width: 1200px;">
            <div class="photo-frame" v-for="(photo, index) in myphotos" :key="index">
              <img :src="photo.src" alt="Photo" class="photo" @click="openPreview(photo)" />
              <div class="info-box">
                <div style="text-align: left; font-size: 14px; padding-left: 15px; padding-top: 2px;line-height: 10px;">
                  <p>日期：{{ photo.date }}</p>
                  <p>地点：{{ photo.location }}</p>
                  <p style="color:crimson">❤️：{{ photo.likes }}</p>
                </div>

              </div>
            </div>
          </div>
          <!-- 图片预览框 -->
          <el-dialog :visible.sync="dialogVisible" width="60%" center>
            <img :src="currentPhoto.src" alt="Preview" style="width: 100%;" />
          </el-dialog>
          <!-- 翻页 -->
          <div style="display: flex; justify-content: center; margin-top: 10px;">
            <el-pagination background layout="prev, pager, next" :total="10">
            </el-pagination>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>
<script>

export default {
  name: 'MyPhotographyWorks',
  data() {
    return {
      myphotos: [
        { src: require('@/assets/images/1.jpg'), date: '2023-07-10', location: '地点1', likes: 1000 },
        { src: require('@/assets/images/3.jpg'), date: '2023-07-12', location: '地点3', likes: 800 },
        { src: require('@/assets/images/5.jpg'), date: '2023-07-14', location: '地点5', likes: 600 },
        { src: require('@/assets/images/7.jpg'), date: '2023-07-15', location: '地点7', likes: 499 },
        { src: require('@/assets/images/9.jpg'), date: '2023-07-17', location: '地点9', likes: 50 },
        { src: require('@/assets/images/10.jpg'), date: '2023-06-10', location: '地点3', likes: 666 },
        { src: require('@/assets/images/11.jpg'), date: '2023-05-22', location: '地点1', likes: 888 },
      ],
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
        .sort((a, b) => new Date(b.date) - new Date(a.date))
        .slice(0, 6);
    }
  },
  methods: {
    navigateTo(_path) {
      this.$router.push({ path: _path }, () => { })
    },
    sortPhotos() {
      if (this.select === '1') {
        // 按日期排序，最新的在前
        this.myphotos.sort((a, b) => new Date(b.date) - new Date(a.date));
      } else if (this.select === '2') {
        // 按点赞数排序，最多的在前
        this.myphotos.sort((a, b) => b.likes - a.likes);
      }
    },
    openPreview(photo) {
      this.currentPhoto = photo;
      this.dialogVisible = true;
    }
  }
}
</script>


<style scoped>
#MyPhotographyWorks {
  height: 100%;
  margin-top: -25px;
  margin-bottom: 50px;
}

.sidebar {
  width: 200px;
  height: 100vh;
  position: fixed;
  background-color: #ffebeb;
  padding: 20px;
  box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1);
}

.sidebar-button {
  width: 100%;
  background-color: #ffebeb;
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

.MyPhotographyWorksContent {
  flex: 1;
  margin: 25px 5%;
  width: 100%;
}

.titlebar {
  display: flex;
  height: 65px;
  width: 100%;
  margin-top: 20px;
}

.back-button {
  background-color: white;
  color: #969696;
  font-size: 50px;
  height: 60px;
  padding-top: 0px;
  margin-left: 10px;
}

.back-button:hover {
  background-color: #ffd7d7;
  font-weight: bold;
}

.recent-works {
  background-color: #ffffff;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  width: 100%;
  height: 340px;
  padding: 20px 30px;
  padding-right: 0;
  margin-top: 10px;
}

.all-works {
  background-color: #ffffff;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  width: 100%;
  height: 900px;
  padding: 20px 30px;
  padding-right: 0;
  margin-top: 30px;
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

.info-box {
  margin-top: 6px;
  width: 100%;
  height: 25%;
  background-color: #f2f2f2;
  border-radius: 8px;
}

.photo-manage-button {
  width: 20px;
  height: 25px;
  margin-top: 6px;
  margin-left: 8px;
  background-color: #409effc7;
  font-size: 13px;
  text-align: center;
}
</style>