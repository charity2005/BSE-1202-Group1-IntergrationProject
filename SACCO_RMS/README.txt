================================================================
  SACCO Records Management System  —  Phase 4
  Ndejje University  |  Faculty of Science and Computing
  Bachelor of Software Engineering  |  Group 1
================================================================

GROUP MEMBERS
  Lyadda Charity       (Project Manager)       — BackupForm
  Kamanyire Esther     (QA Lead)               — OverdueForm
  Lagu Jonathan        (UI/UX Designer)        — MemberForm, ContributionForm
  Kasirye Christopher  (Requirements Analyst)  — LoanForm, ReportForm
  Arinaitwe Anthony    (Systems Analyst)       — MainForm / Dashboard

================================================================
  HOW TO OPEN IN VISUAL STUDIO
================================================================

REQUIREMENTS
  - Windows 10 or 11
  - Visual Studio 2019 or 2022 (Community edition is free)
  - .NET Framework 4.8  (included with Windows 10/11)

STEP 1 — Open the project
  File > Open > Project/Solution
  Browse to this folder and open:  SACCO_RMS.csproj

STEP 2 — Install SQLite NuGet package
  Tools > NuGet Package Manager > Package Manager Console
  Type and press Enter:
    Install-Package System.Data.SQLite.Core

STEP 3 — Reset database (IMPORTANT if you ran a previous version)
  Double-click  RESET_DATABASE.bat  before running

STEP 4 — Run
  Press F5  (or Debug > Start Debugging)

DEFAULT LOGINS
  Administrator:  username = admin    password = admin123
  Member:         username = member   password = member123

================================================================
  WHAT LOADS ON FIRST RUN (SAMPLE DATA)
================================================================
  5 Members:
    1. Lyadda Charity        — Balance UGX 375,000
    2. Kamanyire Esther      — Balance UGX 450,000
    3. Lagu Jonathan         — Balance UGX 525,000
    4. Kasirye Christopher   — Balance UGX 300,000
    5. Arinaitwe Anthony     — Balance UGX 600,000

  8 weeks of contribution history (Feb–Apr 2026)

  3 Active Loans:
    - Kamanyire Esther    UGX 500,000  (school fees)
    - Kasirye Christopher UGX 300,000  (medical)
    - Lyadda Charity      UGX 200,000  (business stock)

  Overdue loans: Kasirye Christopher & Lyadda Charity
  have missed instalments — visible on Overdue Loans tab.

================================================================
  FILE STRUCTURE
================================================================
SACCO_RMS/
├── Program.cs
├── Session.cs
├── SACCO_RMS.csproj
├── packages.config
├── README.txt
├── RESET_DATABASE.bat
├── Database/
│   └── DatabaseHelper.cs
└── Forms/
    ├── LoginForm.cs / .Designer.cs
    ├── MainForm.cs  / .Designer.cs        (Tony)
    ├── MemberForm.cs / .Designer.cs       (Jonathan)
    ├── ContributionForm.cs / .Designer.cs (Jonathan)
    ├── LoanForm.cs / .Designer.cs         (Chris)
    ├── OverdueForm.cs / .Designer.cs      (Esther)
    ├── ReportForm.cs / .Designer.cs       (Chris)
    └── BackupForm.cs / .Designer.cs       (Charity)
================================================================
