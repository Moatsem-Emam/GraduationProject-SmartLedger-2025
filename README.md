# SmartLedger

SmartLedger is a modern desktop application for managing personal or small business financial records. Built using WinUI 3 and clean architecture principles, it offers a robust and modular design across multiple layers: Domain, Application, Infrastructure, and Presentation.

---

## 🗂 Repository Structure

```
/src
├── SmartLedger.Domain         # Core business logic and domain entities
├── SmartLedger.Application    # Interfaces, DTOs, and service contracts
├── SmartLedger.Infrastructure # EF Core DbContext, Repositories, and Service Implementations
├── SmartLedgerPL              # WinUI 3 Presentation Layer (Views, ViewModels)
└── SmartLedger.Migrator       # Tool for database migrations

/exe                          # (Optional) Pre-built binaries

README.md                     # Setup instructions and project overview
```

---

## ⚙️ Prerequisites

- **OS**: Windows 10/11 (x64)
- **.NET SDK**: .NET 8.0+
- **IDE**: Visual Studio 2022+ (with `.NET Desktop Development` workload)
- **Database**: SQLite (default) or SQL Server (can be configured)

---

## 🔧 Build from Source

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/SmartLedger.git
   cd SmartLedger
   ```

2. **Open solution in Visual Studio**:  
   Open `SmartLedger.sln`.

3. **Set startup project**:  
   Right-click on `SmartLedgerPL` and set it as the startup project.

4. **Apply Migrations** (Optional):
   If using SQLite or custom DB:
   ```bash
   cd SmartLedger.Migrator
   dotnet run
   ```

5. **Run the app**:  
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
