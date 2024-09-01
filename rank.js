import request from '@/utils/request'

/*男女混合*/
export function getRank(Event_ID) {
    return request({
        url: '/Result/get_top_players',
        method: 'get',
        params: { Event_id,topN,gender }
    })
}