<template>
  <div id="VolunManagement">
    <div style="display: flex;  width: 100%;">
      <el-main class="Content">
        <div class="Button">
          <div style="margin-left: 55%;margin-top: 10px;">
            <el-button type="primary" @click="save" style="margin: 0 10px;">保存</el-button>
            <el-button type="primary" @click="cancel">取消</el-button>
          </div>
        </div>
        <div v-if="paginatedVolunteers.length > 0">
          <el-table :data="paginatedVolunteers" class="Table" style="width: 100%">
            <el-table-column prop="id" label="ID" width="100"></el-table-column>
            <el-table-column prop="name" label="姓名" width="100"></el-table-column>
            <el-table-column prop="telephone_Number" label="电话" width="200"></el-table-column>
            <el-table-column label="补给点" width="100">
              <template slot-scope="scope">
                <el-checkbox v-model="scope.row.tosupply"
                  @change="handleCheckboxChange(scope.row, 'tosupply')"></el-checkbox>
              </template>
            </el-table-column>
            <el-table-column label="医疗点" width="100">
              <template slot-scope="scope">
                <el-checkbox v-model="scope.row.tomedical"
                  @change="handleCheckboxChange(scope.row, 'tomedical')"></el-checkbox>
              </template>
            </el-table-column>
            <el-table-column label="接驳车" width="100">
              <template slot-scope="scope">
                <el-checkbox v-model="scope.row.tocar" @change="handleCheckboxChange(scope.row, 'tocar')"></el-checkbox>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination class="Pagination" background layout="prev, pager, next" :total="totalVolunteersCount"
            :page-size="pageSize" @current-change="handlePageChange"></el-pagination>
        </div>
        <div v-else class="Empty">
          暂无数据
        </div>
      </el-main>
    </div>
  </div>
</template>

<script>
import { getVolunteersByEventId, scheduleVolunteer, acquireVolunteerInformation } from '@/api/volunteer'; // 假设你的 API 请求方法在这个路径下

export default {
  name: 'VolunManagement',
  data() {
    return {
      currentPage: 1,
      pageSize: 8,
      volunteers: []
    };
  },
  computed: {
    totalVolunteersCount() {
      return this.volunteers.length;
    },
    paginatedVolunteers() { 
      const start = (this.currentPage - 1) * this.pageSize;
      const end = this.currentPage * this.pageSize;
      return this.volunteers.slice(start, end);
    }
  },
  methods: {
    handlePageChange(page) {
      this.currentPage = page;
    },
    handleCheckboxChange(row, selectedField) {
      if (selectedField === 'tosupply') {
        row.tomedical = false;
        row.tocar = false;
      } else if (selectedField === 'tomedical') {
        row.tosupply = false;
        row.tocar = false;
      } else if (selectedField === 'tocar') {
        row.tosupply = false;
        row.tomedical = false;
      }
    },
        async save() {
  try {
    const selectedVolunteers = this.volunteers.filter(volunteer => 
      volunteer.tosupply || volunteer.tomedical || volunteer.tocar
    );

    if (selectedVolunteers.length === 0) {
      this.$message.warning('没有可以保存的信息');
      return; // 如果没有勾选任何志愿者，直接返回
    }

    const eventId = "10001"; // 固定的赛事ID
    const savePromises = selectedVolunteers.map(volunteer => {
      let jobCategory = '';

      if (volunteer.tosupply) {
        jobCategory = '补给点';
      } else if (volunteer.tomedical) {
        jobCategory = '医疗点';
      } else if (volunteer.tocar) {
        jobCategory = '接驳车';
      }

      return scheduleVolunteer({
        Event_Id: eventId,
        Volunteer_Id: volunteer.id,
        Job_category: jobCategory
      });
    });

    await Promise.all(savePromises);
    this.$message.success('保存成功');
    this.loadVolunteers(); // 重新加载志愿者信息

  } catch (error) {
    console.error('Failed to save volunteer assignments:', error);
    this.$message.error('保存失败，请稍后重试');
  }
},

cancel() {
  const anySelection = this.volunteers.some(volunteer =>
    volunteer.tosupply || volunteer.tomedical || volunteer.tocar
  );

  if (!anySelection) {
    this.$message.warning('没有任何勾选可以取消');
    return; // 如果没有勾选任何志愿者，直接返回
  }

  this.volunteers.forEach(p => {
    p.tosupply = false;
    p.tomedical = false;
    p.tocar = false;
  });
  this.$message.info('已取消所有勾选');
},

    async loadVolunteers() {
  try {
    const eventId = "10001"; // 固定的赛事ID
    const response = await getVolunteersByEventId(eventId);
    const volunteersWithNoJobCategory = [];

    // 遍历所有志愿者，查询他们的 job_category
    for (const volunteer of response) {
      const volunteerInfo = await acquireVolunteerInformation(volunteer.id, eventId);

      if (volunteerInfo && volunteerInfo.job_category === "未排班") {
        // 只有未排班的志愿者才加入列表
        volunteersWithNoJobCategory.push({
          id: volunteer.id,
          name: volunteer.name,
          telephone_Number: volunteer.telephone_Number,
          tosupply: false,
          tomedical: false,
          tocar: false
        });
      }
    }

    this.volunteers = volunteersWithNoJobCategory;

    if (this.volunteers.length === 0) {
      this.$message.info('所有志愿者均已分配任务。');
    }

  } catch (error) {
    console.error('Failed to load volunteers:', error);
    this.$message.error('加载志愿者信息失败，请稍后重试。');
  }
}
  },
  created() {
    this.loadVolunteers(); // 加载志愿者信息
  }
};
</script>


<style scoped>
@import "@/assets/css/Base.css";
@import 'element-ui/lib/theme-chalk/index.css';
@import "@/assets/css/PacerAndEmergencyTable.css";

#VolunManagement {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: flex-end;
  background-color: white;
  margin-top: 80px;
  margin-right: 3%;
  width: 67%;
  font-size: 15px;
}
</style>
