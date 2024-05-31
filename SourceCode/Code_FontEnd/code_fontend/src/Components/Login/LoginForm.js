import "bootstrap/dist/css/bootstrap.min.css";
import { signInWithPopup } from "firebase/auth";
import { useFormik } from "formik";
import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import * as Yup from "yup";
import { auth, provider } from "../Config/configAuthenFirebase";
import "./LoginForm.css";
import ForgotPasswordForm from "../ForgotPassword/ForgotPasswordForm";
import SendEmailForm from "../ForgotPassword/SendEmailForm";

export default function LoginForm() {
  const [typeInputForm, setTypeInputForm] = useState("password");
  const listIcon = ["fa-solid fa-eye-slash", "fa-solid fa-eye"];
  const [iconShow, setIconShow] = useState(["fa-solid fa-eye-slash"]);

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
      UserEmail: "",
    },

    validationSchema: Yup.object({
      UserEmail: Yup.string()
        .required("nhập email để đăng nhập!")
        .matches(
          /[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
          "vd: blabla@gmail.com"
        ),
    }),

    onSubmit: (values) => {
      console.log(values);
    },
  });

  const HandleShowEmailForm = () => {};

  const navigate = useNavigate(); //sử dựng để điều hướng trang
  const handleLoginGoogle = async () => {
    signInWithPopup(auth, provider)
      .then((result) => {
        const userValue = {
          email: result.user.email,
          userName: result.user.displayName,
          image: result.user.photoURL,
          userID: result.user.uid,
        };
        localStorage.setItem("userName", JSON.stringify(userValue));
        navigate("/");
      })
      .catch((error) => {
        console.log(error);
      });
  };

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
                <form method="post" onChange={formik.handleSubmit}>
                  <div>
                    <p>
                      Email <span>*</span>
                    </p>
                    <div className="L-input-place">
                      <input
                        type="text"
                        name="UserEmail"
                        placeholder="Input your email"
                        value={formik.values.UserEmail}
                        onChange={formik.handleChange}
                      ></input>
                      {formik.errors.UserEmail && (
                        <p className="errorMsg">{formik.errors.UserEmail}</p>
                      )}
                    </div>
                  </div>
                  <p>
                    password <span>*</span>
                  </p>
                  <div className="L-input-place">
                    <input
                      type={typeInputForm}
                      name="password"
                      placeholder="Input your password"
                    ></input>
                    <i className={iconShow} onClick={handlerOnclickIcon}></i>
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
              <ForgotPasswordForm/>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}
