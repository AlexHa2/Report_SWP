import { Route, Routes } from "react-router-dom";
import RouteAdminLayout from "./Layouts/AdminLayout/RouteAdminLayout";
import DefaultLayout from "./Layouts/DefualtLayout/DefaultLayout";
import { privateRoutes, publicRoutes } from "./Routes/Route";

function App() {

  return (
    <>
      {/* <Header />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/Header" element={<Header />} />
        <Route path="/Footer" element={<Footer />} />
        <Route path="/login" element={<LoginForm />} />
        <Route path="/account" element={<AccountPage />} />
        <Route path="/addresses" element={<AddressPage />} />
        <Route path="/cart" element={<CartPage />} />
        <Route path="/checkout" element={<CheckoutPage />} /> 
        <Route path="/payment" element={<PaymentPage />} />
        <Route path="/Content" element={<Content />} />
        <Route path="/register" element={<RegisterForm />} />
        <Route path="/authenOTP" element={<AuthenEmail />} />
        <Route path="/sendEmailForgot" element={<SendEmailForm />} />
        <Route path="/ForgotPassword" element={<ForgotPasswordForm />} />
        <Route path="/reset-password/:token/:email" element={<ResetPasswordPage />} />
=======
        <Route path="/CategoriesType" element={<ImportCategories/>} />
        {/* Add the route for ResetPasswordPage */}
      {/* <Route path="/FAQ" element={<FAQ />} /> */}
      {/* </Routes> */}
      {/* <Footer /> */}

      <Routes>
        {publicRoutes.map((route, index) => {
          const Layout = DefaultLayout;
          const Page = route.component;
          return (
            <Route
              key={index}
              path={route.path}
              element={
                <Layout>
                  <Page />
                </Layout>
              }
            />
          );
        })}

        {privateRoutes.map((route, index) => {
          const Layout = RouteAdminLayout;
          const Page = route.component;
          return (
            <Route
              key={index}
              path={route.path}
              element={
                <Layout>
                  <Page />
                </Layout>
              }
            />
          );
        })}
      </Routes>
    </>
  );
}

export default App;
