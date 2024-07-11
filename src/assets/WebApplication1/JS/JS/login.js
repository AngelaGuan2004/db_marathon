function validateForm(event) {
  event.preventDefault();

  const name = document.querySelector('.user_name input').value;
  const id = document.querySelector('.id input').value;
  const password = document.querySelector('.password input').value;
  let errorMessage = true;

  if (!checkName(name) || !checkPassword(password) || !checkID(id)) {
    errorMessage = false;
  }

  if (errorMessage) {
    document.querySelector('.login_in_form').submit();
  }
}

function checkName(name) {
  const nameJudge = document.querySelector('#name_judge');
  if (name === "") {
    nameJudge.innerHTML = "用户名不能为空！";
    nameJudge.style.color = "#ff0011";
    return false;
  }

  if (name.length < 3 || name.length > 14) {
    nameJudge.innerHTML = "格式错误，长度6-14个字符";
    nameJudge.style.color = "#ff0011";
    return false;
  }

  const charname1 = name.toLowerCase();
  for (let i = 0; i < name.length; i++) {
    const charname = charname1.charAt(i);
    if (!((charname >= 0 && charname <= 9) || (charname >= 'a' && charname <= 'z') || (charname === '_'))) {
      nameJudge.innerHTML = "用户名包含非法字符";
      nameJudge.style.color = "#ff0011";
      return false;
    }
  }
  nameJudge.innerHTML = "用户名正确";
  nameJudge.style.color = "rgb(137,217,97)";
  return true;
}

function checkPassword(password) {
  const passwordJudge = document.getElementById("password_judge");
  if (password == "") {
    passwordJudge.innerHTML = "密码不能为空";
    passwordJudge.style.color = "#ff0011";
    return false;
  }
  if (password.length < 4 || password.length > 16) {
    passwordJudge.innerHTML = "格式错误，密码长度为4-16位";
    passwordJudge.style.color = "#ff0011";
    return false;
  }
  passwordJudge.innerHTML = "密码格式正确";
  passwordJudge.style.color = "rgb(137,217,97)";
  return true;
}

// 检验身份证号格式
function checkeNO(NO) {
  var str = NO;
  var Expression = /^\d{17}[\d|X]$|^\d{15}$/;
  var objExp = new RegExp(Expression);
  if (objExp.test(str) == true) {
    return true;
  } else {
    return false;
  }
}

function checkID(id) {
  const idJudge = document.getElementById("id_judge");
  if (id == "") {
    idJudge.innerHTML = "请输入身份证号码!";
    idJudge.style.color = "#ff0011";
    return false;
  }
  if (!checkeNO(id)) {
    idJudge.innerHTML = "身份证号码格式错误!";
    idJudge.style.color = "#ff0011";
    return false;
  }
  if (checkeNO(id)) {
    idJudge.innerHTML = "身份证号码格式正确!";
    idJudge.style.color = "rgb(137,217,97)";
  }
  return true;
}

window.onload = function () {
  document.querySelector('.user_name input').addEventListener('blur', function () {
    checkName(this.value);
  });

  document.querySelector('.id input').addEventListener('blur', function () {
    checkID(this.value);
  });

  document.querySelector('.password input').addEventListener('blur', function () {
    checkPassword(this.value);
  });

  document.getElementById('loginButton').addEventListener('click', validateForm);
}
