<template>
  <div id="InjuryEntry">
    <div class="injury-list">
      <div style="margin-left: 45px;height: 30%;display: flex;justify-content: space-between;">
        <div style="display: inline-block;">
          <h2 style="font-size: 24px; color: black;">这里是赛事名称</h2>
          <h2 style="font-size: 24px; color: black;">伤员名单</h2>
        </div>
        <div style="display: inline-block;">
          <div style="margin-right: 25px;margin-top: 100%;">
            <el-button type="primary" @click="handleAddRow"
              style="font-size: 26px; font-weight: bold; background-color: #409effc7;">
              +
            </el-button>
          </div>
        </div>
      </div>
      <div class="table-container">
        <el-table :data="injuredPlayers" stripe height="400">
          <el-table-column prop="name" label="姓名" width="150">
            <template slot-scope="scope">
              <div slot="reference" class="name-wrapper">
                <el-tag size="medium">{{ scope.row.name }}</el-tag>
              </div>
            </template>
          </el-table-column>
          <el-table-column prop="ID" label="身份证号" sortable width="160">
          </el-table-column>
          <el-table-column prop="medicalPoint" label="医疗点">
          </el-table-column>
          <el-table-column label="操作" width="180">
            <template slot-scope="scope">
              <el-button type="primary" size="small" @click="handleEdit(scope.$index, scope.row)"
                style=" font-size: 14px;" icon="el-icon-edit" plain></el-button>
              <el-button size="small" type="primary" @click.native.prevent="handleDelete(scope.$index, scope.row)"
                style="font-size: 14px;" icon="el-icon-delete" plain></el-button>
            </template>
          </el-table-column>
        </el-table>

        <el-dialog :visible.sync="formVisible" width="40%" title="添加伤员">
          <el-form ref="form" :model="form" :rules="rules" label-width="25%" :row-key="getRowKey">
            <el-form-item label="伤员姓名" prop="name">
              <el-input v-model="form.name"></el-input>
            </el-form-item>
            <el-form-item label="伤员身份证号" prop="ID">
              <el-input v-model="form.ID"></el-input>
            </el-form-item>
            <el-form-item label="医疗点" prop="medicalPoint">
              <el-select v-model="form.medicalPoint" placeholder="请选择医疗点">
                <el-option v-for="point in medicalPoints" :key="point.value" :label="point.place" :value="point.id">
                </el-option>
              </el-select>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="onSubmit" plain>上传<i
                  class="el-icon-upload el-icon--right"></i></el-button>
              <el-button type="danger" @click="handleCancel" plain>取消</el-button>
            </el-form-item>
          </el-form>
        </el-dialog>

      </div>
    </div>
  </div>
</template>


<script>
import { getAllMedicalPoints } from '@/api/Services';
import { getInjury } from '@/api/Services';
import { addInjury } from '@/api/Services';

export default {
  name: 'InjuryEntry',
  data() {
    return {
      injuredPlayers: [],
      medicalPoints: [],
      eventID: 10001,//需要一个函数来get
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
          { required: true, message: '请输入伤员身份证号', trigger: 'blur' }
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
      this.$message.success('获取医疗点');
      //获取医疗点
      const response = await getAllMedicalPoints(this.eventID);
      this.medicalPoints = response.data.map(point => ({
        id: point.id,
        place: point.place 
      }));
      console.log('医疗点',this.medicalPoints);


      // 获取伤员名单
      const injuryResponse = await getInjury(this.eventID);
      this.injuredPlayers = injuryResponse.map(injuredPlayer => ({
        name: injuredPlayer.injury.name,
        ID: injuredPlayer.injury.id_Number,
        medicalPoint: injuredPlayer.medicalpoint.place
      }));
      console.log('伤员',this.injuredPlayers);
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
    handleEdit(index, row) {
      this.editingId = row.ID;  // 记录当前正在编辑的行的唯一 ID
      this.editingIndex = index;
      this.form = {
        date: row.date,
        name: row.name,
        ID: row.ID,
        medicalPoint: row.medicalPoint
      };
      this.editingMode = true;
      this.formVisible = true;
    },
    handleDelete(index, row) {
      this.$confirm(`确认删除选手 ${row.name} 吗？`, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
      }).then(() => {
        const deleteIndex = this.injuredPlayers.findIndex(item => item.ID === row.ID);
        if (deleteIndex !== -1) {
          this.injuredPlayers.splice(deleteIndex, 1);
        }
        this.$message.success('删除成功!');
      }).catch(() => {
        this.$message.info('已取消删除');
      });
    },
    handleAddRow() {
      this.formVisible = true;
      this.resetForm();
    },
    async onSubmit() {
    this.$refs.form.validate(async (valid) => {
      if (valid) {
        try {
          // 使用表单中的数据调用 addInjury 函数
          const response = await addInjury({
            medicalpoint_Id: this.form.medicalPoint, // 传递医疗点ID
            player_Id: this.form.id_Number // 传递选手身份证号
          });

          if (response.data.success) {
            // 成功后，将新数据添加到表格中
            this.injuredPlayers.push({
              name: this.form.name, // 伤员姓名
              ID: this.form.id_Number, // 伤员身份证号
              medicalPoint: this.medicalPoints.find(point => point.id === this.form.medicalPoint).place // 查找对应的医疗点名称
            });
            this.$message.success('上传成功');
          } else {
            // 如果后端返回错误，显示错误信息
            this.$message.error(`上传失败: ${response.data.message}`);
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
      this.$refs.form.resetFields();
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
