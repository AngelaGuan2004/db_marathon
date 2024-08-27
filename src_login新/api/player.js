import request from '@/utils/request';

// 注册选手
export function registerPlayer(data) {
  return request({
    url: '/Auth/add_player',
    method: 'post',
    data: data
  });
}

// 选手登录
export function loginPlayer(query) {
  return request({
    url: '/Auth/login_player',
    method: 'post',
    data: data
  });
}

// 志愿者登录
export function loginVolunteer(query) {
  return request({
    url: '/Auth/login_volunteer',
    method: 'post',
    data: data
  });
}

// 摄影师登录
export function loginPhotographer(query) {
  return request({
    url: '/Auth/login_photographer',
    method: 'get',
    data: data
  });
}

// 更新选手信息
export function updatePlayer(data) {
  return request({
    url: '/Auth/update_player',
    method: 'patch',
    data: data
  });
}

// 根据ID查询选手信息
export function getPlayerById(id) {
  return request({
    url: '/Auth/get_by_playerid',
    method: 'get',
    params: { Id: id }
  });
}
