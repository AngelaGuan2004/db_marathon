<template>
  <div id="Home">
    <div class="Carousel">
      <el-carousel :interval="4000" height="600px">
        <el-carousel-item v-for="CarouselPicture in CarouselPictures" :key="CarouselPicture.id">
          <img :src="require(`@/assets/images/${CarouselPicture.url}`)" alt="" class="CarouselPictures">
          <div class="CarouselTextOutside">
            <div class="CarouselText">
              <p style="color: white;">{{ CarouselPicture.text }}</p>
            </div>
          </div>
        </el-carousel-item>
      </el-carousel>
    </div>
    <div class="RecentlyStarted">
      <div class="RecentlyStartedHeader">
        <div class="RecentlyStartedTitle">
          <div>近期开赛</div>
          <div class="RecentlyStartedTitleLine"></div>
        </div>
        <span class="RecentlyStartedMore">查看更多></span>
      </div>
      <div class="RecentlyStartedBody">
        <el-timeline :reverse="false">
          <el-timeline-item v-for="(RecentlyStartedEvent, index) in RecentlyStartedEvents" :key="index"
            :timestamp="RecentlyStartedEvent.timestamp" :hide-timestamp="true">
            <div class="RecentlyStartedTimestamp">{{ RecentlyStartedEvent.timestamp }}</div>
            <div style="display: inline-block;">{{ RecentlyStartedEvent.content }}</div>
          </el-timeline-item>
        </el-timeline>
      </div>
    </div>
    <div class="ShowBest">
      <div class="Record">
        <div class="RecordTitle">国内记录</div>
        <div class="RecordPeople">
          <div class="RecordMan">
            <div>
              <img src="@/assets/images/RecordMan.jpg" alt="">
            </div>
            <div class="RecordManTitle">
              <div style="font-size: 24px;font-weight: bold;">宁夏·何杰</div>
              <div class="RecordManTitleLine"></div>
              <div style="font-size: 22px;margin-top: 10px;line-height: 35px;">
                男子全程马拉松<br>
                2024-03-24
              </div>
              <div class="RecordManScore">
                2:06:57
              </div>
            </div>
          </div>
          <div class="RecordWoMan">
            <div>
              <img src="@/assets/images/RecordWoman.jpg" alt="">
            </div>
            <div class="RecordManTitle">
              <div style="font-size: 24px;font-weight: bold;">辽宁·孙英杰</div>
              <div class="RecordManTitleLine"></div>
              <div style="font-size: 22px;margin-top: 10px;line-height: 35px;">
                女子全程马拉松<br>
                2003-10-19
              </div>
              <div class="RecordManScore">
                2:19:39
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="BestScore Record">
        <div class="RecordTitle BestScoreTitle">国内最好成绩</div>
        <div class="RecordPeople">
          <div class="RecordMan">
            <div>
              <img src="@/assets/images/BestScoreMan.png" alt="">
            </div>
            <div class="RecordManTitle">
              <div style="font-size: 24px;font-weight: bold;">西藏·扎西次仁</div>
              <div class="RecordManTitleLine"></div>
              <div style="font-size: 22px;margin-top: 10px;line-height: 35px;">
                男子半程马拉松<br>
                2024-02-25
              </div>
              <div class="RecordManScore">
                1:01:57
              </div>
            </div>
          </div>
          <div class="RecordWoMan">
            <div>
              <img src="@/assets/images/BestScoreWoman.png" alt="">
            </div>
            <div class="RecordManTitle">
              <div style="font-size: 24px;font-weight: bold;">云南·张德顺</div>
              <div class="RecordManTitleLine"></div>
              <div style="font-size: 22px;margin-top: 10px;line-height: 35px;">
                女子半程马拉松<br>
                2024-02-02
              </div>
              <div class="RecordManScore">
                1:07:55
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Home',
  data() {
    return {
      CarouselPictures: [
        {
          id: "01",
          url: "CarouselOne.png",
          text: "刷新赛会纪录！2023中国10公里精英赛总决赛圆满收官"
        },
        {
          id: "02",
          url: "CarouselTwo.jpg",
          text: "第五届中国马拉松博览会 暨2024福建省路跑产业博览会"
        },
        {
          id: "03",
          url: "CarouselThree.jpg",
          text: "国内最好男女半马成绩出现！男子前三刷新赛会纪录！2024“仁马”新年首马，比快更快！"
        },
        {
          id: "04",
          url: "CarouselFour.png",
          text: `2024无锡马拉松|何杰2:06:57再度打破全国纪录，中国男子马拉松进入206时代`
        }
      ],
      RecentlyStartedEvents: [{
        content: '2024巴东野三关半程马拉松',
        timestamp: '2024.08.25'
      }, {
        content: '2024哈尔滨马拉松',
        timestamp: '2024.08.25'
      }, {
        content: '2024康保马拉松',
        timestamp: '2024.08.25'
      }, {
        content: '2024兰州新区半程马拉松',
        timestamp: '2024.09.01'
      }, {
        content: '2024长春马拉松赛',
        timestamp: '2024.09.01'
      }, {
        content: '2024宁夏黄河金岸（吴忠）马拉松',
        timestamp: '2024.09.08'
      }]
    }
  }
}
</script>

<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";
@import "@/assets/css/Home.css";
</style>