<template>
  <div class="management-container">
    <h2>补给点管理</h2>
    <el-form :model="packageForm" label-width="120px">
      <el-form-item label="比赛名称">
        <el-input v-model="packageForm.eventName" placeholder="比赛名称"></el-input>
      </el-form-item>
    </el-form>
    <el-table :data="supplyPoints">
      <el-table-column prop="id" label="补给点ID"></el-table-column>
      <el-table-column prop="place" label="补给位置"></el-table-column>
      <el-table-column prop="type" label="类型"></el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button type="danger" size="mini" @click="confirmDelete(scope.$index)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <div class="button-group">
      <el-button type="primary" @click="showSupplyPointForm">添加补给点</el-button>
    </div>
    <el-dialog :visible.sync="dialogVisible" width="50%" title="添加补给点">
      <el-form :model="newSupplyPoint" label-width="80px">
        <el-form-item label="补给点ID">
          <el-input v-model="newSupplyPoint.id" placeholder="输入补给点ID"></el-input>
        </el-form-item>
        <el-form-item label="补给位置">
          <el-input v-model="newSupplyPoint.place" placeholder="输入补给位置"></el-input>
        </el-form-item>
        <el-form-item label="类型">
          <el-select v-model="newSupplyPoint.type" placeholder="选择类型">
            <el-option label="水站" value="水站"></el-option>
            <el-option label="功能饮料站" value="功能饮料站"></el-option>
            <el-option label="能量补给点" value="能量补给点"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="addSupplyPoint">添加</el-button>
      </div>
    </el-dialog>

    <el-dialog :visible.sync="confirmDialogVisible" width="30%" title="确认删除">
      <span>确定要删除此补给点？</span>
      <div slot="footer" class="dialog-footer">
        <el-button @click="confirmDialogVisible = false">取消</el-button>
        <el-button type="danger" @click="deleteSupplyPoint">确定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { get_supply_point, add_supply_point, delete_supply_point } from '@/api/supply'; // 导入API函数

export default {
  name: 'SupplyPointManagement',
  data() {
    return {
      packageForm: {
        Event_id: '10001' // 默认Event_id为10001
      },
      supplyPoints: [
       // { id: '1', place: '起点', type: '水站' },
       // { id: '2', place: '终点', type: '功能饮料站' }
      ],
      dialogVisible: false,
      confirmDialogVisible: false,
      deleteIndex: -1,
      newSupplyPoint: {
        id: '',      // 添加补给点时的ID
        place: '',   // 添加补给点时的位置
        type: ''     // 添加补给点时的类型
      }
    };
  },
  methods: {
    async fetchSupplyPoints() {
      // 使用默认的 Event_id 来获取补给点信息
      try {
        const response = await get_supply_point(this.packageForm.Event_id);
        this.supplyPoints = response.data; // 假设API返回的数据在data属性中
      } catch (error) {
        console.error('获取补给点信息失败:', error);
      }
    },
    showSupplyPointForm() {
      this.dialogVisible = true;
    },
    async addSupplyPoint() {
      // 检查补给点ID是否已存在
      const existingPoint = this.supplyPoints.find(point => point.id === this.newSupplyPoint.id);
      if (existingPoint) {
        this.$message.error('补给点ID已存在，请使用唯一ID');
        return;
      }

      try {
        // 调用API添加补给点
        await add_supply_point({
          eventId: this.packageForm.Event_id,
          supplyPointID: this.newSupplyPoint.id,
          supplyPointLocation: this.newSupplyPoint.place,
          supplyPointType: this.newSupplyPoint.type
        });
        this.supplyPoints.push({ ...this.newSupplyPoint });
        this.dialogVisible = false;
        this.$message.success('添加补给点成功!');
      } catch (error) {
        console.error('添加补给点失败:', error);
        this.$message.error('添加补给点失败');
      }

      // 清空表单字段
      this.newSupplyPoint = { id: '', place: '', type: '' };
    },
    confirmDelete(index) {
      this.deleteIndex = index;
      this.confirmDialogVisible = true;
    },
    async deleteSupplyPoint() {
      if (this.deleteIndex !== -1) {
        try {
          const supplyPointID = this.supplyPoints[this.deleteIndex].id;
          await delete_supply_point(supplyPointID);
          this.supplyPoints.splice(this.deleteIndex, 1);
          this.confirmDialogVisible = false;
          this.$message.success('删除成功!');
        } catch (error) {
          console.error('删除补给点失败:', error);
          this.$message.error('删除补给点失败');
        }
      }
    }
  },
  created() {
    this.fetchSupplyPoints(); // 页面加载时默认获取补给点信息
  }
};
</script>

<style scoped>
.management-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 30px;
  background-color: white;
  border: 1px solid grey;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}
.button-group {
  display: flex;
  justify-content: flex-end;
  margin-top: 20px;
}
</style>
