import React from "react";
//OrderHistory

import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import { Rating } from "@mui/material";
import Box from "@mui/material/Box";
import Collapse from "@mui/material/Collapse";
import IconButton from "@mui/material/IconButton";
import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Typography from "@mui/material/Typography";
import { useFormik } from "formik";
import { useEffect, useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import { toast } from "react-toastify";
import * as Yup from "yup";
import { CreateReview } from "../../../Service/ReviewService/ReviewService";
import { useOrderManager, useUserProfile } from "../../../Store";
import "../AccountPage/AccountPage.css";
export default function OrderHistory({ listOrder, page }) {
  return (
    <TableContainer component={Paper}>
      <Table aria-label="collapsible table">
        <TableHead>
          <TableRow>
            <TableCell />
            <TableCell>Mã đơn HÀNG</TableCell>
            <TableCell>Ngày Đặt</TableCell>
            <TableCell>TT vận chuyển</TableCell>
            <TableCell>Thành Tiền</TableCell>
            <TableCell>Trạng thái đơn hàng</TableCell>
            <TableCell></TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {listOrder.items &&
            listOrder.items.length > 0 &&
            listOrder.items.map((order, index) => (
              <Row row={order} key={index} page={page} />
            ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}

function Row({ row, page }) {
  const { getOrderPagin, setListOrder } = useOrderManager();
  const [open, setOpen] = React.useState(false);
  const [status, setStatus] = useState();
  const { userProfile } = useUserProfile();
  const [currentProduct, setCurrentProduct] = useState();
  useEffect(() => {
    if (row.orderStatus === 0) {
      setStatus("chưa thanh toán");
    } else if (row.orderStatus === 1) {
      setStatus("chờ sử lý");
    } else if (row.orderStatus === 2) {
      setStatus("đang giao hàng");
    } else if (row.orderStatus === 3) {
      setStatus("đơn hàng thành công");
    } else if(row.orderStatus === -1){
      setStatus("đơn hàng bị hủy");
    }
  }, [row.orderStatus]);


  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = (product) => {
    setShow(true);
    setCurrentProduct(product)
    console.log("product review",product)
  }
  
  const formik = useFormik({
    initialValues: {
      productId: "",
      dateReview: "",
      grade: 0,
      comment: "",
    },

    validationSchema: Yup.object({
      grade: Yup.number()
        .min(1, "Grade must be greater than 0")
        .required("Required"),
      comment: Yup.string()
        .min(4, "Comment must be at least 4 characters")
        .matches(/^[^\d].*$/, "Comment must not start with a digit")
        .required("Required"),
    }),

    onSubmit: async (values) => {
      console.log("values cuoi reiew",values)
      try {
        setShow(false)
        const res = await CreateReview(userProfile.userToken,values)
        console.log("respone create",res)
        if(res.data.message === "Review added successfully"){
          toast.success("Đánh giá thành công",{
            autoClose:1000,
          })
          formik.resetForm()
        } else {
          toast.error("Mạng đang yếu thử lại nhé",{
            autoClose:1000,
          })
          formik.resetForm()
        }
      } catch (error) {
        console.log("loi create review",error)
      }
    },
  });

  useEffect(() => {
    if(currentProduct){
      formik.setValues({
      productId: currentProduct.productId,
      dateReview: new Date().toISOString(),
      })
    }
  },[currentProduct])
  return (
    <React.Fragment>
      
      <TableRow sx={{ "& > *": { borderBottom: "unset" } }}>
        <TableCell>
          <IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell>{row.orderId}</TableCell>
        <TableCell>{row.orderDate}</TableCell>
        <TableCell>{row.shippingAddress}</TableCell>
        <TableCell>{row.totalAmount.toLocaleString()}</TableCell>
        <TableCell>{status}</TableCell>
        {/* <TableCell>
          <button
            onClick={() => handleComplete(row.orderId)}
            className="tracking-button-order-user-complete"
          >
            Đã Nhận Hàng
          </button>
          {status === "chưa thanh toán" ? (
            <button
              onClick={() => handCancelOrder(row.orderId)}
              className="tracking-button-order-user-cancel"
            >
              Hủy Đơn Hàng
            </button>
          ) : null}
        </TableCell> */}
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography variant="h6" gutterBottom component="div">
                Thông Tin Sản Phẩm
              </Typography>
              <Table size="small" aria-label="purchases">
                <TableHead>
                  <TableRow>
                    <TableCell>Tên sản phẩm</TableCell>
                    <TableCell>Số lượng</TableCell>
                    <TableCell>Tổng Tiền đ</TableCell>
                    <TableCell>Đánh Giá</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {row.orderDetails.length > 0 &&
                    row.orderDetails.map((products, index) => (
                      <TableRow key={index}>
                        <TableCell>{products.product.productName}</TableCell>
                        <TableCell>{products.quantity}</TableCell>
                        <TableCell>{products.price.toLocaleString()}</TableCell>
                        <TableCell><button className="button-review-order-history" onClick={() => handleShow(products)}>Đánh giá</button></TableCell>
                      </TableRow>
                    ))}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Ý Kiến Đánh Giá</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={formik.handleSubmit}>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                Đánh giá{" "}
                <Rating
                  style={{marginLeft: "5px"}}
                  name="grade"
                  value={formik.values.grade}
                  onChange={(event, newValue) => {
                    formik.setFieldValue("grade", newValue);
                  }}
                />
              </Form.Label>
            </Form.Group>
            {formik.errors.grade && (
              <p style={{ margin: "0", color: "red" }}>{formik.errors.grade}</p>
            )}
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Ý kiến đánh giá</Form.Label>
              <Form.Control
                type="text"
                placeholder="ý kiến"
                autoFocus
                name="comment"
                value={formik.values.comment}
                onChange={formik.handleChange}
              />
            </Form.Group>
            {formik.errors.comment && (
              <p style={{ margin: "0", color: "red" }}>
                {formik.errors.comment}
              </p>
            )}
            <Modal.Footer>
              <Button variant="secondary" onClick={handleClose}>
                Close
              </Button>
              <Button variant="primary" type="submit">
                Save Changes
              </Button>
            </Modal.Footer>
          </Form>
        </Modal.Body>
      </Modal>
    </React.Fragment>
  );
}
