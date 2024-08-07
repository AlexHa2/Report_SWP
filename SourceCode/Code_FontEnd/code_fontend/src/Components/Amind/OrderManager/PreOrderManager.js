
//PreOrderManager
import React, { useEffect, useState } from "react";
import Box from "@mui/material/Box";
import Collapse from "@mui/material/Collapse";
import IconButton from "@mui/material/IconButton";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import "../../User/AccountPage/AccountPage.css";
import {
  GetOrderPigingTrackingMember,
  GetOrderPigingWithStatus,
  updateStatusOrder,
} from "../../../Service/OrderService/OrderService";
import { useOrderManager, useUserProfile } from "../../../Store";
import { toast, ToastContainer } from "react-toastify";
export default function PreOrderManager({ listOrder, page }) {
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
  const {listCurrentOrder,setlistCurrentOrder} = useOrderManager()
  useEffect(() => {
    if (row.orderStatus === 0) {
      setStatus("chưa thanh toán");
    } else if (row.orderStatus === 1) {
      setStatus("chờ sử lý");
    } else if (row.orderStatus === 2) {
      setStatus("đang giao hàng");
    } else if (row.orderStatus === 3) {
      setStatus("đơn hàng thành công");
    }
  }, [row.orderStatus]);

  const handleComplete = async (orderID) => {
    console.log("order", orderID);
    const newStatus = {
      newStatus: 3,
    };
    try {
      const res = await updateStatusOrder(orderID, 2);
      if (res.status === 200) {
        const resData = await GetOrderPigingWithStatus(1, page, 6);
        if (resData) {
            setlistCurrentOrder(resData.data);
        } else {
            setlistCurrentOrder([]);
        }
        toast.success("the order is comfirm", {
          autoClose: 1000,
        });
      } else {
        toast.error("weak network!!!", {
          autoClose: 1000,
        });
      }
    } catch (error) {
      console.log("lỗi comfirm đơn hàng", error);
    }
  };

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
        <TableCell>
          
          {status === "chờ sử lý" &&
          (
            <button
            onClick={() => handleComplete(row.orderId)}
            className="tracking-button-order-user-complete"
          >
            Confirm Order
          </button>
          )}
        
        </TableCell>
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
                  </TableRow>
                </TableHead>
                <TableBody>
                  {row.orderDetails.length > 0 &&
                    row.orderDetails.map((products, index) => (
                      <TableRow key={index}>
                        <TableCell>{products.product.productName}</TableCell>
                        <TableCell>{products.quantity}</TableCell>
                        <TableCell>{products.price.toLocaleString()}</TableCell>
                      </TableRow>
                    ))}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}
