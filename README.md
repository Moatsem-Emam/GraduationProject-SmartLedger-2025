# SmartLedger

SmartLedger is a modern desktop application for managing personal or small business financial records. Built using WinUI 3 and clean architecture principles, it offers a robust and modular design across multiple layers: Domain, Application, Infrastructure, and Presentation.

---

## 🗂 SmartLedger Structure

```
/src
├── SmartLedger.Domain         # Core business logic and domain entities
├── SmartLedger.Application    # Interfaces, DTOs, and service contracts
├── SmartLedger.Infrastructure # EF Core DbContext, Repositories, and Service Implementations
├── SmartLedgerPL              # WinUI 3 Presentation Layer (Views, ViewModels)
└── SmartLedger.Migrator       # Tool for database migrations


---

## ⚙️ Prerequisites

- **OS**: Windows 10/11 (x64)
- **.NET SDK**: .NET 8.0+
- **IDE**: Visual Studio 2022+ (with `.NET Desktop Development` workload)
- **Database**: SQL Server (can be configured)

---

## 🔧 Build from Source

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Moatsem-Emam/GraduationProject-SmartLedger-2025.git
   cd SmartLedger
   ```

2. **Open solution in Visual Studio**:  
   Open `SmartLedger.sln`.

3. **Apply Migrations "first run only"** (Most Important Part):
   3.1 First you want to install SqlServer Express & SSMS, important to watch this 3 min video : https://youtu.be/FFp5BLoQLAA?si=k_47y0cCnJIdha0X
   3.2
    After installing, open ssms then copy this user/servername appears like this.
   ![Screenshot 2025-05-24 010129](https://github.com/user-attachments/assets/27064d24-6726-4780-9555-dba3de5f7b4b)
    Replace the Server="" with the user/servername you have copied before.
   ![Screenshot 2025-05-24 010223](https://github.com/user-attachments/assets/ca0a51ea-cdaf-4838-b61c-34fd25e75aa3)
    You can also change the databse name instead of SL01.
   ![image](https://github.com/user-attachments/assets/9732000f-2210-494d-a2ae-f264483d58bd)
   3.3
    Go to solution then set the SmartLedger.Migrator as Startup Project.
   ![Screenshot 2025-05-24 010319](https://github.com/user-attachments/assets/2eaae327-e2b5-4bef-ad86-513e710eb8fc)
    Set SmartLedger.Infrastructure as default project.
   ![Screenshot 2025-05-24 010401](https://github.com/user-attachments/assets/4f2cd7e5-d754-4f4b-9012-18c39dc409c4)

   ![image](https://github.com/user-attachments/assets/6ba0ed32-cc5c-40b0-84df-e09b7059a89e)

   

5. **Set startup project**:  
   Right-click on `SmartLedgerPL` and set it as the startup project.

6. **Run the app**:  
   Press `F5` to build and run the application.

---

## 🔄 Configuration

- Connection strings are in `SmartLedger.Migrator/appsettings.json`.
- Update them to point to a valid SQLite or SQL Server DB as needed.

---

## 🛠 Troubleshooting

- **Missing EF Tools**:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

- **Build Errors**:
  - Make sure all projects are restored.
  - Check for missing SDKs or mismatched .NET versions.

- **Database Issues**:
  - Delete `bin` and `obj` folders then rebuild.
  - Ensure `AppDbContext` is properly configured.

---

## 📦 Pre-built Executables (Optional)

> If you provide ready-to-run `.exe` files, place them under `/exe` and update this section.

1. Download latest `.zip` from [Releases](https://github.com/your-username/SmartLedger/releases)
2. Extract and run `SmartLedgerPL.exe`.

---

## 📬 Contact

For questions, please reach out via GitHub Issues or email at your.email@example.com
