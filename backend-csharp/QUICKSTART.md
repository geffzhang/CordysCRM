# Quick Start Guide - CordysCRM C# Backend

## 🚀 5 分钟快速开始 (5-Minute Quick Start)

### 前提条件 (Prerequisites)

```bash
# 检查 .NET SDK (Check .NET SDK)
dotnet --version
# 需要 9.0 或更高版本 (Requires 9.0 or later)
```

### 步骤 1: 克隆并构建 (Clone and Build)

```bash
# 克隆仓库
git clone https://github.com/geffzhang/CordysCRM.git
cd CordysCRM/backend-csharp

# 恢复依赖
dotnet restore

# 构建项目
dotnet build

# 预期输出: Build succeeded. 0 Warning(s). 0 Error(s).
```

### 步骤 2: 配置 (Configuration)

**最小配置 - 无需数据库即可运行 API:**

`CordysCRM.App/appsettings.json` 已配置默认值，可以直接运行。

**可选: 配置数据库和 Redis:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=cordys_crm;Uid=root;Pwd=your_password;",
    "Redis": "localhost:6379"
  }
}
```

### 步骤 3: 运行应用 (Run Application)

```bash
cd CordysCRM.App
dotnet run

# 输出 (Output):
# Now listening on: http://localhost:5074
# Application started. Press Ctrl+C to shut down.
```

### 步骤 4: 测试 API (Test API)

**方法 1: 使用 curl**
```bash
# 测试版本接口
curl http://localhost:5074/system/version

# 预期输出:
# {"version":"1.3.1","buildTime":"2025-11-05 14:24:55","name":"Cordys CRM","description":"新一代的开源 AI CRM 系统"}
```

**方法 2: 使用浏览器**
1. 打开浏览器访问: http://localhost:5074/swagger
2. 浏览 API 文档
3. 测试各个 API 端点

**方法 3: 使用 VS Code REST Client**
创建 `test.http` 文件:
```http
### Get System Version
GET http://localhost:5074/system/version

### Get Dashboard Tree
GET http://localhost:5074/dashboard/module/tree
```

## 📁 项目结构概览 (Project Structure Overview)

```
backend-csharp/
├── 📄 CordysCRM.sln              # 解决方案文件
├── 📁 CordysCRM.Framework/        # 共享框架库
│   ├── Common/                    # 常量、工具类
│   ├── Security/                  # 安全相关
│   └── (其他框架组件)
├── 📁 CordysCRM.CRM/             # 业务逻辑层
│   └── (待实现)
└── 📁 CordysCRM.App/             # Web API 应用
    ├── 📄 Program.cs             # 应用入口
    ├── 📄 appsettings.json       # 配置文件
    └── 📁 Controllers/           # API 控制器
        ├── DashboardModuleController.cs
        └── SystemVersionController.cs
```

## 🔧 开发命令 (Development Commands)

### 构建和运行 (Build & Run)
```bash
# 清理构建输出
dotnet clean

# 构建项目
dotnet build

# 运行项目 (开发模式)
dotnet run --project CordysCRM.App

# 运行项目 (生产模式)
dotnet run --project CordysCRM.App --configuration Release

# 监听文件变化自动重启 (Hot Reload)
dotnet watch run --project CordysCRM.App
```

### 测试 (Testing)
```bash
# 运行所有测试 (Currently no tests)
dotnet test

# 运行特定测试项目
dotnet test CordysCRM.Tests

# 带代码覆盖率
dotnet test /p:CollectCoverage=true
```

### NuGet 包管理 (Package Management)
```bash
# 添加包
dotnet add CordysCRM.App package PackageName

# 更新包
dotnet add CordysCRM.App package PackageName --version x.x.x

# 移除包
dotnet remove CordysCRM.App package PackageName

# 列出已安装的包
dotnet list CordysCRM.App package
```

### 数据库迁移 (Database Migrations)
```bash
# 安装 EF Core 工具
dotnet tool install --global dotnet-ef

# 创建迁移
dotnet ef migrations add InitialCreate --project CordysCRM.App

