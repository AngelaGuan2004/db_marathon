import request from '@/utils/request'

/*获取一场比赛所有的成绩*/
export function get_all_results(Event_id) {
    return request({
        url: '/Result/get_all_results_by_eventid',
        method: 'get',
        params: { Event_id }
    })
}

