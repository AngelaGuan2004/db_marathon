import request from '@/utils/request';

export function fetchEventDetails(eventId) {
  return request({
    url: `/events/${eventId}`,
    method: 'get'
  });
}
