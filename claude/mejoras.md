# Análisis Arquitectónico y Propuestas de Mejora — ShoppingPOC

> Proyecto: Sistema de Punto de Venta (POS) en Blazor Server
> Fecha de análisis: 2026-03-22
> Estado actual: Fase inicial con autenticación funcional y modelos de datos definidos

---

## 1. Estado Actual del Proyecto

### Lo que existe hoy

| Área | Estado |
|---|---|
| Autenticación | Funcional (Login, Register, Password Reset) |
| Modelos de datos | Definidos (Product, Sale, Purchase, Stock, Ticket, Payment...) |
| Home | Solo lista productos en una tabla plana |
| NavMenu | Links a Counter y Weather (no implementados) |
| Servicios | Solo LoginService y EmailService |
| Componentes reutilizables | Spinner, CodeValidation, PasswordUpdate |

### Problemas arquitectónicos identificados

1. **`Home.razor` es a la vez página y contenedor de datos** — mezcla responsabilidades
2. **NavMenu referencia rutas inexistentes** (`/counter`, `/weather`)
3. **Sin dashboards** — no hay vistas que consoliden información
4. **Sin estructura de módulos** — todo en una carpeta `Pages/` plana
5. **`UserSession` es in-memory y scoped incorrectamente** — se pierde al refrescar
6. **Sin autorización por roles** — cualquier usuario logueado accede a todo
7. **`NumberValidation.razor`** — componente huérfano que navega a `/PasswordUpdate` (ruta que no existe)
8. **Credenciales SMTP hardcodeadas** en el código fuente
9. **Contraseñas almacenadas en texto plano** en la base de datos

---

## 2. Arquitectura Propuesta: Dashboard como Parent Component

La idea central es que cada módulo del POS tenga un **Dashboard Parent** que:
- Actúa como contenedor y controlador de estado para sus hijos
- Maneja las llamadas a servicios y distribuye datos como parámetros
- Coordina la comunicación entre componentes hijos mediante EventCallbacks
- Define el layout visual del módulo (tabs, paneles, grillas)

### Estructura de carpetas propuesta

```
ShoppingPOC/
└── Components/
    ├── App.razor
    ├── Routes.razor
    ├── _Imports.razor
    │
    ├── Layout/
    │   ├── MainLayout.razor
    │   └── NavMenu.razor             ← actualizar rutas
    │
    ├── Shared/                        ← componentes genéricos reutilizables
    │   ├── Spinner.razor
    │   ├── ConfirmDialog.razor        ← nuevo: diálogo de confirmación
    │   ├── DataTable.razor            ← nuevo: tabla genérica paginada
    │   ├── AlertMessage.razor         ← nuevo: mensajes de éxito/error/info
    │   └── EmptyState.razor           ← nuevo: estado vacío de listas
    │
    ├── Authentication/                ← sin cambios estructurales
    │   ├── Login.razor
    │   ├── RegisterUser.razor
    │   ├── EmailValidation.razor
    │   ├── CodeValidation.razor
    │   └── PasswordUpdate.razor
    │
    ├── Dashboard/                     ← NUEVO MÓDULO
    │   ├── DashboardPage.razor        ← Parent: ruta "/" (home principal)
    │   ├── SalesSummaryCard.razor     ← Child: tarjeta de ventas del día
    │   ├── StockAlertCard.razor       ← Child: productos con stock bajo
    │   ├── RecentSalesCard.razor      ← Child: últimas ventas
    │   └── QuickActionsCard.razor     ← Child: accesos directos
    │
    ├── Products/                      ← NUEVO MÓDULO
    │   ├── ProductsDashboard.razor    ← Parent: ruta "/products"
    │   ├── ProductsList.razor         ← Child: tabla de productos
    │   ├── ProductForm.razor          ← Child: formulario crear/editar
    │   ├── ProductDetail.razor        ← Child: detalle del producto
    │   └── StockBadge.razor           ← Child: indicador de stock
    │
    ├── Sales/                         ← NUEVO MÓDULO
    │   ├── SalesDashboard.razor       ← Parent: ruta "/sales"
    │   ├── SalesHistory.razor         ← Child: historial de ventas
    │   ├── NewSaleForm.razor          ← Child: formulario de nueva venta
    │   ├── SaleDetail.razor           ← Child: detalle de una venta
    │   ├── ProductPicker.razor        ← Child: selector de productos
    │   └── SaleTicket.razor           ← Child: ticket generado
    │
    ├── Purchases/                     ← NUEVO MÓDULO
    │   ├── PurchasesDashboard.razor   ← Parent: ruta "/purchases"
    │   ├── PurchasesList.razor        ← Child: lista de compras
    │   ├── NewPurchaseForm.razor      ← Child: registrar compra
    │   └── PurchaseDetail.razor       ← Child: detalle de compra
    │
    ├── Reports/                       ← NUEVO MÓDULO
    │   ├── ReportsDashboard.razor     ← Parent: ruta "/reports"
    │   ├── SalesChart.razor           ← Child: gráfico de ventas
    │   ├── TopProductsTable.razor     ← Child: productos más vendidos
    │   └── RevenueCard.razor          ← Child: ingresos del período
    │
    └── Users/                         ← NUEVO MÓDULO
        ├── UsersDashboard.razor       ← Parent: ruta "/users"
        ├── UsersList.razor            ← Child: lista de usuarios
        └── UserForm.razor             ← Child: editar usuario
```

