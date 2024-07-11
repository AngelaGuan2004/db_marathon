### 1.properties

```
launchsettings.json
设置了多个启动配置文件，可以适应开发和调试的不同需求
```



### 2.context

```
DBContext.cs
连接数据库，配置和获取数据库实例
```



### 3.controllers

```
设置控制器类，利用SqlSugar ORM框架连接和操作数据库，我们能够通过简单的条件判断来实现数据库数据的更改。
```

### 4.models

```
使用sqlsuger进行orm映射。

get访问器：用于获取属性的值。当外部代码读取该属性时，会调用get访问器。
set访问器：用于设置属性的值。当外部代码为该属性赋值时，会调用set访问器。

通过这种方式，SqlSugar可以根据类定义自动生成相应的SQL语句，简化了与数据库的交互过程，使得代码更加简洁和可维护。
```

### 外部

```
appsetting.json  
这个配置文件提供了基础的日志记录设置和主机名限制，可以根据需要进行扩展和修改。
program.cs 
应用程序入口代码、创建默认主机构件器、配置Web主机
startup.cs 
启动应用程序，配置应用程序服务、配置http请求管道
weatherforecast 
提供天气预报类
```

