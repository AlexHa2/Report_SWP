import "bootstrap/dist/css/bootstrap.min.css";
import { signInWithPopup } from "firebase/auth";
import { useFormik } from "formik";
import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import * as Yup from "yup";
import { auth, provider } from "../Config/configAuthenFirebase";
import "./LoginForm.css";
import {
  useLoginGoogle,
  userLogin,
} from "../../../Service/UserService/UserService";
export default function LoginForm() {
  const [typeInputForm, setTypeInputForm] = useState("password");
  const listIcon = ["fa-solid fa-eye-slash", "fa-solid fa-eye"];
  const [iconShow, setIconShow] = useState(["fa-solid fa-eye-slash"]);
  const navigator = useNavigate();
  const handlerOnclickIcon = () => {
    if (typeInputForm === "password") {
      setTypeInputForm("text");
      setIconShow(listIcon[1]);
    } else {
      setTypeInputForm("password");
      setIconShow(listIcon[0]);
    }
  };

  const formik = useFormik({
    initialValues: {
      UserName: "",
      Password: "",
      RememberMe: false,
    },

    validationSchema: Yup.object({
      UserName: Yup.string()
        .required("nhập email để đăng nhập!")
        .matches(/^\S+$/, "Không có khoảng trắng"),
    }),

    onSubmit: async (values) => {
      const formData = new FormData();
      formData.append("UserName", values.UserName);
      formData.append("Password", values.Password);
      formData.append("RememberMe", values.RememberMe);
      const res = await userLogin(formData);

      if (res.data.token) {
        localStorage.setItem("userToken", JSON.stringify(res.data.token));
        // console.log("đây là login",res.data.token
        navigator("/");
      } else {
        alert("login false!");
      }
    },
  });

  const [userValueGoogle, setuserValueGoogle] = useState({});
  const navigate = useNavigate(); //sử dựng để điều hướng trang
  const handleLoginGoogle = async () => {
    signInWithPopup(auth, provider)
      .then(async (result) => {
        const userValue = {
          email: result.user.email,
          firstName: result._tokenResponse.firstName,
          lastName: result._tokenResponse.lastName,
        };
        console.log(result);
        setuserValueGoogle(userValue);
      })
      .catch((error) => {
        console.log(error);
      });
  };
  // useEffect(() => {
  //   if (userValueGoogle.length !== 0) {
  //     alert("me");
  //     const LoginGoogle = async () => {
  //       const res = await useLoginGoogle(userValueGoogle);
  //       if (res) {
  //         localStorage.setItem("userToken", res.data.token);
  //         navigate("/");
  //       } else {
  //         alert("login failed!!!");
  //       }
  //     };
  //     LoginGoogle();
  //   }
  // }, [userValueGoogle]);

  return (
    <section className="L-seccion">
      <div className="container mx-auto">
        <div className="wraper-login-page">
          <div className="Header-login">
            <h1>Đăng Nhập</h1>
            <span>Bạn chưa có tài khoảng đăng nhập Đăng ký ?</span>
            <Link to={"/register"}>tại đây</Link>
          </div>
          <div className="row">
            <div className="wraper-form col-12 col-md-6 col-lg-5 offset-md-3 mx-auto">
              <div className="L-form">
                <form onSubmit={formik.handleSubmit}>
                  <div>
                    <p>
                      Tên đăng nhập <span>*</span>
                    </p>
                    <div className="L-input-place">
                      <input
                        type="text"
                        name="UserName"
                        placeholder="Input your email"
                        value={formik.values.UserName}
                        onChange={formik.handleChange}
                      ></input>
                    </div>
                    {formik.errors.UserName && (
                      <p className="errorMsg">{formik.errors.UserName}</p>
                    )}
                  </div>
                  <p>
                    Mật Khẩu <span>*</span>
                  </p>
                  <div className="L-input-place">
                    <input
                      type={typeInputForm}
                      name="Password"
                      placeholder="nhập mật khẩu của bạn"
                      value={formik.values.Password}
                      onChange={formik.handleChange}
                    ></input>
                    <i className={iconShow} onClick={handlerOnclickIcon}></i>
                  </div>
                  <div className="button-rememberme">
                    <input
                      type="checkbox"
                      name="RememberMe"
                      id="RememberMe"
                      value={formik.values.RememberMe}
                      onChange={formik.handleChange}
                    />
                    <label htmlFor="RememberMe">Remember me</label>
                  </div>
                  <div className="link-Fogot-password">
                    <p>
                      quên mật khẩu ?
                      <Link to={"/sendEmailForgot"}> Nhấn Vào Đây</Link>
                    </p>
                  </div>
                  <div className="b-form-login">
                    <button type="submit">
                      <p>Đăng Nhập</p>
                    </button>
                  </div>
                </form>
                <hr className="hr"></hr>
                <div className="b-form-login-google">
                  <button onClick={handleLoginGoogle}>
                    <p>Đăng nhập Google</p>
                  </button>
                  <img
                    src="https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Google_%22G%22_logo.svg/2048px-Google_%22G%22_logo.svg.png"
                    alt="Google image"
                    width={"30px"}
                  ></img>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}