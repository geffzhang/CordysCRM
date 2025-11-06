# Java to C# Migration Guide for CordysCRM

## 概述 (Overview)

本文档详细说明了如何从 Java/Spring Boot 迁移到 C#/ASP.NET Core 的过程和最佳实践。

This document details the process and best practices for migrating from Java/Spring Boot to C#/ASP.NET Core.

## 架构对比 (Architecture Comparison)

### 项目结构映射 (Project Structure Mapping)

| Java (Spring Boot) | C# (ASP.NET Core) | 说明 |
|-------------------|-------------------|------|
| `backend/framework` | `CordysCRM.Framework` | 框架层 / Framework layer |
| `backend/crm` | `CordysCRM.CRM` | 业务逻辑层 / Business logic layer |
| `backend/app` | `CordysCRM.App` | 应用程序入口 / Application entry point |
| `pom.xml` | `.csproj` | 项目配置文件 / Project configuration |
| `Application.java` | `Program.cs` | 应用程序入口点 / Application entry point |

### 依赖管理 (Dependency Management)

| Java | C# | 用途 |
|------|-----|------|
| Maven (`mvn`) | NuGet (`dotnet`) | 包管理器 |
| `<dependency>` | `<PackageReference>` | 依赖声明 |
| `mvn install` | `dotnet restore` | 安装依赖 |
| `mvn clean package` | `dotnet build` | 构建项目 |

### 框架对比 (Framework Comparison)

#### 1. Web 框架 (Web Framework)

**Java (Spring MVC)**
```java
@RestController
@RequestMapping("/api/users")
public class UserController {
    @GetMapping("/{id}")
    public User getUser(@PathVariable Long id) {
        return userService.findById(id);
    }
}
```

**C# (ASP.NET Core MVC)**
```csharp
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase {
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(long id) {
        return userService.FindById(id);
    }
}
```

#### 2. 依赖注入 (Dependency Injection)

**Java (Spring)**
```java
@Service
public class UserService {
    @Resource  // or @Autowired
    private UserRepository userRepository;
}
```

**C# (ASP.NET Core)**
```csharp
public class UserService {
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }
}
```

注册服务 (Service Registration):
```csharp
// In Program.cs
builder.Services.AddScoped<IUserService, UserService>();
```

#### 3. 配置管理 (Configuration Management)

**Java (`application.properties`)**
```properties
spring.datasource.url=jdbc:mysql://localhost:3306/db
spring.datasource.username=root
server.port=8081
```

**C# (`appsettings.json`)**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=db;User=root;"
  },
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:8081"
      }
    }
  }
}
```

#### 4. 数据访问 (Data Access)

**Java (MyBatis)**
```java
@Mapper
public interface UserMapper {
    @Select("SELECT * FROM users WHERE id = #{id}")
    User findById(@Param("id") Long id);
}
```

**C# (Entity Framework Core)**
```csharp
public class ApplicationDbContext : DbContext {
    public DbSet<User> Users { get; set; }
}

// Repository
public class UserRepository {
    private readonly ApplicationDbContext _context;
    
    public User FindById(long id) {
        return _context.Users.Find(id);
    }
}
```

#### 5. 异步编程 (Asynchronous Programming)

**Java**
```java
@Async
public CompletableFuture<User> getUserAsync(Long id) {
    return CompletableFuture.completedFuture(
        userRepository.findById(id)
    );
}
```

**C# (async/await)**
```csharp
public async Task<User> GetUserAsync(long id) {
    return await _context.Users.FindAsync(id);
}
```

## 命名约定转换 (Naming Convention Conversion)

### Java → C#

| Java | C# | 示例 |
|------|-----|------|
| `UserService` | `UserService` | 类名保持 PascalCase |
| `getUserById` | `GetUserById` | 方法名改为 PascalCase |
| `private String userName` | `private string _userName` | 私有字段加下划线前缀 |
| `public static final String` | `public const string` | 常量 |
| `List<User>` | `List<User>` | 泛型语法相同 |
| `Map<String, Object>` | `Dictionary<string, object>` | Map → Dictionary |

### 特殊转换

**Java Lombok → C# Records**
```java
// Java with Lombok
@Data
public class UserDTO {
    private String name;
    private String email;
}
```

```csharp
// C# Record
public record UserDTO(string Name, string Email);

