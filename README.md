- Project Description: 
"Ce projet vise à développer une plateforme SaaS enterprise de gestion de ressources et de planification de projets, conçue pour optimiser la collaboration inter-équipes au sein des grandes organisations internationales. L'application offrira un tableau de bord centralisé et intuitif, permettant aux chefs de projet de suivre en temps réel l'avancement des tâches, d'allouer les ressources humaines et matérielles de manière optimale, et de générer des rapports analytiques détaillés et exportables. Les fonctionnalités principales incluent la gestion agile des workflows complexes, l'intégration native et sécurisée avec des outils de communication tiers (tels que Slack, Microsoft Teams et Jira), ainsi qu'un module avancé de contrôle d'accès basé sur les rôles (RBAC) pour garantir la confidentialité absolue des données sensibles. L'architecture technique repose sur une approche microservices moderne, assurant une haute disponibilité, une scalabilité horizontale sans friction et une maintenance simplifiée. L'interface utilisateur sera développée avec un framework frontend réactif pour garantir une expérience fluide, responsive et accessible sur tous les appareils. Ce système permettra aux organisations de réduire leurs délais de livraison de 30 %, d'améliorer significativement la transparence opérationnelle globale, et de s'adapter facilement aux futures évolutions métier ainsi qu'aux normes de conformité réglementaire en vigueur."

- Project Architecture: 
```text
TestMVCSharpC/
├── Controllers/                        # Generated MVC Controllers
├── Data/                               # Database context
│   └── TestMVCSharpCContext
├── Models/
│   └── Dto/                            # Data Transfer Objects
├── Repositories/                       # Data Access Layer
├── Services/                           # Business Logic
├── ViewModels/                         # View Models
├── Views/                              # Generated Razor Views
├── Filters/                            # Filters (including GlobalExceptionFilter)
├── Specification/                      # DTO ↔ Entity Mapping
├── Utils/                              # Utility Classes
├── wwwroot/                            # Static Files
│   ├── css/                            # Stylesheets
│   ├── js/                             # JavaScript scripts
│   ├── img/                            # Images
│   └── i18n/                           # Internationalization files
└── Program.cs                          # Application Entry Point
```

- Technical Prerequisites: 
  - IDE: Visual Studio 2022 or VS Code
  - .NET 8.0
  - Database : PostgreSQL 16

- Run the Application: 
  - Option 1 - Terminal:
      dotnet run
  - Option 2 - Visual Studio:
      Press F5 or Ctrl+F5
  -Option 3 - VS Code:
      Terminal → Run Task → build
      Press F5

- Access the Application:
  Open your browser and navigate to: https://localhost:7273 or http://localhost:8086

