# 📢 NotificationServiceApi

A lightweight and extensible ASP.NET Core API to send email and SMS notifications using a generic and scalable service architecture. It uses a factory pattern for notification types and stores logs in MongoDB for observability.

---

## 🚀 Features

- ✅ Generic `NotificationService<T>` for different notification types.
- 🏭 Factory pattern for clean notification sender logic.
- 🧩 Minimal APIs with dependency injection.
- 🐞 MongoDB logging for all notification actions.
- 🛠️ Environment-based configuration using `OptionsSettings`.
- 🧪 Ready for future extensibility (e.g., Push, WhatsApp).

---

## 📁 Project Structure

NotificationServiceApi/
│
├── Program.cs # API setup and endpoint registration
├── Infrastructuur/
│ ├── Bootstrapper/ # Service registration
│ ├── Dtos/ # Data transfer objects
│ ├── Enums/ # Notification and logging enums
│ ├── Factories/ # Notification factory logic
│ ├── Models/ # Core entity and log models
│ ├── Repositories/ # MongoDB abstraction
│ └── Services/ # Notification service implementation


---

## 🛠️ Tech Stack

- ASP.NET Core 8 (Minimal API)
- MongoDB for logging
- C# 12
- Swagger / OpenAPI for testing

---

## ⚙️ Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/GaelLouage/NotificationServiceApi.git
cd NotificationServiceApi

2. Set Up Configuration

Create an appsettings.json with the following format:

{
  "ConnectionStrings": {
    "MongoDatabase": "mongodb://localhost:27017"
  },
  "OptionsSettings": {
    "SenderEmail": "your@email.com",
    "SenderPhone": "+123456789"
  }
}

3. Run the API

dotnet run

Navigate to https://localhost:<port>/swagger to explore and test the endpoints.
📬 API Endpoints
🔹 Send Email Notification

POST /notification-email

Body:

{
  "Type": 0,
  "Message": {
    "To": "user@example.com",
    "Subject": "Hello!",
    "Body": "This is a test email."
  }
}

🔸 Send SMS Notification

POST /notification-sms

Body:

{
  "Type": 1,
  "Message": {
    "PhoneNumber": "+1234567890",
    "Message": "Test SMS message"
  }
}

📒 Logging

Each notification request (success or failure) is logged to MongoDB in the Notification.NotificationCollection with:

    Timestamp

    Notification message

    Log type (INFO/ERROR)
