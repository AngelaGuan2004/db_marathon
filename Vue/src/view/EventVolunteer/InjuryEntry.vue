<template>
  <div id="InjuryEntry">
    <div class="injury-list">
      <el-main v-if="injuredPlayers.length > 0">
        <div style="margin-left: 45px;display: flex;justify-content: space-between;">
          <div style="display: inline-block;">
            <h2 style="font-size: 24px; color: black;">{{ this.$route.params.name }}</h2>
            <h2 style="font-size: 24px; color: black;">伤员名单</h2>
          </div>
        </div>
        <div class="table-container">
          <el-table :data="injuredPlayers" stripe max-height="415">
            <el-table-column prop="name" label="姓名" width="150"></el-table-column>
            <el-table-column prop="ID" label="编号" width="300"></el-table-column>
            <el-table-column prop="medicalPoint" label="医疗点" width="150"></el-table-column>
            <el-table-column label="操作" width="150">
              <template slot-scope="scope">
                <el-button type="primary" size="small" @click.native.prevent="handleDelete(scope.$index, scope.row)"
                  style="color: rgb(168, 27, 31);" plain>删除</el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
      </el-main>
      <div v-else class="Empty">暂无数据</div>

      <div style="text-align: right;margin-right: 50px;">
        <el-button type="primary" @click="handleAddRow">添加</el-button>
      </div>

      <el-dialog :visible.sync="formVisible" width="40%" title="添加伤员" class="InjuryEntryDialog">
        <el-form ref="InjuryForm" :model="form" :rules="rules" label-width="25%" :row-key="getRowKey">
          <el-form-item label="伤员姓名" prop="name">
            <el-input v-model="form.name"></el-input>
          </el-form-item>
          <el-form-item label="伤员编号" prop="ID">
            <el-input v-model="form.ID"></el-input>
          </el-form-item>
          <el-form-item label="医疗点" prop="medicalPoint">
            <el-select v-model="form.medicalPoint" placeholder="请选择医疗点">
              <el-option v-for="point in medicalPoints" :key="point.id" :label="point.place" :value="point.id">
              </el-option>
            </el-select>
          </el-form-item>
          <div style="text-align: right;margin-right: 50px;margin-top: 15px;">
            <el-button type="primary" style="margin-right: 10px;" @click="onSubmit" plain>上传</el-button>
            <el-button type="danger" @click="handleCancel" plain>取消</el-button>
          </div>
        </el-form>
      </el-dialog>
    </div>
  </div>
</template>

<script>
import { getAllMedicalPoints, addInjury, getInjury, deleteInjury } from '@/api/Services';

export default {
  name: 'InjuryEntry',
  data() {
    return {
      injuredPlayers: [],
      medicalPoints: [],
      eventID: this.$route.params.event_id,//需要一个函数来get

      formVisible: false,
      editingIndex: null,
      editingMode: false,

      form: {
        name: '',
        ID: '',
        medicalPoint: ''
      },

      rules: {
        name: [
          { required: true, message: '请输入伤员姓名', trigger: 'blur' }
        ],
        ID: [
          { required: true, message: '请输入伤员编号', trigger: 'blur' }
        ],
        medicalPoint: [
          { required: true, message: '请选择医疗点', trigger: 'blur' }
        ],
      }
    }
  },
  async mounted() {
    console.log(getInjury); // 确保这不是 undefined 或 null

    try {
      //获取医疗点
      const response = await getAllMedicalPoints(this.eventID);
      this.medicalPoints = response.data.map(point => ({
        id: point.id,
        place: point.place
      }));
      console.log('医疗点', this.medicalPoints);


      // 获取伤员名单
      const injuryResponse = await getInjury(this.eventID);
      this.injuredPlayers = injuryResponse.map(injuredPlayer => ({
        name: injuredPlayer.injury.name,
        ID: injuredPlayer.injury.id_Number,
        medicalPoint: injuredPlayer.medicalpoint.place
      }));
      console.log('伤员', this.injuredPlayers);
    } catch (error) {
      console.error('获取失败:', error);
    }
  },
  methods: {
    getRowKey(row) {
      return row.ID;
    },
    navigateTo(_path) {
      this.$router.push({ path: _path }, () => { })
    },
    handleDelete(index, row) {
      this.$confirm(`确认删除选手 ${row.name} 吗？`, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
      }).then(async () => {
        try {
          console.log(111, row)
          // 发送删除请求
          const response = await deleteInjury(
            (row.ID + ""),
            this.medicalPoints.find(point => point.place === row.medicalPoint).id // 查找对应的医疗点名称
          );

          // 根据后端返回的状态处理结果
          if (response.status === true) {
            const deleteIndex = this.injuredPlayers.findIndex(item => item.ID === row.ID);
            if (deleteIndex !== -1) {
              this.injuredPlayers.splice(deleteIndex, 1);
            }
            this.$message.success('删除成功!');
          } else if (response.status === false) {
            this.$message.error(`删除失败: ${response.message}`);
          }
        } catch (error) {
          this.$message.error('删除失败，请稍后再试');
          console.error('Error deleting data:', error);
        }
      }).catch(() => {
        this.$message.info('已取消删除');
      });
    },
    handleAddRow() {
      this.formVisible = true;
      this.resetForm();
    },
    async onSubmit() {
      this.$refs.InjuryForm.validate(async (valid) => {
        if (valid) {
          try {
            // 使用表单中的数据调用 addInjury 函数
            const response = await addInjury({
              medicalPoint_Id: this.form.medicalPoint, // 传递医疗点ID
              IdNumber: this.form.ID, // 传递选手身份证号
              name: this.form.name
            });
            if (response.status) {
              // 成功后，将新数据添加到表格中
              this.injuredPlayers.push({
                name: this.form.name, // 伤员姓名
                ID: this.form.ID, // 伤员身份证号
                medicalPoint: this.medicalPoints.find(point => point.id === this.form.medicalPoint).place // 查找对应的医疗点名称
              });
              this.$message.success('上传成功');
            } else {
              this.$message.error(`上传失败，请重试`);
            }

            // 重置表单并关闭弹窗
            this.formVisible = false;
            this.resetForm();
          } catch (error) {
            // 捕获异常并显示错误信息
            this.$message.error('上传失败，请稍后再试');
            console.error('Error submitting data:', error);
          }
        } else {
          this.$message.error('表单验证失败');
        }
      });
    },
    resetForm() {
      this.$refs.InjuryForm.resetFields();
      this.form = {
        name: '',
        ID: '',
        medicalPoint: ''
      };
      this.editingMode = false;
      this.editingIndex = null;
    },
    handleCancel() {
      this.formVisible = false;
      this.resetForm();
    }
  }
}
</script>


<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/InjuryEntry.css";
</style>