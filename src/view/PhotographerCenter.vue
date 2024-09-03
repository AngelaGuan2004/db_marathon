<template>
  <div id="PhotographerCenter">
    <div class="MainContent">
      <div class="PhotographerInfo">
        <div class="PhotographerInfoUpper">
          <div class="PhotographerInfoTitle">基本信息</div>
        </div>
        <hr class="line" />
        <div class="PhotographerInfoText">
          <div class="a-button">
            <p class="down-notice">姓名</p>
            <p class="down-notice2">{{ name }}</p>
          </div>
          <div class="a-button">
            <p class="down-notice">ID</p>
            <p class="down-notice2">{{ photographer_Id }}</p>
          </div>
        </div>
      </div>
      <div class="PhotographerContent">
        <div class="myPhotographyWorks" @click="navigateTo('/UserTab/myPhotographyWorks')">
          <h2 style="font-size: 24px;margin-left: 10px;">我的摄影作品</h2>
          <div class="photo-bar">
            <div class="photo-frame" v-for="(photo, index) in photos" :key="index">
              <img :src="photo.address" alt="Photo" class="photo" />
            </div>
          </div>
          <button @click="navigateTo('/UserTab/myPhotographyWorks')"
            style="font-size: 16px; color: #007bff; background: none; border: none; margin-top:10px;">&gt;点击查看详情...</button>
        </div>
        <div class="uploadNewPhotography">
          <h2 style="font-size: 24px;margin-left: 10px;">上传新摄影</h2>
          <div class="upload-button-container">
            <div class="upload-button" @click="handleUpload()">+</div>
            <el-dialog :visible.sync="formVisible" title="（单次上传一张摄影作品）">
              <el-form ref="form" :model="form" :rules="rules" label-width="100px">
                <el-form-item label="摄影时间" prop="time">
                  <el-col :span="15">
                    <el-date-picker type="date" placeholder="选择日期" v-model="form.time"
                      style="width: 100%;"></el-date-picker>
                  </el-col>
                </el-form-item>
                <el-form-item label="赛事" prop="event">
                  <el-select v-model="form.event" placeholder="请选择赛事">
                    <el-option v-for="event in events" :key="event.value" :label="event.name" :value="event.id">                    </el-option>
                  </el-select>
                </el-form-item>
                <el-form-item label="拍摄地点" prop="location">
                  <el-input v-model="form.location"></el-input>
                </el-form-item>
                <el-form-item>
                  <el-upload action="#" list-type="picture-card" limit="1" :on-success="handleSuccess"
                    :file-list="fileList" :on-change="handleChange" :auto-upload="false">
                    <i slot="default" class="el-icon-plus"></i>
                    <div slot="file" slot-scope="{file}">
                      <img class="el-upload-list__item-thumbnail" :src="file.url" alt="">
                      <span class="el-upload-list__item-actions">
                        <span class="el-upload-list__item-preview" @click="handlePictureCardPreview(file)">
                          <i class="el-icon-zoom-in"></i>
                        </span>
                        <span v-if="!disabled" class="el-upload-list__item-delete" @click="handleRemove(file)">
                          <i class="el-icon-delete"></i>
                        </span>
                      </span>
                    </div>
                  </el-upload>
                  <el-dialog :visible.sync="dialogVisible">
                    <img width="100%" :src="dialogImageUrl" alt="">
                  </el-dialog>
                </el-form-item>
                <el-form-item>
                  <el-button type="primary" @click="onSubmit" class="PhotographerCenterButton">上传</el-button>
                  <el-button @click="handleCancel" class="PhotographerCenterButton">取消</el-button>
                </el-form-item>
              </el-form>
            </el-dialog>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
//import { getPhotographerInfor } from '@/api/UserCenter.js'
import { getAllEvents, inquiryPhotoByPhotographer,inquiryPhotographerNameById } from '@/api/Photo';

