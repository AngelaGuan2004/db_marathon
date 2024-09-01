import request from '@/utils/request'

/*获取一场比赛所有的成绩*/
export function getAllResults(Player_ID) {
    return request({
        url: '/Result/',
        method: 'get',
        params: { Event_id,Type,order }
    })
}