---

## 3. Patrón de Dashboard Parent + Hijos (Implementación Detallada)

### 3.1 Patrón base del Dashboard Parent

El Parent Dashboard sigue este patrón:
- Tiene la ruta `@page`
- Inyecta servicios y hace las llamadas a datos
- Maneja el estado de carga global del módulo
- Pasa datos a hijos mediante `[Parameter]`
- Recibe eventos de hijos mediante `EventCallback`
- Decide qué vista mostrar (lista, detalle, formulario)

**Ejemplo: `ProductsDashboard.razor`**

```razor
@page "/products"
@inject IProductService ProductService
@inject IStockService StockService

<PageTitle>Productos</PageTitle>

@if (_loading)
{
    <Spinner Label="Cargando productos..." />
}
else
{
    <div class="dashboard-container">
        <div class="dashboard-header">
            <h2>Gestión de Productos</h2>
            <button class="btn btn-primary" @onclick="ShowCreateForm">
                + Nuevo Producto
            </button>
        </div>

        @switch (_currentView)
        {
            case DashboardView.List:
                <ProductsList Products="_products"
                              OnEdit="ShowEditForm"
                              OnDelete="HandleDeleteProduct"
                              OnViewDetail="ShowDetail" />
                break;

            case DashboardView.Form:
                <ProductForm Product="_selectedProduct"
                             IsEditing="_isEditing"
                             OnSave="HandleSaveProduct"
                             OnCancel="ShowList" />
                break;

            case DashboardView.Detail:
                <ProductDetail Product="_selectedProduct"
                               OnBack="ShowList"
                               OnEdit="() => ShowEditForm(_selectedProduct)" />
                break;
        }

        @if (!string.IsNullOrEmpty(_statusMessage))
        {
            <AlertMessage Message="_statusMessage" Type="_alertType" />
        }
    </div>
}

@code {
    private List<Product> _products = new();
    private Product _selectedProduct;
    private DashboardView _currentView = DashboardView.List;
    private bool _loading = true;
    private bool _isEditing = false;
    private string _statusMessage = string.Empty;
    private AlertType _alertType;

    protected override async Task OnInitializedAsync()
    {
        await LoadProducts();
    }

    private async Task LoadProducts()
    {
        _loading = true;
        _products = await ProductService.GetAllAsync();
        _loading = false;
    }

    private void ShowCreateForm()
    {
        _selectedProduct = new Product();
        _isEditing = false;
        _currentView = DashboardView.Form;
    }

    private void ShowEditForm(Product product)
    {
        _selectedProduct = product;
        _isEditing = true;
        _currentView = DashboardView.Form;
    }

    private void ShowDetail(Product product)
    {
        _selectedProduct = product;
        _currentView = DashboardView.Detail;
    }

    private void ShowList() => _currentView = DashboardView.List;

    private async Task HandleSaveProduct(Product product)
    {
        await ProductService.SaveAsync(product);
        await LoadProducts();
        ShowList();
        ShowAlert("Producto guardado exitosamente.", AlertType.Success);
    }

    private async Task HandleDeleteProduct(Product product)
    {
        await ProductService.DeleteAsync(product.Id);
        await LoadProducts();
        ShowAlert("Producto eliminado.", AlertType.Info);
    }

    private void ShowAlert(string message, AlertType type)
    {
        _statusMessage = message;
        _alertType = type;
        _ = Task.Delay(3000).ContinueWith(_ =>
        {
            _statusMessage = string.Empty;
            InvokeAsync(StateHasChanged);
        });
    }

    private enum DashboardView { List, Form, Detail }
}
```

