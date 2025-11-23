import React from 'react';
import '../../assets/styles/ContactsPage.css';

const ContactsPage: React.FC = () => {
  return (
    <div className="contacts-page">
      <div className="contacts-header">
        <h1>Контакты</h1>
        <p>Свяжитесь с нами удобным для вас способом</p>
      </div>

      <div className="contacts-content">
        <div className="contact-info">
          <div className="contact-item">
            <div className="contact-icon">📞</div>
            <div className="contact-details">
              <h3>Телефон</h3>
              <p>+7 (800) 123-45-67</p>
              <p>+7 (495) 123-45-67</p>
              <span className="contact-note">Бесплатный звонок по России</span>
            </div>
          </div>

          <div className="contact-item">
            <div className="contact-icon">✉️</div>
            <div className="contact-details">
              <h3>Email</h3>
              <p>info@example.com</p>
              <p>support@example.com</p>
              <span className="contact-note">Ответим в течение 24 часов</span>
            </div>
          </div>

          <div className="contact-item">
            <div className="contact-icon">📍</div>
            <div className="contact-details">
              <h3>Адрес</h3>
              <p>г. Москва, ул. Примерная, д. 123</p>
              <p>БЦ "Примерный", 5 этаж</p>
              <span className="contact-note">Пн-Пт: 9:00-18:00</span>
            </div>
          </div>

          <div className="contact-item">
            <div className="contact-icon">💬</div>
            <div className="contact-details">
              <h3>Мессенджеры</h3>
              <p>WhatsApp: +7 (900) 123-45-67</p>
              <p>Telegram: @example_support</p>
              <span className="contact-note">Онлайн-консультация</span>
            </div>
          </div>
        </div>

        <div className="contact-form">
          <h3>Напишите нам</h3>
          <form>
            <div className="form-group">
              <input type="text" placeholder="Ваше имя" required />
            </div>
            <div className="form-group">
              <input type="email" placeholder="Ваш email" required />
            </div>
            <div className="form-group">
              <input type="tel" placeholder="Ваш телефон" />
            </div>
            <div className="form-group">
              <textarea placeholder="Ваше сообщение" rows={5} required></textarea>
            </div>
            <button type="submit" className="submit-btn">Отправить сообщение</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ContactsPage;