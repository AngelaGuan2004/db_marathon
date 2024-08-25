import request from '@/utils/request';

// 查询接驳车信息
export function inquireShuttlecarInfo(event_id) {
  return request({
    url: '/Shuttlecar/inquire_shuttlecar_info',
    method: 'get',
    params: { event_id: event_id }
  });
}


// 查询补给点信息
export function getAllSupplyPoints(event_id) {
  return request({
    url: '/Supply/get_all_supplypoint',
    method: 'get',
    params: { Event_Id: event_id }
  });
}


// 查询医疗点信息
export function getAllMedicalPoints(event_id) {
  return request({
    url: '/Medical/get_all_medicalpoint',
    method: 'get',
    params: { Event_Id: event_id }
  });
}

