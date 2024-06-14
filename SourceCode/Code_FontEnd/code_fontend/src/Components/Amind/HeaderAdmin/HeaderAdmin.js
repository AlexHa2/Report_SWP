import React from "react";
import "./HeaderAdmin.css";
import { Col, Row } from "react-bootstrap";
export default function HeaderAdmin() {
  return (
    <>
      <div>
        <Row className="navbar">
          <Col xl={2} className="logo-header-admin">
            <img
              src="https://theme.hstatic.net/1000186075/1000909086/14/logo.png?v=4468"
              alt="logo"
              width={248}
              height={53}
            />
          </Col>
          <Col  xl={10} className="sub-header-admin">
            <div className="user-profile">
              <div>ha</div>
            </div>
          </Col>
        </Row>
      </div>
    </>
  );
}
