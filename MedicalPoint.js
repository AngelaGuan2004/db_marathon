import request from '@/utils/request'

/*查询显示该比赛的所有医疗点*/
export function get_medical_point(Event_id) {
    return request({
        url: '/Medical/get_all_medical_point',
        method: 'get',
        params: { 
           Event_id, 
        }
    })
}

/*增加医疗点*/
export function add_medical_point(Event_id){
    return request({
        url:'/Medical/add_medicalpoint',
        method:'patch',
        data:{
            data: {
                Event_id: Event_id,
                medicalPointID: medicalPointID,
                medicalPointLocation: medicalPointLocation
            }
        }
    }
    )
}

/*删除医疗点*/
export function delete_medical_point(medicalPointID) {
    return request({
        url: '/Medical/delete_medicalpoint',
        method: 'delete',
        params: {
            medicalPointID: medicalPointID
        }
    });
}

