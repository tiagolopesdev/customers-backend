### Modelo de infraestrutura

```
src/
├── Infrastructure/
│   ├── Persistence/
│   │   ├── Repositories/
│   │   │   ├── CustomerRepository.cs   # Implementa ICustomerRepository do Domain
│   │   │   └── OrderRepository.cs      # Implementa IOrderRepository do Domain
│   │   │
│   │   ├── Configurations/            # Configurações de entidades (Entity Framework)
│   │   │   ├── CustomerConfiguration.cs
│   │   │   └── OrderConfiguration.cs
│   │   │
│   │   ├── Migrations/                # Migrações do EF Core
│   │   ├── ApplicationDbContext.cs    # DbContext principal
│   │   └── DapperExtensions.cs        # Extensões para Dapper (se usado)
│   │
│   ├── Identity/
│   │   ├── IdentityService.cs         # Implementa IIdentityService do Application
│   │   └── JwtService.cs              # Serviço de geração de tokens JWT
│   │
│   ├── FileStorage/
│   │   ├── AzureBlobStorageService.cs # Implementa IFileStorageService
│   │   └── LocalFileStorageService.cs # Implementação alternativa
│   │
│   ├── Email/
│   │   ├── SmtpEmailService.cs        # Implementa IEmailService
│   │   └── SendGridEmailService.cs    # Implementação alternativa
│   │
│   ├── Caching/
│   │   ├── RedisCacheService.cs       # Implementa ICacheService
│   │   └── MemoryCacheService.cs      # Implementação alternativa
│   │
│   └── ExternalServices/
│       ├── PaymentGateway/            # Integração com gateway de pagamento
│       └── ShippingService/           # Integração com serviço de frete
```

