<template>
  <div id="app">
    <Header></Header>
    <Footer></Footer>
  </div>
</template>

<script>
import Header from './components/Header.vue';
import Footer from './components/Footer.vue';
export default {
  name: 'App',
  components: {
    Header, Footer
  },
  methods: {
    GetApi() {
      this.$axios.get('http://localhost:8080/api').then(
        response => {
          console.log('请求成功了', response.data)
        },
        error => {
          console.log('请求失败了', error.message)
        }
      )
    },
  }
}
</script>
