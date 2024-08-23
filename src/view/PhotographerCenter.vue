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
        <div class="myPhotographyWorks" @click="navigateTo('/photographerCenter/myPhotographyWorks')">
          <h2 style="text-align:left; font-size: 24px; color: #565656;">我的摄影作品</h2>
          <div class="photo-bar">
            <div class="photo-frame" v-for="(photo, index) in photos" :key="index">
              <img :src="photo.src" alt="Photo" class="photo" />
            </div>
          </div>
          <button @click="navigateTo('/photographerCenter/myPhotographyWorks')" style="font-size: 16px; color: #007bff; background: none; border: none; margin-top:10px;">&gt;点击查看详情...</button>
        </div>
    
        <div class="uploadNewPhotography">
          <h2 style="text-align:left; font-size: 24px; color: #565656;">上传新摄影</h2>
          <div class="upload-button-container">
            <div class="upload-button" @click="handleUpload()">+</div>

            <el-dialog :visible.sync="formVisible" title="（单次上传一张摄影作品）">
              <el-form  ref="form" :model="form" :rules="rules" label-width="80px">
                <el-form-item label="照片名称">
                  <el-input v-model="form.name"></el-input>
                </el-form-item>
                <el-form-item label="摄影时间" prop="date1">
                  <el-col :span="15">
                    <el-date-picker type="date" placeholder="选择日期" v-model="form.date1" style="width: 100%;"></el-date-picker>
                  </el-col>
                </el-form-item>

                <el-form-item>
                  <el-upload action="#"
                       list-type="picture-card"
                       limit="1"
                       :on-success="handleSuccess"
                       :file-list="fileList"
                       :on-change="handleChange"
                       :auto-upload="false">
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
                  <el-button type="primary" @click="onSubmit">上传</el-button>
                  <el-button @click="handleCancel">取消</el-button>
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
export default {
  name: 'PhotographerCenter',
  props: {msg: String},
  data() {
    return {
      subWeb: 'PhotoWall',
      photos: [
        { src: require('@/assets/1.jpg') },
        { src: require('@/assets/2.jpg') },
        { src: require('@/assets/3.jpg') },
        { src: require('@/assets/4.jpg') },
        { src: require('@/assets/5.jpg') },
        { src: require('@/assets/6.jpg') },
      ],

      dialogImageUrl: '',
      dialogVisible: false,
      formVisible: false,
      disabled: false,
      fileSelected: false,
      fileList: [],

      form: {
        name: '',
        date1: '',
        desc: ''
      },
      rules: {
        //name: [
        //  { required: true, message: '请输入照片名称', trigger: 'blur' }
        //],
        date1: [
          { required: true, message: '请选择摄影时间', trigger: 'change' }
        ]
      }
    }
  },

  methods: {
    navigateTo(_path) {
      this.$router.push({path:_path},()=>{})
    },
    handleUpload(){
      this.formVisible=true;
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
      
      this.fileSelected=true;
      return (isJPG || isPNG) && isLt10M;
    },
    handleChange(file, fileList) {
      //const isValid = this.beforeUpload(file);
      const isValid=true;
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
  border-radius: 5px; 
  background-color: rgb(210, 210, 210);
  margin-left: 17px;
}

.photo {
  width: 100%;
  height: 100%;
  object-fit: cover;
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
  margin-top: 20px;
  margin-left:210px;
}

.uploadNewPhotography {
  background-color: #ffffff;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  width: 990px;
  height:260px;
  padding: 20px;
  margin-top: 25px;
  margin-left:210px;
}

</style>

