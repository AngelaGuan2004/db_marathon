document.getElementById('loginButton').addEventListener('click', function () {
  var username = document.getElementById('username').value;
  var idnumber = document.getElementById('idnumber').value;
  var password = document.getElementById('password').value;

  var data = {
    username: username,
    idnumber: idnumber,
    password: password
  };

  var xhr = new XMLHttpRequest();
  xhr.open('POST', 'https://localhost:7105/Users/AddUser', true);
  xhr.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');

  xhr.onreadystatechange = function () {
    if (xhr.readyState === XMLHttpRequest.DONE) {
      if (xhr.status === 200) {
        console.log('登录成功');
      } else {
        console.error('登录失败');
      }
    }
  };

  xhr.send(JSON.stringify(data));
});