---

### 3.2 Patrón del componente hijo (Child)

Los hijos solo reciben datos por parámetros y emiten eventos. **Nunca inyectan servicios directamente** (esa responsabilidad es del Parent).

**Ejemplo: `ProductsList.razor`**

```razor
@if (Products is null || !Products.Any())
{
    <EmptyState Message="No hay productos registrados." />
}
else
{
    <table class="table table-hover">
        <thead>
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Precio</th>
                <th>Stock</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var product in Products)
            {
                <tr>
                    <td>@product.Id</td>
                    <td>@product.Name</td>
                    <td>@product.Price.ToString("C")</td>
                    <td><StockBadge Quantity="product.Stock?.Quantity ?? 0" /></td>
                    <td>
                        <button class="btn btn-sm btn-outline-info"
                                @onclick="() => OnViewDetail.InvokeAsync(product)">
                            Ver
                        </button>
                        <button class="btn btn-sm btn-outline-warning"
                                @onclick="() => OnEdit.InvokeAsync(product)">
                            Editar
                        </button>
                        <button class="btn btn-sm btn-outline-danger"
                                @onclick="() => OnDelete.InvokeAsync(product)">
                            Eliminar
                        </button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    [Parameter, EditorRequired]
    public List<Product> Products { get; set; } = new();

    [Parameter]
    public EventCallback<Product> OnEdit { get; set; }

    [Parameter]
    public EventCallback<Product> OnDelete { get; set; }

    [Parameter]
    public EventCallback<Product> OnViewDetail { get; set; }
}
```

---

### 3.3 Dashboard Principal (Home): `DashboardPage.razor`

El nuevo Home no es una tabla plana — es un dashboard con tarjetas informativas.

```razor
@page "/"

<PageTitle>Dashboard</PageTitle>

<div class="row g-3">
    <div class="col-md-3">
        <SalesSummaryCard TodaySales="_todaySales" TotalRevenue="_todayRevenue" />
    </div>
    <div class="col-md-3">
        <StockAlertCard LowStockProducts="_lowStockProducts" />
    </div>
    <div class="col-md-6">
        <QuickActionsCard />
    </div>
    <div class="col-12">
        <RecentSalesCard Sales="_recentSales" />
    </div>
</div>
```

---

## 4. Mejoras en Servicios

### 4.1 Separar servicios por módulo (Single Responsibility)

**Actualmente:** solo `ILoginService` e `IEmailService`

**Propuesto:**

```
BusinessLogic/
├── Contracts/
│   ├── IAuthService.cs         ← login, register, change password
│   ├── IEmailService.cs        ← sin cambios
│   ├── IProductService.cs      ← CRUD productos
│   ├── IStockService.cs        ← gestión de stock
│   ├── ISaleService.cs         ← ventas
│   ├── IPurchaseService.cs     ← compras
│   └── IReportService.cs       ← reportes
└── Services/
    ├── AuthService.cs
    ├── EmailService.cs
    ├── ProductService.cs
    ├── StockService.cs
    ├── SaleService.cs
    ├── PurchaseService.cs
    └── ReportService.cs
```

**Ejemplo: `IProductService.cs`**

```csharp
public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Result> SaveAsync(Product product);    // crea o actualiza
    Task<Result> DeleteAsync(int id);
    Task<List<Product>> GetLowStockAsync(int threshold = 5);
}
```

---

### 4.2 Patrón Repository (opcional pero recomendado)

Para separar la lógica de acceso a datos:

```
Data/
├── ApplicationDbContext.cs
├── Migrations/
└── Repositories/
    ├── Contracts/
    │   ├── IProductRepository.cs
    │   └── ISaleRepository.cs
    └── ProductRepository.cs
```

