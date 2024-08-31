import request from '@/utils/request'

  // 获取所有照片信息
  export function getAllPhotos() {
    return request({
      url: '/PhotoView/get_all_photos',
      method: 'get'
    });
  }

  // 获取所有匹配的照片拍摄地址列表
  export function inquiryLocation(location_key) {
    return request({
      url: '/PhotoView/inquiry_location',
      method: 'get',
      params: { location_key }
    })
  }
  
  // 根据拍摄地址获取匹配的照片列表
  export function inquiryPhotoByLocation(location) {
    return request({
      url: '/PhotoView/inquiry_photo_by_location',
      method: 'get',
      params: { location }
    })
  }
  
  // 根据赛事名称获取匹配的照片列表
  export function inquiryPhotoByEventName(event_name) {
    return request({
      url: '/PhotoView/inquiry_photo_by_eventname',
      method: 'get',
      params: { event_name }
    })
  }
  
  // 点赞照片
  export function likePhoto(id) {
    return request({
      url: '/PhotoView/like_photo',
      method: 'post',
      data: { id }
    })
  }
  
  // 根据摄影师ID获取匹配的照片列表
  export function inquiryPhotoByPhotographer(id) {
    return request({
      url: '/PhotoView/inquiry_photo_by_photographer',
      method: 'get',
      params: { id }
    })
  }
  

  // 上传照片
  export function uploadPhoto(id, time, location, photographer_id, file) {
    // 创建FormData对象
    const formData = new FormData();
    formData.append('id', id);
    formData.append('time', time);
    formData.append('location', location);
    formData.append('photographer_id', photographer_id);
    formData.append('file', file); // 将文件添加到表单数据中

    return request({
      url: '/Photo/upload_photo',
      method: 'post',
      data: formData,
      headers: {
        'Content-Type': 'multipart/form-data' // 指定内容类型为表单数据
      }
    });
  }

  // 删除照片
  export function deletePhoto(id) {
    return request({
      url: '/Photo/delete_photo',
      method: 'delete',
      data: { id }
    });
  }