# 应用迁移
dotnet ef database update --project CordysCRM.App

# 回滚迁移
dotnet ef database update PreviousMigration --project CordysCRM.App

# 删除最后一个迁移
dotnet ef migrations remove --project CordysCRM.App
```

## 🐳 Docker 快速启动 (Docker Quick Start)

```bash
# 构建镜像
docker build -t cordyscrm-backend:latest .

# 运行容器
docker run -d \
  --name cordyscrm-backend \
  -p 8081:8081 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  cordyscrm-backend:latest

# 查看日志
docker logs -f cordyscrm-backend

# 停止容器
docker stop cordyscrm-backend

# 删除容器
docker rm cordyscrm-backend
```

## 🔍 调试 (Debugging)

### Visual Studio Code
1. 安装 C# 扩展
2. 按 F5 开始调试
3. 在代码中设置断点
4. 使用调试控制台

### Visual Studio 2022
1. 打开 `CordysCRM.sln`
2. 设置 `CordysCRM.App` 为启动项目
3. 按 F5 开始调试

### JetBrains Rider
1. 打开 `CordysCRM.sln`
2. 右键点击 `CordysCRM.App` → Debug
3. 设置断点并开始调试

## 📊 API 端点列表 (API Endpoints)

### 系统相关 (System)
- `GET /system/version` - 获取系统版本信息

### 仪表板模块 (Dashboard Module)
- `POST /dashboard/module/add` - 添加仪表板文件夹
- `POST /dashboard/module/rename` - 重命名文件夹
- `POST /dashboard/module/delete` - 删除文件夹
- `GET /dashboard/module/tree` - 获取文件树
- `GET /dashboard/module/count` - 获取文件数量
- `POST /dashboard/module/move` - 移动文件夹

### Swagger 文档
- `GET /swagger` - Swagger UI
- `GET /swagger/v1/swagger.json` - OpenAPI 规范

## 🛠️ 常见问题 (Troubleshooting)

### 问题 1: 端口被占用
```bash
# 修改端口
# 编辑 appsettings.json 或 launchSettings.json
# 或使用命令行参数
dotnet run --urls "http://localhost:5000"
```

### 问题 2: 缺少 .NET SDK
```bash
# 下载并安装 .NET 9.0 SDK
# https://dotnet.microsoft.com/download
```

### 问题 3: NuGet 包恢复失败
```bash
# 清理 NuGet 缓存
dotnet nuget locals all --clear

# 重新恢复包
dotnet restore
```

### 问题 4: 数据库连接失败
```bash
# 检查数据库是否运行
mysql -h localhost -u root -p

# 检查连接字符串是否正确
# 编辑 appsettings.json 中的 ConnectionStrings
```

## 🎯 下一步 (Next Steps)

1. **阅读文档**
   - 📖 [README.md](README.md) - 项目概览
   - 📖 [MIGRATION_GUIDE.md](MIGRATION_GUIDE.md) - 迁移指南

2. **开始开发**
   - 创建新的 Controller
   - 添加业务逻辑 Service
   - 实现数据访问 Repository

3. **参与贡献**
   - Fork 项目
   - 创建分支
   - 提交 Pull Request

## 📞 获取帮助 (Get Help)

- 📧 Email: support@fit2cloud.com
- 💬 GitHub Issues: [提交问题](https://github.com/geffzhang/CordysCRM/issues)
- 📚 文档: [在线文档](https://cordys.cn/docs/)

## 🎓 学习资源 (Learning Resources)

### ASP.NET Core
- [官方文档](https://docs.microsoft.com/aspnet/core)
- [ASP.NET Core 教程](https://docs.microsoft.com/aspnet/core/tutorials)

### C# 语言
- [C# 指南](https://docs.microsoft.com/dotnet/csharp)
- [C# 编程指南](https://docs.microsoft.com/dotnet/csharp/programming-guide)

### Entity Framework Core
- [EF Core 文档](https://docs.microsoft.com/ef/core)
- [EF Core 教程](https://docs.microsoft.com/ef/core/get-started)

---

**Happy Coding! 🚀**
