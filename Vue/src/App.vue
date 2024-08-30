<template>
  <div id="app">
    <Header></Header>
    <Footer></Footer>
    <!-- <button @click.prevent="GetApi"></button>
    <button @click.prevent="PostApi"></button> -->
  </div>
</template>

<script>
import Header from './components/Header.vue';
import Footer from './components/Footer.vue';
import service from './utils/request.js'
export default {
  name: 'App',
  components: {
    Header, Footer
  },
  methods: {
    GetApi() {
      service.get('PhotoView/inquiry_photo_by_location?location=梦创街').then(
        response => {
          console.log('请求成功了', response)
        },
        error => {
          console.log('请求失败了', error.message)
        }
      )
    },
    PostApi() {
      service.post('Auth/login_player', {
        Id: 1,
        Gender: '女',
        Age: 41,
        Id_Number: '610080198308020900',
        Region: '陕西省',
        Telephone_Number: '10178212087',
        Name: '安诗雨',
        Password: '12345678'
      }).then(
        response => {
          console.log('请求成功了', response)
        },
        error => {
          console.log('请求失败了', error.message)
        }
      )
    },
  }
}
</script>
