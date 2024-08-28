//获取志愿者详情
import request from '@/utils/request';

export function getVolunteerInformation(data) {
  return request({
    url: '/Volunteer/acquire_volunteer_information',
    method: 'post',
    data: data
  });
}
