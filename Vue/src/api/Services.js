import request from '@/utils/request'

// 获取某场赛事的所有医疗点
export function getAllMedicalPoints(Event_Id) {
    return request({
        url: '/Medical/get_all_medicalpoint',
        method: 'get',
        params: { Event_Id }
    })
}

// 添加伤员数据
export function addInjury(data) {
    return request({
        url: '/Medical/add_injury',
        method: 'post',
        data: { 
          name: data.name,
          ID: data.ID,
          medicalPoint: data.medicalPoint
        }
    })
}

