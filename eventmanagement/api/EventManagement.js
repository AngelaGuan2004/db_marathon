import request from '@/utils/request';

export function getParticipantsByEventId(eventId) {
  return request({
    url: '/EventManage/get_players_by_event',  // 确保后端 API 地址正确
    method: 'get',
    params: { id: eventId }  // 传递赛事 id
  });
}
