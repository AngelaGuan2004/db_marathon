import request from '@/utils/request'

/*查询显示该比赛的所有接驳车信息*/
export function get_shuttleInfo(Event_id) {
    return request({
        url: '/Shuttlecar/inquire_shuttlecar_info',
        method: 'get',
        params: { 
           Event_id, 
        }
    })
}

/*添加接驳车*/
export function add_shuttleInfo(Event_Id) {
    return request({
        url: '/Shuttlecar/release_shuttlecar_info',
        method: 'patch',
        data: {
            Event_id: Event_id,
            ShuttlecarID: shuttlecar_id,
            departTime: departtime,
            arriveTime: arrivetime
        }
    });
}

/*删除接驳车*/
export function delete_shuttleInfo(supplyPointID) {
    return request({
        url: '/Shuttlecar/delete_shuttlecar_info',
        method: 'delete',
        params: {
            Event_id: Event_id,
            shuttlecarID: shuttlecar_id
        }
    });
}
