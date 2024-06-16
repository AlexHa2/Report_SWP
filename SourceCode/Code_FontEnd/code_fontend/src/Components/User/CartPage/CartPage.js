import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './CartPage.css';

const CartPage = () => {
  const [cartItems, setCartItems] = useState([
    {
      id: 1,
      name: 'Combo 2 Ion Thực phẩm dinh dưỡng Meiji số 9 800g 1 - 3 tuổi',
      price: 900000,
      quantity: 1,
      image: 'https://tse4.mm.bing.net/th?id=OIP.E1DyiEUNLJcx-UvjKKdNkwHaHa&pid=Api&P=0&h=180'
    },
    {
      id: 2,
      name: 'Sữa bột Abbott Grow Gold số 4 900g cho trẻ 2-6 tuổi',
      price: 500000,
      quantity: 1,
      image: 'https://bizweb.dktcdn.net/100/172/234/products/sua-bot-abbott-grow-gold-3-huong-vani-400g-1502939576-4289062-1559d640cd84f525e2cec0a2981a2b4a.jpg?v=1514607202967'
    }
  ]);

  useEffect(() => {
    // Save cart items to localStorage
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
  }, [cartItems]);

  const handleQuantityChange = (itemId, delta) => {
    setCartItems(cartItems.map(item =>
      item.id === itemId ? { ...item, quantity: Math.max(1, item.quantity + delta) } : item
    ));
  };

  const handleRemoveItem = (itemId) => {
    setCartItems(cartItems.filter(item => item.id !== itemId));
  };

  const total = cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0);

  return (
    <div className="cart-page container">
      {/* <div className="breadcrumb">
        <Link to="/">Trang chủ</Link> / Giỏ hàng 
      </div> */}
      <div className="cart-container">
        <div className="cart-content">
          <h2>Giỏ hàng</h2>
          {cartItems.map(item => (
            <div className="cart-item" key={item.id}>
              <img src={item.image} alt={item.name} />
              <div className="cart-item-details">
                <h3>{item.name}</h3>
                <p className="cart-item-price">{item.price.toLocaleString()}₫</p>
                <div className="quantity-controls">
                  <button onClick={() => handleQuantityChange(item.id, -1)}>-</button>
                  <input type="text" value={item.quantity} readOnly />
                  <button onClick={() => handleQuantityChange(item.id, 1)}>+</button>
                </div>
              </div>
              <button className="remove-button" onClick={() => handleRemoveItem(item.id)}>X</button>
            </div>
          ))}
        </div>
        <div className="payment-info">
          <div className="invoice-option">
            <input type="checkbox" id="invoice" />
            <label htmlFor="invoice">Xuất hóa đơn công ty</label>
          </div>
          <div className="total-price">
            <span>TỔNG CỘNG: </span>
            <span>{total.toLocaleString()}₫</span>
            <span>(Đã bao gồm VAT nếu có)</span>
          </div>
          <div className="discount-code">
            <span>Mã giảm giá</span>
            <Link to="/discount">Chọn mã giảm giá</Link>
          </div>
          <Link to="/checkout" className="checkout-button">Thanh Toán</Link>
          <div className="payment-methods">
            <h4>Hình thức thanh toán</h4>
            <img src="https://tse3.mm.bing.net/th?id=OIP.FsfqOWtwUfPKv44mVE60eQHaB4&pid=Api&P=0&h=180" alt="Payment Methods" />
          </div>
        </div>
      </div>
    </div>
  );
};

export default CartPage;