// Or traditional class with properties
public class UserDTO {
    public string Name { get; set; }
    public string Email { get; set; }
}
```

## 注解映射 (Annotation Mapping)

| Java Spring | C# ASP.NET Core | 说明 |
|------------|-----------------|------|
| `@RestController` | `[ApiController]` | REST 控制器 |
| `@RequestMapping` | `[Route]` | 路由映射 |
| `@GetMapping` | `[HttpGet]` | GET 请求 |
| `@PostMapping` | `[HttpPost]` | POST 请求 |
| `@PathVariable` | `[FromRoute]` | 路径参数 |
| `@RequestBody` | `[FromBody]` | 请求体 |
| `@RequestParam` | `[FromQuery]` | 查询参数 |
| `@Service` | N/A (通过 DI 注册) | 服务层 |
| `@Component` | N/A (通过 DI 注册) | 组件 |
| `@Autowired` / `@Resource` | 构造函数注入 | 依赖注入 |
| `@Value("${prop}")` | `IConfiguration` | 配置注入 |
| `@Transactional` | `TransactionScope` | 事务管理 |

## 关键技术迁移 (Key Technology Migration)

### 1. Apache Shiro → ASP.NET Core Identity

**Java (Shiro)**
```java
@RequiresPermissions("user:read")
public User getUser(Long id) {
    Subject subject = SecurityUtils.getSubject();
    return userService.findById(id);
}
```

**C# (ASP.NET Core)**
```csharp
[Authorize(Policy = "UserRead")]
public async Task<User> GetUser(long id) {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    return await _userService.FindByIdAsync(id);
}
```

### 2. Quartz → Quartz.NET or Hangfire

**Java**
```java
@Scheduled(cron = "0 0 * * * ?")
public void scheduledTask() {
    // Task logic
}
```

**C# (Hangfire)**
```csharp
RecurringJob.AddOrUpdate(
    "scheduled-task",
    () => ScheduledTask(),
    Cron.Hourly
);
```

### 3. Flyway → EF Core Migrations

**迁移命令 (Migration Commands)**
```bash
# Create migration
dotnet ef migrations add InitialCreate

# Apply migrations
dotnet ef database update

# Revert migration
dotnet ef database update PreviousMigration
```

### 4. Redis Session → ASP.NET Core Session

已在 `Program.cs` 中配置:
```csharp
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = redisConnection;
});

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromHours(12);
});
```

## 测试迁移 (Testing Migration)

### 单元测试 (Unit Tests)

**Java (JUnit 5)**
```java
@Test
public void testGetUser() {
    User user = userService.getUserById(1L);
    assertNotNull(user);
    assertEquals("John", user.getName());
}
```

**C# (xUnit)**
```csharp
[Fact]
public void TestGetUser() {
    var user = _userService.GetUserById(1);
    Assert.NotNull(user);
    Assert.Equal("John", user.Name);
}
```

## 性能优化 (Performance Optimization)

### 1. 异步编程
C# 的 `async/await` 比 Java 的 `CompletableFuture` 更简洁和高效。

### 2. 内存管理
- C# 有垃圾回收但更高效
- 使用 `Span<T>` 和 `Memory<T>` 减少内存分配
- 使用 `ArrayPool<T>` 复用数组

### 3. 并发处理
```csharp
// Parallel processing
await Task.WhenAll(tasks);

// Concurrent collections
var concurrentBag = new ConcurrentBag<User>();
```

## 迁移步骤 (Migration Steps)

### 1. 准备阶段
- [ ] 分析现有 Java 代码结构
- [ ] 确定依赖关系
- [ ] 创建 C# 项目结构

### 2. 基础设施迁移
- [ ] 配置文件转换
- [ ] 数据库连接配置
- [ ] 日志框架配置
- [ ] 缓存配置

### 3. 代码迁移
- [ ] 实体类 (Entity/Domain models)
- [ ] DTO 类
- [ ] Repository 层
- [ ] Service 层
- [ ] Controller 层

### 4. 测试
- [ ] 单元测试
- [ ] 集成测试
- [ ] API 测试
- [ ] 性能测试

### 5. 部署
- [ ] Docker 容器化
- [ ] CI/CD 配置
- [ ] 监控和日志

## 常见问题 (Common Issues)

### 1. 命名空间冲突
C# 使用 `namespace` 而不是 `package`，注意避免与系统命名空间冲突。

### 2. 空值处理
C# 有可空引用类型 (Nullable Reference Types)，需要显式声明:
```csharp
string? nullableString = null;  // Can be null
string nonNullString = "";      // Cannot be null
```

### 3. 集合初始化
```csharp
// C# collection initializers
var list = new List<string> { "a", "b", "c" };
var dict = new Dictionary<string, int> { 
    ["key1"] = 1, 
    ["key2"] = 2 
};
```

## 最佳实践 (Best Practices)

1. **使用依赖注入**: 所有服务通过构造函数注入
2. **异步优先**: 尽可能使用 `async/await`
3. **使用 Records**: 对于不可变的 DTO 使用 `record`
4. **配置管理**: 使用 `IOptions<T>` 模式
5. **错误处理**: 使用中间件统一处理异常
6. **日志记录**: 使用 `ILogger<T>` 接口
7. **API 文档**: 使用 Swagger/OpenAPI

## 工具推荐 (Recommended Tools)

1. **IDE**: Visual Studio 2022, JetBrains Rider, VS Code
2. **调试**: dotnet-trace, dotnet-dump
3. **性能分析**: BenchmarkDotNet, dotnet-counters
4. **代码质量**: SonarQube, Roslyn Analyzers
5. **单元测试**: xUnit, NUnit, MSTest
6. **Mocking**: Moq, NSubstitute

## 参考资源 (Reference Resources)

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [C# Language Reference](https://docs.microsoft.com/dotnet/csharp)
- [.NET API Browser](https://docs.microsoft.com/dotnet/api)
