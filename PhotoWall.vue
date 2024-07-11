<template>
  <nav class="navbar">
    <img src="@/assets/logo.png" alt="Logo" class="logo" style="height: 55px;">
    <button @click="navigateTo('/')">首页</button>
    <button @click="navigateTo('/profile')">个人空间</button>
    <button class="current_button" @click="navigateTo('/photowall')">照片墙</button>
  </nav>

  <div class="content">
    <h1 style="text-align: left; color: #c81623; margin: 20px;">比赛照片，精彩直击</h1>
    <div class="filter-section">
      <select id="sort" v-model="sortOrder" @change="sortPhotos">
        <option value="latest">最新</option>
        <option value="hottest">最热</option>
      </select>
      <input type="text" v-model="searchQuery" placeholder="输入拍摄地点搜索照片..." @keyup.enter="searchPhotos" class="search-input"/>
    </div>
  </div>
 
  <div class="photo-gallery">
    <div class="photo-frame" v-for="(photo, index) in photos" :key="index">
        <img :src="photo.src" alt="Photo" class="photo" />
        <div class="info-box">
          <div style="text-align: left; font-size: 14px; padding-left: 5px; padding-top: 5px;">
            <p>摄影师: {{ photo.photographer }}</p>
            <p>日期: {{ photo.date }}</p>
            <p>地点: {{ photo.location }}</p>
          </div>
          <button @click="toggleLike(index)" class="like-button" :class="{ liked: photo.liked }">
            {{ photo.liked ? '❤️'+'1001' : '🤍'+'1000' }}
          </button>
        </div>
    </div>
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
        // 示例照片数据
        { src: require('@/assets/1.jpg'), liked: false, photographer: '摄影师1', date: '2023-07-10', location: '地点1' },
        { src: require('@/assets/2.jpg'), liked: false, photographer: '摄影师2', date: '2023-07-11', location: '地点2' },
        { src: require('@/assets/3.jpg'), liked: false, photographer: '摄影师3', date: '2023-07-12', location: '地点3' },
        { src: require('@/assets/4.jpg'), liked: false, photographer: '摄影师4', date: '2023-07-13', location: '地点4' },
        { src: require('@/assets/5.jpg'), liked: false, photographer: '摄影师5', date: '2023-07-14', location: '地点5' },
        { src: require('@/assets/6.jpg'), liked: false, photographer: '摄影师6', date: '2023-07-15', location: '地点6' },
        // 其他照片数据
      ]
    }
  },
  methods: {
    navigateTo(path) {
      this.$router.push(path)
    },
    toggleLike(index) {
      this.photos[index].liked = !this.photos[index].liked;
      this.photos[index].likes += this.photos[index].liked ? 1 : -1;
      // 这里可以添加与数据库的交互来更新点赞数
    },
    getPhotoTooltip(photo) {
      return `摄影师: ${photo.photographer}<br>日期: ${photo.date}<br>地点: ${photo.location}`;
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
  background-color: #c81623; /* 酒红色 */
  padding: 20px;
  display: flex;
  justify-content: space-around;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  z-index: 1000; /* 确保 navbar 在最上层 */
}

.content {
  margin-top: 80px; /* 根据 navbar 的高度和 padding 调整 */
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.filter-section {
  display: flex;
  align-items: center;
  margin-left: auto; 
}

.filter-section select {
  padding: 5px;
  font-size: 16px;
  border-radius: 10px;
  background-color: #c3c3c3; 
  color: white; 
}

.filter-section input.search-input {
  padding: 8px;
  font-size: 16px;
  margin-left: 20px;
  background-color: #c3c3c3; 
  color:white;
  border: 1px solid #c3c3c3; 
  border-radius: 10px;
  width: 300px; /* 设置搜索框的宽度 */
}

.filter-section input::placeholder {
  color: white; /* 修改占位符文本颜色 */
}

button {
  background-color: #c81623; /* 酒红色 */
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background-color: white;
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
  gap: 30px; 
  background-color: #e8e8e8;
  padding: 10px;
}

.photo-frame {
  width: 220px;
  height: 400px;
  position: relative;
  overflow: hidden;
  border: 2px;
  background-color: #e8e8e8;
}

.photo {
  width: 100%;
  height: 75%;
  object-fit: cover;
  border-radius: 8px; 
}

.info-box{
  margin-top: 6px; 
  width: 100%;
  height: 25%;
  background-color: white;
  border-radius: 8px; 
}

.like-button {
  position: absolute;
  bottom: 5px; 
  right: 5px; 
  background-color: #c3c3c3;
  border: none;
  cursor: pointer;
  font-size: 13wpx;
  padding: 2px 10px;
  border-radius: 8px; 
}

.like-button.liked {
  background-color: #ff6666;
}

</style>

