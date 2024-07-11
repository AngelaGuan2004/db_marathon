import request from '@/utils/request'

export function adduser(params) {
    /*用户注册*/
    return request({
        url: '/Users/AddUser',
        method: 'post',
        params: {
            user_name: params.user_name,
            id: params.id,
            password: params.password,

        }
    })
}