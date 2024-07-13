<template>
  <div>
      <div id="upper">
          <div id="upper-left" @click="goToShouye">
              <img src="@/assets/返回.png" alt="" class="img-left">
              <p class="p-upper">首页</p>
          </div>
          <img src="@/assets/上马.png" alt="" class="img-right">
      </div>
      <div id="down">
          <div id="div-left">
              <div class="break"></div> 
              <div>
                  <button :class="checkClass(0)" @click="goToBaoming">我的报名</button>
              </div>
              <div>
                  <button :class="checkClass(1)" @click="goToChengji">我的成绩</button>
              </div>
              <div>
                  <button :class="checkClass(2)" >个人信息</button>
              </div>
              <div>
                  <button :class="checkClass(3)" @click="goToZhiyuan">我的志愿</button>
              </div>
          </div>
          <div id="div-middle">
              <button class="relative-button" @click="changeButton">{{ buttonText }}</button>
              <div id="down-upper">
                 <p class="jbxx">基本信息</p>
              </div>
              <hr class="a-line" />
              <div id="down-down">
                  <div class="a-button">
                      <p class="down-notice">姓名</p>
                      <p class="down-notice2">{{ name }}</p>
                  </div>
                  <div class="a-button">
                      <p class="down-notice">手机号</p>
                      <p class="down-notice2" v-if="!bianji">{{ telephone }}</p>
                      <input  v-model="telephone" placeholder="请输入您的电话"  v-if="bianji" class="down-notice2 border"/>
                      <p class='error-notice' v-if="bianji && !tele_right">输入有误</p>
                  </div>
                  <div class="a-button">
                      <p class="down-notice">证件</p>
                      <p class="down-notice2">{{ ID_number }}</p>
                  </div>
                  <div class="a-button">
                      <p class="down-notice">ID</p>
                      <p class="down-notice2">{{ ID }}</p>
                  </div>
                  <div class="a-button">
                      <p class="down-notice">性别</p>
                      <p class="down-notice2">{{ gender }}</p>
                  </div>
                  <div class="a-button">
                      <p class="down-notice">地区</p>
                      <p class="down-notice2" v-if="!bianji && region !== ''">{{region}}</p>
                      <div class="select-reg down-notice2"  v-if="bianji">
                          <div>
                              <label for="province" class="label-p">省份：</label>
                              <select id="province" v-model="selectedProvince" @change="updateCities">
                                  <option v-for="province in provinces" :key="province" :value="province">{{ province }}</option>
                              </select>
                          </div>
                          <div>
                              <label for="province" class="label-c">城市：</label>
                              <select id="city" v-model="selectedCity">
                                  <option v-for="city in cities" :key="city" :value="city">{{ city }}</option>
                              </select>
                          </div>
                      </div>  
                  </div>
                  <div class="a-button">
                      <p class="down-notice">出生日期</p>
                      <p class="down-notice2" v-if="!bianji &&birthday!== ''">{{birthday}}</p>
                      <div class="down-notice2"  v-if="bianji">
                        <input type="date" v-model="birthday" />
                      </div>
                     
                      
                  </div>
              </div>
          </div>
          <div id="div-right1">
              <img src="@/assets//veer-141859030.jpg" alt="" class="down-right-img">
          </div>
      </div>
  </div>