export default {
  name: 'PhotographerCenter',
  props: { msg: String },
  data() {
    return {
      name: '',
      photographer_Id: '',//暂时把摄影师用户信息写死
      photos:[],
      dialogImageUrl: '',
      dialogVisible: false,
      formVisible: false,
      disabled: false,
      fileSelected: false,
      fileList: [],
      events: [],

      form: {
        time: '',
        event: '',
        location: '',
      },
      rules: {
        time: [
          { required: true, message: '请选择摄影时间', trigger: 'change' }
        ],
        event: [
          { required: true, message: '请选择照片所属赛事', trigger: 'change' }
        ],
        location: [
          { required: true, message: '请输入拍摄地点', trigger: 'blur' }
        ]
      }
    }
  },
  async mounted(){
    this.photographer_Id = localStorage.getItem('UserId')
    const photographerName = await inquiryPhotographerNameById(this.photographer_Id);

    console.log('得到摄影师名',photographerName);

    try { 
      // 获取所有赛事
      const eventsResponse = await getAllEvents();
      this.events = eventsResponse.map(event => ({
        id: event.id,
        name: event.name
      }));
      console.log('赛事信息：',this.events);
    } catch (error) {
      console.error('获取赛事失败:', error);
    }
    
      //获取当前摄影师照片
      const photoResponse = await inquiryPhotoByPhotographer(photographerName);

      this.photos=photoResponse.map(photo=>{
        return {
            ...photo,
            time: photo.time.split(' ')[0],  // 只保留年月日部分
            address:'http://'+photo.address
        };
      });
      console.log('收到的照片',this.photos);

    
  },
  methods: {
    navigateTo(_path) {
      this.$router.push({ path: _path }, () => { })
    },
    handleUpload() {
      this.formVisible = true;
    },
    handleRemove(file) {
      this.fileList = this.fileList.filter(f => f.uid !== file.uid);
      this.fileSelected = this.fileList.length > 0;
    },
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    handleSuccess(res, file) {
      this.fileList.push(file);
      this.fileSelected = true;
      this.imageUrl = URL.createObjectURL(file.raw);
    },
    beforeUpload(file) {
      const isJPG = file.type === 'image/jpeg';
      const isPNG = file.type === 'image/png';
      const isLt10M = file.size / 1024 / 1024 < 10;

      if (!isJPG && !isPNG) {
        this.$message.error('上传的摄影作品只能是 JPG 或 PNG 格式!');
      }
      if (!isLt10M) {
        this.$message.error('上传摄影作品大小不能超过 10MB!');
      }

      this.fileSelected = true;
      return (isJPG || isPNG) && isLt10M;
    },
    handleChange(file, fileList) {
      //const isValid = this.beforeUpload(file);
      const isValid = true;
      if (isValid) {
        this.fileList = fileList;
        this.fileSelected = true;
      } else {
        this.fileList = this.fileList.filter(f => f.uid !== file.uid);
      }
    },
    onSubmit() {
      this.$refs.form.validate((valid) => {
        if (valid) {
          if (!this.fileSelected) {
            this.$message.error('请上传摄影作品');
            return false;
          }
          this.$message.success('上传成功');
          this.formVisible = false;
        } else {
          this.$message.error('表单验证失败');
          return false;
        }
      });
    },
    resetForm() {
      this.$refs.form.resetFields();
      this.fileList = [];
      this.fileSelected = false;
    },
    handleCancel() {
      this.formVisible = false;
      this.resetForm();
    }
  },
  // mounted() {
  //   this.ID = localStorage.getItem('UserId')
  //   getPhotographerInfor(this.ID)
  //     .then((res) => {
  //       this.name = res.Name;
  //     })
  //     .catch(error => {
  //       console.error('查询失败:', error);
  //       alert('查询失败');
  //     });
  // }
}
</script>


<style scoped>
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/Base.css";
@import "@/assets/css/PhotographerCenter.css";
</style>
