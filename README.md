# 📦 StockFlow

**StockFlow** is a warehouse management information system designed to manage electronic product inventory between the main warehouse and branch stores. The system provides two user roles with different responsibilities:

- **Admin Gudang Utama** — Manages product data and inventory for branch stores.
- **Admin Toko Cabang** — Handles product transactions and generates transaction reports.

## ⚙️ Installation

1. Import the `Gudang.bacpac` file using **SQL Server Management Studio (SSMS)**.
2. Configure the `connection.cs` file with your SQL Server connection string.

   ```csharp
   public SqlConnection GetConnection()
   {
       SqlConnection con = new SqlConnection("Your ConnectionString");
       return con;
   }
   ```

3. Open the project in Visual Studio.
4. Run the application.

## 🔐 Login Credentials

Use the following accounts to access the application.

### 1. Admin Gudang Utama

| Username | Password |
|---|---|
| `admin` | `admin123` |

### 2. Admin Toko Cabang

| Branch | Username | Password |
|---|---|---|
| Toko A | `TokoA` | `tokoa123` |
| Toko B | `TokoB` | `tokob123` |

---

**Built with C# and SQL Server.**

