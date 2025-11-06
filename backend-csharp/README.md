# CordysCRM - C# / ASP.NET Core Backend

## 概述 (Overview)

这是 CordysCRM 后端从 Java/Spring Boot 到 C#/ASP.NET Core 的迁移实现。

This is the migration of the CordysCRM backend from Java/Spring Boot to C#/ASP.NET Core.

## 项目结构 (Project Structure)

```
backend-csharp/
├── CordysCRM.sln                    # Solution file
├── CordysCRM.Framework/             # Framework layer (shared components)
│   ├── Common/                      # Common constants and utilities
│   ├── Context/                     # Context management
│   ├── Security/                    # Security components
│   ├── AspectJ/                     # AOP components (converted from AspectJ)
│   └── File/                        # File management
├── CordysCRM.CRM/                   # CRM business logic layer
│   └── (To be implemented)
└── CordysCRM.App/                   # Main ASP.NET Core Web API application
    ├── Controllers/                 # REST API Controllers
    ├── Program.cs                   # Application entry point
    └── appsettings.json            # Configuration settings
```

## 技术栈 (Technology Stack)

### 从 Java 到 C# 的映射 (Java to C# Mapping)

| Java/Spring Boot Component | C#/ASP.NET Core Equivalent |
|---------------------------|----------------------------|
| Spring Boot 3.5.7 | ASP.NET Core 9.0 |
| Maven | NuGet / .NET CLI |
| Spring MVC | ASP.NET Core MVC |
| Spring Data JPA | Entity Framework Core |
| MyBatis | Dapper / EF Core |
| Apache Shiro | ASP.NET Core Identity / Custom Auth |
| Spring Session | ASP.NET Core Session |
| SpringDoc OpenAPI | Swashbuckle |
| Lombok | C# Records / Properties |
| HikariCP | Built-in Connection Pooling |
| Flyway | EF Core Migrations / FluentMigrator |
| Redisson | StackExchange.Redis |
| Quartz | Quartz.NET / Hangfire |

### 主要依赖 (Main Dependencies)

- **ASP.NET Core 9.0** - Web framework
- **Pomelo.EntityFrameworkCore.MySql** - MySQL database provider
- **StackExchange.Redis** - Redis cache and session
- **Swashbuckle.AspNetCore** - API documentation (Swagger)
- **Microsoft.AspNetCore.ResponseCompression** - Response compression

## 开始使用 (Getting Started)

### 先决条件 (Prerequisites)

- .NET 9.0 SDK or later
- MySQL 8.0 or later
- Redis 6.0 or later

### 安装 (Installation)

1. 克隆仓库 (Clone the repository):
```bash
git clone https://github.com/geffzhang/CordysCRM.git
cd CordysCRM/backend-csharp
```

2. 恢复依赖 (Restore dependencies):
```bash
dotnet restore
```

3. 配置数据库连接 (Configure database connection):
编辑 `CordysCRM.App/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=cordys_crm;Uid=root;Pwd=your_password;",
    "Redis": "localhost:6379"
  }
}
```

4. 构建项目 (Build the project):
```bash
dotnet build
```

5. 运行应用 (Run the application):
```bash
cd CordysCRM.App
dotnet run
```

6. 访问 API 文档 (Access API documentation):
打开浏览器访问: http://localhost:8081/swagger

## 配置 (Configuration)

主要配置文件 `appsettings.json` 对应于原 Java 版本的 `commons.properties`:

- **ApplicationSettings**: 应用程序基本设置
- **ConnectionStrings**: 数据库和 Redis 连接字符串
- **Compression**: 响应压缩设置
- **FileUpload**: 文件上传限制
- **Session**: 会话超时设置
- **Swagger**: API 文档设置

## 迁移状态 (Migration Status)

### 已完成 (Completed)
- [x] 项目结构创建
- [x] ASP.NET Core Web API 基础配置
- [x] 数据库连接配置 (MySQL)
- [x] Redis 会话管理配置
- [x] Swagger API 文档配置
- [x] 响应压缩配置
- [x] CORS 配置
- [x] 示例控制器 (DashboardModuleController)
- [x] 常量类转换 (PermissionConstants)

### 待实现 (To Do)
- [ ] Entity Framework Core DbContext 实现
- [ ] 数据库迁移 (Flyway → EF Core Migrations)
- [ ] 认证和授权系统 (Shiro → ASP.NET Core Identity)
- [ ] AOP 实现 (AspectJ → Middleware/Filters)
- [ ] 文件存储系统
- [ ] 业务逻辑层转换
- [ ] Service 层实现
- [ ] Repository 层实现
- [ ] DTO 和 Domain 模型转换
- [ ] 单元测试
- [ ] 集成测试

## 开发指南 (Development Guidelines)

### 命名约定 (Naming Conventions)

- **C# 类名**: PascalCase (例如: `DashboardModuleController`)
- **方法名**: PascalCase (例如: `AddFileModule`)
- **私有字段**: _camelCase (例如: `_logger`)
- **常量**: PascalCase (例如: `DashboardRead`)

### 代码组织 (Code Organization)

1. **Controllers**: REST API 端点
2. **Services**: 业务逻辑
3. **Repositories**: 数据访问
4. **Models/DTOs**: 数据传输对象
5. **Entities**: 数据库实体

## 性能对比 (Performance Comparison)

C# / ASP.NET Core 相比 Java / Spring Boot 的优势:
- 更快的启动时间
- 更低的内存占用
- 原生异步支持 (async/await)
- 更好的跨平台支持

## 许可证 (License)

本项目遵循 [FIT2CLOUD Open Source License](../LICENSE)

## 贡献 (Contributing)

欢迎贡献! 请参考 [CONTRIBUTING.md](../CONTRIBUTING.md)

## 联系方式 (Contact)

如有问题，请在 GitHub Issues 中提出。
