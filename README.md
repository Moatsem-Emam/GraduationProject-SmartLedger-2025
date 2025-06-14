# 📊 SmartLedger
[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![WinUI 3](https://img.shields.io/badge/WinUI-3.0-purple.svg)](https://docs.microsoft.com/en-us/windows/apps/winui/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()


A modern, feature-rich desktop application for managing personal and small business financial records. Built with WinUI 3 and following clean architecture principles, SmartLedger provides an intuitive interface for comprehensive financial management.

## ✨ Features

- **📈 Financial Tracking**: Comprehensive income and expense management
- **📊 Reporting & Analytics**: Visual charts and detailed financial reports
- **🏷️ Category Management**: Organize transactions with custom categories
- **🔍 Advanced Search**: Quick filtering and search capabilities
- **💾 Data Export**: Export data in multiple formats (CSV, PDF, Excel)
- **🔒 Secure Storage**: Local database with optional encryption
- **🎨 Modern UI**: Clean, responsive interface built with WinUI 3
- **📱 Touch-Friendly**: Optimized for both mouse and touch interactions

## 📸 Screenshots

![Screenshot 2025-05-23 232457](https://github.com/user-attachments/assets/8c1f6206-4ca1-4e01-8cea-6e29bb9ef261)
![Screenshot 2025-05-23 232007](https://github.com/user-attachments/assets/be361385-2a46-4ff5-82a2-bd0f29e078a7)
![Screenshot 2025-05-23 231753](https://github.com/user-attachments/assets/2048a293-c9d1-4c70-84f5-0e0a75199d41)
![Screenshot 2025-05-23 232115](https://github.com/user-attachments/assets/f8751463-818b-408e-bf11-4ee06e4cbc87)
![Screenshot 2025-05-23 232308](https://github.com/user-attachments/assets/3ce1e073-d1f9-4761-baaa-501148c08c7b)


## 🏗️ Architecture

SmartLedger follows **Clean Architecture** principles with clear separation of concerns:

```
📁 src/
├── 🎯 SmartLedger.Domain/         # Core business logic and domain entities
│   ├── Entities/                  # Domain models (Transaction, Category, etc.)
│   ├── ValueObjects/              # Domain value objects
│   └── Interfaces/                # Domain service contracts
├── 🔧 SmartLedger.Application/    # Application services and use cases
│   ├── Services/                  # Application services
│   ├── DTOs/                      # Data transfer objects
│   └── Interfaces/                # Application contracts
├── 🗄️ SmartLedger.Infrastructure/ # Data access and external services
│   ├── Data/                      # EF Core DbContext and configurations
│   ├── Repositories/              # Repository implementations
│   └── Services/                  # External service implementations
├── 🖥️ SmartLedgerPL/              # WinUI 3 Presentation Layer
│   ├── Views/                     # XAML pages and user controls
│   ├── ViewModels/                # MVVM view models
│   └── Converters/                # Value converters
└── 🔄 SmartLedger.Migrator/       # Database migration utility
```

## 📋 Prerequisites

Before you begin, ensure you meet the following requirements:

### System Requirements
- **Operating System**: Windows 10 version 1809 (build 17763) or later / Windows 11
- **Architecture**: x64 processor
- **Memory**: 4 GB RAM minimum, 8 GB recommended
- **Storage**: 500 MB available disk space

### Development Requirements
- **.NET SDK**: [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- **IDE**: [Visual Studio 2022](https://visualstudio.microsoft.com/) version 17.0+ with:
  - `.NET Desktop Development` workload
  - `Windows App SDK` components
- **Database**: [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or full instance)
- **Tools**: [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (recommended)

## 🚀 Quick Start

### 1. Clone the Repository
```bash
git clone https://github.com/Moatsem-Emam/GraduationProject-SmartLedger-2025.git
cd SmartLedger
```

### 2. Configuration Manager

   **Build => Configuration Manager**
![Screenshot 2025-06-14 095759](https://github.com/user-attachments/assets/82b30224-b123-4344-8841-524628bd1ba8)

### 2. Database Setup

1. **Install SQL Server Express**:
   - Download from [Microsoft SQL Server Downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
   - Watch this [3-minute setup guide](https://youtu.be/FFp5BLoQLAA?si=k_47y0cCnJIdha0X)

2. **Configure Connection String**:
   - Open SQL Server Management Studio (SSMS)
   - Copy your server name (e.g., `DESKTOP-ABC123\SQLEXPRESS`) Or take this without using SSMS (`.\SQLEXPRESS`)
   
3. **Update Configuration**:
   - Open `SmartLedger.Migrator/appsettings.json`
   - Replace the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=SmartLedgerDB;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

### 3. Apply Database Migrations

1. **Set Migration Project as Startup**:
   - Right-click `SmartLedger.Migrator` → "Set as Startup Project"
   
2. **Run Migrations**:
   - Open Package Manager Console: `Tools` → `NuGet Package Manager` → `Package Manager Console`
   - Set default project to `SmartLedger.Infrastructure`
   - Execute: `Update-Database`
   
### 4. Build and Run

1. **Set Presentation Layer as Startup**:
   - Right-click `SmartLedgerPL` → "Set as Startup Project"

2. **Run the Application**:
   - Press `F5` or click the "Start" button
   - The application should launch and display the main dashboard

## 🔧 Configuration

### Database Configuration
Edit the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SmartLedgerDB;Trusted_Connection=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  }
}
```

### Application Settings
Customize application behavior in `SmartLedgerPL/appsettings.json`:
```json
{
  "AppSettings": {
    "Theme": "Light", // Light, Dark, or System
    "AutoBackup": true,
    "BackupInterval": "Daily",
    "CurrencySymbol": "$",
    "DateFormat": "MM/dd/yyyy"
  }
}
```

## 📖 Usage

### Getting Started
1. **First Launch**: The application will create sample data for demonstration
2. **Dashboard**: View your financial overview with charts and summaries
3. **Transactions**: Add, edit, and categorize your income and expenses
4. **Reports**: Generate detailed reports filtered by date, category, or amount
5. **Settings**: Customize the application to your preferences

### Key Features

#### Adding Transactions
- Click the "+" button or use `Ctrl+N`
- Fill in transaction details (amount, description, category, date)
- Save to automatically update your financial overview

#### Generating Reports
- Navigate to the Reports section
- Select date range and filters
- Export reports as PDF, Excel, or CSV

#### Managing Categories
- Access Settings → Categories
- Create custom categories with colors and icons
- Organize transactions for better tracking

## 🛠️ Development

### Project Structure
The solution follows Domain-Driven Design (DDD) and Clean Architecture:

- **Domain Layer**: Contains business entities, value objects, and domain services
- **Application Layer**: Implements use cases and application services
- **Infrastructure Layer**: Handles data persistence and external integrations
- **Presentation Layer**: WinUI 3 application with MVVM pattern

### Contributing
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Make your changes and add tests
4. Commit your changes: `git commit -am 'Add some feature'`
5. Push to the branch: `git push origin feature/your-feature-name`
6. Submit a pull request

### Code Style
- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful names for variables and methods
- Add XML documentation for public APIs
- Write unit tests for business logic

## 🐛 Troubleshooting

### Common Issues

#### Build Errors
```bash
# Restore NuGet packages
dotnet restore

# Clean and rebuild
dotnet clean
dotnet build
```

#### Missing `appsettings.json` File

**Issue:**  
When running the application or database migrator, you may encounter an error indicating that `appsettings.json` is missing from a specific path.

**Solution:**


# 1. Check the error message to find the expected path
# Example error:
# Could not find file '/path/to/project/bin/Debug/net8.0/appsettings.json'

# 2. Navigate to the SmartLedger.Migrator directory
cd /path/to/SmartLedger.Migrator/

# 3. Copy the appsettings.json file to the required location
cp appsettings.json /path/to/project/bin/Debug/net8.0/appsettings.json

# 4. Run the application or migrator again
dotnet run --project SmartLedger.Migrator


#### Database Connection Issues
- Verify SQL Server is running
- Check connection string format
- Ensure database permissions are correct

#### Missing EF Core Tools
```bash
# Install globally
dotnet tool install --global dotnet-ef

# Update to latest version
dotnet tool update --global dotnet-ef
```

#### WinUI 3 Runtime Issues
- Install [Windows App Runtime](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/downloads)
- Update Windows to the latest version
- Check Windows Event Viewer for detailed error messages

### Getting Help
- Check the [Issues](https://github.com/Moatsem-Emam/GraduationProject-SmartLedger-2025/issues) page for known problems
- Search existing issues before creating a new one
- Provide detailed information including error messages and system specs

## 📊 Testing

### Running Tests
```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test src/SmartLedger.Tests/
```

### Test Structure
- **Unit Tests**: Test individual components in isolation
- **Integration Tests**: Test component interactions
- **UI Tests**: Automated UI testing with WinAppDriver

## 📦 Deployment

### Creating a Release Build
```bash
# Build in Release mode
dotnet build --configuration Release

# Publish self-contained application
dotnet publish --configuration Release --self-contained --runtime win-x64
```

### Installation Package
The project includes scripts to create an MSIX package for easy distribution:
```bash
# Generate MSIX package
./scripts/create-package.ps1
```

## 🔄 Version History

### v2.0.0 (Latest)
- ✨ Added advanced reporting features
- 🎨 Improved UI/UX with new themes
- 🔧 Enhanced performance and stability
- 📊 New dashboard widgets

### v1.5.0
- 📈 Added chart visualizations
- 💾 Export functionality
- 🏷️ Category management improvements

### v1.0.0
- 🎉 Initial release
- ✅ Core financial tracking features
- 🖥️ WinUI 3 interface

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Acknowledgments

- Built with [WinUI 3](https://docs.microsoft.com/en-us/windows/apps/winui/) and [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- Icons from [Fluent UI System Icons](https://github.com/microsoft/fluentui-system-icons)
- Charts powered by [WinUI 3 Community Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/winui/)

## 📞 Support & Contact

- **Email**: [motsememamhussain@gmail.com](mailto:your.email@example.com)

---

<p align="center">
  <strong>Made with ❤️ for better financial management</strong>
</p>

<p align="center">
  <a href="#top">Back to top</a>
</p>
