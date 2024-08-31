import request from '@/utils/request'
export function getInfor(Id) {
    return request({
        url: '/Auth/get_by_playerid',
        method: 'get',
        params: {
            Id: Id
        }
    })
}

export function getPhotographerInfor(Id) {
  return request({
      url: '/Auth/get_by_photographerid',
      method: 'get',
      params: {
          Id: Id
      }
  })
}
export function completeInfor(params) {
    return request({
      url: '/Auth/update_player',
      method: 'patch',
      data:{
        Id:params.Id,
        Name:params.Name,
        Gender:params.Gender,
        Age:params.Age,
        Id_Number:params.Id_Number,
        Region:params.Region,
        Telephone_Number:params.Telephone_Number,
        Password:params.Password,
      }
    })
}
export function getMyVolunteering(Id) {
  return request({
      url: '/Volunteer/acquire_volunteer_event',
      method: 'get',
      params: {
        volunteer_id: Id
      }
  })
}
export function getMyRegistrations(Id) {
  return request({
      url: '/Player/get_events_by_playerid',
      method: 'get',
      params: {
        Id: Id
      }
  })
}
/*
export function getEventInfo(Player_Id,Event_Id) {
  return request({
      url: '/Event/get_by_id',
      method: 'get',
      params: {
        Player_Id:Player_Id,
        Event_Id:Event_Id
      }
  })
}
  */




