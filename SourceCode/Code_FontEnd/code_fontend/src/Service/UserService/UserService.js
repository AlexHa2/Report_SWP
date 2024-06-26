import * as request from "../../utility/CustomAxios";
//Register user
export const userRegister = async (bodyInfor) => {
  try {
    const res = await request.Post("Users/register", bodyInfor, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return res;
  } catch (error) {
    console.log("Error register", error);
  }
};

export const authenEmailForgotPassword = async ( emailForgot ) => {
  try {
    console.log(emailForgot.get("email"));
    const res = await request.Post("Users/ForgotPassword", emailForgot, {
      headers: {
        "Content-Type": "multipart/form-data",
         
      },
    });
    return res;
  } catch (error) {
    console.log("lỗi send email", error);
  }
};

export const authenCodeOTP = async (otp) => {
  try {
    console.log(otp);
    const res = await request.Get(`Users/ConfirmEmail?otp=${otp}`);
    return res;
  } catch (error) {
    console.log("lỗi authenOTP", error);
  }
};

export const userLogin = async (UserLoginInfor) => {
  try {
    const res = await request.Post("Users/authenticate", UserLoginInfor, {
      headers: {
       'Content-Type': 'application/json',
      },
    });
    return res;
  } catch (error) {
    console.log("lỗi login", error);
  }
};

export const userFogotPassword = async ( PasswordReset ) => {
  try {
    const res = await request.Post("Users/ResetPassword",PasswordReset, {
      headers:{
        'Content-Type': 'multipart/form-data',
      }
    })
    return res;
  } catch (error) {
    console.log("lỗi quên mật khẩu " + error)
  }
}

export const userLoginGoogle = async ( useGoogleLogin ) => {
  try {
    const res = await request.Post("Users/google-login",useGoogleLogin, {
      headers:{
        'Content-Type': 'application/json',
      }
    })
    return res;
  } catch (error) {
    console.log("lỗi đăng nhập google " + error)
  }
}

export const getUserInfor = async (TokenUser) => {
  try {
    const res = await request.Post("Users/GetMemberByToken",TokenUser,{
      headers:{
        'Content-Type': 'application/json'
      }
    })
    return res
  } catch (error) {
    console.log("lỗi lấy thông tin người dùng",error)
  }
  
}

export const userAddAdress = async (TokenUser,userAdress) => {
  const token = TokenUser.slice(1,-1);
  try {
    const res = await request.Post(`Users/AddAddressByToken?jwtToken=${token}`,userAdress,{
      headers:{
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json',
      }
    })
    return res
  } catch (error) {
    console.log("lỗi nhập address  người dùng",error)
  }
  
}


export const getUserAddAdress = async (TokenUser) => {
  const token = TokenUser.slice(1,-1);
  try {
    const res = await request.Get(`Users/GetAddressByToken?jwtToken=${token}`,{
      headers:{
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json',
      }
    })
    return res
  } catch (error) {
    console.log("lỗi lấy adress  người dùng",error)
  }
  
}

export const updateUserAdress = async (TokenUser,userAdress) => {
  const token = TokenUser.slice(1,-1);
  try {
    const res = await request.Get(`Users/UpdateAddressByToken?jwtToken=${token}`,userAdress,{
      headers:{
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json',
      }
    })
    return res
  } catch (error) {
    console.log("lỗi update adress  người dùng",error)
  }
  
}