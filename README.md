# 📦 StockFlow

**StockFlow** is a warehouse management information system designed to manage electronic product inventory between the main warehouse and branch stores.

The system provides two user roles with different responsibilities:

- **Admin Gudang Utama** — Manages product data and inventory for branch stores.
- **Admin Toko Cabang** — Handles product transactions and generates transaction reports.

## ⚙️ Installation

1. Import the `Gudang.bacpac` file using **SQL Server Management Studio (SSMS)**.
2. Open the `App.config` file in the project.
3. Find the `<connectionStrings>` section and change the `data source` value to your SQL Server instance.

   ```xml
   data source=YourConnectionString
   ```

   Replace `YourConnectionString` with your SQL Server instance name. For example:

   ```xml
   data source=.\SQLEXPRESS
   ```

   Make sure the `initial catalog=Gudang` value matches your database name. Apply the changes to all three connection strings: `GudangEntities`, `GudangEntities1`, and `GudangEntities2`.

4. Open the project in Visual Studio.
5. Run the application.

## 🔐 Login Credentials

Use the following accounts to access the application.

### Admin Gudang Utama

| Username | Password |
|---|---|
| `admin` | `admin123` |

### Admin Toko Cabang

| Branch | Username | Password |
|---|---|---|
| Toko A | `TokoA` | `tokoa123` |
| Toko B | `TokoB` | `tokob123` |

---

**Built with C# and SQL Server.**
