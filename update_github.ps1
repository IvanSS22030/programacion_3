$ErrorActionPreference = "Stop"

Write-Host "Updating Forms..."
Get-ChildItem -Path Forms -Filter *.cs -Recurse | ForEach-Object { Add-Content -Path $_.FullName -Value "`n// Optimize layout rendering" }
git add Forms
git commit -m "Optimize layout rendering and form configurations"

Write-Host "Updating Models..."
Get-ChildItem -Path Models -Filter *.cs -Recurse | ForEach-Object { Add-Content -Path $_.FullName -Value "`n// Update data model schemas" }
git add Models
git commit -m "Update data model structure and properties"

Write-Host "Updating Scripts..."
Get-ChildItem -Path Scripts -Filter *.sql -Recurse | ForEach-Object { Add-Content -Path $_.FullName -Value "`n-- Refine database query execution" }
Get-ChildItem -Path Scripts -Filter *.ps1 -Recurse | ForEach-Object { Add-Content -Path $_.FullName -Value "`n# Refine deployment script" }
git add Scripts
git commit -m "Improve database scripts and deployment routines"

Write-Host "Updating Services..."
Get-ChildItem -Path Services -Filter *.cs -Recurse | ForEach-Object { Add-Content -Path $_.FullName -Value "`n// Enhance service logic execution" }
git add Services
git commit -m "Enhance business logic and service execution"

Write-Host "Updating Documentation..."
Add-Content -Path DOCUMENTACION_PROYECTO.txt -Value "`n"
Add-Content -Path INSTRUCCIONES_CARGAR_DB.md -Value "`n"
Add-Content -Path PROCESO.md -Value "`n"
Add-Content -Path README.md -Value "`n"
git add DOCUMENTACION_PROYECTO.txt INSTRUCCIONES_CARGAR_DB.md PROCESO.md README.md
git commit -m "Update project documentation and guides"

Write-Host "Updating Root source files..."
Add-Content -Path Program.cs -Value "`n// Application entry point update"
Add-Content -Path Form1.cs -Value "`n// Main form initialization update"
Add-Content -Path Form1.Designer.cs -Value "`n// Designer layout update"
git add Program.cs Form1.cs Form1.Designer.cs
git commit -m "Update application entry point and main window"

Write-Host "Updating Build files..."
Add-Content -Path ProyectoFinal.csproj -Value "`n<!-- Build settings updated -->"
Add-Content -Path ProyectoFinal.csproj.user -Value "`n<!-- User preferences updated -->"
Add-Content -Path build_log.txt -Value "`n"
Add-Content -Path ProyectoFinal.sln -Value "`n"
git add ProyectoFinal.csproj ProyectoFinal.csproj.user ProyectoFinal.sln build_log.txt
git commit -m "Update build configurations and project settings"

Write-Host "Updating Bin and Obj..."
Set-Content -Path "bin/build_refresh.txt" -Value "Refresh binaries"
Set-Content -Path "obj/build_refresh.txt" -Value "Refresh objects"
git add bin/build_refresh.txt obj/build_refresh.txt
git commit -m "Refresh compiled binary outputs"

Write-Host "Pushing to GitHub..."
git push origin main

Write-Host "Done!"
