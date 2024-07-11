import request from '@/utils/request'

/*函数功能 */
export function functionName(params) {
  return request({
    url: '/[Controller]/[Action]',
    method: 'get',
    params: {
      key: value
    }
  })
}
