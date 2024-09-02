import request from '@/utils/request'

/*查询显示该比赛的所有补给点*/
export function get_supply_point(Event_id) {
    return request({
        url: '/Supply/get_all_supplypoint',
        method: 'get',
        params: { 
           Event_id, 
        }
    })
}

/*添加补给点*/
export function add_supply_point(Event_Id) {
    return request({
        url: '/Supply/add_supplypoint',
        method: 'patch',
        data: {
            Event_id: eventId,
            SupplyPointID: supplyPointID,
            SupplyPointLocation: supplyPointLocation,
            SupplyPointType: supplyPointType
        }
    });
}

/*删除补给点*/
export function delete_supply_point(supplyPointID) {
    return request({
        url: '/Supply/delete_supplypoint',
        method: 'delete',
        params: {
            SupplyPointID: supplyPointID
        }
    });
}
