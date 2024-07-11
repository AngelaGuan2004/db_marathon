import axios from 'axios'

const service = axios.create({
    baseURL: 'https://localhost:5001',
    timeout: 50000,
    async: true,
    crossDomain: true
}
)

//暴露出去
export default service