Los servicios de negocio llaman a repositorios; los componentes llaman a servicios.

---

## 5. Mejoras en Autenticación y Sesión

### 5.1 Problema: `UserSession` se pierde al refrescar

**Actualmente:**
```csharp
// UserSession es Singleton in-memory — se pierde con Ctrl+F5
public class UserSession
{
    public User CurrentUser { get; set; }
    public bool IsLoggedIn => CurrentUser is not null;
}
```

**Propuesto: usar `ProtectedSessionStorage` de Blazor**

```csharp
// AuthStateService.cs
public class AuthStateService
{
    private readonly ProtectedSessionStorage _storage;

    public AuthStateService(ProtectedSessionStorage storage)
    {
        _storage = storage;
    }

    public async Task SetUserAsync(User user)
    {
        await _storage.SetAsync("currentUser", user);
    }

    public async Task<User?> GetUserAsync()
    {
        var result = await _storage.GetAsync<User>("currentUser");
        return result.Success ? result.Value : null;
    }

    public async Task ClearAsync()
    {
        await _storage.DeleteAsync("currentUser");
    }
}
```

### 5.2 Crear un componente `AuthGuard` reutilizable

En lugar de duplicar la lógica de redirección en cada página:

```razor
<!-- AuthGuard.razor -->
@inject AuthStateService Auth
@inject NavigationManager Nav

@if (_authorized)
{
    @ChildContent
}

@code {
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public string RequiredRole { get; set; } = "";

    private bool _authorized = false;

    protected override async Task OnInitializedAsync()
    {
        var user = await Auth.GetUserAsync();
        if (user is null)
        {
            Nav.NavigateTo("/Login");
            return;
        }
        // Verificar rol si se requiere
        _authorized = true;
    }
}
```

**Uso en cualquier página:**
```razor
@page "/products"

<AuthGuard RequiredRole="Admin">
    <ProductsDashboard />
</AuthGuard>
```

---

## 6. Mejoras en NavMenu

### Actual (incorrecto)
```razor
<NavLink href="counter">Counter</NavLink>   <!-- no existe -->
<NavLink href="weather">Weather</NavLink>   <!-- no existe -->
```

### Propuesto
```razor
<NavLink href="">Dashboard</NavLink>
<NavLink href="products">Productos</NavLink>
<NavLink href="sales">Ventas</NavLink>
<NavLink href="purchases">Compras</NavLink>
<NavLink href="reports">Reportes</NavLink>
<NavLink href="users">Usuarios</NavLink>
```

---

## 7. Seguridad: Problemas Críticos

### 7.1 Contraseñas en texto plano

**Actualmente:** Las contraseñas se guardan sin hash en la BD.

**Propuesto:**
```csharp
// En AuthService al registrar/cambiar contraseña:
user.Password = BCrypt.HashPassword(plainTextPassword);

// Al validar:
bool isValid = BCrypt.Verify(plainTextPassword, user.HashedPassword);
```
Instalar: `dotnet add package BCrypt.Net-Next`

### 7.2 Credenciales SMTP en código fuente

**Actualmente:**
```csharp
new NetworkCredential("isainokia@gmail.com", "ftpn focn knbq swsv") // ← PELIGROSO
```

**Propuesto:** usar `appsettings.json` + secrets
```json
// appsettings.json
{
  "Email": {
    "FromAddress": "isainokia@gmail.com",
    "SmtpHost": "smtp.gmail.com",
    "SmtpPort": 587
  }
}
```
```bash
# Guardar la contraseña en secrets (nunca en código)
dotnet user-secrets set "Email:Password" "tu_app_password"
```

### 7.3 Generación de código de validación insegura

**Actualmente:** `Random` no es criptográficamente seguro.

**Propuesto:**
```csharp
private int GenerateSecureCode()
{
    return RandomNumberGenerator.GetInt32(100000, 999999);
}
```

---

## 8. Componentes Shared Reutilizables a Crear

