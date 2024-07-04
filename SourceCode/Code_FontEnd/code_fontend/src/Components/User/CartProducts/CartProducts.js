import React, { useEffect, useState } from "react";
import "./CartProducts.css";
import { Button, Card, Col, Container, Row } from "react-bootstrap";
import { productGetAll } from "../../../Service/ProductService/ProductService";

import { Actions, useProduct } from "../../../Store";
import { useStore } from "../../../Store";
import { imageGetAll } from "../../../Service/ProductService/imageService";
import HomePage from "../../../Pages/HomePage/HomePage";
import { ToastContainer, toast } from "react-toastify";
export default function CartProducts({listProduct}) {

  const [state, dispatch] = useStore();
  // const  {listProduct,getAllProductToContext } = useProduct();

  // useEffect(  () => {
  //   const getProduct = async () => {
  //     await getAllProductToContext(1,12)
  //   }
  //   getProduct(); 
  // },[])


  const addToCart = (product) => {
    dispatch(Actions.addListToCart(product))
    toast.success("sản phẩm đã được thêm",{
      autoClose:1000,
    })
  }
  return (
    <Container style={{marginTop:"0"}}>
      <ToastContainer/>
      {/* <HomePage/> */}
      <Row>
        {listProduct && listProduct.map((product, index) => {
          return (
            <Col  key={index} className="row-product-cart">
              <Card  className="cart-product-page">
                {/* <Card.Img variant="top" src={listProductImage ? listProductImage[product.productId][0].imagePath : "productimage"} /> */}
                <Card.Img variant="top" src={`https://localhost:44358/user-content/${product.images[0] ? product.images[0].imagePath : "productImage"}`} className="image-cart-product-user"/>
                <Card.Body>
                  <Card.Text className="cart-product-text">{product.productName}</Card.Text>
                  <Card.Text className="cart-product-text-money">{product.price.toLocaleString()} đ</Card.Text>
                  <button
                    variant="primary"
                    onClick={() => addToCart(product)}
                    className="button-cartProduct"
                  >
                   Thêm vào giỏ hàng
                  </button>
                </Card.Body>
              </Card>
            </Col>
          );
        })}
      </Row>
    </Container>
  );
}
