🧠 HealthShoper.BLL — Бизнес-логика приложения

Слой **Business Logic Layer (BLL)** отвечает за реализацию всей бизнес-логики приложения, обработку данных, взаимодействие с репозиторием (DAL) и возврат DTO/ViewModel на верхний уровень (API).

---

## 📁 Общая структура

``` text
HealthShoper.BLL/
├── Constants/
│   └── ClaimsConst.cs
│
├── Exceptions/
│   ├── HttpException.cs
│   └── Models/
│
├── Extensions/
│   ├── ApiResultExtensions.cs
│   ├── ConstantsJson.cs
│   ├── QueryStringExtensions.cs
│   ├── StringExtensions.cs
│   └── JsonConverters/
│
├── Interfaces/
│   ├── IAuthService.cs
│   ├── IClientService.cs
│   └── IItemService.cs
│
├── Mappers/
│   ├── ClientMapper.cs
│   └── ItemMapper.cs
│
├── Models/
│   ├── Dtos/
│   │   ├── ClientDto.cs
│   │   ├── ItemDto.cs
│   │   └── TokenDto.cs
│   │
│   └── ViewModels/
│       ├── ClientViewModel.cs
│       ├── LogInViewModel.cs
│       ├── QueryFilter.cs
│       ├── SortBy.cs
│       └── SortDirection.cs
│
├── Services/
│   ├── AuthService.cs
│   ├── ClientService.cs
│   └── ItemService.cs
│
├── ServiceCollectionExtensions.cs

```

---

## 🧩 Описание компонентов

### **1. Constants**
**Назначение:**  
Содержит константы, используемые во всем BLL-слое.

- **ClaimsConst.cs** — хранит ключи claim-данных (например, `UserId`, `CompanyId`, `Role` и т.п.), чтобы избежать "магических строк" в коде.

---


### **2. Exceptions**
**Назначение:**  
Реализует модели и структуры для централизованной обработки ошибок.

- **Models/ErrorResponseModel.cs** — структура стандартного ответа об ошибке, возвращаемого API.
- **Models/MessageKeys.cs** — содержит набор констант для унификации текстов ошибок и сообщений (например, `UserNotFound`, `InvalidPassword`, `AccessDenied`).

---


### **3. Interfaces**
**Назначение:**  
Содержит контракты сервисов для дальнейшей реализации в `Services`.

- **IAuthService.cs** — интерфейс аутентификации (регистрация, вход, валидация токена).
- **IClientService.cs** — интерфейс для работы с клиентами (CRUD-операции, получение профиля и т.д.).
- **IItemService.cs** — интерфейс для работы с товарами (список, добавление, редактирование, удаление).

---


### **4. Mappers**
**Назначение:**  
Реализует логику маппинга между доменными моделями, DTO и ViewModel.

- **ClientMapper.cs** — выполняет преобразование между моделями `Client`, `ClientDto`, `ClientViewModel`.

---


### **5. Models**
**Назначение:**  
Содержит все модели, используемые в BLL.

#### 🔹 Dtos/
Модели, используемые для обмена данными между слоями.

- **ClientDto.cs** — переносит основные данные клиента между слоями.
- **TokenDto.cs** — содержит данные JWT-токена (access и refresh токены, время жизни и т.п.).

#### 🔹 ViewModels/
Модели, которые возвращаются наружу (в API/UI).

- **ClientViewModel.cs** — модель представления клиента для фронтенда.

---


### **6. Services**
**Назначение:**  
Основная бизнес-логика приложения. Здесь реализуются интерфейсы из `Interfaces`.

- **AuthService.cs** — реализует авторизацию, регистрацию и управление токенами.
- **ClientService.cs** — реализует логику работы с клиентами (обновление профиля, получение данных).
- **ItemService.cs** — реализует бизнес-логику для работы с товарами.
- **ServiceCollectionExtensions.cs** — содержит метод расширения для регистрации всех сервисов в `Dependency Injection`.

---