</template>
  
  <script>
  /*import {completeInfor} from '@/api/UserCenter.js'*/
  /*import {getInfor} from '@/api/UserCenter.js' */
  export default {
    name:'UserInfo',
    data() {
      return {
        buttonText: "编辑",
        bianji:false,
        tele_right:true,
        ischecked: [false, false, true, false],
        name:'李靓',
        telephone:'12637152637',
        ID_number:'273618633622772662',
        ID:'2272186183',   /* 这个参数怎么来？存疑 */
        gender:'女',
        birthday:'',
        region:'',
        provinces: [
          '北京', '天津', '上海', '重庆', '河北', '山西', '辽宁', '吉林', '黑龙江', 
          '江苏', '浙江', '安徽', '福建', '江西', '山东', '河南', '湖北', '湖南', 
          '广东', '海南', '四川', '贵州', '云南', '陕西', '甘肃', '青海', '台湾', 
          '内蒙古', '广西', '西藏', '宁夏', '新疆', '香港', '澳门'
        ],
        allCities: {
          北京: ['北京'],
          天津: ['天津'],
          上海: ['上海'],
          重庆: ['重庆'],
          河北: ['石家庄', '唐山', '秦皇岛', '邯郸', '邢台', '保定', '张家口', '承德', '沧州', '廊坊', '衡水'],
          山西: ['太原', '大同', '阳泉', '长治', '晋城', '朔州', '晋中', '运城', '忻州', '临汾', '吕梁'],
          辽宁: ['沈阳', '大连', '鞍山', '抚顺', '本溪', '丹东', '锦州', '营口', '阜新', '辽阳', '盘锦', '铁岭', '朝阳', '葫芦岛'],
          吉林: ['长春', '吉林', '四平', '辽源', '通化', '白山', '松原', '白城', '延边朝鲜族自治州'],
          黑龙江: ['哈尔滨', '齐齐哈尔', '鸡西', '鹤岗', '双鸭山', '大庆', '伊春', '佳木斯', '七台河', '牡丹江', '黑河', '绥化', '大兴安岭'],
          江苏: ['南京', '无锡', '徐州', '常州', '苏州', '南通', '连云港', '淮安', '盐城', '扬州', '镇江', '泰州', '宿迁'],
          浙江: ['杭州', '宁波', '温州', '嘉兴', '湖州', '绍兴', '金华', '衢州', '舟山', '台州', '丽水'],
          安徽: ['合肥', '芜湖', '蚌埠', '淮南', '马鞍山', '淮北', '铜陵', '安庆', '黄山', '滁州', '阜阳', '宿州', '巢湖', '六安', '亳州', '池州', '宣城'],
          福建: ['福州', '厦门', '莆田', '三明', '泉州', '漳州', '南平', '龙岩', '宁德'],
          江西: ['南昌', '景德镇', '萍乡', '九江', '新余', '鹰潭', '赣州', '吉安', '宜春', '抚州', '上饶'],
          山东: ['济南', '青岛', '淄博', '枣庄', '东营', '烟台', '潍坊', '济宁', '泰安', '威海', '日照', '莱芜', '临沂', '德州', '聊城', '滨州', '菏泽'],
          河南: ['郑州', '开封', '洛阳', '平顶山', '安阳', '鹤壁', '新乡', '焦作', '濮阳', '许昌', '漯河', '三门峡', '南阳', '商丘', '信阳', '周口', '驻马店', '济源'],
          湖北: ['武汉', '黄石', '十堰', '宜昌', '襄阳', '鄂州', '荆门', '孝感', '荆州', '黄冈', '咸宁', '随州', '恩施土家族苗族自治州', '仙桃', '潜江', '天门', '神农架'],
          湖南: ['长沙', '株洲', '湘潭', '衡阳', '邵阳', '岳阳', '常德', '张家界', '益阳', '郴州', '永州', '怀化', '娄底', '湘西土家族苗族自治州'],
          广东: ['广州', '深圳', '珠海', '汕头', '韶关', '佛山', '江门', '湛江', '茂名', '肇庆', '惠州', '梅州', '汕尾', '河源', '阳江', '清远', '东莞', '中山', '潮州', '揭阳', '云浮'],
          海南: ['海口', '三亚', '三沙', '儋州'],
          四川: ['成都', '自贡', '攀枝花', '泸州', '德阳', '绵阳', '广元', '遂宁', '内江', '乐山', '南充', '眉山', '宜宾', '广安', '达州', '雅安', '巴中', '资阳', '阿坝藏族羌族自治州', '甘孜藏族自治州', '凉山彝族自治州'],
          贵州: ['贵阳', '六盘水', '遵义', '安顺', '毕节', '铜仁', '黔西南布依族苗族自治州', '黔东南苗族侗族自治州', '黔南布依族苗族自治州'],
          云南: ['昆明', '曲靖', '玉溪', '保山', '昭通', '丽江', '普洱', '临沧', '楚雄彝族自治州', '红河哈尼族彝族自治州', '文山壮族苗族自治州', '西双版纳傣族自治州', '大理白族自治州', '德宏傣族景颇族自治州', '怒江傈僳族自治州', '迪庆藏族自治州'],
          陕西: ['西安', '铜川', '宝鸡', '咸阳', '渭南', '延安', '汉中', '榆林', '安康', '商洛'],
          甘肃: ['兰州', '嘉峪关', '金昌', '白银', '天水', '武威', '张掖', '平凉', '酒泉', '庆阳', '定西', '陇南', '临夏回族自治州', '甘南藏族自治州'],
          青海: ['西宁', '海东', '海北藏族自治州', '黄南藏族自治州', '海南藏族自治州', '果洛藏族自治州', '玉树藏族自治州', '海西蒙古族藏族自治州'],
          台湾: ['台北', '高雄', '基隆', '台中', '台南', '新竹', '嘉义'],
          内蒙古: ['呼和浩特', '包头', '乌海', '赤峰', '通辽', '鄂尔多斯', '呼伦贝尔', '巴彦淖尔', '乌兰察布', '兴安盟', '锡林郭勒盟', '阿拉善盟'],
          广西: ['南宁', '柳州', '桂林', '梧州', '北海', '防城港', '钦州', '贵港', '玉林', '百色', '贺州', '河池', '来宾', '崇左'],
          西藏: ['拉萨', '日喀则', '昌都', '林芝', '山南', '那曲', '阿里'],
          宁夏: ['银川', '石嘴山', '吴忠', '固原', '中卫'],
          新疆: ['乌鲁木齐', '克拉玛依', '吐鲁番', '哈密', '昌吉回族自治州', '博尔塔拉蒙古自治州', '巴音郭楞蒙古自治州', '阿克苏地区', '克孜勒苏柯尔克孜自治州', '喀什地区', '和田地区', '伊犁哈萨克自治州', '塔城地区', '阿勒泰地区'],
          香港: ['香港'],
          澳门: ['澳门']
        },
        selectedProvince: '',
        selectedCity: ''
      }
    },
    mounted(){
      /* 
      getInfor(this.ID)
        .then((res) => {
          this.name = res.name;
          this.telephone = res.telephone;
          this.ID_number= res.ID_number;
          this.gender=res.gender;
          this.region=this.region;
        })
        .catch(error => {
          console.error('查询失败:', error);
          alert('查询失败');
        });
      */
    },
    methods: {
      goToShouye(){
        
      },
      goToBaoming() {
        this.$router.push({ name: 'MyRegistrations' });
      },
      goToChengji() {
        this.$router.push({ name: 'MyResults' });
      },
      goToZhiyuan() {
        this.$router.push({ name: 'MyVolunteering' });
      },
      checkClass(index) {
        return this.ischecked[index] ? 'active' : 'inactive'
      },
      changeButton() {
        if(this.bianji==false){
          this.buttonText='保存'
          this.bianji=true
        }
        else if(this.bianji==true){
          if(this.tele_right){
           /* 调用js */
           /*this.submit();*/
            

            this.buttonText='编辑'
            this.bianji=false
          }
          else{
            console.log("无法保存")
   
          }
        }
      },
      /*
      submit() {
        try{
          completeInfor({ID: this.ID, region:this.region,telephone:this.telephone}).then((res) => {
            if (res.data === false) {
              alert("总会成功的！");
            } else {
              alert("提交成功");
            }
          })
        }catch(error){
          console.error(error);
          alert('总会成功的！');
        }
      },
      */
      updateCities() {
        if (this.cities.length > 0) {
          this.selectedCity = this.cities[0]
        } else {
          this.selectedCity = ''
        }
      },
    },
    computed: {
  
      cities() {
        return this.allCities[this.selectedProvince] || []
      },
    },
    watch: {
      telephone(newVal) {
        var reg = /^1\d{10}$/;
        this.tele_right= reg.test(newVal);
      },
      selectedProvince() {
      this.region = this.selectedProvince + ' ' + this.selectedCity;
    },
    selectedCity() {
      this.region = this.selectedProvince + ' ' + this.selectedCity;
    }

    },
  
  }
  
  
  
  </script>
  
  <style scoped>
  /* 把我们所有标签的内外边距清零 */
  * {
      margin: 0;
      padding: 0;
      /* css3盒子模型 */
      box-sizing: border-box;
  }
  
  /* em 和 i 斜体的文字不倾斜 */
  em,
  i {
      font-style: normal
  }
  
  /* 去掉li 的小圆点 */
  li {
      list-style: none
  }
  
  img {
      /* border 0 照顾低版本浏览器 如果 图片外面包含了链接会有边框的问题 */
      border: 0;
      /* 取消图片底侧有空白缝隙的问题 */
      vertical-align: middle
  }
  
  button {
      /* 当我们鼠标经过button 按钮的时候，鼠标变成小手 */
      cursor: pointer
  }
  
  a {
      color: #666;
      text-decoration: none
  }
  
  a:hover {
      color: #c81623
  }
  
  button,
  input {
      /* "\5B8B\4F53" 就是宋体的意思 这样浏览器兼容性比较好 */
      font-family: Microsoft YaHei, Heiti SC, tahoma, arial, Hiragino Sans GB, "\5B8B\4F53", sans-serif;
      /* 默认有灰色边框我们需要手动去掉 */
      border: 0;
      outline: none;
  }
  
  body {
      /* CSS3 抗锯齿形 让文字显示的更加清晰 */
      -webkit-font-smoothing: antialiased;
      background-color: #fff;
      font: 12px/1.5 Microsoft YaHei, Heiti SC, tahoma, arial, Hiragino Sans GB, "\5B8B\4F53", sans-serif;
      color: #666
  }
  
  .hide,
  .none {
      display: none
  }
  
  /* 清除浮动 */
  .clearfix:after {
      visibility: hidden;
      clear: both;
      display: block;
      content: ".";
      height: 0
  }
  
  /*上部分*/ 
  
  #upper{
    margin-top: 0px;
    display: flex;
    justify-content: space-between;
    background-color: rgb(168, 27, 31);
    height: 120px;
  }
  #upper-left{
    display: flex;
    justify-content: space-between;
  }
  .img-left{
    margin-top: 30px;
    margin-left: 50px;
    height: 50px;
    width: 50px;
  }
  .img-right{
    margin-right: 20px;
  }
  .p-upper{
    margin-top: 32px;
    margin-left:20px;
    color:white;
    font-size: 30px;
  }
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  
  }
  
  /* 下部分 */
  #down{
    display: flex;
    justify-content: space-between;
    background-color:#f4f4f6;
  }
  
  #div-left{
    width: 150px;
    height: 500px;
    /*border: 1px solid red;*/
    background-color:white;
    margin-left: 30px;
    margin-top: 30px;
  }
  #div-middle{
    width: 500px;
    height: 500px;
    /*border: 1px solid green;*/
    background-color:white;
    margin-left: 20px;
    margin-top: 30px;
    margin-bottom: 28px;
    position: relative;
  }
  #div-right1{
    width: 500px;
    height: 500px;
    margin-right: 30px;
    margin-top: 30px;
  }
  .down-right-img{
    width: 500px;
    height: 420px;
  }
  
  /* 下部分的中间部分 */
  #down-upper{
    width: 400px;
    height: 50px;
  }
  #down-down{
    width: 400px;
    height: 300px;
  }
  .a-line {
    border: none;
    height: 2px; 
    background-color: #f4f4f6;
  }
  .jbxx{
    margin-top: 18px;
    color:black;
    font-size: 20px;
    text-align:left;
    margin-left:50px;
  }
  .down-notice{
    width: 100px;
    color:black;
    font-size: 18px;
    text-align:left;
    margin-left:50px;
  }
  .down-notice2{
    color:black;
    font-size: 18px;
    text-align:left;
    margin-left:10px;
  }

  
  .a-button{
    margin-top:18px;
    display: flex; 
    align-items: center;
  }
  .label-p{
    font-size: 18px;
    text-align:left;
    color:black;
  }
  .label-c{
    font-size: 18px;
    text-align:left;
    margin-left:33px;
    color:black;
  }
  .select-reg{
    display:flex;
  }
  .border{
    border:1px solid #2c3e50;
  }
  .error-notice{
    color:rgb(168, 27, 31);
    margin-right:2px;
  }
  /* 下部分的左间部分 */
  .inactive{
      margin-left:20px;
      font-size:20px;
      color:black;
      background-color: white;
      margin-top: 30px;
    }
    .active{
      margin-left:20px;
      margin-top: 30px;
      font-size:20px;
      color:rgb(168, 27, 31);
      background-color: white;
    }
  .break{
    margin-top:60px;
  }
  
  .relative-button {
    position: absolute;
    top: 18px;
    right: 100px;
    font-size:20px;
    color:rgb(168, 27, 31);
    background-color: white;
  }

  </style>
  