### 8.1 `AlertMessage.razor`
```razor
<!-- Reemplaza los <div class="alert"> dispersos en todo el proyecto -->
@if (!string.IsNullOrEmpty(Message))
{
    <div class="alert alert-@TypeClass alert-dismissible fade show" role="alert">
        @Message
        <button type="button" class="btn-close" @onclick="() => Message = string.Empty"></button>
    </div>
}

@code {
    [Parameter] public string Message { get; set; } = string.Empty;
    [Parameter] public AlertType Type { get; set; } = AlertType.Info;
    private string TypeClass => Type switch
    {
        AlertType.Success => "success",
        AlertType.Error   => "danger",
        AlertType.Warning => "warning",
        _                 => "info"
    };
}
```

### 8.2 `ConfirmDialog.razor`
```razor
<!-- Para confirmar eliminaciones -->
@if (IsVisible)
{
    <div class="modal show d-block" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">@Title</h5>
                </div>
                <div class="modal-body">@Message</div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" @onclick="() => OnCancel.InvokeAsync()">Cancelar</button>
                    <button class="btn btn-danger"    @onclick="() => OnConfirm.InvokeAsync()">Confirmar</button>
                </div>
            </div>
        </div>
    </div>
}

@code {
    [Parameter] public bool IsVisible { get; set; }
    [Parameter] public string Title { get; set; } = "Confirmar";
    [Parameter] public string Message { get; set; } = "¿Estás seguro?";
    [Parameter] public EventCallback OnConfirm { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
}
```

### 8.3 `StockBadge.razor`
```razor
<span class="badge @BadgeClass">@Quantity</span>

@code {
    [Parameter] public int Quantity { get; set; }
    private string BadgeClass => Quantity switch
    {
        0     => "bg-danger",
        <= 5  => "bg-warning",
        _     => "bg-success"
    };
}
```

---

## 9. Eliminar Código Muerto

| Archivo | Problema | Acción |
|---|---|---|
| `Class.cs` | Clase vacía sin uso | Eliminar |
| `NumberValidation.razor` | Duplica lógica de CodeValidation, navega a ruta inexistente | Eliminar o refactorizar |
| `NavMenu` links counter/weather | Rutas inexistentes | Actualizar |

---

## 10. Resumen de Prioridades

### Alta prioridad (seguridad)
1. Hashear contraseñas con BCrypt
2. Mover credenciales SMTP a `user-secrets`
3. Usar `ProtectedSessionStorage` para la sesión

### Media prioridad (arquitectura)
4. Crear módulos con Dashboard Parents (Products, Sales, Purchases)
5. Crear servicios por módulo (`IProductService`, `ISaleService`, etc.)
6. Crear `AuthGuard` component reutilizable
7. Actualizar NavMenu con rutas reales

### Baja prioridad (calidad de código)
8. Crear componentes Shared: `AlertMessage`, `ConfirmDialog`, `StockBadge`
9. Eliminar archivos muertos (`Class.cs`, `NumberValidation.razor`)
10. Agregar validación por roles de usuario

---

## 11. Diagrama de Flujo de Componentes (visión final)

```
App.razor
└── Routes.razor
    └── MainLayout.razor
        ├── NavMenu.razor
        └── @Body
            ├── DashboardPage.razor (/)
            │   ├── SalesSummaryCard.razor
            │   ├── StockAlertCard.razor
            │   ├── RecentSalesCard.razor
            │   └── QuickActionsCard.razor
            │
            ├── ProductsDashboard.razor (/products)
            │   ├── ProductsList.razor
            │   │   └── StockBadge.razor
            │   ├── ProductForm.razor
            │   ├── ProductDetail.razor
            │   └── ConfirmDialog.razor (shared)
            │
            ├── SalesDashboard.razor (/sales)
            │   ├── SalesHistory.razor
            │   ├── NewSaleForm.razor
            │   │   └── ProductPicker.razor
            │   └── SaleTicket.razor
            │
            ├── PurchasesDashboard.razor (/purchases)
            │   ├── PurchasesList.razor
            │   └── NewPurchaseForm.razor
            │
            ├── ReportsDashboard.razor (/reports)
            │   ├── SalesChart.razor
            │   ├── TopProductsTable.razor
            │   └── RevenueCard.razor
            │
            └── Authentication/ (sin cambios mayores)
                ├── Login.razor (/Login)
                ├── RegisterUser.razor (/RegisterUser)
                └── EmailValidation.razor (/EmailValidation)
                    ├── CodeValidation.razor
                    └── PasswordUpdate.razor
```
