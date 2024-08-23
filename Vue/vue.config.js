const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true
})

module.exports = {
  pages: {
    index: {
      //入口
      entry: 'src/main.js',
      template: 'public/index.html',
      filename: 'index.html',
    },
    register: {
      entry: 'src/pages/Register/register.js',
      template: 'src/pages/Register/register.html',
      filename: 'register.html',
      title: "注册"
    },
    login: {
      entry: 'src/pages/Login/login.js',
      template: 'src/pages/Login/login.html',
      filename: 'login.html',
      title: "登录"
    }
  },
  lintOnSave: false, //关闭语法检查
  devServer: {
    proxy: {
      '/api': {
        target: 'http://113.44.75.241:5158',
        pathRewrite: { '^/api': '' },
        ws: true, //用于支持websocket
        changeOrigin: true //用于控制请求头中的host值
      }
    }
  }
}