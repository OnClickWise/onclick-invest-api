🚀 OnClickInvest API

Plataforma SaaS B2B2C de Gestão de Investimentos e Projeções Financeiras, multi-tenant, escalável e orientada a domínio.

📌 Visão Geral

O OnClickInvest é um sistema de gestão financeira para assessores de investimento (modelo B2B2C), onde:

O ADMIN gerencia seus investidores

O INVESTOR acompanha sua carteira e projeções

O SUPER_ADMIN gerencia a plataforma

A aplicação segue princípios de:

Arquitetura Modular

API-first

Multi-Tenant

DDD Light

Separação clara de responsabilidades

🏗 Arquitetura
OnClickInvest.Api
│
├── Modules
│   ├── Identity
│   ├── Tenants
│   ├── Investors
│   ├── Portfolios
│   ├── Plans
│   ├── Subscriptions
│   └── Reports (em desenvolvimento)
│
├── Shared
│   ├── BaseEntity
│   ├── AuditableEntity
│   ├── Enums
│   └── Utilities
│
├── Data
│   ├── AppDbContext
│   └── Configurations
│
└── Infrastructure

🔐 Modelo de Acesso (RBAC)
SUPER_ADMIN  → Controle total da plataforma
ADMIN        → Assessor / Dono do Tenant
INVESTOR     → Cliente final

🧠 Modelo Multi-Tenant

Cada Tenant representa uma organização (assessoria).

Investors pertencem a um Tenant.

Subscriptions vinculam Tenant ↔ Plan.

Isolamento lógico por TenantId.

🗄 Banco de Dados

PostgreSQL

ORM: Entity Framework Core

Migrations versionadas

⚙️ Configuração
1️⃣ Connection String

appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "
  Host=localhost;
  Port=5432;
  Database=Onclick_Invest;
  Username=postgres;
  Password=SUA_SENHA_AQUI"
}

2️⃣ JWT
"Jwt": {
  "Secret": "your-secret-key",
  "Issuer": "OnClickInvest.Api",
  "Audience": "OnClickInvest.Api.Client",
  "ExpirationMinutes": 3000
}

▶️ Executando o Projeto

Dentro da pasta da API:

dotnet restore
dotnet build
dotnet run

🔄 Migrations

Sempre que alterar entidades:

dotnet ef migrations add NomeDaMigration
dotnet ef database update


Se houver erro de pending model changes:

dotnet ef migrations add SyncChanges
dotnet ef database update

📦 Módulos Implementados
✅ Identity

Login

Registro

Hash de senha

JWT

✅ Tenants

Criação de organização

Vinculação ao ADMIN

✅ Plans (Fase 2)

Definição de planos

Estrutura de preço

Controle de recursos

✅ Subscriptions (Fase 2)

Vincula Tenant a Plan

Controle de ciclo de vida

Status (Active, Suspended, Cancelled)

📊 Próximo Módulo (Roadmap Atual)
🔜 Reports & Projections

Funcionalidade central do produto:

Projeção de capital

Simulação com aportes

Cálculo de rentabilidade

Metas financeiras

Cenários comparativos

KPIs financeiros

Esse módulo é estratégico para o diferencial competitivo do produto.

🧩 Entidades Base
BaseEntity

Id (GUID)

CreatedAt

UpdatedAt

AuditableEntity

CreatedBy

UpdatedBy

🔎 Fluxo Inicial do Sistema

SUPER_ADMIN cria plano

ADMIN registra Tenant

Tenant seleciona Plan

Subscription é criada

ADMIN cadastra Investors

Investors possuem Portfolios

Sistema gera projeções

📈 Roadmap de Evolução
Fase 1 – Fundação

Autenticação

Multi-tenant

Base estrutural

Fase 2 – Core Financeiro (Atual)

Plans

Subscriptions

Investors

Portfolios

Reports & Projections

Fase 3 – Experiência & Valor

Dashboards avançados

IA para projeções

Alertas inteligentes

Fase 4 – Escala SaaS

Billing automatizado

Controle de limites por plano

Observabilidade

Auditoria completa

🛡 Boas Práticas Adotadas

Separação por módulos

Repository Pattern

Services Layer

DTOs

Validações centralizadas

Não exposição direta de entidades

Controle de dependências via DI

🚀 Stack Tecnológica

.NET 8

Entity Framework Core

PostgreSQL

JWT Authentication

Swagger

Arquitetura Modular

📌 Objetivo Estratégico

Construir uma plataforma:

Escalável

Multi-tenant real

Orientada a domínio financeiro

Preparada para crescimento SaaS
