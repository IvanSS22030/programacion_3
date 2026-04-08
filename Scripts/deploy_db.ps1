$psql = "C:\Program Files\PostgreSQL\17\bin\psql.exe"
$passwords = @("Lucario2030", "lucario2030", "Lucario2030*", "ivan2030", "Ivan2030")
$env:PGUSER = "postgres"

foreach ($pass in $passwords) {
    $env:PGPASSWORD = $pass
    Write-Host "Checking password: $pass"
    
    # Try connecting to 'postgres' db specifically
    & $psql -d postgres -c "SELECT 1" 2>&1 | Out-Null
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Authentication Successful."
        
        Write-Host "Creating Database..."
        & $psql -d postgres -c 'CREATE DATABASE "PacientesDB";' 2>&1 | Write-Host
        
        Write-Host "Running Schema Script..."
        & $psql -d pacientesdb -f "Scripts/create_pacientes_db.sql" 2>&1 | Write-Host
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "DEPLOYMENT_SUCCESS"
            exit 0
        } else {
             Write-Error "Script execution failed."
             exit 1
        }
    }
}
Write-Error "All passwords failed authentication."
exit 1

# Actualización de repositorio - 2026-04-08

# Refine deployment script
