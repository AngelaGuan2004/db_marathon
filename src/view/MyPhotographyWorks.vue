<template>
  <div id="MyPhotographyWorks">
    <div style="display: flex;">
      <div class="MyPhotographyWorksContent">
        <div class="recent-works">
          <h2 style=" color: black;">近期作品</h2>
          <el-carousel :interval="4000" type="card" height="260px" style="width: 90%;margin-left: 40px;">
            <el-carousel-item v-for="(photo, index) in recentPhotos" :key="index">
              <img :src="photo.address" alt="Photo" class="carousel-image" />
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
              <img :src="photo.address" alt="Photo" class="photo" @click="openPreview(photo)" />
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
            <img :src="currentPhoto.address" alt="Preview" style="width: 100%;" />
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
import { inquiryPhotoByPhotographer } from '@/api/Photo';
export default {
  name: 'MyPhotographyWorks',
  data() {
    return {
      photographer_Id:73,
      // myphotos: [
      //   { src: require('@/assets/images/1.jpg'), date: '2023-07-10', location: '地点1', likes: 1000 },
      //   { src: require('@/assets/images/3.jpg'), date: '2023-07-12', location: '地点3', likes: 800 },
      //   { src: require('@/assets/images/5.jpg'), date: '2023-07-14', location: '地点5', likes: 600 },
      //   { src: require('@/assets/images/7.jpg'), date: '2023-07-15', location: '地点7', likes: 499 },
      //   { src: require('@/assets/images/9.jpg'), date: '2023-07-17', location: '地点9', likes: 50 },
      //   { src: require('@/assets/images/10.jpg'), date: '2023-06-10', location: '地点3', likes: 666 },
      //   { src: require('@/assets/images/11.jpg'), date: '2023-05-22', location: '地点1', likes: 888 },
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
        .slice() 
        .sort((a, b) => new Date(b.date) - new Date(a.date))
        .slice(0, 6);
    }
  },

  async mounted(){
    await this.fetchPhotosByPhotographer(this.photographer_Id)
  },

  methods: {
    async fetchPhotosByPhotographer(ID) {
        try {
          const response = await inquiryPhotoByPhotographer(ID);
          this.photos=response.map(photo=>{
            return {
              ...photo,
              time: photo.time.split(' ')[0],  // 只保留年月日部分
              address:'http://'+photo.address
          };
        });
        console.log("收到的照片数据:", this.myphotos);
        } catch (error) {
          console.error('获取摄影作品时发生错误:', error);
          this.$message.error('获取摄影作品失败');
        }
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
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";
@import "@/assets/css/MyPhotographyWorks.css";
</style>
