import request from '@/utils/request';

// 志愿者报名
export function registerVolunteer(data) {
  return request({
    url: '/Volunteer/add_volunteer',
    method: 'post',
    data: data
  });
}
