import request from '@/utils/request';

// 志愿者报名
export function registerVolunteer(data) {
  return request({
    url: '/Auth/add_volunteer',
    method: 'post',
    data: data
  });
}
//获取志愿者详情
import request from '@/utils/request';

export function getVolunteerInformation(data) {
  return request({
    url: '/Auth/acquire_volunteer_information',
    method: 'post',
    data: data
  });
}
